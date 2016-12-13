using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

    public int count;
    // Use this for initialization
    public Levels levels;
    //index>0&&<....count-1
    public void BuildLevel(int index)
    {
        Level level = levels.list[index];
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
