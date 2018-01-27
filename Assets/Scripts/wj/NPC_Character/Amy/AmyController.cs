//AmyController.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/25/2017 10:04 AM
//Description: 
//

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AmyController : MonoBehaviour 
{
    private const float MAX_HP = 100;
    public float curHp = MAX_HP;
    public float creepSpeed = 1f;

    private Transform _attackTarget;

    private bool _isDead;
    public bool isDead
    {
        get { return _isDead; }
    }

    private Animator _anim;
    private NavMeshAgent _agent;

    public STATE curState;

    public enum STATE
    {
        IDLE,
        WALK,
        RUN,
        CROUCH,
        TALK,
        ATTACK,
        ATTACK_IDLE,
        ATTACK_WALK,
        DEAD,
    }

    public void Init()
    {
        SetState(STATE.IDLE);

    }

    public void SetTarget(Transform attackTarget)
    {
        _attackTarget = attackTarget;
    }

    public void OnHurt(float hp)
    {
        curHp  -=hp;

        if (curHp <= 0)
        {
            SetState(STATE.DEAD);
            Destroy(this.gameObject);
        }
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
                _agent.isStopped = true;
                _anim.SetBool("isIdleBool", true);
                break;
            case STATE.WALK:
                _agent.isStopped = false;
                _anim.SetBool("isWalkBool", true);
                break;
            case STATE.RUN:
                _agent.isStopped = false;
                //_anim.SetBool("isRunBool", true);
                break;
            case STATE.CROUCH:
                _agent.isStopped = true;
                break;
            case STATE.TALK:
                _agent.isStopped = true;
                break;
            case STATE.ATTACK:
                _agent.isStopped = true;
                _anim.SetBool("isAttackBool", true);
                break;
            case STATE.ATTACK_IDLE:
                _agent.isStopped = true;
                _anim.SetBool("isIdleBool", true);
                break;
            case STATE.ATTACK_WALK:
                _agent.isStopped = false;
                _anim.SetBool("isWalkBool", true);
                break;
            case STATE.DEAD:
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
            case STATE.CROUCH:
                DoCrouch();
                break;
            case STATE.TALK:
                DoTalk();
                break;
            case STATE.ATTACK:
                DoAttack();
                break;
            case STATE.ATTACK_IDLE:
                DoAttackIdle();
                break;
            case STATE.ATTACK_WALK:
                DoAttackWalk();
                break;
            case STATE.DEAD:
                break;
        }
    }

    private void DoIdle()
    {
        
    }

    private void DoWalk()
    {
        
    }

    private void DoRun()
    {
        
    }

    private void DoCrouch()
    {
        
    }

    private void DoTalk()
    {
       
    }

    private void DoAttack()
    {
        Vector3 v = _attackTarget.position;
        v.y = transform.position.y;
        float dist = Vector3.Distance(v, transform.position);
        if (dist >= 1.5f)
        {
            SetState(STATE.ATTACK_WALK);
        }
    }

    private void DoAttackIdle()
    {

    }

    private void DoAttackWalk()
    {
        _agent.SetDestination(_attackTarget.position);
        Vector3 v = _attackTarget.position;
        v.y = transform.position.y;
        float dist = Vector3.Distance(v, transform.position);
        if (dist <= 1f)
        {
            SetState(STATE.ATTACK);
        }
    }

    private void ResetAnimator()
    {
        _anim.SetBool("isIdleBool", false);
        _anim.SetBool("isWalkBool", false);
        _anim.SetBool("isAttackBool", false);
    }

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    void Start ()
	{
		
	}
	
	void Update () 
	{
        UpdateState();
	}
}
