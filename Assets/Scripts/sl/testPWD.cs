using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPWD : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject otherCamera;
    public GameObject ui;
    // Use this for initialization
    void Start()
    {
        ui.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Player"))
        {
            ui.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/PWD");
        }
    }

    public void QWE() {
        mainCamera.SetActive(false);
        otherCamera.SetActive(true);
        //PlayerManager.Instance.motionController.IsEnabled = false;
        GameObject.Find("ThirdPersonController").GetComponent<ThirdPersonCharacter_WSM>().canMove = false;

        ThirdPersonUserControl.isb = true;
        ui.SetActive(true);
    }
}
