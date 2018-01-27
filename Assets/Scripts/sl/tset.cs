using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tset : MonoBehaviour
{
    bool isb;
    public void flash()
    {
        if (!isb)
        {
            isb = true;
            UtilFunction.ResourceLoad("flash");
        }
    }
}
