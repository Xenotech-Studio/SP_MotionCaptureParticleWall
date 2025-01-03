using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class cameraMove : MonoBehaviour
{
    

    public Slider Slider;

    public GameObject canvas;
    public TMP_Text text;
    private float lastValue;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
        }
       
    }
    
    public void OnSliderValueChanged()
    {
        transform.position +=new Vector3( Slider.value-lastValue,0,0);
        text.text = Slider.value.ToString();
        lastValue = Slider.value;
    }
}
