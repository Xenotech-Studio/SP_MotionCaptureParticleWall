using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExecuteOnEnable : MonoBehaviour
{
    public UnityEvent ExeOnEnable;

    public float Delay = 0;

    // Start is called before the first frame update
    private void OnEnable()
    {
        if (Delay==0) ExeOnEnable?.Invoke();
        else StartCoroutine(Execute());
    }

    private IEnumerator Execute()
    {
        yield return new WaitForSeconds(Delay);
        ExeOnEnable?.Invoke();
    }
    
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
