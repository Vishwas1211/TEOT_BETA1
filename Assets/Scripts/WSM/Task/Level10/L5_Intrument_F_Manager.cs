using System.Collections;using System.Collections.Generic;
using UnityEngine;


public class L5_Intrument_F_Manager : MonoBehaviour 
{


    #region---------外部变量----------

    private Transform men;  //小门
    private Transform StartButton;  //开始按钮
    private Transform StopButton;  //结束按钮

    private Transform Antidote_A;  //带你针的罐子
    private Transform injection;    //小药瓶

    private Material m_material;   //变色材质


    public float BianHua = 0;  //变色进度

    #endregion

    #region---------内部变量----------

    public float KaiMenJiaoDu = 90;
    public bool isArrive = false;
    public bool isGuan = true;
    public bool CanOpen = true;

    public bool isOK = false;

    private bool isHaveXiaoYaoPing = false;
    private bool isHaveDaYaoPing = false;

    private bool bool_0 = false; // 有小药
    private bool bool_1 = false; // 有大药

    private Vector3 menZhou;

    private bool CanBianSe = false;
    private float YanSe_R = 0;
    private float YanSe_G = 0;
    private float YanSe_B = 0;
    private float YanSe_A = 1;


    private float jia = 1f;

    #endregion

    #region---------调用方法----------

    public void Init()
	{
        men = transform.Find("Door");
        menZhou = men.position + Vector3.forward * 0.053f;
        StartButton = transform.Find("Start");
        StopButton = transform.Find("Stop");
        Antidote_A = transform.Find("Antidote_A_WanCheng");
        Antidote_A.gameObject.SetActive(false);
        injection = transform.Find("injection");
        injection.gameObject.SetActive(false);

        m_material = gameObject.GetComponent<MeshRenderer>().materials[1];
    }

    
    public void CaoZuo()
    {
        if (CanOpen)
        {
            isGuan = !isGuan;
            isArrive = false;
        }
    }
    public void FangYao()
    {
        injection.gameObject.SetActive(true);
        bool_0 = true;
    }

    public void FangYao_2()
    {
        Antidote_A.gameObject.SetActive(true);
        bool_1 = true;
    }

    public void KaiShi()
    {
        if (isGuan)
        {
            if (bool_1 && bool_0)
            {
                CanBianSe = true;
                bool_0 = false;
            }
        }
        else
        {
            //TODO 提示关门
        }
    }

    public void JieShu()
    {
        if (CanBianSe)
        {
            CanBianSe = false;
            isOK = true;
            PlaySoundController.Instance.PlaySoundEffect(gameObject,1111);

            if (BianHua >= 0.3 && BianHua <= 0.8)
            {
                Level_10_Manager.Instance.CompletePharmaceutical = true;
                PlaySoundController.Instance.PlaySoundEffect(gameObject, 1110);
            }

            ChongZhi();
        }
    }

    public void ChongZhi() //机器重置
    {
        BianHua = 0;
        injection.gameObject.SetActive(false);
    }

    #endregion

    #region---------功能方法----------

    public void UpdateOpenDoor()
    {
        if (!isGuan && !isArrive)
        {
            float f = XianZhiJiaoDu(men.transform.localEulerAngles.y);
            if (f > KaiMenJiaoDu || f < -KaiMenJiaoDu)
            {
                isArrive = true;
                return;
            }
            men. transform.RotateAround(menZhou, Vector3.up, -KaiMenJiaoDu * Time.deltaTime);
        }
    }

    public void UpdateCloseDoor()
    {
        if (isGuan && !isArrive)
        {
            float f = XianZhiJiaoDu(men.transform.localEulerAngles.y);
            if (f < 2f && f > -2f)
            {
                isArrive = true;
                return;
            }
            men. transform.RotateAround(menZhou, Vector3.up, KaiMenJiaoDu * Time.deltaTime);
        }
    }

    public void UpdateBianSe()
    {
        if (CanBianSe)
        {
            BianHua += Time.deltaTime*0.1f;
            BianHua = Mathf.Clamp(BianHua, 0, 1);


            //if (YanSe_R >= BianHua)
            //{
            //    jia = - Mathf.Abs(jia);
            //}
            //else if (YanSe_R <= 0)
            //{
            //    jia =Mathf.Abs(jia);
            //}
            YanSe_R = BianHua;

            m_material.color = new Color(YanSe_R, YanSe_G, YanSe_B, YanSe_A);
            Debug.Log(YanSe_R +"  "+ YanSe_G + "  " + YanSe_B + "  " + YanSe_A);
        }
    }

    #endregion

    #region---------工具方法----------

    // 限制角度
    private float XianZhiJiaoDu(float jiaoDu)
    {
        while (jiaoDu < -180 || jiaoDu > 180)
        {
            if (jiaoDu > 180)
            {
                jiaoDu -= 360;
            }
            if (jiaoDu < -180)
            {
                jiaoDu += 360;
            }
        }
        return jiaoDu;
    }

    #endregion

    #region---------生命周期函数----------

    private  void Start ()
	{

        Init();


    }

	
	
	
	void Update () 
	{
        UpdateOpenDoor();
        UpdateCloseDoor();

        UpdateBianSe();
    }



    #endregion

}
