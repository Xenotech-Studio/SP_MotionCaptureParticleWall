using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExecuteOnDisable : MonoBehaviour
{
    public UnityEvent ExeOnEnable;

    public float Delay = 0;

    // Start is called before the first frame update
    private void OnDisable()
    {
        ExeOnEnable?.Invoke();
    }
}


