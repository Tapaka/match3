using UnityEngine;
using System.Collections;

public class ParticleDestroy : MonoBehaviour {

    public GameObject particle;


    void OnDestroy()
    {
        Instantiate(particle, gameObject.transform.position, Quaternion.identity);
    }
}
