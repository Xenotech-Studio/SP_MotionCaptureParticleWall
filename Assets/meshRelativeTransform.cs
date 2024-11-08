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
        shape.position -=new Vector3(0f,0f,(target.transform.position.z-lastPlayerPos.z)/2);
        lastPlayerPos=target.transform.position;
    }
}
