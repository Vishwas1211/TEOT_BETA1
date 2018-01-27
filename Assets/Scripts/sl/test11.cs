using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test11 : MonoBehaviour
{
    public Animator animator;
    public GameObject hand;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Ray r = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit, 0.1f))
        {
            if (hit.transform.name.Contains("Step0") && collision.transform.name.Contains("Step0"))
            {
                hand.transform.Find("RadialMenu/RadialMenuUI/Panel").GetComponent<BackpackController>().RemoveTools(3);
                transform.rotation = Quaternion.Euler(0,0,0);
                transform.parent = null;

                animator.Play("Take 001");
                TaskStepManagaer.Instance.FinishCurTaskImmediately();
            }
        }
    }
}
