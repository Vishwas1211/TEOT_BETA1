using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;

public class UIMain : UIPage
{

    private PlayerInfo _playerInfo;
    private TestToolsManagers _testToolsManagers;
    private Item _curEquipItem;
    private Transform _tempPropGroupTrans;
    private List<Image> _imgList = new List<Image>();


    Slider _healthSlider;
    Text _healthCount;
    Image _weaponImg;
    Image _magazineImg;
    Text _magazineCount;
    Image _grenadeImg;
    Text _grenadeCount;


    private void Awake()
    {
       
        _healthSlider = this.GetComponentInChildren<Slider>();
        _playerInfo = GameObject.FindObjectOfType<PlayerInfo>();
        _testToolsManagers = PlayerManager.Instance.testToolsManagers;
        if (_playerInfo == null)
        {
            Debug.Log(string.Format("<color=red>获取  PlayerInfo 失败</color>"));
        }
        if (_healthSlider == null)
        {
            Debug.Log(string.Format("<color=red>获取  Slider 失败</color>"));
        }


        _weaponImg = UtilFunction.GetChildComponent<Image>(this.transform, "Img_Weapon");
        _magazineImg = UtilFunction.GetChildComponent<Image>(this.transform, "Magazine");
        _grenadeImg = UtilFunction.GetChildComponent<Image>(this.transform, "Grenade");
        _healthCount = UtilFunction.GetChildComponent<Text>(this.transform, "Tex_HealthCount");
        _magazineCount = UtilFunction.GetChildComponent<Text>(this.transform, "Tex_MagazineCount");
        _grenadeCount = UtilFunction.GetChildComponent<Text>(this.transform, "Tex_GrenadeCount");
        _tempPropGroupTrans=  UtilFunction.GetChildNode(this.transform, "TempPropGroup");
    }

    private void Start()
    {
        Init();
        TempPropInit();
    }


    private void Update()
    {
        if (_playerInfo != null)
        {
            UpdateHealth(_playerInfo._curHp, PlayerInfo.MAX_HP);
        }

        UpdateEquipShow();

        UpdateTempPropShow(BagModule.BagManager.Instance.GetTempProp());

    }

    //todo:需要完善
    private void Equip(Item item)
    {
        if (item == null) return;
        Init();
        if (item.ItemType == ItemType.Rifle|| item.ItemType == ItemType.Pistol)
        {
            SHowGun(item);
        }
        else if(item.ItemType == ItemType.CloseWeapon)
        {
            ShowCloseWeapon(item);
        }
        else if(item.ItemType == ItemType.Grenade)
        {
            ShowGrenade(item);
        }
    }


    private void SHowGun(Item item)
    {
        if (item == null) return;
        _weaponImg.enabled = true;
        _weaponImg.sprite = item.image;
        _magazineImg.gameObject.SetActive(true);
        _magazineCount.text = BagModule.BagManager.Instance.GetCount(item).ToString();
    }

    private void ShowCloseWeapon(Item item)
    {
        if (item == null) return;
        _weaponImg.enabled = true;
        _weaponImg.sprite = item.image;
    }

    private void ShowGrenade(Item item)
    {
        if (item == null) return;
        _grenadeImg.gameObject.SetActive(true);
        _grenadeCount.text = BagModule.BagManager.Instance.GetCount(item).ToString();
    }




    private void Init()
    {
        _weaponImg.enabled = false;
        //if (_magazineImg == null) Debug.Log(12314);
        _magazineImg.gameObject.SetActive(false);
        _grenadeImg.gameObject.SetActive(false);

       


    }

    private void TempPropInit()
    {
        for (int i = 0; i < _tempPropGroupTrans.childCount; i++)
        {
            Image img = _tempPropGroupTrans.GetChild(i).GetComponent<Image>();
            img.enabled = false;
            _imgList.Add(img);
        }
    }

   


    private void UpdateHealth(float cur,float max)
    {
        _healthSlider.value = cur / max;
        _healthCount.text = cur.ToString();
    }

    private void UpdateEquipShow()
    {
        if (_testToolsManagers != null)
        {

            if (_curEquipItem != null)
            {
                if (_testToolsManagers.curItem != _curEquipItem)
                {
                    Equip(_testToolsManagers.curItem);
                    _curEquipItem = _testToolsManagers.curItem;
                }
            }
            else
            {
                Equip(_testToolsManagers.curItem);
                _curEquipItem = _testToolsManagers.curItem;
            }


        }
        else
        {
            _testToolsManagers = PlayerManager.Instance.testToolsManagers;
        }
    }


    private void UpdateTempPropShow(List<Item> itemList)
    {
        if (itemList.Count > _imgList.Count)
        {
            Debug.Log("<color=red> 发生错误  预留位置不够！！！</color>");
            return;
        }
        for (int i = 0; i < itemList.Count; i++)
        {
            _imgList[i].enabled = true;
            _imgList[i].sprite = itemList[i].image;
        }

        for (int i = itemList.Count; i < _imgList.Count; i++)
        {
            _imgList[i].enabled = false;
        }

    }


}
