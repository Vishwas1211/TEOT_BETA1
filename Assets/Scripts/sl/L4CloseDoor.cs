using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L4CloseDoor : MonoBehaviour
{
    private bool isb;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!DoorManager.Instance.level_04_Door_Script[17].isGuan && !isb)
        {
            isb = true;
            StartCoroutine(qwe());
        }
    }

    IEnumerator qwe()
    {
        yield return new WaitForSeconds(1);
        if (!Level_04_Manager.Instance.HaveKey)
        {
            DoorManager.Instance.level_04_Door_Script[17].CanOpen = false;
        }
        DoorManager.Instance.level_04_Door_Script[17].isGuan = true;
        DoorManager.Instance.level_04_Door_Script[17].isArrive = false;
    }
}
