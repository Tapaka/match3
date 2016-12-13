using UnityEngine;
using System.Collections;
using UnityEditor;

public class LevelTemplate : EditorWindow {

    static EditorWindow window = null;
    public static Level template;

    public static void Init(Level level)
    {
        if (window != null)
        {
            window.ShowNotification(
                new GUIContent(
                    "Please close this template editor \n " +
                    "before editing other template"));
            window.Repaint();
            return;
        }

        template = level;
        window = ScriptableObject.CreateInstance<LevelTemplate>();
        window.position = new Rect(Screen.width / 2,
            Screen.height / 2, 400, 500);
        window.minSize = new Vector2(300, 100);
        window.wantsMouseMove = true;
        window.titleContent.text = "Level Editor";
        window.ShowUtility();
    }

    void OnGUI()
    {
        template.name = EditorGUILayout.TextField("Level name", template.name);
        template.width = EditorGUILayout.IntField("Width", template.width);
        template.height = EditorGUILayout.IntField("Height", template.height);
        if (GUILayout.Button("Generate Grid"))
        {
            template.cells.Clear();
            for (int i = 0; i < template.height; i++)
            {
                for (int j = 0; j < template.width; j++)
                {
                    template.cells.Add(
                        new LevelCell() 
                        { x = j, y = i, type = LevelCell.typeCell.Empty });
                }
            }
            template.isReady = true;
        }

        if (template.isReady)
        {
            int iterator = 0;
            for (int i = 0; i < template.height; i++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int j = 0; j < template.width; j++)
                {

                    switch (template.cells[iterator].type)
                    {
                        case LevelCell.typeCell.Empty:
                            GUI.color = Color.white;
                            break;
                        case LevelCell.typeCell.Blocked:
                            GUI.color = Color.red;
                            break;
                    }

                        template.cells[iterator].type =
                        (LevelCell.typeCell)EditorGUILayout.EnumPopup(
                            template.cells[iterator].type);
                        iterator++;   
                }

                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
