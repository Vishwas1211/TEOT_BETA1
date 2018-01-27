using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 物品类
/// </summary>
[System.Serializable]
public class Item
{
    public uint Id;
    public string Name;
    public ItemType ItemType;
    public string Des;
    public string GoPath;
    public string ImagePath;
    public bool IsStack;
    public int Identifier;//标识符(工牌、资料页、硬盘、步枪、手雷)
    public Sprite image;//test

}



public enum ItemType:byte
{
    Rifle,//步枪(识别码用于区分唯一性)  同一把步枪出现多次  必须设置识别码
    Pistol,//手枪
    Grenade,//手雷(识别码用于判断位置，同一种手雷的识别码一样 1,2,3)
    CloseWeapon,//近战武器 剑 斧子 电锯

    Disk,//硬盘(识别码用于用于区分唯一性和判断位置1~9)
    DataPaper,//资料页(识别码用于用于区分唯一性和判断位置1~9)
    WorkCard,//工牌(识别码用于用于区分唯一性和判断位置1~3)


    Antidote,//解毒剂
    BloodPack,//血包
   
    RifleBullet,
    PistolBullet,

    TempProp,

    None
}
