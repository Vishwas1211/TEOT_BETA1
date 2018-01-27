using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMode : SingletonMono<PlayerMode> {
    public enum ePlayerMode {
        vive,
        pc,
    }
    public ePlayerMode playerMode = ePlayerMode.pc;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public bool GetPlayerMode(ePlayerMode e) {
        switch (e)
        {
            case ePlayerMode.vive:
                return true;
            case ePlayerMode.pc:
                return true;
            default:
                break;
        }
        return false;
    }
}
