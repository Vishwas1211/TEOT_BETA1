//LizzyController.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/26/2017 9:55 AM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LizzyController : MonoBehaviour 
{
    private const float MAX_HP = 100;
    private float _curHP = MAX_HP;
    private float _moveSpeed;

    private bool _isDead;
    private bool _isCanSkipStep;

    private Vector3 _targetPoint;

    private Animator _anim;
    private NavMeshAgent _agent;

    public STATE curState;

    public enum STATE
    {
        IDLE,
        WALK,
        RUN,
        PUSH,
        ATTACK,
        LAY_DOWN,
        DEATH,
    }

    public void Init()
    {
        SetState(STATE.IDLE);
    }

    public void OnHurt(float damage) //Íâ½çµ÷ÓÃ
    {
        if (_isDead)
            return;

        _curHP -= damage;
        if (_curHP <= 0)
        {
            _curHP = 0;
            _isDead = true;
        }
    }

    public void SetTargetPoint(Vector3 v)
    {
        _targetPoint = v;
    }

    public void SetCanSkipStep(bool b)
    {
        _isCanSkipStep = b;
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
                _agent.enabled = false;
                _anim.SetBool("isIdleBool", true);
                break;
            case STATE.WALK:
                _agent.isStopped = false;
                _anim.SetBool("isWalkBool", true);
                break;
            case STATE.RUN:
                _agent.isStopped = false;
                _anim.SetBool("isRunBool", true);
                break;
            case STATE.PUSH:
                _anim.SetBool("isPushBool", true);
                break;
            case STATE.ATTACK:
                _anim.SetBool("isAttackBool", true);
                break;
            case STATE.LAY_DOWN:

                break;
            case STATE.DEATH:
                _anim.SetTrigger("DeadTrigger");
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
            case STATE.PUSH:
                DoPush();
                break;
            case STATE.ATTACK:
                break;
            case STATE.DEATH:
                break;
        }
    }

    private void DoIdle()
    {
        //_agent.isStopped = true;
        
    }

    private void DoWalk()
    {
        _agent.SetDestination(_targetPoint);

        float distance = Vector3.Distance(_targetPoint, transform.position);
        if (distance <= 0.2f)
        {
            if (_isCanSkipStep)
                LizzyManager.Instance.FinishCurTaskImmediately();
            else
                SetState(STATE.IDLE);
        }
    }

    private void DoRun()
    {
        //if (_targetPoint == Vector3.zero)
        //    return;

       
        _agent.SetDestination(_targetPoint);

        float distance = Vector3.Distance(_targetPoint, transform.position);
        if (distance <= 0.2f)
        {
            if (_isCanSkipStep)
                LizzyManager.Instance.FinishCurTaskImmediately();
            else
                SetState(STATE.IDLE);
        }
    }

    private void DoPush()
    {

    }

    private void ResetAnimator()
    {
        _anim.SetBool("isIdleBool", false);
        _anim.SetBool("isWalkBool", false);
        _anim.SetBool("isRunBool", false);
        _anim.SetBool("isAttackBool", false);
    }

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        UpdateState();
    }
}
