using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestToolsManagers : MonoBehaviour
{
    [SerializeField]
    List<GameObject> toolsList;

    private Item _curItem;
    public Item curItem
    {
        get { return _curItem; }
    }

    // Use this for initialization
    void Start()
    {
        Transform tools = transform.Find("SOD/SOD_joint/SOD_SpineJ_1/SOD_SpineJ_2/SOD_SpineJ_3/SOD_SpineJ_4/SOD_SpineJ_5/SOD_SpineJ_6/SOD_SpineJ_7/SOD_SpineJ_8/SOD_LShoulderPlateJ_1/SOD_LShoulderJ_1/SOD_LForeArmJ_1/SOD_LForeArmJ_2/SOD_LWristJ_1/SOD_LWristJ_1_ctrl/SOD_LMidFinBaseJ_1_sd/Tools");
            for (int i = 0; i < tools.childCount; i++)
        {
            toolsList.Add(tools.GetChild(i).gameObject);
        }
    }

    public void HideAllItem()
    {
        for (int i = 0; i < toolsList.Count; i++)
        {
            toolsList[i].SetActive(false);
        }
    }

    public void setItem(Item item)
    {
        _curItem = item;
    }
}
