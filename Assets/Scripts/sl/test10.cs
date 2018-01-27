using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class test10 : MonoBehaviour
{
    public bool isOpenDoor = false;
    public GameObject door;
    Rigidbody r;
    //VRTK_InteractableObject r;
    // Use this for initialization
    void Start()
    {
        r = door.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isOpenDoor)
        {
            r.constraints = RigidbodyConstraints.None;
        }
        else
        {
            r.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("key"))
        {
            isOpenDoor = true;
            TaskStepManagaer.Instance.FinishCurTaskImmediately();
        }
    }
}
