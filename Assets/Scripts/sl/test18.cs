using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test18 : MonoBehaviour {
    static Rigidbody rig;
	// Use this for initialization
	void Start () {
        rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKeyDown(KeyCode.Y))
        //{

        //    Fly();
        //}
	}

    public static void Fly() {
        //rig.AddForce(-1000, 100, -300);
        rig.AddForce(new Vector3(-1000, 100, -450));
        rig.AddTorque(100, 100, 100);
    }
}
