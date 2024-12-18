using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixPosition : MonoBehaviour
{
    public Vector3 lastPlayerPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position+=new Vector3(0,-transform.position.y+lastPlayerPos.y,-transform.position.z+lastPlayerPos.z);
        lastPlayerPos=transform.position;
    }
}
