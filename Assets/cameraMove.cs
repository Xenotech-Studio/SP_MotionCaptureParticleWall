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
    public Slider Slider_Human_X;
    public Slider Slider_Size;
    public Slider Slider_Camera_fov;
    public Slider Slider_DisappearingPt_Left;
    public Slider Slider_DisappearingPt_Right;
    
    public GameObject canvas;
    public TMP_Text text_X;
    public TMP_Text text_Y;
    public TMP_Text text_Height;
    public TMP_Text text_Human_X;
    public TMP_Text text_Size;
    public TMP_Text text_Camera_fov;
    public TMP_Text text_DisappearingPt_Left;
    public TMP_Text text_DisappearingPt_Right;
    
    private float lastValue_X;
    private float lastValue_Y;
    private float lastValue_Human_X;
    private float lastValue_Height;
    private float lastValue_disappearingPt_Left;
    private float lastValue_disappearingPt_Right;
    
    public string key_X;
    public string key_Y;
    public string key_Height;
    public string key_Size;
    public string human_X;
    public string key_Camera_fov;
    public string key_DisappearingPt_Left;
    public string key_DisappearingPt_Right;

    public GameObject targetCamera;
    public List<GameObject> playerList;
    public GameObject disappearingPt_Left;
    public GameObject disappearingPt_Right;
    // Start is called before the first frame update
    public float time;
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
        
        Slider_Human_X.value = PlayerPrefs.GetFloat(human_X, 0);
        lastValue_Human_X = Slider_Human_X.value;
        text_Human_X.text = Slider_Human_X.value.ToString();
        targetCamera.transform.position +=new Vector3(Slider_Human_X.value,0,0);
        
        Slider_Camera_fov.value = PlayerPrefs.GetFloat(key_Camera_fov, 54);
        text_Camera_fov.text = Slider_Camera_fov.value.ToString();
        targetCamera.GetComponent<Camera>().orthographicSize= Slider_Camera_fov.value;
        
        Slider_DisappearingPt_Left.value = PlayerPrefs.GetFloat(key_DisappearingPt_Left, 0);
        lastValue_disappearingPt_Left = Slider_DisappearingPt_Left.value;
        text_DisappearingPt_Left.text = Slider_DisappearingPt_Left.value.ToString();
        disappearingPt_Left.transform.position +=new Vector3(Slider_DisappearingPt_Left.value,0,0);
        
        Slider_DisappearingPt_Right.value = PlayerPrefs.GetFloat(key_DisappearingPt_Right, 0);
        lastValue_disappearingPt_Right = Slider_DisappearingPt_Right.value;
        text_DisappearingPt_Right.text = Slider_DisappearingPt_Right.value.ToString();
        disappearingPt_Right.transform.position +=new Vector3(Slider_DisappearingPt_Right.value,0,0);
    }

    void Update()
    {
        time+=Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
            disappearingPt_Left.SetActive(!disappearingPt_Left.activeSelf);
            disappearingPt_Right.SetActive(!disappearingPt_Right.activeSelf);
        }
    }
    
    public void OnSliderValueChanged_X()
    {
        if (!gameObject.activeSelf || time<1f)
        {
            return;
        }
        Debug.Log("IIIII");
        transform.position +=new Vector3( Slider_X.value-lastValue_X,0,0);
        text_X.text = Slider_X.value.ToString();
        lastValue_X = Slider_X.value;
        PlayerPrefs.SetFloat(key_X, Slider_X.value);
    }
    
    public void OnSliderValueChanged_Y()
    {
        if (!gameObject.activeSelf|| time<1f)
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
        if (!gameObject.activeSelf|| time<1f)
        {
            return;
        }
        GetComponent<Camera>().orthographicSize= Slider_Size.value;
        text_Size.text = Slider_Size.value.ToString();
        PlayerPrefs.SetFloat(key_Size, Slider_Size.value);
    }
    
    public void OnSliderValueChanged_Height()
    {
        if (!gameObject.activeSelf|| time<1f)
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
    
    public void OnSliderValueChanged_Human_X()
    {
        if (!gameObject.activeSelf|| time<1f)
        {
            return;
        }
        targetCamera.transform.position +=new Vector3(Slider_Human_X.value-lastValue_Human_X,0,0);
        text_Human_X.text = Slider_Human_X.value.ToString();
        lastValue_Human_X = Slider_Human_X.value;
        PlayerPrefs.SetFloat(human_X, Slider_Human_X.value);
    }
    
    
    public void OnSliderValueChanged_TargetCamera_Size()
    {
        if (!gameObject.activeSelf|| time<1f)
        {
            return;
        }
        targetCamera.GetComponent<Camera>().orthographicSize= Slider_Camera_fov.value;
        text_Camera_fov.text = Slider_Camera_fov.value.ToString();
        PlayerPrefs.SetFloat(key_Camera_fov, Slider_Camera_fov.value);
    }
    
    public void OnSliderValueChanged_DisappearingPt_Left()
    {
        if (!gameObject.activeSelf|| time<1f)
        {
            return;
        }
        disappearingPt_Left.transform.position +=new Vector3(Slider_DisappearingPt_Left.value-lastValue_disappearingPt_Left,0,0);
        text_DisappearingPt_Left.text = Slider_DisappearingPt_Left.value.ToString();
        lastValue_disappearingPt_Left = Slider_DisappearingPt_Left.value;
        PlayerPrefs.SetFloat(key_DisappearingPt_Left, Slider_DisappearingPt_Left.value);
    }
    
    public void OnSliderValueChanged_DisappearingPt_Right()
    {
        if (!gameObject.activeSelf|| time<1f)
        {
            return;
        }
        disappearingPt_Right.transform.position +=new Vector3(Slider_DisappearingPt_Right.value-lastValue_disappearingPt_Right,0,0);
        text_DisappearingPt_Right.text = Slider_DisappearingPt_Right.value.ToString();
        lastValue_disappearingPt_Right = Slider_DisappearingPt_Right.value;
        PlayerPrefs.SetFloat(key_DisappearingPt_Right, Slider_DisappearingPt_Right.value);
    }
    
}
