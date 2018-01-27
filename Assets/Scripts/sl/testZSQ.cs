using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testZSQ : MonoBehaviour
{
    public bool isGetKey;
    bool isDeath = false;
    private float _curHp = 5;
    public Animator ani;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShoShang(float hp)
    {
        if (isDeath) return;
        _curHp -= hp;
        //if (_curHp <= 2 && (TaskStepManagaer.Instance.IsEqualTaskId(19005)))
        //{
        //    if (!testControllerSog.isDeath)
        //    {
        //GameObject.Find("reshuihu (106)").GetComponent<testChangePlayer>().ewq();
        //    }
        //    testControllerSog.isDeath = true;
        //}
        if (_curHp <= 0)
        {
            transform.GetComponent<BoxCollider>().enabled = false;
            ani.SetTrigger("isDeathTrigger");
            isDeath = true;
            if (isGetKey)
            {
                UtilFunction.ResourceLoadOnPosition("L13key", transform.position, transform.rotation);
            }
        }
    }
}
