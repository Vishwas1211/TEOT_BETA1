using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testElectricute : MonoBehaviour
{
    public static bool ist;
    public static int i;
    public bool isb;

    public void setB(bool b) {
        isb = b;
    }

    public static void qwe()
    {
        i++;
        if (i > 0)
        {
            ist = true;
        }
    }

    public static void ewq() {
        i--;
        Debug.Log("i="+i);
        if (i <= 0)
        {
            ist = false;
        }
    }
}
