using System.Collections;using System.Collections.Generic;
using UnityEngine;


public class Level_20_Door_PuDong : MonoBehaviour 
{

    //区分是哪一种门


    #region---------外部变量----------



    private BoxCollider m_BoxCollider;



    private Vector3 XuanZhuanZhou;    // 旋转轴

    public bool isFanXiang    //轴是否要反转
    {
        set
        {
            _isFanXiang = value;
            if (_isFanXiang)
            {
                fanZhan = -1;
            }
            else
            {
                fanZhan = 1;
            }
            SingleDoorInit();
        }
        get { return _isFanXiang; }
    }
    private bool _isFanXiang = false;
    private int fanZhan = 1;

    public bool isGuan = true;            //是否关门
    public bool isArrive = true;          //是否完成

    public float KaiMenJiaoDu = 30;     //旋转角度
    private float _KaiMenJiaoDu;   //内部旋转角度

    private bool IsShangSanMen;

    public bool CanOpen = true;
    private bool IsZ = false;


    #endregion

    #region---------内部变量----------

    private bool isInit = false;

    #endregion

    #region---------调用方法----------

    public void Init()
    {
        if (!isInit)
        {
            isInit = true;
                SingleDoorInit();
            _KaiMenJiaoDu = KaiMenJiaoDu;
        }
    }

    public void CaoZuo()
    {
        _KaiMenJiaoDu = -KaiMenJiaoDu;

        if (CanOpen)
        {
            isGuan = !isGuan;
            isArrive = false;
        }
    }

    #endregion

    #region---------功能方法----------




    private void SingleDoorInit()   //单扇门的初始化
    {
        if (!gameObject.GetComponent<BoxCollider>())
        {
            m_BoxCollider = gameObject.AddComponent<BoxCollider>();
        }
        else
        {
            m_BoxCollider = gameObject.GetComponent<BoxCollider>();
        }
        if (FangXiangIsZ(m_BoxCollider))   //判断门的朝向
        {
            XuanZhuanZhou = new Vector3(transform.position.x + fanZhan * m_BoxCollider.size.x / 2, transform.position.y, transform.position.z);
            IsZ = true;
        }
        else
        {

            XuanZhuanZhou = new Vector3(transform.position.x, transform.position.y, transform.position.z + fanZhan * m_BoxCollider.size.z / 2);
            IsZ = false;
        }
        transform.localEulerAngles = Vector3.zero; //初始化归零
    }

 



    public void UpdateOpenDoor()
    {
        if (!isGuan && !isArrive)
        {
         
            {
                float f = XianZhiJiaoDu(transform.localEulerAngles.y);
                if (f > KaiMenJiaoDu || f < -KaiMenJiaoDu)
                {
                    isArrive = true;
                Level_20_Manager.Instance.PoMen();
                    return;
                }

                transform.RotateAround(XuanZhuanZhou, Vector3.up, -30 * Time.deltaTime);


            }
        }
    }

    public void UpdateCloseDoor()
    {
        if (isGuan && !isArrive)
        {
      
            {
                float f = XianZhiJiaoDu(transform.localEulerAngles.y);
                if (f < 2f && f > -2f)
                {
                    isArrive = true;
                    return;
                }

                if (f > 0)
                {
                    transform.RotateAround(XuanZhuanZhou, Vector3.up, -KaiMenJiaoDu * Time.deltaTime);
                }
                else
                {
                    transform.RotateAround(XuanZhuanZhou, Vector3.up, KaiMenJiaoDu * Time.deltaTime);
                }
            }
        }
    }


    #endregion

    #region---------工具方法----------
    int i = 0;
    //判断门是否面向Z
    private bool FangXiangIsZ(BoxCollider box)
    {
        if (box == null)
        {
            Debug.Log(transform.name);
        }

        if (box.size.x > box.size.z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //判断是否是双扇门
    private bool IsDoubleDoor()
    {
        if (transform.childCount == 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

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

    private void Awake()
    {
        Init();
    }

    void Update()
    {
        UpdateOpenDoor();
        UpdateCloseDoor();
    }
    #endregion

}
