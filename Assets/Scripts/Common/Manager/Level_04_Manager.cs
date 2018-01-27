using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_04_Manager : SingletonMono<Level_04_Manager>
{

    public Transform[] PlayerPositions;

    public bool HaveKey = false;

    private bool is_17;
    private bool is_12;
    private bool is_16;
    private bool is_15;

    public void Init()
    {
        GameObject go = GameObject.Find("Level04GameObjectManager");
        PlayerPositions = new Transform[go.transform.Find("PlayerPosition").childCount];
        for (int i = 0; i < PlayerPositions.Length; i++)
        {
            PlayerPositions[i] = go.transform.Find("PlayerPosition").GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (TaskStepManagaer.Instance.IsEqualTaskId(6005))
        {
            if (!is_17 && UtilFunction.IsReachDistanceXYZ(DoorManager.Instance.level_04_Door_GameObjects[17].transform.position, JayceeManager.Instance.humanState.transform.position, 1))
            {
                is_17 = true;
                DoorManager.Instance.level_04_Door_Script[17].CanOpen = true;
                DoorManager.Instance.level_04_Door_Script[17].Operation();
            }
        }
        if (TaskStepManagaer.Instance.IsEqualTaskId(6007))
        {
            if (!is_12 && UtilFunction.IsReachDistanceXYZ(DoorManager.Instance.level_04_Door_GameObjects[12].transform.position, JayceeManager.Instance.humanState.transform.position, 1))
            {
                is_12 = true;
                DoorManager.Instance.level_04_Door_Script[12].CanOpen = true;
                DoorManager.Instance.level_04_Door_Script[12].Operation();
            }
        }
        if (TaskStepManagaer.Instance.IsEqualTaskId(6007))
        {
            if (!is_16 && UtilFunction.IsReachDistanceXYZ(DoorManager.Instance.level_04_Door_GameObjects[16].transform.position, JayceeManager.Instance.humanState.transform.position, 1))
            {
                is_16 = true;
                DoorManager.Instance.level_04_Door_Script[16].CanOpen = true;
                DoorManager.Instance.level_04_Door_Script[16].Operation();
            }
        }
        if (TaskStepManagaer.Instance.IsEqualTaskId(7001))
        {
            if (!is_15 && UtilFunction.IsReachDistanceXYZ(DoorManager.Instance.level_04_Door_GameObjects[15].transform.position, JayceeManager.Instance.humanState.transform.position, 1))
            {
                is_15 = true;
                DoorManager.Instance.level_04_Door_Script[15].CanOpen = true;
                DoorManager.Instance.level_04_Door_Script[15].Operation();
            }
        }
    }

}
