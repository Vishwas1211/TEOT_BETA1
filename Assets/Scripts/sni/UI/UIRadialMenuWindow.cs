using BagModule;
using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;

public class UIRadialMenuWindow : UIWindow
{
    //_rifle,//步枪(识别码用于区分唯一性)
    //_pistol,//手枪
    //_grenade,//手雷(识别码用于判断位置，同一种手雷的识别码一样 1,2,3)
    //_closeWeapon,//近战武器 剑 斧子 电锯

    //Disk,//硬盘(识别码用于用于区分唯一性和判断位置1~9)
    //DataPaper,//资料页(识别码用于用于区分唯一性和判断位置1~9)
    //WorkCard,//工牌(识别码用于用于区分唯一性和判断位置1~3)


    //Antidote,//解毒剂
    //_bloodPack,//血包

    //public GameObject slotPrefab;

    public Transform[] rifleSlotParents=new Transform[2];
    public Transform[] grenadeSlotParents = new Transform[3];
    public Transform[] pistolSlotParents = new Transform[1];
    public Transform[] closeWeaponSlotTBParents = new Transform[1];
    public Transform[] bloodPackSlotParents = new Transform[1];

    [Space(10)]
    public GameObject slotTBPrafab;
    public GameObject slotLRPrafab;


    private Slot[] _rifle = new Slot[2];
    private Slot[] _grenade = new Slot[3];
    private Slot[] _pistol = new Slot[1];
    private Slot[] _closeWeapon = new Slot[1];
    private Slot[] _bloodPack = new Slot[1];

    private Transform _radialMenu;

    //public Slot[] antidote = new Slot[1];


    private void Awake()
    {

        SetSlot(_rifle, ItemType.Rifle, slotTBPrafab, rifleSlotParents);
        SetSlot(_grenade, ItemType.Grenade, slotLRPrafab, grenadeSlotParents);
        SetSlot(_pistol, ItemType.Pistol, slotLRPrafab, pistolSlotParents);
        SetSlot(_closeWeapon, ItemType.CloseWeapon, slotLRPrafab, closeWeaponSlotTBParents);
        SetSlot(_bloodPack, ItemType.BloodPack, slotLRPrafab, bloodPackSlotParents);



        RelationBagGrid(_rifle, BagManager.Instance.GetBagGrid(ItemType.Rifle));
        RelationBagGrid(_grenade, BagManager.Instance.GetBagGrid(ItemType.Grenade));
        RelationBagGrid(_pistol, BagManager.Instance.GetBagGrid(ItemType.Pistol));
        RelationBagGrid(_closeWeapon, BagManager.Instance.GetBagGrid(ItemType.CloseWeapon));
        RelationBagGrid(_bloodPack, BagManager.Instance.GetBagGrid(ItemType.BloodPack));

    }


    private void Start()
    {
        Init();
    }

    void Init()
    {
        _radialMenu = this.transform.Find("RadialMenu");
        if (_radialMenu != null)
        {
            _radialMenu.gameObject.SetActive(false);
            _radialMenu.gameObject.SetActive(true);
        }
    }

    private void SetSlot(Slot[] slotArray, ItemType itemType, GameObject slotPrefab,Transform[] parent)
    {
        for (int i = 0; i < slotArray.Length; i++)
        {
            Slot slot = Creat(slotPrefab, parent[i]);
            slot.itemType = itemType;
            slotArray[i] = slot;
        }
    }

    private Slot Creat(GameObject go, Transform parent)
    {
        if (go == null) return null;
        GameObject temp = Instantiate(go);
        temp.transform.SetParent(parent);
        temp.transform.localScale = Vector3.one;
        temp.transform.localPosition = Vector3.zero;
        Slot tempSlot = temp.GetComponent<Slot>();
        return tempSlot;
    }

    private void RelationBagGrid(Slot[] slotArray, Grid[] bagGridArray)
    {
        for (int i = 0; i < slotArray.Length; i++)
        {
            slotArray[i].SetGrid(bagGridArray[i]);
        }
    }



    

}
