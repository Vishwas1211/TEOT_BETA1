//
//  backpack.cs
//  TEOT_ONLINE
//
//  Created by 孙磊 on 8/7/2017 3:01 PM.
//
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using VRTK;

public class BackpackItems
{
    public GameObject item;
    public VRTK_RadialMenu.RadialMenuButton button;
    public bool isShow;
}

public delegate void BackpackDelegate(int id);
public class Backpack : SingletonMono<Backpack>
{
    public static event BackpackDelegate AddBackpackEvent;
    public static event BackpackDelegate RemoveBackpackEvent;

    public void AddToolsEvent(int id)
    {
        if (AddBackpackEvent != null)
        {
            AddBackpackEvent(id);
        }
        else
        {
            Debug.Log("Delegate null");
        }
    }

    public void RemoveToolsEvent(int id)
    {
        if (RemoveBackpackEvent != null)
        {
            RemoveBackpackEvent(id);
        }
        else
        {
            Debug.Log("Delegate null");
        }
    }
    public Dictionary<int, BackpackItems> dicBackpack = new Dictionary<int, BackpackItems>();
    public BackpackItems[] backpackItems = new BackpackItems[7];
    public List<BackpackItems> listBackpack = new List<BackpackItems>();
    /// <summary>
    /// 初始化添加道具
    /// </summary>
    public void Init()
    {

        for (int i = 0; i < backpackItems.Length; i++)
        {
            backpackItems[i] = new BackpackItems();
            backpackItems[i].button = new VRTK_RadialMenu.RadialMenuButton();
            backpackItems[i].button.OnClick = new UnityEngine.Events.UnityEvent();
            backpackItems[i].button.OnHold = new UnityEngine.Events.UnityEvent();
        }
        InitBackpackItem(0, "Prefabs/Tools/Bare_Knuckled", "Icons/icon_gun");
        InitBackpackItem(1, "Prefabs/Weapon/Gun/HandGun", "Icons/icon_gun");
        InitBackpackItem(2, "Prefabs/Tools/SaoMiaoQiang", "Icons/icon_energy");
        InitBackpackItem(3, "Prefabs/Tools/XinHaoQi", "Icons/icon_gun");
        InitBackpackItem(4, "Prefabs/Weapon/ColdWeapon/Knife", "Icons/icon_gun");
        InitBackpackItem(5, "Prefabs/Tools/GuaGou", "Icons/icon_gun");
        InitBackpackItem(6, "Prefabs/Tools/Health_Box", "Icons/icon_gun");
        //backpackItems[4].item = Resources.Load<GameObject>("Prefabs/Weapon/ColdWeapon/Knife");
        //backpackItems[4].button.ButtonIcon = Resources.Load<Sprite>("Icons/icon_energy");
        //backpackItems[4].button.OnClick.AddListener(delegate
        //{
        //    this.UseTools(4, RadialMenu.strName);
        //});

        dicBackpack.Add(0, backpackItems[0]);
        dicBackpack.Add(1, backpackItems[1]);
        dicBackpack.Add(2, backpackItems[2]);
        //dicBackpack.Add(3, backpackItems[3]);

        listBackpack.Add(backpackItems[0]);
        listBackpack.Add(backpackItems[1]);
        listBackpack.Add(backpackItems[2]);
        //listBackpack.Add(backpackItems[3]);

    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    //ResetLeftTools();
        //    for (int i = 0; i < 100; i++)
        //    {
        //        PlayerManager.Instance.rightHandController.transform.Find("RadialMenu/RadialMenuUI/Panel").GetComponent<RadialMenu>().RemoveButtons(1);
        //    }
        //}
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    for (int i = 0; i < 100; i++)
        //    {
        //        PlayerManager.Instance.rightHandController.transform.Find("RadialMenu/RadialMenuUI/Panel").GetComponent<RadialMenu>().AddButton(dicBackpack[1].button);
        //    }
        //}
    }


