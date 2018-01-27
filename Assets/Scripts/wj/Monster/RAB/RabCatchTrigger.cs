//RabCatchTrigger.cs
//TEOT_ONLINE
//
//Create by WangJie on 12/28/2017 4:43 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabCatchTrigger : MonoBehaviour 
{
    private bool _isCatchPlayer;
    public bool isCatchPlayer
    {
        get { return _isCatchPlayer; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Player"))
        {
            _isCatchPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag.Equals("Player"))
        {
            _isCatchPlayer = false;
        }
    }
}
