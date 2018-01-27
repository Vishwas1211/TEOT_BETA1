using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testBlue : MonoBehaviour
{
    public GameObject Camera_3rd;
    public GameObject Camera_1st;
    public GameObject go;
    public GameObject ui;
    public GameObject move;
    public bool isb;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isb)

        //if (Input.GetKeyDown(KeyCode.Escape))
        {
            Camera_3rd.SetActive(true);
            Camera_1st.SetActive(false);
            Camera_3rd.transform.root.GetComponent<FreeLookCam>().enabled = true;
            move.transform.GetComponent<ThirdPersonCharacter_WSM>().enabled = true;
            go.SetActive(false);
            ui.SetActive(false);
            isb = false;
        }
    }

    public void qwe()
    {
        Camera_3rd.SetActive(false);
        Camera_1st.SetActive(true);
        Camera_3rd.transform.root.GetComponent<FreeLookCam>().enabled = false;
        move.transform.GetComponent<ThirdPersonCharacter_WSM>().enabled = false;
        go.SetActive(true);
        ui.SetActive(true);
        isb = true;
    }

    public void Finish()
    {
        if (go.activeSelf)
        {
            Camera_3rd.SetActive(true);
            Camera_1st.SetActive(false);
            Camera_3rd.transform.root.GetComponent<FreeLookCam>().enabled = true;
            move.transform.GetComponent<ThirdPersonCharacter_WSM>().enabled = true;
            go.SetActive(false);
            ui.SetActive(false);
            isb = false;

            testCrab.isb = false;
            GameObject.Find("Crab").GetComponent<testCrab>().ewq();
            Destroy(GameObject.Find("XinHaoQi(Clone)"));
            GameObject[] go1 = GameObject.FindGameObjectsWithTag("test");
            NoLockView_Camera.can_21_4 = false;
            NoLockView_Camera.is_21_5_0 = false;
            NoLockView_Camera.is_21_5_1 = false;
            NoLockView_Camera.is_21_5_2 = false;
            NoLockView_Camera.is_21_5_3 = false;
            for (int i = 0; i < go1.Length; i++)
            {
                go1[i].GetComponent<Rigidbody>().isKinematic = false;
                go1[i].GetComponent<Rigidbody>().AddForce(0, 1000, 0);
                go1[i].GetComponent<testElectricute>().setB(false);
            }
        }
    }
}
