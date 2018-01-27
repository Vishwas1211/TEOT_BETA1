//DoorTrigger.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/15/2017 11:13 AM
//Description: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorTrigger : MonoBehaviour
{
    public static event OpenDoorEventHandler OpenDoorEvent;
    public static event OpenDoorEventHandler OpenJayceeEvent;
    public static event OpenDoorEventHandler OpenNo112Event;
    public static event OpenDoorEventHandler OpenTask6004Event;
    public static event OpenDoorEventHandler OutDoorEvent;

    public TYPE curType;

    public enum TYPE
    {
        FIRST_FLOOR,
        JAYCEE_ROOM,
        No112_ROOM,
        Task6004_ROOM,
        FIRST_FLOOR_OUT,
        JAYCEE_1006,
    }

    public bool isEnterDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(8))//Íæ¼Ò²ã
        {
            isEnterDoor = true;
            this.gameObject.GetComponent<Collider>().enabled = false;
            //TriggerObjectManagement.instance.DoorTriggerForFirstFloor();
            switch (curType)
            {
                case TYPE.FIRST_FLOOR:
                    if (OpenDoorEvent != null)
                    {
                        OpenDoorEvent();
                    }
                    break;
                case TYPE.JAYCEE_ROOM:
                    if (OpenJayceeEvent != null)
                    {
                        OpenJayceeEvent();
                    }
                    break;
                case TYPE.No112_ROOM:
                    if (OpenNo112Event != null)
                    {
                        OpenNo112Event();
                    }
                    break;
                case TYPE.Task6004_ROOM:
                    if (OpenTask6004Event != null)
                    {
                        OpenTask6004Event();
                    }
                    break;
                case TYPE.FIRST_FLOOR_OUT:
                    if (OutDoorEvent != null)
                    {
                        OutDoorEvent();
                    }
                    break;
            }
        }
    }
}
