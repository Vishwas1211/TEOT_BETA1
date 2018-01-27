using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test28Animation : MonoBehaviour {
    public Animator animator;
    public Animator animator2;
    public Animator animator3;
    // Use this for initialization
    void Start () {
        //animator.Play("WarPlatformGlow");
        //animator2.Play("Take 001");
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    PlayAnimtion();
        //}
    }
    public void PlayAnimtion() {
        animator.Play("Take 001 (1)");
        animator2.Play("WarPlatformGlow");
        animator3.Play("Building_C");
    }
}
