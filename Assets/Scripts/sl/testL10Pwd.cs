using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testL10Pwd : MonoBehaviour
{
    public Animator animator;
    bool isb;
    AnimatorStateInfo info; // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        info = animator.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("Power0"))
        {
            if (info.normalizedTime > 1 && !isb)
            {
                isb = true;
                transform.GetComponent<testPWD>().QWE();
            }
        }
        if (info.IsName("Power2"))
        {
            if (info.normalizedTime > 1)
            {
                OpenTV("Power3");
            }
        }
        if (info.IsName("Power3"))
        {
            if (info.normalizedTime > 1)
            {

            }
        }
        if (info.IsName("Power4"))
        {
            if (info.normalizedTime > 1)
            {
                OpenTV("New State");
            }
        }
    }

    public void OpenTV(string s)
    {
        if (s.Equals("Power0"))
        {
            if (info.IsName("Power0") && info.normalizedTime > 1)
            {
                transform.GetComponent<testPWD>().QWE();
                return;
            }
        }
        animator.Play(s);
    }


}