    private void InitBackpackItem(int id,string path,string logoPath) {
        backpackItems[id].item = Resources.Load<GameObject>(path);
        backpackItems[id].button.ButtonIcon = Resources.Load<Sprite>(logoPath);
        backpackItems[id].button.OnClick.AddListener(delegate
        {
            this.UseTools(id);
        });
        //dicBackpack.Add(id, backpackItems[id]);
    }

    public void UseTools(int id)
    {
        if ((PlayerManager.Instance.leftHand.transform.Find("Tools/" + backpackItems[id].item.name).gameObject.activeSelf || PlayerManager.Instance.rightHand.transform.Find("Tools/" + backpackItems[id].item.name).gameObject.activeSelf) && id != 0)
        {
            return;
        }
        if (name.Contains("Controller (right)"))
        {
            PlayerManager.Instance.rightBackpackController.UseTools(id);
        }
        else
        {
            PlayerManager.Instance.leftBackpackController.UseTools(id);
        }
        //Debug.Log(id);
        //if ((PlayerManager.Instance.rightHandTools.transform.Find(backpackItems[id].item.name).gameObject.activeSelf || PlayerManager.Instance.leftHandTools.transform.Find(backpackItems[id].item.name).gameObject.activeSelf) && id != 0)
        //{
        //    return;
        //}
        //if (name.Contains("Controller (right)"))
        //{
        //    //ResetRightTools();
        //    backpackItems[id].isShow = true;
        //    PlayerManager.Instance.rightHandTools.transform.Find(backpackItems[id].item.name).gameObject.SetActive(backpackItems[id].isShow);
        //    switch (id)
        //    {
        //        case 0:

        //            //PlayerManager.Instance.playerRightHandAnimation.SetRightHandState(0);
        //            //PlayerManager.Instance.playerRightHandAnimation.TriggerReleased();
        //            break;
        //        case 1:
        //            //PlayerManager.Instance.playerRightHandAnimation.SetRightHandState(1);
        //            //PlayerManager.Instance.playerRightHandAnimation.Gun();
        //            break;
        //        case 2:
        //            //PlayerManager.Instance.playerRightHandAnimation.SetRightHandState(2);
        //            //PlayerManager.Instance.playerRightHandAnimation.TriggerPressed();
        //            break;
        //    }
        //}
        //else
        //{
        //    ResetLeftTools();
        //    _backpackItems[id].isShow = true;
        //    PlayerManager.Instance.leftHandTools.transform.Find(_backpackItems[id].item.name).gameObject.SetActive(_backpackItems[id].isShow);
        //    switch (id)
        //    {
        //        case 0:
        //            //PlayerManager.Instance.playerLeftHandAnimation.SetLeftHandState(0);
        //            //PlayerManager.Instance.playerLeftHandAnimation.TriggerReleased();
        //            break;
        //        case 1:
        //            //PlayerManager.Instance.playerLeftHandAnimation.SetLeftHandState(1);
        //            //PlayerManager.Instance.playerLeftHandAnimation.Gun();
        //            break;
        //        case 2:
        //            //PlayerManager.Instance.playerLeftHandAnimation.SetLeftHandState(2);
        //            //PlayerManager.Instance.playerLeftHandAnimation.TriggerPressed();
        //            break;
        //    }
        //}
        //_backpackItems[id].item.SetActive(_backpackItems[id].isShow);
    }

    public void AddTools(int id)
    {
        if (!dicBackpack.ContainsKey(id))
        {
            dicBackpack.Add(id, backpackItems[id]);
            listBackpack.Add(backpackItems[id]);
            AddToolsEvent(id);
        }
    }

    public void RemoveTools(int id)
    {
        if (dicBackpack.ContainsKey(id))
        {
            Debug.Log(id);
            dicBackpack.Remove(id);
            listBackpack.Remove(backpackItems[id]);
            RemoveToolsEvent(id);
        }
    }
}