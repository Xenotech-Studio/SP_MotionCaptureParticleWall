using System.Collections;
using System.Collections.Generic;
using Crest;
using UnityEngine;

public class lateUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SphereWaterInteraction>().enabled = true;
    }
}
