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
public class testDRO : MonoBehaviour
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

    private MOTION_LEFT_TYPE _curMode;
    private MOTION_RIGHT_TYPE _curRightMode = MOTION_RIGHT_TYPE.IDLE;

    private float _leftSpeed;
    private float _rightSpeed;
    public enum MOTION_LEFT_TYPE
    {
        START,
        STOP,
        IDLE,
        SUSPENSION,
        DECLINE,
        CLOCKWISE,
        ANTICLOCKWISE,
    }

    public enum MOTION_RIGHT_TYPE
    {
        FORWARD,
        BACK,
        LEFT,
        RIGHT,
        IDLE,
    }

    public enum MODE
    {
        AUTOMATIC,
        MANUAL,
    }

    public void Init()
    {

    }

    public void ClimbUp()
    {
        transform.Translate(Vector3.up * _propellerRotationRate * _leftSpeed * 0.01f * Time.deltaTime, Space.Self);
    }

    public void Decline()
    {
        transform.Translate(Vector3.up * -_propellerRotationRate * _leftSpeed * 0.01f * Time.deltaTime, Space.Self);
    }


    public void Launch(float speed)
    {
        SetCanStartOver(true);
        SetMode(MOTION_LEFT_TYPE.START, speed);
    }

    public void FallAlight()
    {
        SetMode(MOTION_LEFT_TYPE.STOP, 0);
    }

    public void SetSuspension()
    {
        SetMode(MOTION_LEFT_TYPE.SUSPENSION, 0);
    }

    public void SetDecline(float speed)
    {
        SetCanStartOver(true);
        SetMode(MOTION_LEFT_TYPE.DECLINE, speed);
    }

    public void SetCanStartOver(bool b)
    {
        if (b == _isCanStartOver)
            return;

        _isCanStartOver = b;
    }

    public void SetMode(MOTION_LEFT_TYPE mode, float speed)
    {
        _leftSpeed = speed;
        if (mode == _curMode)
            return;

        _curMode = mode;
    }

    public void SetRightMode(MOTION_RIGHT_TYPE mode, float speed)
    {
        _rightSpeed = speed;
        if (mode == _curRightMode)
            return;

        _curRightMode = mode;
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
            case MOTION_LEFT_TYPE.START:
                {
                    Working();

                    _propellerRotationRate = Mathf.Lerp(_propellerRotationRate, _maxRotationRate, Time.deltaTime * 0.2f);
                    _propellerRotationRate = Mathf.Clamp(_propellerRotationRate, _minRotationRate, _maxRotationRate);
                    //Debug.Log("--------------------" + _propellerRotationRate);
                    UpdateDrag(2.5f);
                    ClimbUp();
                    Debug.Log("isRuning.....");
                }
                break;
            case MOTION_LEFT_TYPE.STOP:
                {
                    if (_propellerRotationRate <= _minRotationRate + 0.05f)
                    {
                        SetCanStartOver(false);
                        SetMode(MOTION_LEFT_TYPE.IDLE, 0);
                        return;
                    }

                    _propellerRotationRate = Mathf.Lerp(_propellerRotationRate, _minRotationRate, Time.deltaTime * 0.5f);
                    _propellerRotationRate = Mathf.Clamp(_propellerRotationRate, _minRotationRate, _maxRotationRate);

                    //Debug.Log("--------------------" + _propellerRotationRate);
                    UpdateDrag(0.4f);
                    Working();
                    Debug.Log("isStopping.....");
                }
                break;
            case MOTION_LEFT_TYPE.IDLE:
                {
                    _rigidbody.drag = 0f;
                }
                break;
            case MOTION_LEFT_TYPE.SUSPENSION:
                {
                    Working();
                    _propellerRotationRate = Mathf.Lerp(_propellerRotationRate, _maxRotationRate, Time.deltaTime * 0.2f);
                    _propellerRotationRate = Mathf.Clamp(_propellerRotationRate, _minRotationRate, _maxRotationRate);
                    //Debug.Log("--------------------" + _propellerRotationRate);
                    _rigidbody.drag = 500 + 1;
                    Debug.Log("isRuning.....");
                }
                break;
            case MOTION_LEFT_TYPE.DECLINE:
                {
                    Working();

                    _propellerRotationRate = Mathf.Lerp(_propellerRotationRate, _maxRotationRate, Time.deltaTime * 0.2f);
                    _propellerRotationRate = Mathf.Clamp(_propellerRotationRate, _minRotationRate, _maxRotationRate);
                    //Debug.Log("--------------------" + _propellerRotationRate);
                    UpdateDrag(2.5f);
                    Decline();
                    Debug.Log("isRuning.....");
                }
                break;
            case MOTION_LEFT_TYPE.CLOCKWISE:
                {
                    Working();
                    UpdateRotationRight();
                }
                break;
            case MOTION_LEFT_TYPE.ANTICLOCKWISE:
                {
                    Working();
                    UpdateRotationLeft();
                }
                break;
        }
        switch (_curRightMode)
        {
            case MOTION_RIGHT_TYPE.FORWARD:
                {
                    UpdateMoveForward();
                }
                break;
            case MOTION_RIGHT_TYPE.BACK:
                {
                    UpdateMoveBack();
                }
                break;
            case MOTION_RIGHT_TYPE.LEFT:
                {
                    UpdateMoveLeft();
                }
                break;
            case MOTION_RIGHT_TYPE.RIGHT:
                {
                    UpdateMoveRight();
                }
                break;
            case MOTION_RIGHT_TYPE.IDLE:
                {

                }
                break;
        }
    }

    private void UpdateDrag(float f)
    {
        _rigidbody.drag = Mathf.Clamp(_propellerRotationRate * f, _minRotationRate, _maxRotationRate);
    }

    #region 移动
    public void UpdateMoveForward() //前进
    {
        SpeedRise();
        transform.Translate(Vector3.forward * _rightSpeed * Time.deltaTime, Space.Self);
        //transform.Translate(Vector3.forward * 2 * Time.deltaTime, Space.Self);
    }

    public void UpdateMoveBack()    //后退
    {
        SpeedDecrease();
        transform.Translate(Vector3.back * _rightSpeed * Time.deltaTime, Space.Self);
        //transform.Translate(Vector3.back * 2 * Time.deltaTime, Space.Self);
    }

    public void UpdateMoveLeft()    //向左走
    {
        //SpeedRise();
        //transform.Translate(Vector3.left * _curMoveSpeed * Time.deltaTime, Space.Self);
        transform.Translate(Vector3.left * _rightSpeed * Time.deltaTime, Space.Self);
    }

    public void UpdateMoveRight()   //向右走
    {
        //SpeedDecrease();
        //transform.Translate(Vector3.left * _curMoveSpeed * Time.deltaTime, Space.Self);
        transform.Translate(Vector3.right * _rightSpeed * Time.deltaTime, Space.Self);
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
        //transform.rotation = Quaternion.Lerp(_subBody.transform.rotation, Quaternion.Euler(new Vector3(0, -360, 0)), Time.deltaTime * 2f);
        transform.Rotate(new Vector3(0, -1, 0) * _leftSpeed * 5);
    }

    private void UpdateRotationRight()      //向右转
    {

        transform.Rotate(new Vector3(0, 1, 0) * _leftSpeed * 5);
        //transform.rotation = Quaternion.Lerp(_subBody.transform.rotation, Quaternion.Euler(new Vector3(0, 360, 0)), Time.deltaTime * 2f);
    }

    private void UpdateFonte(bool htc)
    {
        //右手控制器，前进后退
        if (htc)
        {
            transform.Translate(0, 0, PlayerHandController.front * Time.deltaTime * 10);

        }
        else
        {
            v = Input.GetAxis("Vertical");
            transform.Translate(0, 0, v * Time.deltaTime * 2);
        }
    }

    private void UpdateLeft(bool htc)
    {
        //右手控制器，左右移动
        if (htc)
        {
            if (PlayerHandController.left > 0.3f || PlayerHandController.left < -0.3f)
            {
                transform.Translate(PlayerHandController.left * Time.deltaTime * 10, 0, 0);
            }
            else
            {
                transform.Translate(0 * 10, 0, 0);
            }

        }
        else
        {
            v = Input.GetAxis("Vertical");
            transform.Translate(v * Time.deltaTime * 2, 0, 0);
        }
    }

    private void UpdateRotation(bool htc)
    {
        //左右控制器，旋转
        if (htc)
        {
            if (PlayerHandController.up > 0.3f || PlayerHandController.up < -0.3f)
            {
                transform.Rotate(transform.up, PlayerHandController.up * 200 * Time.deltaTime);
            }
            else
            {
                transform.Rotate(transform.up, 0 * 200 * Time.deltaTime);
            }

        }
        else
        {
            h = Input.GetAxis("Horizontal");
            transform.Rotate(transform.up, h * 50 * Time.deltaTime);
        }
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

    #endregion


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
        _subBody = transform.Find("DRO_Body").gameObject;
        SetMode(MOTION_LEFT_TYPE.IDLE, 0);
    }

    private void Start()
    {
        //SetStartOver(true);
    }

    void Update()
    {
        UpdateRateLerp();
        //UpdateFonte(true);
        //UpdateRotation(true);
        //UpdateLeft(true);
        //test();
    }

    void test()
    {
        transform.position = PlayerManager.Instance.rightHandController.transform.position;
        transform.rotation = PlayerManager.Instance.rightHandController.transform.rotation;
    }
}
