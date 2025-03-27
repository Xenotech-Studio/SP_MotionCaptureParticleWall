using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixPosition : MonoBehaviour
{
    public Vector3 lastPlayerPos;

    public Vector3 initialPos;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position=new Vector3(transform.position.x,initialPos.y,initialPos.z);
    }
}
