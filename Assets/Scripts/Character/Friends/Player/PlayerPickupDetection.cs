using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupDetection : MonoBehaviour
{
    public static bool isb;
    HighlighterController hc;

    private void OnTriggerStay(Collider other)
    {
        if (hc = other.GetComponent<HighlighterController>())
        {
            hc.MouseOver();
            isb = true;
        }
        if (PlayerManager.Instance.rig && PlayerManager.Instance.rig.velocity.y < -20f)
        {
            //PlayerManager.Instance.playerInfo.OnHurt(100);
            return;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<HighlighterController>())
        {
            isb = false;
        }
    }
}
