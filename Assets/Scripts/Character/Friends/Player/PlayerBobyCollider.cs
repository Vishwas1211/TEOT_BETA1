using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBobyCollider : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name.Equals("Arrow3"))
        {
            NoLockView_Camera.can_1_7 = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name.Equals("Arrow3"))
        {
            NoLockView_Camera.can_1_7 = false;
        }
    }
}
