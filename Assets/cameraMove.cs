using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class cameraMove : MonoBehaviour
{
    public Slider Slider_X;
    public Slider Slider_Y;
    public Slider Height;
    public Slider Slider_Size;
    
    public GameObject canvas;
    public TMP_Text text_X;
    public TMP_Text text_Y;
    public TMP_Text text_Height;
    public TMP_Text text_Size;
    
    private float lastValue_X;
    private float lastValue_Y;
    private float lastValue_Height;
    
    public string key_X;
    public string key_Y;
    public string key_Height;
    public string key_Size;
    
    public List<GameObject> playerList;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Start()
    {
        Slider_X.value = PlayerPrefs.GetFloat(key_X, 0);
        lastValue_X = Slider_X.value;
        text_X.text = Slider_X.value.ToString();
        transform.position +=new Vector3( Slider_X.value,0,0);
        
        Slider_Y.value = PlayerPrefs.GetFloat(key_Y, 0);
        lastValue_Y = Slider_Y.value;
        text_Y.text = Slider_Y.value.ToString();
        transform.position +=new Vector3( 0,0,Slider_Y.value);
        
        Height.value = PlayerPrefs.GetFloat(key_Height, 0);
        lastValue_Height = Height.value;
        text_Height.text = Height.value.ToString();
        for (int i = 0; i < playerList.Count; i++)
        {
            playerList[i].GetComponent<fixPosition>().initialPos -= new Vector3(0, 0,Height.value-lastValue_Height);
        }
        
        
        Slider_Size.value = PlayerPrefs.GetFloat(key_Size, 54);
        text_Size.text = Slider_Size.value.ToString();
        GetComponent<Camera>().orthographicSize= Slider_Size.value;
        
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
        }
    }
    
    public void OnSliderValueChanged_X()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        transform.position +=new Vector3( Slider_X.value-lastValue_X,0,0);
        text_X.text = Slider_X.value.ToString();
        lastValue_X = Slider_X.value;
        PlayerPrefs.SetFloat(key_X, Slider_X.value);
    }
    
    public void OnSliderValueChanged_Y()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        transform.position +=new Vector3( 0,0,Slider_Y.value-lastValue_Y);
        text_Y.text = Slider_Y.value.ToString();
        lastValue_Y = Slider_Y.value;
        PlayerPrefs.SetFloat(key_Y, Slider_Y.value);
    }
    
    public void OnSliderValueChanged_Size()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        GetComponent<Camera>().orthographicSize= Slider_Size.value;
        text_Size.text = Slider_Size.value.ToString();
        PlayerPrefs.SetFloat(key_Size, Slider_Size.value);
    }
    
    public void OnSliderValueChanged_Height()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        for (int i = 0; i < playerList.Count; i++)
        {
            playerList[i].GetComponent<fixPosition>().initialPos -= new Vector3(0, 0,Height.value-lastValue_Height);
        }
        text_Height.text = Height.value.ToString();
        lastValue_Height = Height.value;
        PlayerPrefs.SetFloat(key_Height, Height.value);
    }
    
}
