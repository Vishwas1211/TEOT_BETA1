//CiviliansController.cs
//TEOT_ONLINE
//
//Create by WangJie on 9/15/2017 10:27 AM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class CiviliansController : MonoBehaviour
{
    private const float MAX_HP = 100;
    private float _curHP = MAX_HP;
    [SerializeField]private float _moveSpeed = 3f;
    [SerializeField]private float _runSpeed = 6f;
    [SerializeField]private float _minArriveDist = 0.2f;
    [SerializeField]private float _minArriveTargetDist = 2f;

    private bool _isDead;
    private bool _isCanSkipStep;

    private Vector3 _targetPoint;

    private Transform _target;

    private Animator _anim;
    private NavMeshAgent _agent;

    public STATE curState;

    public enum STATE
    {
        IDLE,
        WALK,
        RUN,
        ELUDE,  //躲避
        CROUCH,
        WALK_TO, //向……走
        DEATH,
        TALK,
    }

    public void Init()
    {
        _agent.speed = _moveSpeed;
        SetState(STATE.IDLE);
        SetCanSkip(true);
    }

    public void SetCanSkip(bool b)
    {
        _isCanSkipStep = b;
    }

    public void OnHurt(float damage)
    {
        if (_isDead)
            return;
        _curHP -= damage;

        if (_curHP <= 0)
        {
            Level_20_Manager.Instance.SetFenZhi(Level_20_Manager.FenZhi.C);
            _curHP = 0;
            _isDead = true;
            SetState(STATE.DEATH);
        }
    }

    public void SetTargetPoint(Vector3 v)
    {
        if(v == Vector3.zero)
        {
            v = transform.position;
            //Debug.Log("-------------------位置设置有误");
            return;
        }
        _targetPoint = v;
    }

    public void SetState(STATE state)
    {
        if (state == curState)
            return;
        curState = state;

        ResetAnimator();
        _agent.enabled = true;

        switch (curState)
        {
                
            case STATE.IDLE:
                _agent.isStopped = true;
                _anim.SetBool("isIdleBool", true);
                break;
            case STATE.WALK:
                _agent.isStopped = false;
                _anim.SetBool("isWalkBool", true);
                _agent.speed = _moveSpeed;
                break;
            case STATE.RUN:
                _agent.isStopped = false;
                _anim.SetBool("isRunBool", true);
                _agent.speed = _runSpeed;
                break;
            case STATE.ELUDE:
                _agent.isStopped = true;
                _anim.SetBool("isIdleBool", true);
                break;
            case STATE.CROUCH:
                _agent.isStopped = true;
                _anim.SetBool("isCrouchBool", true);
                break;
            case STATE.WALK_TO:
                _agent.isStopped = false;
                _anim.SetBool("isWalkBool", true);
                break;
            case STATE.DEATH:
                _agent.isStopped = true;
                _anim.SetBool("isDeadBool", true);
                break;
            case STATE.TALK:
                _agent.isStopped = true;
                _anim.SetBool("isIdleBool", true);
                break;
        }
    }

    private void UpdateState()
    {
        switch (curState)
        {
            case STATE.IDLE:
                DoIdle();
                break;
            case STATE.WALK:
                DoWalk();
                break;
            case STATE.RUN:
                DoRun();
                break;
            case STATE.ELUDE:
                DoElude();
                break;
            case STATE.CROUCH:
                DoCrouch();
                break;
            case STATE.WALK_TO:
                DoWalkTo();
                break;
            case STATE.DEATH:
                DoDeath();
                break;
            case STATE.TALK:
                DoTalk();
                break;
        }
    }

    private void DoIdle()
    {

    }

    private void DoWalk()
    {
        _agent.SetDestination(_targetPoint);

        float dist = Vector3.Distance(transform.position, _targetPoint);
        if (dist <= _minArriveDist)
        {
            if (_isCanSkipStep)
                CiviliansManager.Instance.FinishCurTaskImmediately();
            else
                SetState(STATE.IDLE);
        }
    }

    private void DoRun()
    {
        _agent.SetDestination(_targetPoint);

        float dist = Vector3.Distance(transform.position, _targetPoint);
        if (dist <= _minArriveDist)
        {
            if (_isCanSkipStep)
                CiviliansManager.Instance.FinishCurTaskImmediately();
            else
                SetState(STATE.IDLE);
        }
    }

    private void DoElude()
    {

    }

    private void DoCrouch()
    {

    }

    private void DoWalkTo()
    {
        _agent.SetDestination(_targetPoint);

        float dist = Vector3.Distance(transform.position, _targetPoint);
        if (dist <= _minArriveTargetDist)
        {
            CiviliansManager.Instance.FinishCurTaskImmediately();
        }
    }

    private void DoDeath()
    {

    }

    private void DoTalk()
    {

    }

    private void ResetAnimator()
    {
        _anim.SetBool("isDeadBool", false);
        _anim.SetBool("isIdleBool", false);
        _anim.SetBool("isWalkBool", false);
        _anim.SetBool("isRunBool", false);
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateState();
    }
}
