using UnityEngine;
using System.Collections;

public class DestroyCube : MonoBehaviour {

    public MeshRenderer renderer;
    public Color[] colors;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter(Collider col)
    {
        //col.GetComponent<MeshRenderer>().material.color = colors[Random.Range(0, colors.Length)];
        Destroy(col.gameObject);
    }
}
