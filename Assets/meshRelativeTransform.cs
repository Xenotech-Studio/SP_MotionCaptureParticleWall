using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshRelativeTransform : MonoBehaviour
{
    public ParticleSystem ps;
    public Vector3 lastPlayerPos;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        lastPlayerPos=target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ParticleSystem.ShapeModule shape = ps.shape;
        //shape.position -=new Vector3(0f,0f,(target.transform.position.z-lastPlayerPos.z));
        //lastPlayerPos=target.transform.position;
        shape.position+=new Vector3(0f,0f,(-shape.position.z-46.7f));


	
    }
}
