//MelissaController.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/23/2017 4:08 PM
//Description: Melissa¿ØÖÆ
//

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using DG.Tweening;

public class MelissaController : MonoBehaviour
{
    private const string PIPELINE_PREFAB_PATH = "Prefabs/Character/NPCs/Melissa/MelissaPath";
    private Vector3[] paths = new Vector3[3];
    private const int MAX_HP = 100;
    public int curHp = MAX_HP;
    private float creepSpeed = 1f;
    private float _timer = 0f;
    private float _walkSpeed = 1f;
    private float _runSpeed = 3f;

    private Vector3 _curTargetPoint;
    private Vector3 _previousPoint; //Ç°Ò»¸öµã
    private GameObject _pipelinePathPrefab;
    private GameObject[] _pipelinePoints;

    private GameObject _target;

    private bool _isCanSkipStep;
    public bool _isCanCreep;

    private bool _isDead;
    public bool isDead
    {
        get { return _isDead; }
    }

    private Animator _anim;
    private NavMeshAgent _agent;

    public STATE curState;
    public STATE lastState;

    public enum STATE
    {
        EMPTY,
        IDLE,
        WALK,
        FOLLOW,
        FOLLOW_IDLE,
        CREEP,
        CLIMB,//ÏòÉÏÅÀ
        WALK_TO_POINT,
        LADDER_UP,
        LADDER_DOWN,
        WAIT_PLAYER,
        RUN,
        CROUCH,
        JUMP,
        LOOK,
        OPEN_DOOR,
        TALK,
        OPEN_HAND,
        PA,
        DEAD,
    }

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();

    }

    public void Init()
    {
        SetState(STATE.IDLE);

        _pipelinePathPrefab = UtilFunction.ResourceLoad(PIPELINE_PREFAB_PATH);
        _pipelinePoints = new GameObject[_pipelinePathPrefab.transform.childCount];
        for (int i = 0; i < _pipelinePathPrefab.transform.childCount; i++)
        {
            _pipelinePoints[i] = _pipelinePathPrefab.transform.GetChild(i).gameObject;
        }

        _target = Level_10_Manager.Instance.playerGO;
    }

    public void SetCanSkipStep(bool b)
    {
        _isCanSkipStep = b;
    }

    public void SetToCreep()
    {
        if (_isCanCreep)
        {
            _isCanCreep = false;
            transform.position = new Vector3(transform.position.x, 48f, transform.position.z);
        }
    }

    public void SetState(STATE state)
    {
        if (_isDead)
            return;

        if (state == curState)
            return;
        curState = state;

        ResetAnimator();
        _agent.enabled = true;

        switch (curState)
        {
            case STATE.EMPTY:
                _agent.enabled = false;
                _anim.SetBool("isIdleBool", true);
                break;
            case STATE.IDLE:
                _agent.isStopped = true;
                _anim.SetBool("isIdleBool", true);
                break;
            case STATE.WALK:
                _agent.isStopped = false;
                _agent.speed = _walkSpeed;
                _anim.SetBool("isWalkBool", true);
                break;
            case STATE.FOLLOW:
                _agent.isStopped = false;
                _anim.SetBool("isWalkBool", true);
                break;
            case STATE.FOLLOW_IDLE:
                _agent.isStopped = true;
                _anim.SetBool("isIdleBool", true);
                break;
            case STATE.CREEP:
                _agent.enabled = false;
                lastState = STATE.CREEP;
                _anim.SetBool("isCreepBool", true);
                break;
            case STATE.CLIMB:
                _agent.enabled = false;
                transform.DOMove(_pipelinePoints[index + 1].transform.position, 1.2f);
                Debug.Log("<color=blue>" + _pipelinePoints[index].transform.name + "</color>");
                _anim.SetBool("isClimbBool", true);
                break;
            case STATE.WALK_TO_POINT:
                _agent.speed = _walkSpeed;
                lastState = STATE.WALK_TO_POINT;
                _anim.SetBool("isWalkBool", true);
                break;
            case STATE.LADDER_UP:
                _agent.enabled = false;
                transform.DOMove(_pipelinePoints[index + 1].transform.position, 2);
                _anim.SetBool("isIdleBool", true);
                break;
            case STATE.LADDER_DOWN:
                _agent.enabled = false;
                transform.DOMove(_pipelinePoints[index + 1].transform.position, 2);
                _anim.SetBool("isIdleBool", true);
                break;
            case STATE.WAIT_PLAYER:
                _agent.enabled = false;
                switch (lastState)
                {
                    case STATE.EMPTY:
                        break;
                    case STATE.IDLE:
                        break;
                    case STATE.WALK:
                        break;
                    case STATE.FOLLOW:
                        break;
                    case STATE.FOLLOW_IDLE:
                        break;
                    case STATE.CREEP:
                        _anim.SetBool("isCreepIdleBool", true);
                        break;
                    case STATE.CLIMB:
                        break;
                    case STATE.WALK_TO_POINT:
                        _anim.SetBool("isIdleBool", true);
                        break;
                    case STATE.LADDER_UP:
                        break;
                    case STATE.LADDER_DOWN:
                        break;
                    case STATE.WAIT_PLAYER:
                        break;
                    case STATE.RUN:
                        break;
                    case STATE.CROUCH:
                        break;
                    case STATE.JUMP:
                        break;
                    case STATE.LOOK:
                        break;
                    case STATE.OPEN_DOOR:
                        break;
                    case STATE.TALK:
                        break;
                    case STATE.OPEN_HAND:
                        break;
                    case STATE.PA:
                        break;
                    case STATE.DEAD:
                        break;
                    default:
                        break;
                }
                break;
            case STATE.RUN:
                _agent.isStopped = false;
                _agent.speed = _runSpeed;
                _anim.SetBool("isRunBool", true);
                break;
            case STATE.CROUCH:
                _agent.isStopped = true;
                _anim.SetBool("isCrouchBool", true);
                break;
            case STATE.JUMP:
                _agent.enabled = false;
                transform.DOMove(_pipelinePoints[index + 1].transform.position, 1f);
                _anim.SetBool("isIdleBool", true);
                break;
            case STATE.LOOK:
                _agent.isStopped = true;
                //_anim.SetBool("isLookBool", true);
                break;
            case STATE.OPEN_DOOR:
                _agent.isStopped = true;
                //_anim.SetBool("isOpenDoorBool", true);
                break;
            case STATE.TALK:
                _agent.isStopped = true;
                //_anim.SetBool("isTalkBool", true);
                _anim.SetBool("isIdleBool", true);
                break;
            case STATE.OPEN_HAND:
                _agent.enabled = false;
                _anim.SetBool("isBaoBool", true);
                break;
            case STATE.PA:
                _anim.SetBool("isTestBool", true);
                break;
            case STATE.DEAD:
                _agent.isStopped = true;
                //_anim.SetBool("isDeadBool", true);
                break;
        }
    }

    public void OnHurt(int damage)
    {
        if (_isDead)
            return;

        curHp -= damage;
        if (curHp <= 0)
        {
            curHp = 0;
            _isDead = true;
            SetState(STATE.DEAD);
        }
    }

    public void SetTargetPoint(Vector3 v)
    {
        if (v == Vector3.zero)
            return;

        _curTargetPoint = v;
    }

    private void UpdateState()
    {
        if (_isDead)
            return;

        switch (curState)
        {
            case STATE.IDLE:
                DoIdle();
                break;
            case STATE.WALK:
                DoWalk();
                break;
            case STATE.FOLLOW:
                DoFollow();
                break;
            case STATE.FOLLOW_IDLE:
                DoFollowIdle();
                break;
            case STATE.CREEP:
                DoCreep();
                break;
            case STATE.CLIMB:
                DoClimb();
                break;
            case STATE.WALK_TO_POINT:
                DoWalkToPoint();
                break;
            case STATE.LADDER_UP:
                DoLadderUp();
                break;
            case STATE.LADDER_DOWN:
                DoLadderDown();
                break;
            case STATE.WAIT_PLAYER:
                DoWaitPlayer();
                break;
            case STATE.RUN:
                DoRun();
                break;
            case STATE.CROUCH:
                DoCrouch();
                break;
            case STATE.JUMP:
                DoJump();
                break;
            case STATE.LOOK:
                DoLook();
                break;
            case STATE.OPEN_DOOR:
                DoOpenDoor();
                break;
            case STATE.TALK:
                DoTalk();
                break;
            case STATE.OPEN_HAND:
                //DoOpenHand();
                break;
            case STATE.PA:
                DoPa();
                break;
        }
    }

    private void DoIdle()
    {

    }

    private void DoWalk()
    {
        _agent.SetDestination(_curTargetPoint);
        float dist = Vector3.Distance(transform.position, _curTargetPoint);
        if (dist <= 0.2f)
        {
            if (_isCanSkipStep)
            {
                SetState(STATE.EMPTY);
                MelissaManager.Instance.FinishCurTaskImmediately();
            }
            else
            {
                SetState(STATE.EMPTY);
            }
        }
    }

    private void DoFollow()
    {
        _agent.SetDestination(_curTargetPoint);
        Vector3 pos = _curTargetPoint;
        pos.y = transform.position.y;
        float dist = Vector3.Distance(transform.position, pos);
        if (dist <= 1f)
        {
            SetState(STATE.FOLLOW_IDLE);
        }
    }

    private void DoFollowIdle()
    {
        Vector3 pos = _curTargetPoint;
        pos.y = transform.position.y;
        float dist = Vector3.Distance(transform.position, pos);
        if (dist >= 1f)
        {
            SetState(STATE.FOLLOW);
        }
    }

    private int index = 0;
    private void DoCreep()
    {
        transform.Translate(Vector3.forward * creepSpeed * Time.deltaTime, Space.Self);
        transform.LookAt(_pipelinePoints[index].transform.position);

        Vector3 pos = _target.transform.position;
        pos.y = transform.position.y;
        float distTarget = Vector3.Distance(transform.position, pos);
        if (distTarget >= 3)
        {
            SetState(STATE.WAIT_PLAYER);
            return;
        }

        float dist = Vector3.Distance(transform.position, _pipelinePoints[index].transform.position);
        if (dist <= 0.1f)
        {
            Debug.Log("<color=green>" + _pipelinePoints[index].transform.name + "</color>");
            CheckPoint();
        }
    }

    private void DoClimb()
    {
        float dist = Vector3.Distance(transform.position, _pipelinePoints[index].transform.position);
        if (dist <= 0.1f)
        {
            CheckPoint();
        }
        //_timer += Time.deltaTime;
        //if (_timer >= 1.3f)
        //{
        //    _timer = 0f;
        //    CheckPoint();
        //}
    }

    private void DoWalkToPoint()
    {
        _agent.SetDestination(_pipelinePoints[index].transform.position);

        Vector3 pos = _target.transform.position;
        pos.y = transform.position.y;
        float distTarget = Vector3.Distance(transform.position, pos);
        if (distTarget >= 3)
        {
            SetState(STATE.WAIT_PLAYER);
            return;
        }

        float dist = Vector3.Distance(transform.position, _pipelinePoints[index].transform.position);
        if (dist <= 0.2f)
        {
            CheckPoint();
        }
    }

    private void DoLadderUp()
    {
        _timer += Time.deltaTime;
        if (_timer >= 2.1f)
        {
            _timer = 0f;
            CheckPoint();
        }
    }


    private void DoLadderDown()
    {
        _timer += Time.deltaTime;
        if (_timer >= 2.1f)
        {
            _timer = 0f;
            CheckPoint();
        }
    }

    private void DoWaitPlayer()
    {
        float dist = Vector3.Distance(transform.position, _target.transform.position);
        if (dist <= 2)
        {
            SetState(lastState);
            //lastState = STATE.EMPTY;
        }
    }

    private void DoRun()
    {
        _agent.SetDestination(_curTargetPoint);

        float dist = Vector3.Distance(transform.position, _curTargetPoint);
        if (dist <= 0.2f)
        {
            if (_isCanSkipStep)
            {
                SetState(STATE.EMPTY);
                MelissaManager.Instance.FinishCurTaskImmediately();
            }
            else
            {
                SetState(STATE.EMPTY);
            }
        }
    }

    private void DoCrouch()
    {
    }

    private void DoJump()
    {
        _timer += Time.deltaTime;
        if (_timer >= 1.1f)
        {
            _timer = 0f;
            CheckPoint();
        }
    }

    private void DoLook()
    {

    }

    private void DoOpenDoor()
    {

    }

    private void DoTalk()
    {
    }


    private void CheckPoint()
    {
        if (_pipelinePoints[index].name.Contains("climb"))
        {
            SetState(STATE.CLIMB);
        }
        else if (_pipelinePoints[index].name.Contains("stand"))
        {
            SetState(STATE.WALK_TO_POINT);
        }
        else if (_pipelinePoints[index].name.Contains("ladderUp"))
        {
            SetState(STATE.LADDER_UP);
        }
        else if (_pipelinePoints[index].name.Contains("ladderDown"))
        {
            SetState(STATE.LADDER_DOWN);
        }
        else if (_pipelinePoints[index].name.Contains("jump"))
        {
            SetState(STATE.JUMP);
        }
        else
        {
            SetState(STATE.CREEP);
        }

        if (index < _pipelinePoints.Length - 1) //56 55 
        {
            index++;
        }
        else
        {
            SetState(STATE.EMPTY);
            MelissaManager.Instance.FinishCurTaskImmediately();
        }
    }

    private void DoOpenHand()
    {
        paths[0] = transform.position;
        paths[1] = GameObject.Find("Pos(8)").transform.position;
        paths[2] = GameObject.Find("Pos(7)").transform.position;
        transform.rotation = Quaternion.Euler(0,-90,0);
        if (transform.position.y > 47)
        {
            transform.DOPath(paths, 2);
            SetState(STATE.CREEP);
        }
    }

    private void DoPa() {

    }
    private void ResetAnimator()
    {
        _anim.SetBool("isIdleBool", false);
        _anim.SetBool("isWalkBool", false);
        _anim.SetBool("isRunBool", false);
        _anim.SetBool("isCrouchBool", false);
        _anim.SetBool("isCreepBool", false);
        _anim.SetBool("isTestBool", false);
        _anim.SetBool("isBaoBool", false);
        _anim.SetBool("isClimbBool", false);
        _anim.SetBool("isCreepIdleBool", false);
        //_anim.SetBool("isJumpBool", false);
        //_anim.SetBool("isTalkBool", false);
        //_anim.SetBool("isDeadBool", false);
    }

    void Update()
    {
        UpdateState();
    }
}
