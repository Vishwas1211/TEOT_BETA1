using System.Collections;using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpenDoor_WSM : InteractiveGoods
{
    //区分是哪一种门


    #region---------外部变量----------

   

    private BoxCollider m_BoxCollider;
    private BoxCollider m_BoxCollider_L;
    private BoxCollider m_BoxCollider_R;

    public Transform Door_L_Transform;
    public Transform Door_R_Transform;

    private Vector3 XuanZhuanZhou;    // 旋转轴
    private Vector3 XuanZhuanZhou_L;  // 旋转轴
    private Vector3 XuanZhuanZhou_R;  // 旋转轴

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

    public bool isGuan  = true;            //是否关门
    public bool isArrive = true;           //是否完成

    public float KaiMenJiaoDu = 90;        //旋转角度
    private float _KaiMenJiaoDu;           //内部旋转角度

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
            IsShangSanMen = IsDoubleDoor();

            if (IsDoubleDoor())
            {
                DoudleDoorInit();
            }
            else
            {
                SingleDoorInit();
            }
            _KaiMenJiaoDu = KaiMenJiaoDu;
        }
    }


    public override void Operation()
    {
        _KaiMenJiaoDu = KaiMenJiaoDu;
        if (IsZ)
        {
            if (DoorManager.Instance.Player_GameObject.transform.position.z > transform.position.z)  //TODO 获取玩家的方式不对
            {
                _KaiMenJiaoDu = -1* fanZhan* _KaiMenJiaoDu;
            }
        }
        else
        {
            if (IsShangSanMen) //单扇门和双扇门的轴向变化不同
            {
                if (DoorManager.Instance.Player_GameObject.transform.position.x > transform.position.x) 
                {
                    _KaiMenJiaoDu *= -1;
                }
            }
            else
            {
                if (DoorManager.Instance.Player_GameObject.transform.position.x < transform.position.x)  
                {
                    _KaiMenJiaoDu = -1 * fanZhan * _KaiMenJiaoDu;
                }
            }
        }

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
        else {
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

    private void DoudleDoorInit()
    {
        if (transform.GetChild(0).position.x-transform.GetChild(1).position.x <-0.2f)
        {
            Door_L_Transform = transform.GetChild(0);
            Door_R_Transform = transform.GetChild(1);
        }
        else if (transform.GetChild(0).position.x - transform.GetChild(1).position.x > 0.2f)
        {
            Door_R_Transform = transform.GetChild(0);
            Door_L_Transform = transform.GetChild(1);
        }
        else if (transform.GetChild(0).position.z - transform.GetChild(1).position.z < -0.2f)
        {
            Door_R_Transform = transform.GetChild(0);
            Door_L_Transform = transform.GetChild(1);
        }
        else if (transform.GetChild(0).position.z - transform.GetChild(1).position.z > 0.2f)
        {
            Door_L_Transform = transform.GetChild(0);
            Door_R_Transform = transform.GetChild(1);
        }

        Door_L_Transform.localEulerAngles = Vector3.zero; //初始化归零
        Door_R_Transform.localEulerAngles = Vector3.zero; //初始化归零

        m_BoxCollider_L = Door_L_Transform.gameObject.AddComponent<BoxCollider>();
        m_BoxCollider_R =  Door_R_Transform.gameObject.AddComponent<BoxCollider>();

        if (FangXiangIsZ(m_BoxCollider_L))   //判断门的朝向
        {
            XuanZhuanZhou_L = new Vector3(Door_L_Transform.position.x - m_BoxCollider_L.size.x / 2, Door_L_Transform.position.y, Door_L_Transform.position.z);
            XuanZhuanZhou_R = new Vector3(Door_R_Transform.position.x + m_BoxCollider_R.size.x / 2, Door_R_Transform.position.y, Door_R_Transform.position.z);
            IsZ = true;
        }
        else
        {
            XuanZhuanZhou_L = new Vector3(Door_L_Transform.position.x , Door_L_Transform.position.y, Door_L_Transform.position.z + m_BoxCollider_L.size.z / 2);
            XuanZhuanZhou_R = new Vector3(Door_R_Transform.position.x , Door_R_Transform.position.y, Door_R_Transform.position.z - m_BoxCollider_R.size.z / 2);
            IsZ = false;
        }


    }

    public void UpdateOpenDoor()
    {
        if (!isGuan && !isArrive)
        {
            if (IsDoubleDoor())
            {
                float f = XianZhiJiaoDu(Door_L_Transform.transform.localEulerAngles.y);
                if (f > KaiMenJiaoDu || f < -KaiMenJiaoDu)
                {
                    isArrive = true;
                    return;
                }
                Door_L_Transform.transform.RotateAround(XuanZhuanZhou_L, Vector3.up, -_KaiMenJiaoDu * Time.deltaTime);
                Door_R_Transform.transform.RotateAround(XuanZhuanZhou_R, Vector3.up, _KaiMenJiaoDu * Time.deltaTime);
            }
            else
            {
                float f = XianZhiJiaoDu(transform.localEulerAngles.y);
                if (f > KaiMenJiaoDu || f < -KaiMenJiaoDu)
                {
                    isArrive = true;
                    return;
                }

                transform.RotateAround(XuanZhuanZhou, Vector3.up, _KaiMenJiaoDu * Time.deltaTime);
            }
        }
    }

    public void UpdateCloseDoor()
    {
        if (isGuan && !isArrive)
        {
            if (IsDoubleDoor())
            {
                float f = XianZhiJiaoDu(Door_L_Transform.transform.localEulerAngles.y);
                if (f <2f  && f >-2f)
                {
                    isArrive = true;
                    return;
                }

                if (f > 0)
                {
                    Door_L_Transform.transform.RotateAround(XuanZhuanZhou_L, Vector3.up, -KaiMenJiaoDu * Time.deltaTime);
                    Door_R_Transform.transform.RotateAround(XuanZhuanZhou_R, Vector3.up, KaiMenJiaoDu * Time.deltaTime);
                }
                else
                {
                    Door_L_Transform.transform.RotateAround(XuanZhuanZhou_L, Vector3.up, KaiMenJiaoDu * Time.deltaTime);
                    Door_R_Transform.transform.RotateAround(XuanZhuanZhou_R, Vector3.up, -KaiMenJiaoDu * Time.deltaTime);
                }
                //if (Door_R_Transform.rotation.y > 0)
                //{
                //    Door_R_Transform.transform.RotateAround(XuanZhuanZhou_R, Vector3.up, _KaiMenJiaoDu * Time.deltaTime);
                //}
                //else
                //{
                //    Door_R_Transform.transform.RotateAround(XuanZhuanZhou_R, Vector3.up, -_KaiMenJiaoDu * Time.deltaTime);
                //}
            }
            else
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
    private bool FangXiangIsZ(BoxCollider box )
    {
        if (box == null )
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

    private  void Awake ()
	{

        Init();

    }

	
	
	
	void Update () 
	{
        UpdateOpenDoor();
        UpdateCloseDoor();


    }



 #endregion

}
