using UnityEngine;
using System.Collections;
using UnityEditor;

public class OptionEditor : Editor {

    [MenuItem("Assets/Create/Jewels")]
    public static void Test()
    {
        CustomAssetUtility.CreateAsset<Jewels>();
    }

}
