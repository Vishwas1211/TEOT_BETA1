//TimeTest.cs
//TEOT_ONLINE
//
//Create by WangJie on 9/9/2017 11:44 AM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTest : MonoBehaviour 
{
    public void TimeScaleDown()
    {
        Time.timeScale = 0.25f;
    }

    public void TimeScaleUp()
    {
        Time.timeScale = 1f;
    }
}
