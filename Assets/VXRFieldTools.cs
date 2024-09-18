using System;
using System.Reflection;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Versee.Scripts.Utils
{
    public class VXRFieldTools
    {
        #if UNITY_EDITOR
        public static void DrawHiddenFields(ref bool ShowHiddenFields, SerializedObject serializedObject, string title = "Hidden Fields")
        {
            ShowHiddenFields = EditorGUILayout.Foldout(ShowHiddenFields, title);
            if (ShowHiddenFields)
            {
                serializedObject.Update();
                BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
                FieldInfo[] fields = serializedObject.targetObject.GetType().GetFields(bindingFlags);

                foreach (FieldInfo field in fields)
                {
                    if (Attribute.IsDefined(field, typeof(HideInInspector)))
                    {
                        SerializedProperty property = serializedObject.FindProperty(field.Name);
                        if (property != null)
                        {
                            EditorGUILayout.PropertyField(property, new GUIContent(field.Name), true);
                        }
                    }
                }

                serializedObject.ApplyModifiedProperties();
            }
        }
        #endif
        
        public static object ResolveFieldOrProperty(object obj, string fieldOrPropertyPath)
        {
            object targetObj = obj;

            string[] segments = fieldOrPropertyPath.Split('.');
            foreach (var segment in segments)
            {
                if (targetObj == null) return null;

                // 检查是否有索引，例如 [0]，[1] 等
                Match match = Regex.Match(segment, @"(.+)\[(\d+)\]");
                if (match.Success)
                {
                    string fieldName = match.Groups[1].Value;
                    int index = int.Parse(match.Groups[2].Value);

                    targetObj = GetValue(targetObj, fieldName);

                    if (targetObj is IList list)
                    {
                        if (index < 0 || index >= list.Count) return null;
                        targetObj = list[index];
                    }
                    else if (targetObj is Array arr)
                    {
                        if (index < 0 || index >= arr.Length) return null;
                        targetObj = arr.GetValue(index);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    targetObj = GetValue(targetObj, segment);
                }
            }

            return targetObj;
        }

        public static Type ResolveFieldOrPropertyType(object obj, string fieldOrPropertyPath)
        {
            object targetObj = obj;
            Type targetType = obj.GetType();

            string[] segments = fieldOrPropertyPath.Split('.');
            foreach (var segment in segments)
            {
                if (targetType == null) return null;

                // 检查是否有索引，例如 [0]，[1] 等
                Match match = Regex.Match(segment, @"(.+)\[(\d+)\]");
                if (match.Success)
                {
                    string fieldName = match.Groups[1].Value;

                    FieldInfo fieldInfo = targetType.GetField(fieldName);
                    PropertyInfo propertyInfo = targetType.GetProperty(fieldName);
                    
                    if (fieldInfo != null)
                    {
                        targetType = fieldInfo.FieldType;
                    }
                    else if (propertyInfo != null)
                    {
                        targetType = propertyInfo.PropertyType;
                    }
                    else
                    {
                        return null;
                    }

                    // 对于数组和列表，获取其元素类型
                    if (typeof(IList).IsAssignableFrom(targetType))
                    {
                        targetType = targetType.IsArray ? targetType.GetElementType() : targetType.GenericTypeArguments[0];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    FieldInfo fieldInfo = targetType.GetField(segment);
                    PropertyInfo propertyInfo = targetType.GetProperty(segment);

                    if (fieldInfo != null)
                    {
                        targetType = fieldInfo.FieldType;
                    }
                    else if (propertyInfo != null)
                    {
                        targetType = propertyInfo.PropertyType;
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            return targetType;
        }

        public static object GetValue(object targetObj, string fieldName)
        {
            Type type = targetObj.GetType();
            PropertyInfo prop = type.GetProperty(fieldName);
            FieldInfo field = type.GetField(fieldName);

            if (prop != null)
            {
                return prop.GetValue(targetObj);
            }
            else if (field != null)
            {
                return field.GetValue(targetObj);
            }
            else
            {
                return null;
            }
        }
    }
}