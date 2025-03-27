using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScreenDataManager : MonoBehaviour
{
    public UnityEngine.UI.Button button1;
    public UnityEngine.UI.Button button2;
    public UnityEngine.UI.Button button3;
    public UnityEngine.UI.Button button4;

    public int key_value;
    
    
    void Start()
    {
        key_value = PlayerPrefs.GetInt("ScreenId", 0);
        if(key_value == 0)
        {
           return;
        }
        else if(key_value == 1)
        {
            button1.onClick.Invoke();
        }
        else if(key_value == 2)
        {
            button2.onClick.Invoke();
        }
        else if(key_value == 3)
        {
            button3.onClick.Invoke();
        }
        else if(key_value == 4)
        {
            button4.onClick.Invoke();
        }
    }

    // Update is called once per frame
    public void SetScreenId1()
    {
        PlayerPrefs.SetInt("ScreenId", 1);
    }
    
    public void SetScreenId2()
    {
        PlayerPrefs.SetInt("ScreenId", 2);
    }
    
    public void SetScreenId3()
    {
        PlayerPrefs.SetInt("ScreenId", 3);
    }
    
    public void SetScreenId4()
    {
        PlayerPrefs.SetInt("ScreenId", 4);
    }
    
    public void SetScreenId0()
    {
        PlayerPrefs.SetInt("ScreenId", 0);
    }
}
