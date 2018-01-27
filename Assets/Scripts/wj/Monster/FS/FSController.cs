//FSController.cs
//TEOT_ONLINE
//
//Create by WangJie on 12/19/2017 9:46 AM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class FSController : MonoBehaviour
{
    private const string CLIMB_POINT_PATH = "Prefabs/Character/Enemy/FS/ClimbPoints";
    private const string CLIMB_ROTATE_PATH = "Prefabs/Character/Enemy/FS/ClimbRotatePoints"; 
    private const string ROOF_POINT_PATH = "Prefabs/Character/Enemy/FS/RoofPathPoints";
    private const string ROOF_ROTATE_PATH = "Prefabs/Character/Enemy/FS/ClimbRoofRotatePoints";
    private const string FLEE_POINT = "Prefabs/Character/Enemy/FS/FSFLeePoint";

    private const float MAX_HP = 100f;
    private float _curHp = MAX_HP;
    private float _walkSpeed = 3f;
    private float _runSpeed = 6f;
    private float _shortRange = 1;

    private float _timer = 0f;

    private int _roofPointIndex = 0;
    private int _walkToPointIndex = 0;
    private int _climbRotateIndex = 0;

    private bool _isDead;
    private bool _isStart;
    private bool _isFirstAppear = true;

    private GameObject _targetPoint;

    private GameObject _target;
    private GameObject _rayPoint;
    private GameObject _climbPointParent;
    private GameObject[] _climbPoints;
    private GameObject _climbRotateParent;
    private GameObject[] _climbRotatePoints;
    private GameObject _roofPathPointParent;
    private GameObject[] _roofPathPoints;
    private GameObject _climbRoofRotateParent;
    private GameObject[] _climbRoofRotatePoints;
    private GameObject _fleePoint;
    private Vector3[] _fleePoistion;

    #region 旋转 DoTween 路径
    /// <summary>
    /// 旋转 DoTween 路径 1
    /// </summary>
    private Vector3[] _dtRotatePath1;
    private Vector3[] _dtRotatePath2;
    private Vector3[] _dtRotatePath3;
    private Vector3[] _dtRotatePath4;
    private Vector3[] _dtRotatePath5;
    private Vector3[] _dtRotatePath6;
    private Vector3[] _dtRotatePath7;

    private Vector3[] _dtRoofRotatePath1;
    private Vector3[] _dtRoofRotatePath2;
    private Vector3[] _dtRoofRotatePath3;
    private Vector3[] _dtRoofRotatePath4;
    private Vector3[] _dtRoofRotatePath5;
    private Vector3[] _dtRoofRotatePath6;
    private Vector3[] _dtRoofRotatePath7;
    #endregion 

    private Animator _anim;
    private NavMeshAgent _agent;

    private FSBulletEffectManager _effectManager;
    public FSBulletEffectManager effectManager
    {
        get { return _effectManager; }
    }


    public FS_STATE _curState;
    public WALK_TYPE _curWalkType;

    public enum FS_STATE
    {
        IDLE,
        WALK,
        RUN,
        JUMP,
        TURN_LEFT,
        TURN_RIGHT,
        IDLE_WALL,
        CLIMB_WALL,
        WALK_WALL,
        WALK_ROOF,
        WALK_TO_POINT,
        TURN_TO_POINT,
        TO_PATH_ROTATE,
        JUMP_TO_WALL,
        JUMP_TO_GROUND,
        FLEE,                //逃跑
        FLEE_TO_WALL, //往墙上跑
        FLEE_IDLE,        //逃跑待机
        ROAR,               //咆哮
        GRAB,               //抓
        LASH,                //抽打
        GRAB_THROW,   //抓甩右
        PRESS,              //按压
        BITE,                 //咬
        STRIKE,             //撞击，顶
        CYCLON,           //旋转
        CYCLON_FAST,    //快速旋转
        CANNON,          //炮
        BLAST_WAVE,    //冲击波
        HURT,
        REBORN,         //复活
        DEATH,
    }

    public enum WALK_TYPE
    {
        GRAOUND,
        WALL,
        ROOF,
    }

    public void Init()
    {
        _effectManager = gameObject.AddComponent<FSBulletEffectManager>();
        _effectManager.Init();
        _rayPoint = transform.Find("FS/RayPoint").gameObject;

        _fleePoint = UtilFunction.ResourceLoad(FLEE_POINT);
        _fleePoistion = new Vector3[_fleePoint.transform.childCount];
        for (int i = 0; i < _fleePoint.transform.childCount; i++)
        {
            _fleePoistion[i] = _fleePoint.transform.GetChild(i).position;
        }

        _climbPointParent = UtilFunction.ResourceLoad(CLIMB_POINT_PATH);
        _climbPoints = new GameObject[_climbPointParent.transform.childCount];
        for (int i = 0; i < _climbPointParent.transform.childCount; i++)
        {
            _climbPoints[i] = _climbPointParent.transform.GetChild(i).gameObject;
        }

        _climbRotateParent = UtilFunction.ResourceLoad(CLIMB_ROTATE_PATH);
        _climbRotatePoints = new GameObject[_climbRotateParent.transform.childCount];
        for (int i = 0; i < _climbRotateParent.transform.childCount; i++)
        {
            _climbRotatePoints[i] = _climbRotateParent.transform.GetChild(i).gameObject;
        }

        _roofPathPointParent = UtilFunction.ResourceLoad(ROOF_POINT_PATH);
        _roofPathPoints = new GameObject[_roofPathPointParent.transform.childCount];
        for (int i = 0; i < _roofPathPointParent.transform.childCount; i++)
        {
            _roofPathPoints[i] = _roofPathPointParent.transform.GetChild(i).gameObject;
        }

        _climbRoofRotateParent = UtilFunction.ResourceLoad(ROOF_ROTATE_PATH);
        _climbRoofRotatePoints = new GameObject[_climbRoofRotateParent.transform.childCount];
        for (int i = 0; i < _climbRoofRotateParent.transform.childCount; i++)
        {
            _climbRoofRotatePoints[i] = _climbRoofRotateParent.transform.GetChild(i).gameObject;
        }

        LoadRotatePath();

        _agent.speed = _walkSpeed;
        _agent.angularSpeed = 1000;

        SetState(FS_STATE.IDLE);
    }

    public void FirstAppear()
    {
        _isStart = true;
    }

    public void OnHurt(float damage)
    {
        if (_isFirstAppear)
            return;
        if (_isDead)
            return;
        _curHp -= damage;
        if (_curHp <= 0)
        {
            _curHp = 0;
            _isDead = true;
            SetState(FS_STATE.DEATH);
        }
    }

    public void SetWalkType(WALK_TYPE type)
    {
        if (type == _curWalkType)
            return;
        _curWalkType = type;
    }

    public void SetState(FS_STATE state)
    {
        if (state == _curState)
            return;
        _curState = state;
        _agent.enabled = true;
        ResetAnimator();

        switch (_curState)
        {
            case FS_STATE.IDLE:
                _agent.enabled = false;
                _anim.SetBool("isIdleBool", true);
                break;
            case FS_STATE.WALK:
                _agent.speed = _walkSpeed;
                transform.Find("FS").rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0);
                _anim.SetBool("isWalkBool", true);
                break;
            case FS_STATE.RUN:
                _agent.speed = _runSpeed;
                _anim.SetBool("isRunBool", true);
                break;
            case FS_STATE.JUMP:
                _agent.enabled = false;
                _anim.SetBool("isJumpBool", true);
                break;
            case FS_STATE.TURN_LEFT:
                _agent.enabled = false;
                _anim.SetBool("isTurnLeftBool", true);
                break;
            case FS_STATE.TURN_RIGHT:
                _agent.enabled = false;
                //transform.DOLocalRotate(new Vector3(transform.localEulerAngles.x, 90, 0), 2);
                _anim.SetBool("isWalkBool", true);
                break;
            case FS_STATE.IDLE_WALL:
                _agent.enabled = false;
                _anim.SetBool("isIdleBool", true);
                break;
            case FS_STATE.CLIMB_WALL:
                _agent.enabled = false;
                //int f = (int)transform.eulerAngles.x;
                //Debug.Log(f);
                //transform.DOLocalRotate(new Vector3(f - 90f, 0, 0), 3);
                _anim.SetBool("isClimbWalkBool", true);
                break;
            case FS_STATE.WALK_WALL:
                _agent.enabled = false;
                switch (_walkToPointIndex)
                {
                    case 0:
                        transform.DOPath(_dtRoofRotatePath1, 2f).plugOptions.orientType = DG.Tweening.Plugins.Options.OrientType.ToPath;
                        break;
                    case 1:
                        transform.DOPath(_dtRoofRotatePath2, 2f).plugOptions.orientType = DG.Tweening.Plugins.Options.OrientType.ToPath;
                        break;
                    case 2:
                        transform.DOPath(_dtRoofRotatePath3, 2f).plugOptions.orientType = DG.Tweening.Plugins.Options.OrientType.ToPath;
                        break;
                    case 3:
                        transform.DOPath(_dtRoofRotatePath4, 2f).plugOptions.orientType = DG.Tweening.Plugins.Options.OrientType.ToPath;
                        break;
                    case 4:
                        transform.DOPath(_dtRoofRotatePath5, 2f).plugOptions.orientType = DG.Tweening.Plugins.Options.OrientType.ToPath;
                        break;
                    case 5:
                        transform.DOPath(_dtRoofRotatePath6, 2f).plugOptions.orientType = DG.Tweening.Plugins.Options.OrientType.ToPath;
                        break;
                    case 6:
                        transform.DOPath(_dtRoofRotatePath7, 2f).plugOptions.orientType = DG.Tweening.Plugins.Options.OrientType.ToPath;
                        break;
                }
                _anim.SetBool("isWalkBool", true);
                break;
            case FS_STATE.WALK_ROOF:
                _agent.enabled = false;
                _anim.SetBool("isWalkBool", true);
                break;
            case FS_STATE.WALK_TO_POINT:
                _agent.speed = _walkSpeed;
                _anim.SetBool("isWalkBool", true);
                break;
            case FS_STATE.TURN_TO_POINT:
                transform.DORotate(_targetPoint.transform.eulerAngles, 0.5f);
                _agent.enabled = false;
                _anim.SetBool("isWalkBool", true);
                break;
            case FS_STATE.TO_PATH_ROTATE:
                _agent.enabled = false;
                _anim.SetBool("isWalkBool", true);
                break;
            case FS_STATE.JUMP_TO_WALL:
                _agent.enabled = false;
                transform.DORotateQuaternion(_climbRotatePoints[_walkToPointIndex].transform.localRotation, 0.5f);
                //transform.rotation = _climbRotatePoints[_walkToPointIndex].transform.localRotation;
                _anim.SetBool("isIdleBool", true);
                break;
            case FS_STATE.JUMP_TO_GROUND:
                _agent.enabled = false;
                transform.DORotate(Vector3.zero, 0.4f);
                transform.DOMove(new Vector3(-11.952f, 46.26f, 24.965f), 0.5f);
                _anim.SetBool("isIdleBool", true);
                break;
            case FS_STATE.FLEE:
                _agent.speed = _walkSpeed + 1;
                _anim.SetBool("isWalkBool", true);
                break;
            case FS_STATE.FLEE_TO_WALL:
                _agent.enabled = false;
                _anim.SetBool("isWalkBool", true);
                transform.DOPath(_fleePoistion, 4f).plugOptions.orientType = DG.Tweening.Plugins.Options.OrientType.ToPath;
                break;
            case FS_STATE.FLEE_IDLE:
                _agent.enabled = false;
                _anim.SetBool("isIdleBool", true);
                transform.position = new Vector3(-12.306f, 46.27104f, 25.029f);
                break;
            case FS_STATE.ROAR:
                _agent.enabled = false;
                _anim.SetBool("isRoarBool", true);
                break;
            case FS_STATE.GRAB:
                _agent.enabled = false;
                transform.DOLookAt(_target.transform.position, 0.3f);
                _anim.SetTrigger("isGrabTrigger");
                break;
            case FS_STATE.LASH:
                _agent.enabled = false;
                transform.DOLookAt(_target.transform.position, 0.3f);
                _anim.SetTrigger("isLashTrigger");
                break;
            case FS_STATE.GRAB_THROW:
                transform.DOLookAt(_target.transform.position, 0.3f);
                _agent.enabled = false;
                _anim.SetTrigger("isGrabThrowTrigger");
                break;
            case FS_STATE.PRESS:
                _agent.enabled = false;
                transform.DOLookAt(_target.transform.position, 0.3f);
                _anim.SetTrigger("isPressTrigger");
                break;
            case FS_STATE.BITE:
                _agent.enabled = false;
                transform.DOLookAt(_target.transform.position, 0.3f);
                _anim.SetTrigger("isBiteTrigger");
                break;
            case FS_STATE.STRIKE:
                _agent.enabled = false;
                transform.DOLookAt(_target.transform.position, 0.3f);
                _anim.SetTrigger("isStrikeTrigger");
                break;
            case FS_STATE.CYCLON:
                _agent.enabled = false;
                _anim.SetTrigger("isCyclonTrigger");
                break;
            case FS_STATE.CYCLON_FAST:
                _agent.enabled = false;
                _anim.SetTrigger("isCyclonFastTrigger");
                break;
            case FS_STATE.CANNON:
                _agent.enabled = false;
                Vector3 pos = _target.transform.position;
                pos.y = transform.position.y;
                transform.LookAt(pos);

                if(_curWalkType == WALK_TYPE.ROOF || _curWalkType == WALK_TYPE.WALL)
                    transform.Find("FS").rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 180);

                Vector3 posCannon = _target.transform.position;
                posCannon.y += 1;
                _effectManager.PlayEffect(FSBulletEffectManager.INST_EFFECT_TYPE.CANNON, posCannon, 2);
                _anim.SetTrigger("isCannonTrigger");
                break;
            case FS_STATE.BLAST_WAVE:
                _agent.enabled = false;
                Vector3 posWave = _target.transform.position;
                posWave.y = transform.position.y;
                transform.LookAt(posWave);

                if (_curWalkType == WALK_TYPE.ROOF || _curWalkType == WALK_TYPE.WALL)
                    transform.Find("FS").rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 180);

                Vector3 posBlast = _target.transform.position;
                posBlast.y += 1;
                _effectManager.PlayEffect(FSBulletEffectManager.INST_EFFECT_TYPE.BLAST_WAVE, posBlast, 2);
                _anim.SetTrigger("isBlastWaveTrigger");
                break;
            case FS_STATE.HURT:
                _agent.enabled = false;
                _anim.SetTrigger("isHurtTrigger");
                break;
            case FS_STATE.REBORN:
                _agent.enabled = false;
                _anim.SetTrigger("isRebornTrigger");
                break;
            case FS_STATE.DEATH:
                _agent.enabled = false;
                _anim.SetTrigger("isDeathTrigger");
                break;
        }
    }

    private void ResetAnimator()
    {
        _anim.SetBool("isIdleBool", false);
        _anim.SetBool("isWalkBool", false);
        _anim.SetBool("isRunBool", false);
        _anim.SetBool("isJumpBool", false);
        _anim.SetBool("isTurnLeftBool", false);
        _anim.SetBool("isTurnRightBool", false);
        _anim.SetBool("isRoarBool", false);
        _anim.SetBool("isClimbWalkBool", false);
    }

    private void UpdateState()
    {
        if (_isDead)
            return;

        switch (_curState)
        {
            case FS_STATE.IDLE:
                DoIdle();
                break;
            case FS_STATE.WALK:
                DoWalk();
                break;
            case FS_STATE.RUN:
                DoRun();
                break;
            case FS_STATE.JUMP:
                DoJump();
                break;
            case FS_STATE.TURN_LEFT:
                DoTurnLeft();
                break;
            case FS_STATE.TURN_RIGHT:
                DoTurnRight();
                break;
            case FS_STATE.IDLE_WALL:
                DoIdleWall();
                break;
            case FS_STATE.CLIMB_WALL:
                DoClimbWall();
                break;
            case FS_STATE.WALK_WALL:
                DoWalkWall();
                break;
            case FS_STATE.WALK_ROOF:
                DoWalkRoof();
                break;
            case FS_STATE.WALK_TO_POINT:
                DoWalkToPoint();
                break;
            case FS_STATE.TURN_TO_POINT:
                DoTurnToPoint();
                break;
            case FS_STATE.TO_PATH_ROTATE:
                DoPathRoate();
                break;
            case FS_STATE.JUMP_TO_WALL:
                DoJumpToWall();
                break;
            case FS_STATE.JUMP_TO_GROUND:
                DoJumpToGround();
                break;
            case FS_STATE.FLEE:
                DoFlee();
                break;
            case FS_STATE.FLEE_TO_WALL:
                DoFleeToWall();
                break;
            case FS_STATE.FLEE_IDLE:
                DoFleeIdle();
                break;
            case FS_STATE.ROAR:
                DoRoar();
                break;
            case FS_STATE.GRAB:
                DoSkill("grab");
                break;
            case FS_STATE.LASH:
                DoSkill("lash");
                break;
            case FS_STATE.GRAB_THROW:
                DoSkill("grab_throw");
                break;
            case FS_STATE.PRESS:
                DoSkill("press");
                break;
            case FS_STATE.BITE:
                DoSkill("bite");
                break;
            case FS_STATE.STRIKE:
                DoSkill("strike");
                break;
            case FS_STATE.CYCLON:
                DoSkill("cyclon");
                break;
            case FS_STATE.CYCLON_FAST:
                DoSkill("cyclon_fast");
                break;
            case FS_STATE.CANNON:
                DoCannon();
                break;
            case FS_STATE.BLAST_WAVE:
                DoBlastWave();
                break;
            case FS_STATE.HURT:
                DoHurt();
                break;
            case FS_STATE.REBORN:
                DoReborn();
                break;
            case FS_STATE.DEATH:
                DoDeath();
                break;
        }
    }

    private void DoIdle()
    {
        if (!_isStart)
            return;

        SetWalkType(WALK_TYPE.GRAOUND);
        transform.Find("FS").rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        _timer += Time.deltaTime;
        if (_timer <= 0.5f)
            return;
        _timer = 0f;
        float dist = Vector3.Distance(transform.position, _target.transform.position);
        if (dist <= _shortRange)
        {
            int ranIndex = 0;
            if (_isFirstAppear) //是否第一次出现
                ranIndex = Random.Range(0, 8);
            else
                ranIndex = Random.Range(0, 15);

            switch (ranIndex)
            {
                case 0:
                    SetState(FS_STATE.BITE);
                    break;
                case 1:
                    SetState(FS_STATE.CYCLON);
                    break;
                case 2:
                    SetState(FS_STATE.CYCLON_FAST);
                    break;
                case 3:
                    SetState(FS_STATE.GRAB);
                    break;
                case 4:
                    SetState(FS_STATE.GRAB_THROW);
                    break;
                case 5:
                    SetState(FS_STATE.LASH);
                    break;
                case 6:
                    SetState(FS_STATE.PRESS);
                    break;
                case 7:
                    SetState(FS_STATE.STRIKE);
                    break;
                case 8:
                    _walkToPointIndex = Random.Range(1, _climbPoints.Length);

                    _targetPoint = _climbPoints[_walkToPointIndex];
                    SetState(FS_STATE.WALK_TO_POINT);
                    break;
                case 9:
                    _walkToPointIndex = Random.Range(1, _climbPoints.Length);

                    _targetPoint = _climbPoints[_walkToPointIndex];
                    SetState(FS_STATE.WALK_TO_POINT);
                    break;
                case 10:
                    _walkToPointIndex = Random.Range(1, _climbPoints.Length);

                    _targetPoint = _climbPoints[_walkToPointIndex];
                    SetState(FS_STATE.WALK_TO_POINT);
                    break;
                case 11:
                    _walkToPointIndex = Random.Range(1, _climbPoints.Length);

                    _targetPoint = _climbPoints[_walkToPointIndex];
                    SetState(FS_STATE.WALK_TO_POINT);
                    break;
                case 12:
                    _walkToPointIndex = Random.Range(1, _climbPoints.Length);

                    _targetPoint = _climbPoints[_walkToPointIndex];
                    SetState(FS_STATE.WALK_TO_POINT);
                    break;
                case 13:
                    _walkToPointIndex = Random.Range(1, _climbPoints.Length);

                    _targetPoint = _climbPoints[_walkToPointIndex];
                    SetState(FS_STATE.WALK_TO_POINT);
                    break;
                case 14:
                    _walkToPointIndex = Random.Range(1, _climbPoints.Length);

                    _targetPoint = _climbPoints[_walkToPointIndex];
                    SetState(FS_STATE.WALK_TO_POINT);
                    break;
            }
        }
        else if (dist <= 7)
        {
            SetState(FS_STATE.WALK);
        }
        else
        {
            int ranIndex = Random.Range(2, 4);
            switch (ranIndex)
            {
                case 0:
                    SetState(FS_STATE.CANNON);
                    break;
                case 1:
                    SetState(FS_STATE.BLAST_WAVE);
                    break;
                case 2:
                    SetState(FS_STATE.RUN);
                    break;
                case 3:
                    SetState(FS_STATE.ROAR);
                    break;
            }
        }

    }

    private void DoWalk()
    {
        _agent.SetDestination(_target.transform.position);

        _timer += Time.deltaTime;
        if (_timer >= 3)
        {
            _timer = 0f;
            SetState(FS_STATE.RUN);
        }

        //if (!_agent.hasPath)
        //{
        //    _timer = 0f;
        //    float dist1 = Vector3.Distance(transform.position, _climbPoints[0].transform.position);
        //    float dist2 = Vector3.Distance(transform.position, _climbPoints[1].transform.position);
        //    if (dist1 < dist2)
        //    {
        //        _targetPoint = _climbPoints[0];
        //    }
        //    else
        //    {
        //        _targetPoint = _climbPoints[1];
        //    }
        //    SetState(FS_STATE.WALK_TO_POINT);
        //}

        float dist = Vector3.Distance(transform.position, _target.transform.position);
        if (dist <= _shortRange)
        {
            _timer = 0f;
            SetState(FS_STATE.IDLE);
        }
    }

    private void DoRun()
    {
        _agent.SetDestination(_target.transform.position);

        _timer += Time.deltaTime;
        if (_timer >= 4)
        {
            _timer = 0f;
            RaycastHit hit;
            Ray ray = new Ray(_rayPoint.transform.position, _target.transform.position - transform.position);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Player")
                {
                    SetState(FS_STATE.ROAR);
                }
            }
        }

        //if (!_agent.hasPath)
        //{
        //    _timer = 0f;
        //    float dist1 = Vector3.Distance(transform.position, _climbPoints[0].transform.position);
        //    float dist2 = Vector3.Distance(transform.position, _climbPoints[1].transform.position);
        //    if (dist1 < dist2)
        //    {
        //        _targetPoint = _climbPoints[0];
        //    }
        //    else
        //    {
        //        _targetPoint = _climbPoints[1];
        //    }
        //    SetState(FS_STATE.WALK_TO_POINT);
        //}

        float dist = Vector3.Distance(transform.position, _target.transform.position);
        if (dist <= _shortRange)
        {
            _timer = 0f;
            SetState(FS_STATE.IDLE);
        }
    }

    private void DoJump()
    {
        _timer += Time.deltaTime;
        if(_timer >= 0.5f)
        {
            _timer = 0f;
            //_isCanTurn = true;
            SetState(FS_STATE.IDLE);
        }
    }

    private void DoTurnLeft()
    {

    }

    private void DoTurnRight()
    {
        _timer += Time.deltaTime;
        if (_timer >= 1.5f)
        {
            _timer = 0f;
            SetState(FS_STATE.WALK_WALL);
        }

        transform.Rotate(Vector3.up, 1f);
    }

    //private bool _isCanTurn = true;
    //private bool _isRoofWall;
    private void DoIdleWall()
    {
        transform.Find("FS").rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 180);
        _timer += Time.deltaTime;
        if (_timer <= 0.5f)
            return;
        _timer = 0f;
        
        RaycastHit hit;
        Ray ray = new Ray(_rayPoint.transform.position, _target.transform.position - _rayPoint.transform.position);
        Debug.DrawRay(_rayPoint.transform.position, _target.transform.position - _rayPoint.transform.position);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.name == "ThirdPersonController")
            {
                int ranIndex = Random.Range(0, 2);
                switch (ranIndex)
                {
                    case 0:
                        SetState(FS_STATE.CANNON);
                        break;
                    case 1:
                        SetState(FS_STATE.BLAST_WAVE);
                        break;
                }
            }
            else
            {
                SetState(FS_STATE.WALK_ROOF);
            }
        }

    }

    private void DoClimbWall()
    {
        _timer += Time.deltaTime;


        //transform.Translate(Vector3.forward * _walkSpeed * 0.5f * Time.deltaTime, Space.Self);
        if (_timer >= 2.1f)
        {
            _timer = 0f;
            //    switch (_curWalkType)
            //    {
            //        case WALK_TYPE.GRAOUND:
            //            break;
            //        case WALK_TYPE.WALL:
            //            SetState(FS_STATE.IDLE_WALL);
            //            return;
            //        case WALK_TYPE.ROOF:
            //            break;
            //    }
            //    //SetState(FS_STATE.WALK_WALL);
            SetState(FS_STATE.IDLE_WALL);
            //    SetWalkType(WALK_TYPE.WALL);
        }
    }

    private void DoWalkWall()
    {
        _timer += Time.deltaTime;
        if (_timer >= 2f)
        {
            _timer = 0f;
            SetState(FS_STATE.IDLE_WALL);
            SetWalkType(WALK_TYPE.ROOF);
        }

        //RaycastHit hit;
        //Ray ray = new Ray(_rayPoint.transform.position, transform.forward);
        //Debug.DrawRay(_rayPoint.transform.position, transform.forward, Color.red, 1);
        //if (Physics.Raycast(ray, out hit, 2))
        //{
        //    Debug.Log(hit.transform.name);
        //    if (hit.transform.name.Contains("Wall"))
        //    {
        //        if (hit.transform.name.Equals("RoofWall"))
        //        {
        //            _isRoofWall = true;
        //        }
        //        _timer = 0f;
        //        //SetState(FS_STATE.CLIMB_WALL);
        //    }
        //}
    }

    private void DoWalkRoof()
    {
        Vector3 pos = _roofPathPoints[_roofPointIndex].transform.position;
        pos.y = transform.position.y;
        transform.LookAt(pos);
        transform.Find("FS").rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 180);


        transform.Translate(Vector3.forward * _walkSpeed * 1.5f * Time.deltaTime, Space.Self);

        float dist = Vector3.Distance(transform.position, pos);
        if (dist <= 0.3f)
        {
            if (_roofPointIndex < _roofPathPoints.Length - 1)
            {
                _roofPointIndex++;
            }
            else
            {
                _roofPointIndex = 0;
                //transform.position = _roofPathPoints[_roofPathPoints.Length - 1].transform.position;
                SetState(FS_STATE.JUMP_TO_GROUND);
                return;
            }

            SetState(FS_STATE.IDLE_WALL);
        }
    }

    private void DoWalkToPoint()
    {
        _agent.SetDestination(_targetPoint.transform.position);
        float dist = Vector3.Distance(transform.position, _targetPoint.transform.position);
        if (dist <= 0.2f)
        {
            SetState(FS_STATE.TURN_TO_POINT);
        }
    }

    private void DoTurnToPoint()
    {
        _timer += Time.deltaTime;
        if (_timer >= 0.5f)
        {
            _timer = 0f;
            //SetState(FS_STATE.WALK_WALL);

            switch (_walkToPointIndex)
            {
                case 0://+++
                    transform.DOPath(_dtRotatePath1, 2f).plugOptions.orientType = DG.Tweening.Plugins.Options.OrientType.ToPath;

                    SetState(FS_STATE.TO_PATH_ROTATE);
                    break;
                case 1://***
                    transform.DOPath(_dtRotatePath2, 1f, PathType.CatmullRom, PathMode.Full3D);
                    SetState(FS_STATE.JUMP_TO_WALL);
                    break;
                case 2://***
                    transform.DOPath(_dtRotatePath3, 1f, PathType.CatmullRom, PathMode.Full3D);
                    SetState(FS_STATE.JUMP_TO_WALL);
                    break;
                case 3://***
                    transform.DOPath(_dtRotatePath4, 1f, PathType.CatmullRom, PathMode.Full3D);
                    SetState(FS_STATE.JUMP_TO_WALL);
                    break;
                case 4://+++
                    transform.DOPath(_dtRotatePath5, 2f).plugOptions.orientType = DG.Tweening.Plugins.Options.OrientType.ToPath;
                    SetState(FS_STATE.TO_PATH_ROTATE);
                    break;
                case 5://***
                    transform.DOPath(_dtRotatePath6, 1f, PathType.CatmullRom, PathMode.Full3D);
                    SetState(FS_STATE.JUMP_TO_WALL);
                    break;
                case 6://+++
                    transform.DOPath(_dtRotatePath7, 2f).plugOptions.orientType = DG.Tweening.Plugins.Options.OrientType.ToPath;
                    SetState(FS_STATE.TO_PATH_ROTATE);
                    break;
            }
        }
    }

    private void DoPathRoate()
    {
        _timer += Time.deltaTime;
        if (_timer >= 2.1f)
        {
            _timer = 0f;
            SetState(FS_STATE.WALK_WALL);
        }
    }

    private void DoJumpToWall()
    {
        _timer += Time.deltaTime;
        if (_timer >= 1.1f)
        {
            _timer = 0f;
            SetState(FS_STATE.WALK_WALL);
        }
    }

    private void DoJumpToGround()
    {
        _timer += Time.deltaTime;
        if (_timer >= 0.5f)
        {
            _timer = 0f;
            SetState(FS_STATE.WALK);
        }
    }

    private void DoFlee()
    {
        _agent.SetDestination(_fleePoistion[0]);
        float dist = Vector3.Distance(transform.position, _fleePoistion[0]);
        if (dist <= 0.2f)
        {
            SetState(FS_STATE.FLEE_TO_WALL);
        }
    }

    private void DoFleeToWall()
    {
        _timer += Time.deltaTime;
        if (_timer >= 4f)
        {
            _timer = 0;
            SetState(FS_STATE.FLEE_IDLE);
        }
    }

    private void DoFleeIdle()
    {

    }

    private void DoRoar()
    {
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("rest_paoxiao") && info.normalizedTime >= 1)
        {
            int ranIndex = Random.Range(0, 2);
            switch (ranIndex)
            {
                case 0:
                    SetState(FS_STATE.CANNON);
                    break;
                case 1:
                    SetState(FS_STATE.BLAST_WAVE);
                    break;
            }
        }
    }

    private void DoSkill(string name)
    {
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName(name) && info.normalizedTime >= 1)
        {
            SetState(FS_STATE.IDLE);
        }
    }

    private void DoCannon()
    {
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("kaipao") && info.normalizedTime >= 1)
        {
            switch (_curWalkType)
            {
                case WALK_TYPE.GRAOUND:
                    SetState(FS_STATE.IDLE);
                    break;
                case WALK_TYPE.WALL:
                    SetState(FS_STATE.IDLE_WALL);
                    break;
                case WALK_TYPE.ROOF:
                    SetState(FS_STATE.WALK_ROOF);
                    break;
            }
        }
    }

    private void DoBlastWave()
    {
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("zuibu_fachuchongjibo") && info.normalizedTime >= 1)
        {
            switch (_curWalkType)
            {
                case WALK_TYPE.GRAOUND:
                    SetState(FS_STATE.IDLE);
                    break;
                case WALK_TYPE.WALL:
                    SetState(FS_STATE.IDLE_WALL);
                    break;
                case WALK_TYPE.ROOF:
                    SetState(FS_STATE.WALK_ROOF);
                    break;
            }
        }
    }

    private void DoHurt()
    {
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("beaten_R") && info.normalizedTime >= 1)
        {
            SetState(FS_STATE.IDLE);
        }
    }

    private void DoReborn()
    {
        //AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        //if (info.IsName("rest_paoxiao") && info.normalizedTime >= 1)
        //{

        //}
    }

    private void DoDeath()
    {
        //AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        //if (info.IsName("rest_paoxiao") && info.normalizedTime >= 1)
        //{

        //}
    }

    private void LoadRotatePath()
    {
        _dtRotatePath1 = new Vector3[_climbRotatePoints[0].transform.childCount];
        _dtRotatePath2 = new Vector3[_climbRotatePoints[1].transform.childCount];
        _dtRotatePath3 = new Vector3[_climbRotatePoints[2].transform.childCount];
        _dtRotatePath4 = new Vector3[_climbRotatePoints[3].transform.childCount];
        _dtRotatePath5 = new Vector3[_climbRotatePoints[4].transform.childCount];
        _dtRotatePath6 = new Vector3[_climbRotatePoints[5].transform.childCount];
        _dtRotatePath7 = new Vector3[_climbRotatePoints[6].transform.childCount];

        _dtRoofRotatePath1 = new Vector3[_climbRoofRotatePoints[0].transform.childCount];
        _dtRoofRotatePath2 = new Vector3[_climbRoofRotatePoints[1].transform.childCount];
        _dtRoofRotatePath3 = new Vector3[_climbRoofRotatePoints[2].transform.childCount];
        _dtRoofRotatePath4 = new Vector3[_climbRoofRotatePoints[3].transform.childCount];
        _dtRoofRotatePath5 = new Vector3[_climbRoofRotatePoints[4].transform.childCount];
        _dtRoofRotatePath6 = new Vector3[_climbRoofRotatePoints[5].transform.childCount];
        _dtRoofRotatePath7 = new Vector3[_climbRoofRotatePoints[6].transform.childCount];
        for (int i = 0; i < _climbRotatePoints[0].transform.childCount; i++)
        {
            _dtRotatePath1[i] = _climbRotatePoints[0].transform.GetChild(i).position;
            _dtRotatePath2[i] = _climbRotatePoints[1].transform.GetChild(i).position;
            _dtRotatePath3[i] = _climbRotatePoints[2].transform.GetChild(i).position;
            _dtRotatePath4[i] = _climbRotatePoints[3].transform.GetChild(i).position;
            _dtRotatePath5[i] = _climbRotatePoints[4].transform.GetChild(i).position;
            _dtRotatePath6[i] = _climbRotatePoints[5].transform.GetChild(i).position;
            _dtRotatePath7[i] = _climbRotatePoints[6].transform.GetChild(i).position;

            _dtRoofRotatePath1[i] = _climbRoofRotatePoints[0].transform.GetChild(i).position;
            _dtRoofRotatePath2[i] = _climbRoofRotatePoints[1].transform.GetChild(i).position;
            _dtRoofRotatePath3[i] = _climbRoofRotatePoints[2].transform.GetChild(i).position;
            _dtRoofRotatePath4[i] = _climbRoofRotatePoints[3].transform.GetChild(i).position;
            _dtRoofRotatePath5[i] = _climbRoofRotatePoints[4].transform.GetChild(i).position;
            _dtRoofRotatePath6[i] = _climbRoofRotatePoints[5].transform.GetChild(i).position;
            _dtRoofRotatePath7[i] = _climbRoofRotatePoints[6].transform.GetChild(i).position;
        }
    }

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        //test
        //Init();
        _target = GameObject.Find("ThirdPersonController");
        FirstAppear();
    }

    private float _timerFlee = 0f;
    private void Update()
    {
        UpdateState();

        if (_isFirstAppear)
        {
            if (_isStart)
            {

                _timerFlee += Time.deltaTime;
                if (_timerFlee > 10f)
                {
                    _isFirstAppear = false;
                    _timerFlee = 0f;
                    SetState(FS_STATE.FLEE);

                    Level_10_Manager.Instance.FS_1();
                }
            }
        }
    }

    #region 旧技能

    //private void DoGrab()
    //{
    //    AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
    //    if (info.IsName("attack2") && info.normalizedTime >= 1)
    //    {
    //        SetState(FS_STATE.IDLE);
    //    }
    //}

    //private void DoLash()
    //{
    //    AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
    //    if (info.IsName("attack8") && info.normalizedTime >= 1)
    //    {
    //        SetState(FS_STATE.IDLE);
    //    }
    //}

    //private void DoGrabThrow()
    //{
    //    AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
    //    if (info.IsName("attack6") && info.normalizedTime >= 1)
    //    {

    //    }
    //}

    //private void DoPress()
    //{
    //    AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
    //    if (info.IsName("attack9") && info.normalizedTime >= 1)
    //    {

    //    }
    //}

    //private void DoBite()
    //{
    //    AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
    //    if (info.IsName("attack3") && info.normalizedTime >= 1)
    //    {

    //    }
    //}

    //private void DoStrike()
    //{
    //    AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
    //    if (info.IsName("attack5") && info.normalizedTime >= 1)
    //    {

    //    }
    //}

    //private void DoCyclon()
    //{
    //    AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
    //    if (info.IsName("attack10") && info.normalizedTime >= 1)
    //    {

    //    }
    //}

    //private void DoCyclonFast()
    //{
    //    AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
    //    if (info.IsName("attack4") && info.normalizedTime >= 1)
    //    {

    //    }
    //}

    #endregion
}
