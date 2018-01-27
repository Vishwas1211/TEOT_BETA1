using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class testL18Boss : MonoBehaviour
{
    public Animator ani;
    private float _curHp = 5;
    private bool isDeath;
    private DOTweenVisualManager tp;

    private void Start()
    {
        tp = GetComponent<DOTweenVisualManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("flash"))
        {
            ShoShang(5);
        }
    }

    public void ShoShang(float hp)
    {
        if (isDeath) return;
        _curHp -= hp;
        if (_curHp <= 0)
        {
            UtilFunction.ResourceLoadOnPosition("L18key", transform.position, transform.rotation);
            tp.enabled = false;
            transform.GetComponent<BoxCollider>().enabled = false;
            ani.SetTrigger("isDeathTrigger");
            isDeath = true;
        }
    }

    public void ShouShang(float hp)
    {
        if (isDeath) return;
        _curHp -= hp;
        if (_curHp <= 0)
        {
            tp.enabled = false;
            transform.GetComponent<BoxCollider>().enabled = false;
            ani.SetTrigger("isDeathTrigger");
            isDeath = true;
            LizzyManager.Instance.FinishCurTaskImmediately(1019);
        }
    }

    public void GongJi() {
        ani.SetTrigger("isGrabTrigger");
    }
}
