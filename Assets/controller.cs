using System.Collections;
using System.Collections.Generic;
using Crest;
using UnityEngine;

public class controller : MonoBehaviour
{
    public GameObject player;
    public GameObject vectorField1;
    public GameObject vectorField2;
    public GameObject vectorField3;
    public GameObject vectorField4;
    public ShapeFFT shapeFFT;
    public OceanRenderer oceanRenderer;
    public Vector3 lastPlayerPos;
    private float stillTime;
    public int isleft = 1;
    public float distance;


    // Start is called before the first frame update
    void Start()
    {
        lastPlayerPos=player.transform.position;
       // shapeFFT._windSpeed= 0;
        transform.position.Set(player.transform.position.x,transform.position.y,transform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Mathf.Abs(player.transform.position.x -  lastPlayerPos.x)<0.4f)//记得换成误差型
        {
            vectorField1.SetActive(false);
            vectorField2.SetActive(false);
            vectorField3.SetActive(false);
            vectorField4.SetActive(false);
            return;
        }
        else if (player.transform.position.x > lastPlayerPos.x && isleft > 0 && player.transform.position.x - lastPlayerPos.x > 2.3f)
        {
            isleft *= -1;
        }

        if (player.transform.position.x < lastPlayerPos.x && isleft < 0 &&  lastPlayerPos.x -player.transform.position.x> 2.3f)
        {
            isleft *= -1;
        }
        
        transform.position +=new Vector3(player.transform.position.x-lastPlayerPos.x,0,0);
       
        if(isleft>0)
        { 
            vectorField1.SetActive(true);
            vectorField2.SetActive(true);
        }
        else
        {
            vectorField3.SetActive(true);
            vectorField4.SetActive(true);
        }
        
        lastPlayerPos=player.transform.position;
        distance=player.transform.position.x - lastPlayerPos.x;
        
    }
}
