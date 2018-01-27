using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L13Login : MonoBehaviour
{
    InputField pswInputField;
    InputField nameInputField;
    InputField codeInputField;
    GameObject wrongPsw;
    GameObject wrongCode;
    public GameObject go;
    public Camera Camera1;
    public Camera Camera2;
    // Use this for initialization
    void Awake()
    {
        pswInputField = this.transform.Find("Password/InputField (PASS)").GetComponent<InputField>();
        nameInputField = this.transform.Find("Password/InputField (NAME)").GetComponent<InputField>();
        codeInputField = this.transform.Find("Password/InputField (CODE)").GetComponent<InputField>();
        wrongPsw = this.transform.Find("WrongPsw").gameObject;
        wrongCode = this.transform.Find("WrongCode").gameObject;
        pswInputField.ActivateInputField();
    }
    private void OnEnable()
    {
        wrongPsw.SetActive(false);
        wrongCode.SetActive(false);
    }

    private void OnDisable()
    {
        Camera1.transform.root.GetComponent<FreeLookCam>().enabled = true;
        Camera1.enabled = true;
        Camera2.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void L13PSW()
    {
        if (!(TaskStepManagaer.Instance.IsEqualTaskId(19003) && codeInputField.textComponent.text.Equals("1234")))
        {
            ThirdPersonUserControl.isb = false;
            wrongCode.SetActive(true);
            StartCoroutine(ewq(false));
            return;
        }

        if (pswInputField.textComponent.text.Equals("QWE") && TaskStepManagaer.Instance.IsEqualTaskId(19003) && nameInputField.textComponent.text.Equals("MIKI"))
        {
            PlaySoundController.Instance.PlaySoundEffect(this.gameObject, 10001);
            ThirdPersonUserControl.isb = false;
            TaskStepManagaer.Instance.FinishCurTaskImmediately();
            GameObject.Find("Computer_B_Keyboard_1").transform.GetComponent<testL10Pwd>().OpenTV("Power2");
            StartCoroutine(ewq(false));
        }
        else
        {
            ThirdPersonUserControl.isb = false;
            wrongPsw.SetActive(true);
            StartCoroutine(ewq(false));
        }
    }

    IEnumerator ewq(bool b)
    {
        yield return new WaitForSeconds(1);
        go.SetActive(b);
    }
}
