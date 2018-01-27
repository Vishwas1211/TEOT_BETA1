using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    private bool once = true;
    InputField inputField;
    Text text;
    public GameObject Camera1;
    public GameObject Camera2;
    public GameObject go;
    // Use this for initialization
    public void Init()
    {

    }

    // Update is called once per frame
    void Update()
    {
        inputField.ActivateInputField();

        //if (transform.position.y > 1.2f && once)
        //{
        //    once = false;
        //    test2.isLive = true;
        //}
    }

    private void Awake()
    {
        inputField = this.transform.Find("InputField").GetComponent<InputField>();
        text = this.transform.Find("Text").GetComponent<Text>();
        inputField.ActivateInputField();
    }


    public void QWE()
    {
        if (inputField.text.Equals("JAYCEE LEE") && TaskStepManagaer.Instance.IsEqualTaskId(4002))
        {
            text.text = "Success";
            TaskStepManagaer.Instance.FinishCurTaskImmediately();
            text.color = Color.green;
            ThirdPersonUserControl.isb = false;
            StartCoroutine(ewq());
        }
        else
        {
            text.text = "Password mistake";
            text.color = Color.red;
            ThirdPersonUserControl.isb = false;
            StartCoroutine(ewq());
        }
    }

    IEnumerator ewq()
    {
        yield return new WaitForSeconds(1);
        transform.gameObject.SetActive(false);
        Camera1.SetActive(true);
        Camera2.SetActive(false);
        //PlayerManager.Instance.motionController.IsEnabled = true;
        GameObject.Find("ThirdPersonController").GetComponent<ThirdPersonCharacter_WSM>().canMove = true;
    }

    //public void L13PSW() {
    //    if (inputField.textComponent.text.Contains("JAYCEE LEE") && TaskStepManagaer.Instance.IsEqualTaskId(4002))
    //    {
    //        text.text = "Success";
    //        TaskStepManagaer.Instance.FinishCurTaskImmediately();
    //        text.color = Color.green;
    //        ThirdPersonUserControl.isb = false;
    //        StartCoroutine(ewq(false));
    //    }
    //    else
    //    {
    //        text.text = "Password mistake";
    //        text.color = Color.red;
    //        ThirdPersonUserControl.isb = false;
    //        StartCoroutine(ewq(false));
    //    }
    //}
}
