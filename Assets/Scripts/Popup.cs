using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Popup: MonoBehaviour  {

    public GameObject window;
    public Text messageField;

    public Popup()
    {
    }
   

    public void Show(string message)
    {
       
        messageField.text =  message;
        window.SetActive(true);
    }

    public void Hide()
    {
        window.SetActive(false);
    }
        

}

