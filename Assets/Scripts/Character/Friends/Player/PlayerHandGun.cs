//
//  PlayerHandGun.cs
//  TEOT_ONLINE
//
//  Created by 孙磊 on 8/11/2017 7:26 PM.
//
//

using UnityEngine;
using System.Collections;
using System;
using VRTK;

public class PlayerHandGun : PlayerGunBase
{
    protected override void OnEnable()
    {
        base.OnEnable();
        events.TriggerPressed += new ControllerInteractionEventHandler(DoTrigger);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        events.TriggerPressed -= new ControllerInteractionEventHandler(DoTrigger);
    }

    void Start()
    {
        bulletNumMax = 25;
        shootTimer = 0.3f;
        shootPos = transform.Find("ShootPos").gameObject;
        star = transform.Find("UIStar/star").gameObject;
        scal = star.transform.localScale;
        gunFX = transform.Find("MuzzleFlash7/GUN XIAO").GetComponent<ParticleSystem>();
        gunAnimator = transform.GetComponent<Animator>();
    }

    private void Update()
    {
        ray = new Ray(shootPos.transform.position, shootPos.transform.forward);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            star.transform.position = hit.point;
            star.transform.localScale = scal * hit.distance * 2;
            //if (PlayerRightController.isTrigger)
            //{
            //    PlayerRightController.isTrigger = false;
            //    if (_hit.transform.name == "ZMB_0")
            //    {
            //        _hit.transform.GetComponent<HumanlikeWounded>().Harm(2);
            //    }
            //}
        }
    }

    //public override void DoTrigger(object sender, ControllerInteractionEventArgs e)
    //{
    //    if (hit.transform.name == "ZMB_0")
    //    {
    //        hit.transform.GetComponent<HumanlikeWounded>().Harm(2);
    //    }
    //}

    public override void UseTheTools()
    {
        base.UseTheTools();
        if (hit.transform == null)
        {
            return;
        }
        if (hit.transform.root.name == "SogBoss_1F Root(Clone)")
        {
            hit.transform.root.Find("SogBoss_1F").GetComponent<SogBossController>().OnHurt(10);
        }
    }

    public override void BulletUI(int currentBullet)
    {
    }

    public override void GunTriggerAnimator()
    {
        gunAnimator.Play("Take 001");
    }

    public override void PlayFX()
    {
        gunFX.Play();
    }

    public override void GunUnTriggerAnimator()
    {
    }
}