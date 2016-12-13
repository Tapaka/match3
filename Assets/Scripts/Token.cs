using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Token : MonoBehaviour
{
    public string access_token { get; set; }
    public int expires_in;
    public Token()
    {

    }
}


