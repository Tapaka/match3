using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Jewels))]
public class JewelSetEditor : Editor {

    public Jewels jewels;
    public void Awake()
    {
        jewels = (Jewels)target;
    }
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Add Jewel"))
        {
            jewels.Add();
        }

        foreach (var item in jewels.list)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(item.type.ToString()))
            {
                JewelTemplate.Init(item);
            }
            GUI.color = Color.red;
            if (GUILayout.Button("Remove"))
            {
                jewels.Remove(item);
                break;
            }
            GUILayout.EndHorizontal();
            GUI.color = Color.white;
        }
    }
}
