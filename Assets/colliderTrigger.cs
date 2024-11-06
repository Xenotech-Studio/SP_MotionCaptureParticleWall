using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderTrigger : MonoBehaviour
{
    public List<GameObject> seedList;
    private int seedInt;
    public float maxinterval;
    private float accumulatedTime;
    public float playTime;
    private bool isTriggered;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered)
        {
            accumulatedTime += Time.deltaTime;
            if (accumulatedTime > playTime)
            {
                ParticleSystem.MainModule _mainModule=seedList[seedInt%seedList.Count].GetComponent<ParticleSystem>().main;
                _mainModule.simulationSpeed =1f;
            }
            if(accumulatedTime>maxinterval)
            {
                accumulatedTime = 0;
                isTriggered = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name=="joint_PisiformLT"&&isTriggered==false)
        {
            ParticleSystem.MainModule _mainModule=seedList[seedInt%seedList.Count].GetComponent<ParticleSystem>().main;
            _mainModule.simulationSpeed =10f;
            _mainModule.prewarm = false;
            seedList[seedInt%seedList.Count].GetComponent<ParticleSystem>().Stop();
            seedInt += 1;
            
            //execute after 1 second
            Invoke("playSeed", 8.0f);
            
        }
        
    }
    
    void playSeed()
    {
        seedList[(seedInt)%seedList.Count].GetComponent<ParticleSystem>().Play();
        ParticleSystem.MainModule _mainModule2=seedList[(seedInt)%seedList.Count].GetComponent<ParticleSystem>().main;
        _mainModule2.simulationSpeed =10f;
        isTriggered = true;
    }
}
