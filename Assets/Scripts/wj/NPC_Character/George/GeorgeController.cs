//GeorgeController.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/26/2017 12:31 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GeorgeController : MonoBehaviour 
{
    private const float MAX_HP = 100;
    private float _curHP = MAX_HP;
    private float _moveSpeed;

    private Transform _attackTarget;

    private Animator _anim;
    private NavMeshAgent _agent;

    public STATE curState;

    public enum STATE
    {
        IDLE,
        WALK,
        RUN,
        ATTACK,
        ATTACK_IDLE,
        ATTACK_WALK,
        DEATH,
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
        _curHP -= hp;

        if (_curHP <= 0)
        {
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
                _anim.SetBool("isIdleBool", true);
                break;
            case STATE.WALK:
                _anim.SetBool("isWalkBool", true);
                break;
            case STATE.RUN:
                _anim.SetBool("isRunBool", true);
                break;
            case STATE.ATTACK:
                _anim.SetBool("isAttackBool", true);
                break;
            case STATE.ATTACK_IDLE:
                _anim.SetBool("isIdleBool", true);
                break;
            case STATE.ATTACK_WALK:
                _anim.SetBool("isWalkBool", true);
                break;
            case STATE.DEATH:
                _anim.SetTrigger("isDeadTrigger");
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
            case STATE.ATTACK:
                DoAttack();
                break;
            case STATE.ATTACK_IDLE:
                DoAttackIdle();
                break;
            case STATE.ATTACK_WALK:
                DoAttackWalk();
                break;
            case STATE.DEATH:
                DoDeath();
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

    private void DoDeath()
    {

    }

    private void ResetAnimator()
    {
        _anim.SetBool("isIdleBool", false);
        _anim.SetBool("isWalkBool", false);
        _anim.SetBool("isRunBool", true);
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
