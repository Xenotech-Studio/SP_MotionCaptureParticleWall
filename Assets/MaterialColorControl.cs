using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MaterialColorControl : MonoBehaviour
{
    // Start is called before the first frame update
   
        public float progress;
        
        
        public Material Material;

        public string PropertyName = "_";
        
        public Color oldColor;
        public Color newColor;
        public float allTime;
        public AnimationCurve Curve = AnimationCurve.Linear(0, 0, 1, 1);

        private void Start()
        {
            //Material.SetColor(PropertyName, oldColor);
        }

        private void OnDisable()
        {
            progress = 0;
           // Material.SetColor(PropertyName, oldColor);
        }

        private void Update()
        {
            if (progress < allTime)
            {
                progress +=Time.deltaTime;
            }
            else
            {
                GetComponent<MaterialColorControl>().enabled = false;
                
            }
            
            Color newercolor =  Color.Lerp(oldColor, newColor, Curve.Evaluate(progress/allTime));
            /*if (0.95 - progress / allTime < 0.1f)
            {
                GetComponent<VideoPlayer>().time = 0;
                GetComponent<VideoPlayer>().Pause();
                GetComponent<VideoPlayer>().time = 0;
            }*/
            Material.SetColor(PropertyName, newercolor);
        }
    
}
