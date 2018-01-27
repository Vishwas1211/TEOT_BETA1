using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level5_Radio : MonoBehaviour
{
    private bool CanUpdate = false;

    public bool isOpen = false;
    public float BoChang;  //当前波长
    public float ZhengQue = 5;      //正确波长

    public float YinLiang = 0.5f;//音量大小

    public AudioSource Radio1;
    public AudioSource Radio2;
    public AudioClip ZaoYin;
    public AudioClip XinXi;

    public float BoChangBeiShu = 10;

    public int DangQian = 0;  //当前按下的按钮序号 

    private Image RadioUI;
    private Image ZhiZen;
    private Image XuanNiou_1;
    private Image XuanNiou_2;

    private Image[] butten = new Image[6];

    public Level_05_Radio_UI CurrentUI;  //当前UI

    //public ThirdPersonCharacter_WSM playerScript;

    #region---------外部变量----------



    #endregion

    #region---------内部变量----------



    #endregion

    #region---------调用方法----------

    public void Init()
    {
        Radio1 = gameObject.AddComponent<AudioSource>();
        Radio2 = gameObject.AddComponent<AudioSource>();
        Radio1.loop = true;
        Radio2.loop = true;
        Radio1.clip = XinXi;
        Radio2.clip = ZaoYin;

        //playerScript =Level_05_Manager.Instance.playerGO.GetComponent<ThirdPersonCharacter_WSM>();
        RadioUI = GameObject.Find("Canvas").transform.Find("RadioUI").GetComponent<Image>();
        RadioUI.gameObject.SetActive(false);

        ZhiZen = RadioUI.transform.Find("ZhiZen").GetComponent<Image>();
        XuanNiou_1 = RadioUI.transform.Find("XuanNiou_1").GetComponent<Image>();
        XuanNiou_2 = RadioUI.transform.Find("XuanNiou_2").GetComponent<Image>();

        butten[0] = RadioUI.transform.Find("Button/Button_0").GetComponent<Image>();
        butten[1] = RadioUI.transform.Find("Button/Button_1").GetComponent<Image>();
        butten[2] = RadioUI.transform.Find("Button/Button_2").GetComponent<Image>();
        butten[3] = RadioUI.transform.Find("Button/Button_3").GetComponent<Image>();
        butten[4] = RadioUI.transform.Find("Button/Button_4").GetComponent<Image>();
        butten[5] = RadioUI.transform.Find("Button/Button_5").GetComponent<Image>();

        OpenRadio(false);
        DianKa(false);


        CanUpdate = true;
    }

    public void OpenRadio(bool isOpen)
    {
        this.isOpen = isOpen;
        if (isOpen)
        {
            Radio1.Play();
            Radio2.Play();
        }
        else
        {
            Radio1.Stop();
            Radio2.Stop();
        }
    }

    public void DianKa(bool isOpen)  //玩家点开收音机
    {
        if (isOpen)
        {
            //改为2D音效
            Radio1.spatialBlend = 0;
            Radio2.spatialBlend = 0;
            //锁死玩家移动
            //PlayerManager.Instance.motionController.IsEnabled = false;
            GameObject.Find("ThirdPersonController").GetComponent<ThirdPersonCharacter_WSM>().canMove = false;
        }
        else
        {
            //改为3D音效
            Radio1.spatialBlend = 1;
            Radio2.spatialBlend = 1;
            //开启移动
            //playerScript.canMove = true;
            //PlayerManager.Instance.motionController.IsEnabled = true;
            GameObject.Find("ThirdPersonController").GetComponent<ThirdPersonCharacter_WSM>().canMove = true;
        }
        //操作UI
        RadioUI.gameObject.SetActive(isOpen);
        //操作相机脚本
        Level_05_Manager.Instance.GuanBiXiangJi(!isOpen);
    }

    public void TiaoJie(float b)
    {
        if ((ZhiZen.transform.localPosition.x + b) > -64 && (ZhiZen.transform.localPosition.x + b) < 66)
        {
            //UI 变化  + -120度
            ZhiZen.transform.localPosition = new Vector3(ZhiZen.transform.localPosition.x + b, -71.3f, 0);

            //按钮旋转
            XuanNiou_1.transform.Rotate(0, 0, b);

            BoChang += b / BoChangBeiShu;
            BianHua();
        }
    }

    public void TiaoJieYinLiang(float b)
    {
        if ((YinLiang + b / BoChangBeiShu) < 1 && (YinLiang + b / BoChangBeiShu) > 0)
        {

            //按钮旋转
            XuanNiou_2.transform.Rotate(0, 0, b * BoChangBeiShu);

            YinLiang += b / BoChangBeiShu;

            BianHua();
        }

    }

    #endregion

    #region---------功能方法----------

    public void BianHua()
    {
        float v = Mathf.Abs(ZhengQue - BoChang);
        Radio1.volume = (1 - v / ZhengQue) * YinLiang;
        Radio2.volume = (v / ZhengQue) * YinLiang;
    }

    #endregion

    #region---------工具方法----------



    #endregion

    #region-------生命周期函数--------

    private void Start()
    {
    }



    void Update()
    {
        if (!CanUpdate)
        {
            return;
        }


        if (RadioUI.gameObject.activeSelf)
        {

            if (!CurrentUI)
            {
                return;
            }
            switch (CurrentUI.m_type)
            {
                case Level_05_Radio_UI.UI_Type.Button:
                    {
                        //切换状态
                    }
                    break;
                case Level_05_Radio_UI.UI_Type.BoChang:
                    {
                        TiaoJie(Input.GetAxis("Mouse ScrollWheel") * BoChangBeiShu);
                    }
                    break;
                case Level_05_Radio_UI.UI_Type.YiLiang:
                    {
                        TiaoJieYinLiang(Input.GetAxis("Mouse ScrollWheel") * BoChangBeiShu);
                    }
                    break;
                default:
                    break;
            }




            if (Input.GetKeyDown(KeyCode.Escape))
            {
                DianKa(false);
            }
        }

    }



    #endregion

}

public class RadioFrequency
{
    public AudioClip XinXi;    //音频
    public float aaa = 5;      //正确波长
}