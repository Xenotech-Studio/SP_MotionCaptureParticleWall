using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using UnityEngine;
using Versee.Scripts.Utils;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

public class FollowTransform : MonoBehaviour
{
    public Transform FollowedTransform;
    
    [HideInInspector] public bool FixPositionX = false;
    [HideInInspector] public bool FixPositionY = false;
    [HideInInspector] public bool FixPositionZ = false;

    [HideInInspector] public bool FixRotationY = false;
    [HideInInspector] public bool FixRotationX = false;
    [HideInInspector] public bool FixRotationZ = false;
    
    // A cache that speeds up the execution
    [HideInInspector] public bool CompletelyFollow = true;
    [HideInInspector] public bool Validated = false;

    [HideInInspector] public bool ForceFix = false;
    
    [HideInInspector] public bool DoFixScale = false;
    [HideInInspector] public Vector3 FixedScale = Vector3.one;

    public bool Initialize()
    {
        bool changed = false;
        
        // If no fix positions and fix rotations, let CompletelyFollow be true
        if (!FixPositionX && !FixPositionY && !FixPositionZ &&
            !FixRotationX && !FixRotationY && !FixRotationZ)
        {
            if (CompletelyFollow == false)
            {
                CompletelyFollow = true;
                changed = true;
            }

        }
        else if (CompletelyFollow == true)
        {
            CompletelyFollow = false;
            changed = true;
        }
        
        // if not all rotation and positions are fixed, and instance is not null let Validated be true
        if (!(FixPositionX && FixPositionY && FixPositionZ &&
              FixRotationX && FixRotationY && FixRotationZ))
        {
            if (Validated == false)
            {
                Validated = true;
                changed = true;
            }
        }
        else if (Validated == true)
        {
            Validated = false;
            changed = true;
        }

        InitialRotation = transform.rotation;

        return changed;
    }

    private Quaternion InitialRotation = new Quaternion();
    
    protected virtual void Awake()
    {
        Initialize();
    }

    // Update is called once per frame
    void FixedUpdate()
    {  
        
        if (DoFixScale)
             { 
                 transform.localScale = FixedScale;
             }
        if(FollowedTransform == null) return;
        if (Validated)
        {
            Follow();
        }
        
      
    }

    public void Follow()
    {
        if (CompletelyFollow)
        {
            transform.position = FollowedTransform.position;
            transform.rotation = FollowedTransform.rotation;
        }
        else
        {
            Vector3 position = transform.position;
            Vector3 rotation = transform.rotation.eulerAngles;
                
            if (!FixPositionX)
            {
                position.x = FollowedTransform.position.x;
            }
            if (!FixPositionY)
            {
                position.y = FollowedTransform.position.y;
            }
            if (!FixPositionZ)
            {
                position.z = FollowedTransform.position.z;
            }
            
            
            if(ForceFix) rotation = InitialRotation.eulerAngles;
                
            if (!FixRotationX)
            {
                rotation.x = FollowedTransform.rotation.eulerAngles.x;
            }
            if (!FixRotationY)
            {
                rotation.y = FollowedTransform.rotation.eulerAngles.y;
            }
            if (!FixRotationZ)
            {
                rotation.z = FollowedTransform.rotation.eulerAngles.z;
            }
                
            transform.position = position;
            transform.rotation = Quaternion.Euler(rotation);
        }
        
        
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(FollowTransform))]
public class FollowTransformEditor : Editor
{
    public FollowTransform instance;

    public bool ShowHiddenFields = false;
        
    
    private void Awake()
    {
        instance = (FollowTransform) target;
    }
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        bool changed = instance.Initialize();

        // A horizontal like that have three toggles followed by title "X", "Y", "Z"
        // The three toggles are for FixPositionX, FixPositionY, FixPositionZ
        // set changed to be true if some of them are changed
        GUILayout.BeginHorizontal();
        GUILayout.Label("Fix Positions: ", GUILayout.Width(100));
        var newFixPositionX = EditorGUILayout.Toggle(instance.FixPositionX, GUILayout.Width(20));
        GUILayout.Label("X", GUILayout.Width(20));
        var newFixPositionY = EditorGUILayout.Toggle(instance.FixPositionY, GUILayout.Width(20));
        GUILayout.Label("Y", GUILayout.Width(20));
        var newFixPositionZ = EditorGUILayout.Toggle(instance.FixPositionZ, GUILayout.Width(20));
        GUILayout.Label("Z", GUILayout.Width(20));
        GUILayout.EndHorizontal();
        
        if (newFixPositionX != instance.FixPositionX || newFixPositionY != instance.FixPositionY ||
            newFixPositionZ != instance.FixPositionZ)
        {
            instance.FixPositionX = newFixPositionX;
            instance.FixPositionY = newFixPositionY;
            instance.FixPositionZ = newFixPositionZ;
            changed = true;
        }
        
        // do the same to rotation
        GUILayout.BeginHorizontal();
        GUILayout.Label("Fix Rotations: ", GUILayout.Width(100));
        var newFixRotationX = EditorGUILayout.Toggle(instance.FixRotationX, GUILayout.Width(20));
        GUILayout.Label("X", GUILayout.Width(20));
        var newFixRotationY = EditorGUILayout.Toggle(instance.FixRotationY, GUILayout.Width(20));
        GUILayout.Label("Y", GUILayout.Width(20));
        var newFixRotationZ = EditorGUILayout.Toggle(instance.FixRotationZ, GUILayout.Width(20));
        GUILayout.Label("Z", GUILayout.Width(20));
        GUILayout.EndHorizontal();
        
        if (newFixRotationX != instance.FixRotationX || newFixRotationY != instance.FixRotationY ||
            newFixRotationZ != instance.FixRotationZ)
        {
            instance.FixRotationX = newFixRotationX;
            instance.FixRotationY = newFixRotationY;
            instance.FixRotationZ = newFixRotationZ;
            changed = true;
        }
        
        
        if (changed)
        {
            serializedObject.ApplyModifiedProperties();
            if (EditorApplication.isPlaying) return;
            EditorUtility.SetDirty(instance);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
        
        VXRFieldTools.DrawHiddenFields(ref ShowHiddenFields, serializedObject);

        if (ShowHiddenFields)
        {
            if (GUILayout.Button("Follow Now"))
            {
                instance.Follow();
                // if not in play mode, set dirty
                if (!EditorApplication.isPlaying)
                {
                    EditorUtility.SetDirty(instance);
                    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                }
            }
        }
    }

    
}
#endif