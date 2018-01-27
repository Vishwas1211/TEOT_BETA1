using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test28Door : MonoBehaviour
{
    public GameObject good;
    public GameObject bad;
    public Animator animator;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name.Contains("Struggle"))
        {
            good.SetActive(false);
            bad.SetActive(true);
            animator.Play("Take 001");
            this.GetComponent<Collider>().enabled = false;
        }
    }
}
