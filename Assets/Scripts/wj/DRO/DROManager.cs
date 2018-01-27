//DROManager.cs
//TEOT_ONLINE
//
//Create by WangJie on 9/21/2017 11:15 AM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DROManager : SingletonMono<DROManager>
{
    private const string DRO_PREFAB_PATH = "Prefabs/Character/DRO/DRO";

    private GameObject _dro;
    public GameObject dro
    {
        get { return _dro; }
    }

    private testDRO _droController;
    public testDRO droController
    {
        get { return _droController; }
    }

    public void Init()
    {
        //_dro = UtilFunction.ResourceLoad(DRO_PREFAB_PATH);
        _dro = GameObject.Find("DRO");

        _dro.name = UtilFunction.GetPrefabName(DRO_PREFAB_PATH);
        _droController = _dro.GetComponent<testDRO>();
    }

    #region 操控
    /// <summary>
    /// 启动
    /// </summary>
    public void Lunch(bool isUp,float speed)
    {
        if (isUp)
        {
            _droController.Launch(speed);
            //_droController.ClimbUp();
        }
        else
        {
            _droController.SetSuspension();
            //_droController.Decline();
            //_droController.FallAlight();
        }
    }

    public void Decline(bool isDown,float speed)
    {
        if (isDown)
        {
            _droController.SetDecline(speed);
            //_droController.ClimbUp();
        }
        else
        {
            _droController.SetSuspension();
            //_droController.Decline();
            //_droController.FallAlight();
        }
    }

    /// <summary>
    /// 前进
    /// </summary>
    public void MoveForward(float speed)
    {
        //_droController.SetFlyState(DROController.FLY_TYPE.FRONT);
        _droController.SetRightMode(testDRO.MOTION_RIGHT_TYPE.FORWARD,speed);
    }

    /// <summary>
    /// 后退
    /// </summary>
    public void MoveBack(float speed)
    {
        _droController.SetRightMode(testDRO.MOTION_RIGHT_TYPE.BACK,speed);
        //_droController.SetFlyState(DROController.FLY_TYPE.BACK);
        //_droController.UpdateMoveBack();
    }

    /// <summary>
    /// 左转 
    /// </summary>
    public void TurnLeft(float speed)
    {
        _droController.SetRightMode(testDRO.MOTION_RIGHT_TYPE.LEFT,speed);

        //_droController.SetFlyState(DROController.FLY_TYPE.LEFT);
        //_droController.UpdateMoveLeft();
    }

    /// <summary>
    /// 右转
    /// </summary>
    public void TurnRight(float speed)
    {
        _droController.SetRightMode(testDRO.MOTION_RIGHT_TYPE.RIGHT,speed);
        //_droController.SetFlyState(DROController.FLY_TYPE.RIGHT);
        //_droController.UpdateMoveRight();
    }

    public void SetIdle()
    {
        _droController.SetRightMode(testDRO.MOTION_RIGHT_TYPE.IDLE,0);
    }

    public void SetLeft(float speed) {
        _droController.SetMode(testDRO.MOTION_LEFT_TYPE.ANTICLOCKWISE,speed);
    }

    public void SetRight(float speed) {
        _droController.SetMode(testDRO.MOTION_LEFT_TYPE.CLOCKWISE,speed);
    }
    #endregion


    //private void Awake()
    //{
    //    _droController = GetComponent<DROController>();
    //}

    private void Start()
    {
        Init();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            //Lunch(true);
        }
        if (Input.GetKey(KeyCode.E))
        {
            //Lunch(false);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _droController.FallAlight();
        }
        if (Input.GetKey(KeyCode.W))
        {
            //MoveForward();
        }

        if (Input.GetKey(KeyCode.S))
        {
            //MoveBack();
        }

        if (Input.GetKey(KeyCode.A))
            //TurnLeft();

            if (Input.GetKey(KeyCode.D)) { }
            //TurnRight();
    }
}
