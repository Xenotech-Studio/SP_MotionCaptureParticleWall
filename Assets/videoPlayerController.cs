using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class videoPlayerController : MonoBehaviour
{
    public VideoPlayer VideoPlayer;

    public Transform TriggerPt;
    public List<Transform> playerPos;
    public float startTime;

    public float length;
    // Start is called before the first frame update
    void Start()
    {
        VideoPlayer.time = startTime;
        GetComponent<VideoPlayer>().Pause();
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (var VARIABLE in playerPos)
        {
            if (Mathf.Abs(TriggerPt.position.x-VARIABLE.position.x) < length&&VideoPlayer.isPlaying==false)
            {
                VideoPlayer.Play();
                if (gameObject.GetComponent<MaterialColorControl>() != null)
                {
                    gameObject.GetComponent<MaterialColorControl>().enabled = true;
                }
                return;
            }
        }
       /*if (VideoPlayer.isPlaying==false)
        {
            VideoPlayer.time = startTime;
            GetComponent<VideoPlayer>().Pause();
        }*/
       
        

        
            
    }
}
