using UnityEngine;
using System.Collections;

[System.Serializable]
public class Jewel : MonoBehaviour
{

    public enum JewelType
    { 
        Aqua,
        Red,
        Blue,
        Yellow,
        Green,
        Pink
    }
    public JewelType type;
    public Sprite sprite;

    public Jewel Init()
    {
        Generate();
        return this;
    }

    public void Generate()
    {
        type = (JewelType)Random.Range(0, 6);
        sprite = GridBuilder.GetSprite((int)type);
    }

    public void Explode()
    {
        
    }
}
