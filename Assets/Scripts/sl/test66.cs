//
//  test66.cs
//  TEOT_ONLINE
//
//  Created by 孙磊 on 8/14/2017 2:26 PM.
//
//

using UnityEngine;
//using System.Collections;
using System.Collections.Generic;

public class test66 : MonoBehaviour
{
    bool isOnce = true;
    private void Update()
    {
        if (isOnce)
        {
            if (transform.position.x < -25.3f)
            {
                isOnce = false;
                GameObject.FindGameObjectWithTag("MainCamera").transform.Find("NIC").GetComponent<test33>().Step2();
            }
        }
    }

    public void qqqq()
    {

    }
}