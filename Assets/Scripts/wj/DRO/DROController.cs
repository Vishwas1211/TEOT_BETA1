//DROController.cs
//TEOT_ONLINE
//
//Create by WangJie on 9/21/2017 11:15 AM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DROController : MonoBehaviour
{
    private float _propellerRotationRate = 0f;
    private float _minRotationRate = 0f;
    private float _maxRotationRate = 50f;
    private float _minMoveSpeed = 0f;
    private float _maxMoveSpeed = 2f;
    private float _curMoveSpeed = 0f;
    private float _moveSpeedDelat = 0f;
    private float _timeDelat = 0f;
    private float h;
    private float v;

    private bool _isCanStartOver;

    [SerializeField]
    private GameObject _leftPropeller;  //螺旋桨
    [SerializeField]
    private GameObject _rightPropeller;  //螺旋桨
    private GameObject _subBody;

    private bool _isCanBack;

    private AudioSource _audioSource;
    private Rigidbody _rigidbody;

    private MOTION_TYPE _curMode;
    public FLY_TYPE curFlyType;

    public enum MOTION_TYPE
    {
        START,
        STOP,
        IDLE,
    }

    public enum MODE
    {
        AUTOMATIC,
        MANUAL,
    }

    public enum FLY_TYPE
    {
        FRONT,
        BACK,
        LEFT,
        RIGHT,
        BALANCED,
        EMPTY,
    }

    public void Init()
    {

    }

    public void ClimbUp()
    {
        transform.Translate(Vector3.up * _propellerRotationRate * 0.01f * Time.deltaTime, Space.Self);
    }

    public void Decline()
    {

    }


    public void Launch()
    {
        SetCanStartOver(true);
        SetMode(MOTION_TYPE.START);
    }

    public void FallAlight()
    {
        SetMode(MOTION_TYPE.STOP);
    }

    public void SetCanStartOver(bool b)
    {
        if (b == _isCanStartOver)
            return;

        _isCanStartOver = b;
    }

    public void SetMode(MOTION_TYPE mode)
    {
        if (mode == _curMode)
            return;

        _curMode = mode;
    }

    private void Working()
    {
        _leftPropeller.transform.Rotate(new Vector3(0, 1, 0) * _propellerRotationRate);
        _rightPropeller.transform.Rotate(new Vector3(0, 1, 0) * _propellerRotationRate);
    }

    private void UpdateRateLerp()
    {
        //if (!_isCanStartOver)
        //    return;

        switch (_curMode)
        {
            case MOTION_TYPE.START:
                {
                    Working();
                    if (_propellerRotationRate >= _maxRotationRate - 1)
                        return;

                    _propellerRotationRate = Mathf.Lerp(_propellerRotationRate, _maxRotationRate, Time.deltaTime * 0.2f);
                    //Debug.Log("--------------------" + _propellerRotationRate);
                    UpdateDrag(2.5f);
                    Debug.Log("isRuning.....");
                }
                break;
            case MOTION_TYPE.STOP:
                {
                    if (_propellerRotationRate <= _minRotationRate + 0.05f)
                    {
                        SetCanStartOver(false);
                        SetMode(MOTION_TYPE.IDLE);
                        return;
                    }

                    _propellerRotationRate = Mathf.Lerp(_propellerRotationRate, _minRotationRate, Time.deltaTime * 0.5f);
                    //Debug.Log("--------------------" + _propellerRotationRate);
                    UpdateDrag(0.4f);
                    Working();
                    Debug.Log("isStopping.....");
                }
                break;
            case MOTION_TYPE.IDLE:
                {
                    _rigidbody.drag = 0f;
                }
                break;
        }
    }

    private void UpdateDrag(float f)
    {
        _rigidbody.drag = _propellerRotationRate * f;
    }

    #region 移动
    public void UpdateMoveForward() //前进
    {
        SpeedRise();
        transform.Translate(Vector3.forward * _curMoveSpeed * Time.deltaTime, Space.Self);
        //transform.Translate(Vector3.forward * 2 * Time.deltaTime, Space.Self);
    }

    public void UpdateMoveBack()    //后退
    {
        SpeedDecrease();
        transform.Translate(Vector3.forward * _curMoveSpeed * Time.deltaTime, Space.Self);
        //transform.Translate(Vector3.back * 2 * Time.deltaTime, Space.Self);
    }

    public void UpdateMoveLeft()    //向左走
    {
        //SpeedRise();
        //transform.Translate(Vector3.left * _curMoveSpeed * Time.deltaTime, Space.Self);
        transform.Translate(Vector3.left * 2 * Time.deltaTime, Space.Self);
    }

    public void UpdateMoveRight()   //向右走
    {
        //SpeedDecrease();
        //transform.Translate(Vector3.left * _curMoveSpeed * Time.deltaTime, Space.Self);
        transform.Translate(Vector3.right * 2 * Time.deltaTime, Space.Self);
    }

    private void UpdateRotationForward()    //向前转
    {
        _subBody.transform.rotation = Quaternion.Lerp(_subBody.transform.rotation, Quaternion.Euler(new Vector3(45f, 0, 0)), Time.deltaTime * 2f);
    }

    private void UpdateRotationBalanced()  //平衡
    {
        _subBody.transform.rotation = Quaternion.Lerp(_subBody.transform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), Time.deltaTime * 2f);
    }

    private void UpdateRotationBack()       //向后转
    {
        //if (!_isCanBack)
        //    return;
        _subBody.transform.rotation = Quaternion.Lerp(_subBody.transform.rotation, Quaternion.Euler(new Vector3(-45, 0, 0)), Time.deltaTime * 2f);
    }

    private void UpdateRotationLeft()       //向左转
    {
        _subBody.transform.rotation = Quaternion.Lerp(_subBody.transform.rotation, Quaternion.Euler(new Vector3(0, 0, 45)), Time.deltaTime * 2f);
    }

    private void UpdateRotationRight()      //向右转
    {
        _subBody.transform.rotation = Quaternion.Lerp(_subBody.transform.rotation, Quaternion.Euler(new Vector3(0, 0, -45)), Time.deltaTime * 2f);
    }

    private void UpdateFonte()
    {
        v = Input.GetAxis("Vertical");
        transform.Translate(0, 0, v * Time.deltaTime * 10);
    }

    private void UpdateRotation()
    {
        h = Input.GetAxis("Horizontal");
        transform.Rotate(transform.up, h);
    }

    private void SpeedRise()
    {
        if (_curMoveSpeed >= _maxMoveSpeed)
        {
            _moveSpeedDelat = 0f;
            return;
        }

        _curMoveSpeed += _moveSpeedDelat;
        _moveSpeedDelat += 0.00008f;
        Debug.Log(_curMoveSpeed);
    }

    private void SpeedDecrease()
    {
        if (_curMoveSpeed <= -_maxMoveSpeed)
        {
            _moveSpeedDelat = 0f;
            return;
        }

        _curMoveSpeed -= _moveSpeedDelat;
        _moveSpeedDelat += 0.00005f;
        Debug.Log(_curMoveSpeed);
    }
    public void SetFlyState(FLY_TYPE state)
    {
        if (state == curFlyType)
            return;
        curFlyType = state;
    }

    private void UpdateFlyState()
    {
        switch (curFlyType)
        {
            case FLY_TYPE.FRONT:
                {
                    UpdateFonte();
                    //UpdateRotationForward();
                    //UpdateMoveForward();
                }
                break;
            case FLY_TYPE.BACK:
                {
                    UpdateFonte();
                    //UpdateRotationBack();
                    //UpdateMoveBack();
                }
                break;
            case FLY_TYPE.LEFT:
                {
                    UpdateRotation();
                    //UpdateRotationLeft();
                    //UpdateMoveLeft();
                }
                break;
            case FLY_TYPE.RIGHT:
                {
                    UpdateRotation();
                    //UpdateRotationRight();
                    //UpdateMoveRight();
                }
                break;
            case FLY_TYPE.BALANCED:
                {
                    UpdateRotationBalanced();
                    _curMoveSpeed = Mathf.Lerp(_curMoveSpeed, _minMoveSpeed, Time.deltaTime * 0.5f);
                    transform.Translate(Vector3.forward * _curMoveSpeed * Time.deltaTime, Space.Self);
                    if (Mathf.Abs(_curMoveSpeed) <= 0.2f)
                    {
                        SetFlyState(FLY_TYPE.EMPTY);
                    }
                }
                break;
            case FLY_TYPE.EMPTY:
                _curMoveSpeed = 0;
                break;
        }
    }
    #endregion


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
        _subBody = transform.Find("DRO_Body").gameObject;
        SetFlyState(FLY_TYPE.EMPTY);
        SetMode(MOTION_TYPE.IDLE);
    }

    private void Start()
    {
        //SetStartOver(true);
    }

    void Update()
    {
        UpdateFlyState();
        UpdateRateLerp();
        SetFlyState(FLY_TYPE.BALANCED);
        UpdateFonte();
        UpdateRotation();

        //test
        //if (Input.GetKey(KeyCode.W))
        //{
        //    //FallAlight();
        //    SetFlyState(FLY_TYPE.FRONT);
        //}

        //if (Input.GetKey(KeyCode.S))
        //{
        //    //FallAlight();
        //    SetFlyState(FLY_TYPE.BACK);
        //}

        //if (Input.GetKey(KeyCode.A))
        //{
        //    SetFlyState(FLY_TYPE.LEFT);
        //}

        //if (Input.GetKey(KeyCode.D))
        //{
        //    SetFlyState(FLY_TYPE.RIGHT);
        //}

        ////if (Input.GetKeyDown(KeyCode.Q))
        ////{
        ////}
        //if (Input.GetKey(KeyCode.Q))
        //{
        //    Launch();
        //    ClimbUp();
        //}

        //if (Input.GetKeyUp(KeyCode.Q))
        //{
        //    FallAlight();
        //}
        //if(Input.GetKey(KeyCode.L))
        //{
        //    SpeedRise();
        //}
    }
}
