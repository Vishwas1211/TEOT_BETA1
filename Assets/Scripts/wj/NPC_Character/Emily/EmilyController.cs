//EmilyController.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/25/2017 6:16 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmilyController : MonoBehaviour 
{
    private const float MAX_HP = 100;
    private float _curHP = MAX_HP;
    private float _moveSpeed = 3;
    private float _minArriveDist = 0.2f;

    private bool _isCanSkipStep;
    private Animator _anim;
    private NavMeshAgent _agent;

    private Vector3 _targetPoint;
    public STATE curState;

    public enum STATE
    {
        IDLE,
        WALK,
        RUN,
        WALK_TO,
        ATTACK,
        DEATH,
    }
	
	public void Init ()
	{
        SetState(STATE.IDLE);
        SetCanSkip(true);
    }

    public void SetCanSkip(bool b)
    {
        _isCanSkipStep = b;
    }

    public void SetTargetPoint(Vector3 v)
    {
        if (v == Vector3.zero)
        {
            v = transform.position;
            //Debug.Log("-------------------Î»ÖÃÉèÖÃÓÐÎó");
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

        switch (curState)
        {
            case STATE.IDLE:
                _anim.SetBool("isIdleBool", true);
                break;
            case STATE.WALK:
                _anim.SetBool("isWalkBool", true);
                break;
            case STATE.RUN:
                break;
            case STATE.WALK_TO:
                _anim.SetBool("isWalkBool", true);
                break;
            case STATE.ATTACK:
                break;
            case STATE.DEATH:
                break;
        }
    }

    private void UpdateState()
    {
        switch (curState)
        {
            case STATE.IDLE:
                break;
            case STATE.WALK:
                DoWalk();
                break;
            case STATE.RUN:
                break;
            case STATE.WALK_TO:
                DoWalkTo();
                break;
            case STATE.ATTACK:
                break;
            case STATE.DEATH:
                break;
        }
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

    private void DoWalkTo()
    {
        _agent.SetDestination(_targetPoint);

        float dist = Vector3.Distance(transform.position, _targetPoint);
        if (dist <= 0.8f)
        {
            SetState(STATE.IDLE);
        }
    }

    private void ResetAnimator()
    {
        _anim.SetBool("isIdleBool", false);
        _anim.SetBool("isWalkBool", false);
    }

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    void Update () 
	{
        UpdateState();
	}
}
