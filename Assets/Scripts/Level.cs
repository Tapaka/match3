using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Level {

    public string name;
    public List<LevelCell> cells = new List<LevelCell>();
    public int width, height;
    public bool isReady = false;
}

[System.Serializable]
public class LevelCell
{
    public enum typeCell
    { 
        Empty,
        Blocked
    }

    public int x, y;
    public typeCell type;
}
