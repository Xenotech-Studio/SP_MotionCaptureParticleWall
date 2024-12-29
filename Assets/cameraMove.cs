using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class cameraMove : MonoBehaviour
{
    public Camera _Camera;

    public Slider Slider;

    public GameObject canvas;
    public TMP_Text text;
    private float lastValue;
    // Start is called before the first frame update
    void Start()
    {
        _Camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            canvas.SetActive(!canvas.activeSelf);
        }
       
    }
    
    public void OnSliderValueChanged()
    {
        _Camera.transform.position +=new Vector3( Slider.value-lastValue,0,0);
        text.text = Slider.value.ToString();
        lastValue = Slider.value;
    }
}
