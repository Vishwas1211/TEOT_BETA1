//
//  BackpackController.cs
//  TEOT_ONLINE
//
//  Created by 孙磊 on 8/14/2017 1:28 PM.
//
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using VRTK;

public delegate void BackpackControllerEventHandler(PlayerToolsBase p);

public class BackpackController : MonoBehaviour
{
    public static event BackpackControllerEventHandler ChangeTools;
    public static event BackpackControllerEventHandler AddToolsEvent;
    public static event BackpackControllerEventHandler RemoveToolsEvent;

    public void PlayerChangeTools(PlayerToolsBase p)
    {
        if (ChangeTools != null)
        {
            ChangeTools(p);
        }
        else
        {
            Debug.Log("Delegate null");
        }
    }

    public void PlayerAddToolsEvent(PlayerToolsBase p)
    {
        if (AddToolsEvent != null)
        {
            AddToolsEvent(p);
        }
        else
        {
            Debug.Log("Delegate null");
        }
    }

    public void PlayerRemoveToolsEvent(PlayerToolsBase p)
    {
        if (RemoveToolsEvent != null)
        {
            RemoveToolsEvent(p);
        }
        else
        {
            Debug.Log("Delegate null");
        }
    }

    public GameObject playerHand;

    private VRTK_RadialMenu menu;
    private PlayerHandAnimation _playerHandAnimation;
    private GameObject _tools;
    private PlayerToolsBase _playerToolsBase;
    private Dictionary<int, BackpackItems> dicBackPack;
    private List<BackpackItems> listBackpack;
    private Material[] material;
    private Renderer[] renderers;
    private void OnEnable()
    {
        Backpack.AddBackpackEvent += new BackpackDelegate(DoAddTools);
        Backpack.RemoveBackpackEvent += new BackpackDelegate(DoRemoveTools);
    }
    private void OnDisable()
    {
        Backpack.AddBackpackEvent -= new BackpackDelegate(DoAddTools);
        Backpack.RemoveBackpackEvent -= new BackpackDelegate(DoRemoveTools);
    }

    private void DoAddTools(int id)
    {
        menu.AddButton(dicBackPack[id].button);
    }
    private void DoRemoveTools(int id)
    {
        menu.RemoveButtons(id);
    }

    private void Start()
    {
        menu = GetComponent<VRTK_RadialMenu>();
        _playerHandAnimation = menu.GetComponentInParent<PlayerHandAnimation>();
        _tools = playerHand.transform.Find("Tools").gameObject;
        _playerToolsBase = menu.GetComponentInParent<PlayerHandController>().playerToolsBase;
        listBackpack = Backpack.Instance.listBackpack;
        dicBackPack = Backpack.Instance.dicBackpack;
        for (int i = 0; i < listBackpack.Count; i++)
        {
            //if (dicBackpack.(i))
            {
                menu.AddButton(listBackpack[i].button);
            }
        }
    }

    public void UseTools(int id)
    {
        switch (id)
        {
            case 0:
                _playerHandAnimation.SetHandState(HandState.Hand);
                break;
            case 1:
                _playerHandAnimation.SetHandState(HandState.Gun);
                break;
            case 2:
                _playerHandAnimation.SetHandState(HandState.Grip);
                break;
            case 3:
                _playerHandAnimation.SetHandState(HandState.Grip);
                break;
            case 4:
                _playerHandAnimation.SetHandState(HandState.Grip);
                break;
            case 5:
                _playerHandAnimation.SetHandState(HandState.Grip);
                break;
            default:
                _playerHandAnimation.SetHandState(HandState.Hand);
                break;
        }
        ResetTools();
        dicBackPack[id].isShow = true;
        _tools.transform.Find(dicBackPack[id].item.name).gameObject.SetActive(dicBackPack[id].isShow);
        _playerToolsBase = _tools.transform.Find(dicBackPack[id].item.name).GetComponent<PlayerToolsBase>();
        menu.GetComponentInParent<PlayerHandController>().playerToolsBase = _playerToolsBase;
        ChangToolsShader(id);

        //PlayerChangeTools(_playerToolsBase);
    }

    private void ChangToolsShader(int id)
    {
        for (int i = 0; i < _tools.transform.Find(dicBackPack[id].item.name).childCount; i++)
        {
            if (_tools.transform.Find(dicBackPack[id].item.name).GetChild(i).GetComponent<Renderer>())
            {
                Debug.Log(_tools.transform.Find(dicBackPack[id].item.name).GetChild(i).name);
                material = _tools.transform.Find(dicBackPack[id].item.name).GetChild(i).GetComponent<Renderer>().materials;
                for (int j = 0; j < material.Length; j++)
                {
                    material[j].SetFloat("_DissolveThreshold", 1);
                    material[j].DOFloat(0, "_DissolveThreshold", 0.5f);
                }
            }
        }


        renderers = GetComponentsInChildren<Renderer>();
    }


    private void ResetTools()
    {
        for (int i = 0; i < listBackpack.Count; i++)
        {
            //if (dicBackpack[i].isShow)
            {
                listBackpack[i].isShow = false;
                _tools.transform.Find(listBackpack[i].item.name).gameObject.SetActive(listBackpack[i].isShow);
            }
        }
    }

    public void AddTools(int id)
    {
        PlayerAddToolsEvent(null);
        Backpack.Instance.AddTools(id);
        UseTools(id);
    }
    public void RemoveTools(int id)
    {
        Backpack.Instance.RemoveTools(id);
    }
}