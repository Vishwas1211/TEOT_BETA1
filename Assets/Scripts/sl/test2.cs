//
//  test2.cs
//  TEOT_ONLINE
//
//  Created by 孙磊 on 8/7/2017 2:27 PM.
//
//

using UnityEngine;
using System.Collections;

public class test2 : MonoBehaviour
{
    public static bool isLive;
    Animator ani;
    void Start()
    {
        ani = this.GetComponent<Animator>();
    }

    void Update()
    {
        if (isLive)
        {
            isLive = false;
            ani.Play("rebornFront");
        }
    }

}