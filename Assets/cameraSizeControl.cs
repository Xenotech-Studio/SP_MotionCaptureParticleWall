using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSizeControl : MonoBehaviour
{
    public float height;
    public float length;

    private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        _camera.orthographicSize=height/2;
        _camera.aspect=length/height;
    }
}
