using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Jewels : ScriptableObject {
    public List<JewelScriptable> list = new List<JewelScriptable>();

    public Sprite GetJewelSprite(JewelScriptable.Type type)
    {
        JewelScriptable jewel = list.Find(j => j.type == type);
        return jewel == null ? null : Resources.Load<Sprite>(jewel.spritePath);
    }
    
    public void Add()
    {
        list.Add(new JewelScriptable());
    }

    internal void Remove(JewelScriptable item)
    {
        list.Remove(item);
    }
}

[System.Serializable]
public class JewelScriptable
{
    public enum Type
    {
        A,
        B,
        C,
        D
    }
    public Type type;
    public string spritePath;
}
