//TriggerObjectManagement.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/15/2017 6:41 PM
//Description: 可触发物体管理
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OpenDoorEventHandler();
public class TriggerObjectManagement : SingletonMono<TriggerObjectManagement>
{
    GameObject sog;

    private void Start()
    {
        sog = Resources.Load<GameObject>("Prefabs/Character/Enemy/SOG/SOG");
        DoorTrigger.OpenDoorEvent += new OpenDoorEventHandler(DoorTriggerForFirstFloor);
        DoorTrigger.OpenNo112Event += new OpenDoorEventHandler(DoorTriggerForNo112);
        DoorTrigger.OpenTask6004Event += new OpenDoorEventHandler(DoorTriggerForTask6004);
        DoorTrigger.OutDoorEvent += new OpenDoorEventHandler(DoorTriggerForOutFloor);

    }

    public void DoorTriggerForFirstFloor()
    {
        //进入了一楼的门
        TaskStepManagaer.Instance.FinishCurTaskImmediately();
    }

    public void DoorTriggerForNo112()
    {
        //进入了112的门
        if (TaskStepManagaer.Instance.IsEqualTaskId(5001))
        {
            TaskStepManagaer.Instance.FinishCurTaskImmediately();
        }
    }

    public void DoorTriggerForTask6004()
    {
        if (TaskStepManagaer.Instance.IsEqualTaskId(6004))
        {
            Instantiate(sog, sog.transform.position, sog.transform.rotation);
        }
    }
    public void DoorTriggerForOutFloor()
    {
        if (TaskStepManagaer.Instance.IsEqualTaskId(6005))
        {
            //Instantiate(sog, sog.transform.position, sog.transform.rotation);
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Instantiate(obj, transform.position, transform.rotation);

            TaskStepManagaer.Instance.FinishCurTaskImmediately();
        }
    }
}
