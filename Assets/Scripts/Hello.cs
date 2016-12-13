using UnityEngine;
using System.Collections;

public class Hello : MonoBehaviour {


    public GameObject cube;

    // Test
	// Update is called once per frame


    void OnMouseDown()
    {
        Instantiate(cube);
    }

    public void CreateCube()
    {
        Instantiate(cube);
    }
}
