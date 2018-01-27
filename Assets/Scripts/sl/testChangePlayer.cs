using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testChangePlayer : MonoBehaviour {

    public GameObject go;
    public  GameObject go1;
    public  GameObject go2;
    // Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void qwe() {
        go.SetActive(false);
        go1.SetActive(false);
        go2.SetActive(true);
    }

    public void ewq()
    {
        go.SetActive(true);
        go1.SetActive(true);
        go2.SetActive(false);
    }
}
