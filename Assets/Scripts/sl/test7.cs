using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test7 : MonoBehaviour {
    public static bool isOnce;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isOnce)
        {
            if (transform.position.x < -25.3f)
            {
                isOnce = true;
            }
        }
	}
}
