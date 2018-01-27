
//RabManager.cs
//TEOT_ONLINE
//
//Create by WangJie on 9/18/2017 5:51 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabManager : SingletonMono<RabManager>
{
    private const string PREFAB_PATH = "Prefabs/Character/Enemy/RAB/Rab";

    private GameObject _rab;
    public GameObject rab
    {
        get { return _rab; }
    }

    private RabController _rabController;
    public RabController rabController
    {
        get { return _rabController; }
    }

    private RabAudioController _rabAudioController;
    public RabAudioController rabAudioController
    {
        get { return _rabAudioController; }
    }

    public void Init()
    {
        _rab = UtilFunction.ResourceLoad(PREFAB_PATH);
        _rabController = _rab.GetComponent<RabController>();
        _rabController.Init();
        _rabAudioController = _rab.GetComponent<RabAudioController>();
    }
    Item item = new Item() { Id = 20, Identifier = 0 ,ItemType = ItemType.TempProp};
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            //_rabController.SetState(RabController.RAB_STATE.PATROL);]

            BagModule.BagManager.Instance.TakeOutBag(item);
        }
    }
}
