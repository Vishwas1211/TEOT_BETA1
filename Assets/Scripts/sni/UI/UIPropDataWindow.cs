using BagModule;
using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;

public class UIPropDataWindow : UIWindow
{
    private ToggleGroup _toggleGroup;
    public Toggle[] toggles = new Toggle[3];
    public Transform[] subMenu = new Transform[3];
    public GameObject slotPrefab;

   
    
    //[SerializeField]
    private Slot[] _workCard = new Slot[3];
    //[SerializeField]
    private Slot[] _disk = new Slot[10];
    //[SerializeField]
    private Slot[] _dataPaper = new Slot[10];

    private Vector3[] _dataPaperForm = new Vector3[10]
    {
        //new Vector3(50,-50),
        //new Vector3(170,-50),
        //new Vector3(290,-50),
        //new Vector3(410,-50),
        //new Vector3(530,-50),

        //new Vector3(50,-170),
        //new Vector3(170,-170),
        //new Vector3(290,-170),
        //new Vector3(410,-170),
        //new Vector3(530,-170),
        new Vector3(-240,60),
        new Vector3(-120,60),
        new Vector3(0,60),
        new Vector3(120,60),
        new Vector3(240,60),

        new Vector3(-240,-60),
        new Vector3(-120,-60),
        new Vector3(0,-60),
        new Vector3(120,-60),
        new Vector3(240,-60),
};

    private Vector3[] _dataPaperTo = new Vector3[10]
    {
        //new Vector3(-700,-50),
        //new Vector3(-580,-50),
        //new Vector3(-460,-50),
        //new Vector3(-340,-50),
        //new Vector3(-220,-50),

        //new Vector3(-700,-170),
        //new Vector3(-580,-170),
        //new Vector3(-460,-170),
        //new Vector3(-340,-170),
        //new Vector3(-220,-170),
        new Vector3(-990,60),
        new Vector3(-870,60),
        new Vector3(-750,60),
        new Vector3(-630,60),
        new Vector3(-510,60),

        new Vector3(-990,-60),
        new Vector3(-870,-60),
        new Vector3(-750,-60),
        new Vector3(-630,-60),
        new Vector3(-510,-60),
};

    private Vector3[] _diskPosForm = new Vector3[10]
   {
        //new Vector3(50,-50),
        //new Vector3(170,-50),
        //new Vector3(290,-50),
        //new Vector3(410,-50),
        //new Vector3(530,-50),

        //new Vector3(50,-170),
        //new Vector3(170,-170),
        //new Vector3(290,-170),
        //new Vector3(410,-170),
        //new Vector3(530,-170),
       
        new Vector3(240,60),
        new Vector3(120,60),
        new Vector3(0,60),
        new Vector3(-120,60),
         new Vector3(-240,60),

        new Vector3(240,-60),
        new Vector3(120,-60),
         new Vector3(0,-60),
         new Vector3(-120,-60),
         new Vector3(-240,-60),
};


    private Vector3[] _diskPosTo = new Vector3[10]
   {
       //new Vector3(800,-50),
       // new Vector3(920,-50),
       // new Vector3(1040,-50),
       // new Vector3(1160,-50),
       // new Vector3(1280,-50),

       // new Vector3(800,-170),
       // new Vector3(920,-170),
       // new Vector3(1040,-170),
       // new Vector3(1160,-170),
       // new Vector3(1280,-170),
              
        
        new Vector3(990,60),
        new Vector3(870,60),
        new Vector3(750,60),
        new Vector3(630,60),
        new Vector3(510,60),
        
        new Vector3(990,-60),
         new Vector3(870,-60),
         new Vector3(750,-60),
         new Vector3(630,-60),
         new Vector3(510,-60),



};

    private Vector3[] _workCardPosFrom = new Vector3[3]
   {
        //new Vector3(50,-50),
        //new Vector3(170,-50),
        //new Vector3(290,-50),
        new Vector3(-124,10),
        new Vector3(-4,10),
        new Vector3(116,10),

};

    private Vector3[] _workCardPosTo = new Vector3[3]
  {
        //new Vector3(50,-550),
        //new Vector3(170,-550),
        //new Vector3(290,-550),

        new Vector3(-124,-490),
        new Vector3(-4,-490),
        new Vector3(116,-490),

};



    private void Awake()
    {
        SetSlot(_workCard, ItemType.WorkCard, 0, _workCardPosFrom, _workCardPosTo);
        SetSlot(_disk, ItemType.Disk, 1, _diskPosForm, _diskPosTo);
        SetSlot(_dataPaper, ItemType.DataPaper,2, _dataPaperForm,_dataPaperTo);

        RelationBagGrid(_workCard, BagManager.Instance.GetBagGrid(ItemType.WorkCard));
        RelationBagGrid(_disk, BagManager.Instance.GetBagGrid(ItemType.Disk));
        RelationBagGrid(_dataPaper, BagManager.Instance.GetBagGrid(ItemType.DataPaper));

        _toggleGroup = GetComponent<ToggleGroup>();
    }

