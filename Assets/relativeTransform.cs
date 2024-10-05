using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class relativeTransform : MonoBehaviour
{
    public GameObject target;
    public Vector3 lastPlayerPos;
    // Start is called before the first frame update
    void Start()
    {
        lastPlayerPos=target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position +=new Vector3((target.transform.position.x-lastPlayerPos.x)/20,0,target.transform.position.z-lastPlayerPos.z);
        lastPlayerPos=target.transform.position;
    }
}
