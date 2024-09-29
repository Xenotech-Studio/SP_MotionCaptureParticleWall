using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnterPassword : MonoBehaviour
{
    public GameObject Buttons;
    public TMP_InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        //inputField = gameObject.GetComponent<InputField>();
    }

    // Update is called once per frame
    public void checkInput()
    {
        if (inputField.text == null)
        {
            return;
        }
        if(inputField.text == "XenoTech666!")
        {
            Buttons.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
