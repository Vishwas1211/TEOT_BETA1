using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L10Login : MonoBehaviour
{
    InputField pswInputField;
    InputField nameInputField;
    InputField codeInputField;
    GameObject wrongPsw;
    GameObject wrongCode;
    public GameObject go;
    public Camera Camera1;
    public Camera Camera2;

    public GameObject JianShiQi;

    // Use this for initialization
    void Start()
    {
        pswInputField = this.transform.Find("Password/InputField (PASS)").GetComponent<InputField>();  //ÕË»§
        nameInputField = this.transform.Find("Password/InputField (NAME)").GetComponent<InputField>(); //ÃÜÂë
        codeInputField = this.transform.Find("Password/InputField (CODE)").GetComponent<InputField>(); //ÓÃ»§
        wrongCode = this.transform.Find("WRONG CODE").gameObject;
        wrongPsw  = this.transform.Find("WRONG PASS").gameObject;
        pswInputField.ActivateInputField();


    }
    private void OnEnable()
    {
        //wrongPsw.SetActive(false);
        //wrongCode.SetActive(false);
    }

    private void OnDisable()
    {
        //Camera1.transform.root.GetComponent<FreeLookCam>().enabled = true;
        //Camera1.enabled = true;
        //Camera2.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
   
    }

    public void PingKong()
    {
        codeInputField.text = "";
        pswInputField.text = "";
        nameInputField.text = "";
    }

    public void L13PSW()
    {
        if (Level_10_Manager.Instance.MiTi_0)
        {
            return;
        }
        Level_10_Manager.Instance.MiTi_0 = true;

        if (codeInputField.textComponent.text.Equals("1234") && pswInputField.textComponent.text.Equals("QWE") && nameInputField.textComponent.text.Equals("MIKI"))
        {
            PlaySoundController.Instance.PlaySoundEffect(this.gameObject, 1111);
            Level_10_Manager.Instance.Landing();
        }
        else
        {
            wrongPsw.SetActive(true);
            StartCoroutine(ewq());
        }
        PingKong();

    }

    public void JiXu()
    {

        if (Level_10_Manager.Instance.MiTi_1)
        {
            return;
        }
        Level_10_Manager.Instance.MiTi_1 = true;

        Level_10_Manager.Instance.KanShiMiTi();

    }

    IEnumerator ewq()
    {
        yield return new WaitForSeconds(1);
        wrongPsw.SetActive(false);
    }
}
