using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test13 : MonoBehaviour {
    public GameObject go;
    private void OnCollisionEnter(Collision collision)
    {
        go.SetActive(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        go.SetActive(false);
    }
}
