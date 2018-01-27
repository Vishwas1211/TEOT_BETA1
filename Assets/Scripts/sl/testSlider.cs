using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class testSlider : MonoBehaviour
{
    Button button;
    // Use this for initialization
    void Start()
    {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (CurveSin.isT)
        //{
        //    button.interactable = true;
        //}
        //else
        //{
        //    button.interactable = false;
        //}
    }

    public void Finish()
    {
        button.interactable = false;
        testCrab.isb = false;
        GameObject.Find("Crab").GetComponent<testCrab>().ewq();
        Destroy(GameObject.Find("XinHaoQi(Clone)"));
        GameObject[] go = GameObject.FindGameObjectsWithTag("test");
        NoLockView_Camera.can_21_4 = false;
        NoLockView_Camera.is_21_5_0 = false;
        NoLockView_Camera.is_21_5_1 = false;
        NoLockView_Camera.is_21_5_2 = false;
        NoLockView_Camera.is_21_5_3 = false;
        for (int i = 0; i < go.Length; i++)
        {
            go[i].GetComponent<Rigidbody>().isKinematic = false;    
            go[i].GetComponent<Rigidbody>().AddForce(0, 1000, 0);
            go[i].GetComponent<testElectricute>().setB(false);
        }
    }
}
