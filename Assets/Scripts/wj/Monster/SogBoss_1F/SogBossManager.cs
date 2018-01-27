//SogBossManager.cs
//TEOT_ONLINE
//
//Create by WangJie on 12/11/2017 3:55 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SogBossManager : SingletonMono<SogBossManager> 
{
    private const string SOG_BOSS = "Prefabs/Character/Enemy/SogBoss_1F/SogBoss_1F Root";
    private GameObject _sogBoss;

    private SogBossController _sogBossController;
    public SogBossController sogBossController
    {
        get { return _sogBossController; }
    }

    public void Init()
    {
        _sogBoss = UtilFunction.ResourceLoad(SOG_BOSS);
        _sogBossController = _sogBoss.transform.Find("SogBoss_1F").GetComponent<SogBossController>();
        _sogBossController.Init();
    }

    private void Start()
    {
        //Init();
    }
}