    private void SetSlot(Slot[] slotArray,ItemType itemType,int num,Vector3[] slotPos,Vector3[] slotToPos=null)
    {
        
        for (int i = 0; i < slotArray.Length; i++)
        {
            Slot slot=Creat(slotPrefab,subMenu[num]);
            slot.itemType = itemType;
            slotArray[i] = slot;
            (slot.transform as RectTransform).anchoredPosition3D = slotPos[i];
            slot.SetOrigPos(slotPos[i]);
            if (slotToPos!=null)
            slot.SetDoTween(slotToPos[i]);
            
        }
    }

    private Slot Creat(GameObject go,Transform parent)
    {
        if (go == null) return null;
        GameObject temp = Instantiate(go);
        (temp.transform as RectTransform).anchorMax = new Vector2(0.5f, 0.5f);
        (temp.transform as RectTransform).anchorMin = new Vector2(0.5f, 0.5f);
        temp.transform.SetParent(parent);
        temp.transform.localScale = Vector3.one;
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





   





    protected override void OnEnable()
    {
        base.OnEnable();
        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].onValueChanged.AddListener(OnValueChange);

        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].isOn = false;
            toggles[i].onValueChanged.RemoveListener(OnValueChange);

        }
    }


    private void OnValueChange(bool b)
    {

        IEnumerable<Toggle> activeTog=_toggleGroup.ActiveToggles();
        




        for (int i = 0; i < toggles.Length; i++)
        {
            if (!toggles[i].isOn)
            {
                CloseSubMenu(i);
            }
        }

        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                OpenSubMenu(i);
                break;
            }
        }

    }
    private void OpenSubMenu(int index)
    {
        subMenu[index].gameObject.SetActive(true);

        Slot[] slots = ChooseSlot(index);
        if (slots == null)
        {
            Debug.Log(123123123);
        }
        StartCoroutine("SlotMove", slots);
        //SlotMove1(slots);
        //Debug.Log(slots);
        //ChooseOpenMove(index);
    }



    private void CloseSubMenu(int index)
    {
        //ChooseCloseMove(index);
        //if (!subMenu[index].gameObject.activeSelf) return;
        //Slot[] slots = ChooseSlot(index);
        //SlotAniReset(slots);
        StopAllCoroutines();
        
        subMenu[index].gameObject.SetActive(false);
    }


    private Slot[] ChooseSlot(int index)
    {
        Slot[] temp=null;
        switch (index)
        {
            case 0:
                temp = _workCard;
                break;
            case 1:
                temp = _disk;
                break;
            case 2:
                temp = _dataPaper;
                break;
        }
        return temp;
    }




    private void ChooseOpenMove(int index)
    {
        switch (index)
        {
            case 0:
                StartCoroutine("WorkCard");
                break;
            case 1:
                StartCoroutine("Disk");
                break;
            case 2:
                StartCoroutine("DataPaper");
                break;
        }
    }

    private void ChooseCloseMove(int index)
    {
        switch (index)
        {
            case 0:
                StopCoroutine("WorkCard");
                break;
            case 1:
                StopCoroutine("Disk");
                break;
            case 2:
                StopCoroutine("DataPaper");
                break;
        }
    }


    private IEnumerator WorkCard()
    {
        yield return null;
        Debug.Log("开始移动");
        for (int i = 0; i < _workCard.Length; i++)
        {
            //yield return null;
            _workCard[i].PlayDoTween(0.3f);
        }
    }

    private IEnumerator Disk()
    {
        yield return null;
        Debug.Log("开始移动");
        for (int i = 0; i < _disk.Length; i++)
        {   
            //yield return null;
            _disk[i].PlayDoTween(0.1f);
        }
    }


    private IEnumerator DataPaper()
    {
        yield return null;
        Debug.Log("开始移动");
        for (int i = 0; i < _dataPaper.Length; i++)
        {
            //yield return null;
            _dataPaper[i].PlayDoTween(0.1f);
        }
    }

    private IEnumerator SlotMove(Slot[] array)
    {
        //yield return null;
        Debug.Log("开始移动");
        for (int i = 0; i < array.Length; i++)
        {
            yield return new WaitForSeconds(0.2f);
            array[i].PlayDoTween(0.5f);
        }
    }



    private void  SlotMove1(Slot[] array)
    {
        //yield return null;
        Debug.Log("开始移动");
        for (int i = 0; i < array.Length; i++)
        {
            //yield return new WaitForSeconds(0.2f);
            array[i].PlayDoTween(0.5f);
        }
    }


    private void SlotAniReset(Slot[] array)
    {
        //for (int i = 0; i < array.Length; i++)
        //{
        //    array[i].DoTweenReset();
        //}
    }

}
