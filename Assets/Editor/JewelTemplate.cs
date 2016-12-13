using UnityEngine;
using System.Collections;
using UnityEditor;

public class JewelTemplate : EditorWindow {

    static EditorWindow window = null;
    public static JewelScriptable template;

    public static void Init(JewelScriptable jewel)
    {
        if (window !=null)
        {
            window.ShowNotification(new GUIContent("please close this template editor \n before editing template"));
            window.Repaint();
            return;
        }
        template = jewel;
        window = ScriptableObject.CreateInstance<JewelTemplate>();
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 400, 500);
        window.titleContent.text = "Jewel Editor";
        window.ShowUtility();
    }

    void OnGUI()
    {
        template.spritePath = GUILayout.TextField("Sprite path", template.spritePath);
        template.type = (JewelScriptable.Type)EditorGUILayout.EnumPopup("type", template.type);
    }
}
