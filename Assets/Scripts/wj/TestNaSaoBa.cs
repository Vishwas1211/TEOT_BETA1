using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNaSaoBa : MonoBehaviour {


    private Animator anim;

    private void Start()
    {
    }

    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "saoba")
        {
            other.transform.parent = gameObject.transform;
            other.transform.localRotation = Quaternion.Euler(180, 0, 0);
            other.transform.localPosition = new Vector3(0, 0.661f, 0);
        }
    }
}
