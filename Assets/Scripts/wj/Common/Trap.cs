//
//  Trap.cs
//  TEOT_ONLINE
//
//  Created by 王颉 on 8/4/2017 11:50 AM.
//
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trap : MonoBehaviour {

    public TRAP_TYPE trapType;
    public enum TRAP_TYPE
    {
        GROUNDSPIKE,
        ELECTRICITY,
        CLAMP,
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            //TODO 玩家受伤
            switch (trapType)
            {
                case TRAP_TYPE.GROUNDSPIKE:
                    break;
                case TRAP_TYPE.ELECTRICITY:
                    break;
                case TRAP_TYPE.CLAMP:
                    break;
                default:
                    break;
            }
        }
    }

    void Start () 
    {

    }

    void Update () 
    {

    }
}