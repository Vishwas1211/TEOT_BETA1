//
//  PlayerManager.cs
//  TEOT_ONLINE
//
//  Created by EDSENSES-P1 on 8/2/2017 9:25 AM.
//
//
using UnityEngine;
using System.Collections;
using System;

public class PlayerManager : SingletonMono<PlayerManager>
{
    //private static PlayerManager _instance;
    //public static PlayerManager Instance
    //{
    //    get
    //    {
    //        return _instance;
    //    }
    //}

    private GameObject _eye;
    public GameObject eye
    {
        get { return _eye; }
    }

    private GameObject _playerCollider;
    public GameObject playerCollider
    {
        get { return _playerCollider; }
    }

    private GameObject _leftHandController;
    public GameObject leftHandController
    {
        get { return _leftHandController; }
    }

    private GameObject _leftHand;
    public GameObject leftHand
    {
        get { return _leftHand; }
    }

    private GameObject _rightHand;
    public GameObject rightHand
    {
        get { return _rightHand; }
    }

    private GameObject _rightHandController;
    public GameObject rightHandController
    {
        get { return _rightHandController; }
    }

    private PlayerMove _playerMove;
    public PlayerMove playerMove
    {
        get { return _playerMove; }
    }

    private PlayerStatus _playerStatus;
    public PlayerStatus playerStatus
    {
        get { return _playerStatus; }
    }

    private PlayerToolsBase _playerToolsBase;
    public PlayerToolsBase playerToolsBase
    {
        get { return _playerToolsBase; }
    }

    private BackpackController _leftBackpackController;
    public BackpackController leftBackpackController
    {
        get { return _leftBackpackController; }
    }

    private BackpackController _rightBackpackController;
    public BackpackController rightBackpackController
    {
        get { return _rightBackpackController; }
    }

    private PlayerInfo _playerInfo;
    public PlayerInfo playerInfo
    {
        get { return _playerInfo; }
    }

    private Animator _animator;
    public Animator animator
    {
        get { return _animator; }
    }

    private Rigidbody _rig;
    public Rigidbody rig
    {
        get { return _rig; }
    }

    //private ThirdPersonCharacter_WSM _thirdPersonCharacter_WSM;
    //public ThirdPersonCharacter_WSM thirdPersonCharacter_WSM
    //{
    //    get { return _thirdPersonCharacter_WSM; }
    //}

    private com.ootii.Actors.AnimationControllers.MotionController _motionController;
    public com.ootii.Actors.AnimationControllers.MotionController motionController
    {
        get { return _motionController; }
    }

    private TestToolsManagers _testToolsManagers;
    public TestToolsManagers testToolsManagers
    {
        get { return _testToolsManagers; }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void Init()
    {
        GetPlayerComponent();
        GetPlayerScript();
        InitPlayerFunc();
    }

    private void GetPlayerScript()
    {
        //_playerMove = transform.GetComponent<PlayerMove>();
        _playerStatus = this.GetComponent<PlayerStatus>();
        _playerToolsBase = this.GetComponent<PlayerToolsBase>();
        _playerInfo = this.GetComponent<PlayerInfo>();
        //_thirdPersonCharacter_WSM = this.GetComponent<ThirdPersonCharacter_WSM>();
        _testToolsManagers = this.GetComponent<TestToolsManagers>();
        _motionController = this.GetComponent<com.ootii.Actors.AnimationControllers.MotionController>();
        //_leftBackpackController = _leftHandController.transform.Find("RadialMenu/RadialMenuUI/Panel").GetComponent<BackpackController>();
        //_rightBackpackController = _rightHandController.transform.Find("RadialMenu/RadialMenuUI/Panel").GetComponent<BackpackController>();
    }

    private void GetPlayerComponent()
    {
        switch (PlayerMode.Instance.playerMode)
        {
            case PlayerMode.ePlayerMode.vive:
                {
                    _leftHandController = transform.Find("Controller (left)/LeftController").gameObject;
                    _rightHandController = transform.Find("Controller (right)/RightController").gameObject;

                    _leftHand = transform.Find("LeftHand").gameObject;
                    _rightHand = transform.Find("RightHand").gameObject;

                    _eye = transform.Find("Camera (eye)").gameObject;
                    _playerCollider = transform.Find("[VRTK][AUTOGEN][BodyColliderContainer]").gameObject;
                }
                break;
            case PlayerMode.ePlayerMode.pc:
                {
                    _eye = GameObject.FindGameObjectWithTag("MainCamera");
                    _playerCollider = transform.gameObject;
                    _animator = this.GetComponent<Animator>();
                    _rig = this.GetComponent<Rigidbody>();
                }
                break;
            default:
                break;
        }
    }

    private void InitPlayerFunc()
    {
        switch (PlayerMode.Instance.playerMode)
        {
            case PlayerMode.ePlayerMode.vive:
                {
                    _leftHandController.transform.GetComponent<PlayerHandController>().Init();
                    _rightHandController.transform.GetComponent<PlayerHandController>().Init();
                    //_playerMove.Init();
                }
                break;
            case PlayerMode.ePlayerMode.pc:
                break;
            default:
                break;
        }
    }
}
