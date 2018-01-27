using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test14 : MonoBehaviour {
    public Animator animator;
    void Dead() {
        animator.Play("death1backstyle1");
        TaskStepManagaer.Instance.FinishCurTaskImmediately();
    }
}
