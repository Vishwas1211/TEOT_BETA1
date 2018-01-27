using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUseItemEvent : MonoBehaviour
{
    private void OnEnable()
    {
        GlobalEvent.useItemEvent.AddListener(UseItem);
        GlobalEvent.discardEvent.AddListener(DiscardItem);
    }

    private void OnDisable()
    {
        GlobalEvent.useItemEvent.RemoveListener(UseItem);
        GlobalEvent.discardEvent.RemoveListener(DiscardItem);
    }

    private void UseItem(Item item)
    {
        switch (item.ItemType)
        {
            case ItemType.Rifle:
                if (item.Name == "AK47")
                {
                    ShowItem(item);
                }
                Debug.Log(string.Format("<color=blue> TODO:玩家装备武器：{0}</color>", item.Name));
                break;
            case ItemType.Pistol:
                if (item.Name == "Handgun")
                {
                    ShowItem(item);
                }
                Debug.Log(string.Format("<color=blue> TODO:玩家装备武器：{0}</color>", item.Name));
                break;
            case ItemType.Grenade:
                break;
            case ItemType.CloseWeapon:
                break;
            case ItemType.Disk:
                break;
            case ItemType.DataPaper:
                if (item.Identifier == 1)
                {
                    Debug.Log(1);
                    GameObject go = GameObject.Find(item.GoPath).gameObject;
                    go.GetComponent<testLookZiLiao>().qwe();
                }
                break;
            case ItemType.WorkCard:
                break;
            case ItemType.Antidote:
                break;
            case ItemType.BloodPack:
                Debug.Log(string.Format("<color=blue>TODO:玩家使用血包：{0}</color>", item.Name));
                break;
            case ItemType.None:
                break;
            default:
                Debug.Log(string.Format("<color=blue>TODO:玩家查看资料：{0} 识别码为{1}</color>", item.Name, item.Identifier));
                break;
        }
        //if (item.ItemType == ItemType.Rifle || item.ItemType == ItemType.CloseWeapon || item.ItemType == ItemType.Grenade)
        //{

        //}
        //else if (item.ItemType == ItemType.BloodPack)
        //{
        //}
        //else
        //{
        //}
    }

    private void DiscardItem(Item item)
    {
        if (item.ItemType == ItemType.Rifle || item.ItemType == ItemType.CloseWeapon || item.ItemType == ItemType.Grenade)
        {
            Debug.Log(string.Format("<color=blue>TODO:玩家丢除武器(在脚下实例化)：{0}</color>", item.Name));
        }
    }

    private void ShowItem(Item item)
    {
        PlayerManager.Instance.testToolsManagers.HideAllItem();
        switch (item.Id)
        {
            case 0:
                transform.Find(item.GoPath).gameObject.SetActive(true);
                PlayerManager.Instance.testToolsManagers.setItem(item);
                break;
            case 1:
                transform.Find(item.GoPath).gameObject.SetActive(true);
                PlayerManager.Instance.testToolsManagers.setItem(item);
                break;
            case 2:
                transform.Find(item.GoPath).gameObject.SetActive(true);
                PlayerManager.Instance.testToolsManagers.setItem(item);
                break;
            default:
                break;
        }
    }

}
