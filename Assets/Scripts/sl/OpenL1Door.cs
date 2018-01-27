using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class OpenL1Door : MonoBehaviour
{
    public GameObject red;
    public GameObject green;
    Rigidbody r;
    //VRTK_InteractableObject r;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("key"))
        {
            DoorManager.Instance.level_01_Door_Script[19].CanOpen = true;
            TaskStepManagaer.Instance.FinishCurTaskImmediately();
        }
    }

    public void OpenTheDoor()
    {
        DoorManager.Instance.level_01_Door_Script[19].CanOpen = true;
        PlaySoundController.Instance.PlaySoundEffect(this.gameObject, 10001);
        green.SetActive(true);
        red.SetActive(false);
        TaskStepManagaer.Instance.FinishCurTaskImmediately();
    }
}
