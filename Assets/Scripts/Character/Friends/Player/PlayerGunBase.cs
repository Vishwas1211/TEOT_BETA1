//
//  PlayerGunBase.cs
//  TEOT_ONLINE
//
//  Created by 孙磊 on 8/11/2017 6:39 PM.
//
//

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerGunBase : PlayerToolsBase
{
    // 开枪特效
    protected ParticleSystem gunFX;
    //开枪射速
    protected float shootTimer;
    //开枪动画
    protected Animator gunAnimator;
    //准星
    protected GameObject star;
    protected Vector3 scal;
    //子弹数UI
    protected Text text_Bullet;
    protected Text text_BulletGun;
    protected GameObject GunPrefab;
    //枪口位置
    protected GameObject shootPos;
    //子弹数
    protected int bulletNumMax;
    protected Ray ray;
    protected RaycastHit hit;

    public virtual void Init()
    {
    }
    public virtual void GunTriggerAnimator()
    {
    }
    public virtual void GunUnTriggerAnimator()
    {
    }
    public virtual void PlayFX()
    {
    }
    public virtual void BulletUI(int currentBullet)
    {
    }

    public override void UseTheTools()
    {
        base.UseTheTools();

        GunTriggerAnimator();
        PlayFX();
    }
}