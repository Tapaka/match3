using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Levels))]
public class LevelsSetEditor : Editor {

    private Levels levels;
    
    void Awake()
    { 
        levels = target as Levels;
    }
    
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Add"))
            levels.Add(new Level());

        foreach (var item in levels.list)
        {
            GUILayout.BeginHorizontal();
            GUI.color = Color.white;
            if (GUILayout.Button(item.name + "- Edit"))
                LevelTemplate.Init(item);
            GUI.color = Color.red;
            if (GUILayout.Button("Remove"))
            {
                levels.Remove(item);
                break;
            }
            GUILayout.EndHorizontal();
        }
    }
}
