using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    float t;
    bool isb;
    public float speed;
    public bool isOpen;
    // Update is called once per frame
    void Update()
    {
        if (NoLockView_Camera.can_4_1)
        {
            if (t >= 5) isb = true;

            if (t <= 0) isb = false;

            if (isb)
            {
                t -= Time.deltaTime * speed;
            }
            else
            {
                t += Time.deltaTime * speed;
            }
            transform.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(t, t, t));
        }

    }
}
