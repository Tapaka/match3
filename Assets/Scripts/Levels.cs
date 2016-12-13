using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Levels : ScriptableObject {

    public List<Level> list = new List<Level>();

    public void Add(Level item)
    {
        list.Add(item);
    }

    public void Remove(Level item)
    {
        list.Remove(item);
    }

}
