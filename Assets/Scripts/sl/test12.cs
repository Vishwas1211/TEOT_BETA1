using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using DG.Tweening;

public class test12 : PlayerGunBase
{
    private Vector3[] path = new Vector3[5];
    //protected override void OnEnable()
    //{
    //    base.OnEnable();
    //    events.TriggerPressed += new ControllerInteractionEventHandler(DoTrigger);
    //}

    //protected override void OnDisable()
    //{
    //    base.OnDisable();
    //    events.TriggerPressed -= new ControllerInteractionEventHandler(DoTrigger);
    //}
    // Use this for initialization
    void Start()
    {
        bulletNumMax = 25;
        shootTimer = 0.3f;
        shootPos = transform.Find("EthanGlasses").gameObject;

        //star = transform.Find("UIStar/star").gameObject;
        //scal = star.transform.localScale;
        //gunFX = transform.Find("MuzzleFlash7/GUN XIAO").GetComponent<ParticleSystem>();
        //gunAnimator = transform.GetComponent<Animator>();
    }

    public void UseTheTools1(GameObject goo)
    {
        //base.UseTheTools();
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.GetComponent<Collider>().enabled = false;
        go.transform.position = shootPos.transform.position;
        go.transform.rotation = shootPos.transform.rotation;
        //= Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), shootPos.transform.position,shootPos.transform.rotation);
        if (hit.transform == null)
        {
            //return;
        }
        //if (hit.transform.name == "Capsule")
        {
            path[0] = PlayerManager.Instance.gameObject.transform.position;
            path[4] = goo.transform.position;
            path[2] = (PlayerManager.Instance.transform.position - goo.transform.position) / 2 + goo.transform.position;
            path[2].y = path[4].y+1;
            path[3] = (path[2] - path[4]) / 2 + path[4];
            path[3].y = path[4].y + 2;
            path[1] = (path[0] - path[2]) / 2 + path[2];
            go.transform.DOMove(goo.transform.position, 2);
            StartCoroutine(qwe());
        }
    }

    IEnumerator qwe()
    {
        yield return new WaitForSeconds(2);
        PlayerManager.Instance.transform.DOPath(path, 2, PathType.CatmullRom).OnComplete(asd);
    }

    void asd() {
        TaskStepManagaer.Instance.FinishTaskTo(6003);

        //TaskStepManagaer.Instance.FinishCurTaskImmediately();
    }

    // Update is called once per frame
    private void Update()
    {
        ray = new Ray(shootPos.transform.position, shootPos.transform.forward);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            //star.transform.position = hit.point;
            //star.transform.localScale = scal * hit.distance * 2;
        }
        //Debug.Log("p:"+PlayerManager.Instance.gameObject.transform.position);
        //Debug.Log("h:"+hit.transform.position);
    }

    public void Jump()
    {
        transform.DOJump(new Vector3( -5.96f, 123.65f, 16.934f),3,1,2);
    }

}
