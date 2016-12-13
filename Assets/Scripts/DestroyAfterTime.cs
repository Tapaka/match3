using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

    public float ttl;

	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitForSeconds(ttl);
        Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
