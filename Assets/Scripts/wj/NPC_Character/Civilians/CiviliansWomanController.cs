//CiviliansWomanController.cs
//TEOT_ONLINE
//
//Create by WangJie on 10/20/2017 11:59 AM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class CiviliansWomanController : MonoBehaviour
{
    private const float MAX_HP = 100;
    private float _curHP = MAX_HP;
    private float _moveSpeed = 3f;
    private float _runSpeed = 6f;
    private float _minArriveDist = 0.2f;

    private bool _isDead;

    private Vector3 _targetPoint;

    private Animator _anim;
    private NavMeshAgent _agent;

    public STATE curState;

    public enum STATE
    {
        IDLE,
        WALK,
        RUN,
        ELUDE,  //¶ã±Ü
        CROUCH,
        DEATH,
        TALK,
    }

    public void Init()
    {
        _agent.speed = _moveSpeed;
    }

    public void OnHurt(float damage)
    {
        if (_isDead)
            return;
        _curHP -= damage;

        if (_curHP <= 0)
        {
            _curHP = 0;
            _isDead = true;
            SetState(STATE.DEATH);
        }
    }

    public void SetTargetPoint(Vector3 v)
    {
        if (v == Vector3.zero)
        {
            v = transform.position;
            return;
        }
        _targetPoint = v;
    }

    public void SetState(STATE state)
    {
        if (state == curState)
            return;
        curState = state;
        switch (curState)
        {
            case STATE.IDLE:
                break;
            case STATE.WALK:
                _agent.speed = _moveSpeed;
                break;
            case STATE.RUN:
                _agent.speed = _runSpeed;
                break;
            case STATE.ELUDE:
                break;
            case STATE.CROUCH:
                break;
            case STATE.DEATH:
                break;
            case STATE.TALK:
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
            //CiviliansManager.instance.FinishCurTaskImmediately();
        }
    }

    private void DoRun()
    {
        _agent.SetDestination(_targetPoint);

        float dist = Vector3.Distance(transform.position, _targetPoint);
        if (dist <= _minArriveDist)
        {
            //CiviliansManager.instance.FinishCurTaskImmediately();
        }
    }

    private void DoElude()
    {

    }

    private void DoCrouch()
    {

    }

    private void DoDeath()
    {

    }

    private void DoTalk()
    {

    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {

    }
}