//RickController.cs
//TEOT_ONLINE
//
//Create by WangJie on 9/13/2017 3:36 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RickController : MonoBehaviour 
{
    private const string RICK_FLEE_POINT = "Prefabs/Character/NPCs/Rick/RickFleePoint";

    private const float MAX_HP = 100;
    private float _curHp = MAX_HP;
    private float _moveSpeed = 3f;
    private float _runSpeed = 6f;
    private float _timer = 0f;

    private int _hurtCount = 0;

    private Vector3 _curTargetPos;

    private bool _isCanSkipStep;
    private bool _isDead;

    private GameObject _rickFleePoint;
    private GameObject _target;

    private Animator _anim;
    private NavMeshAgent _agent;

    public float minArriveDistance = 0.2f;
    public float maxArriveDistance = 0.5f;
    public float arriveTargetDistance = 2;
    public STATE curState;
    public enum STATE
    {
        EMPTY,
        IDLE,
        WALK,
        WALK_BACK,
        WALK_TO,
        RUN,
        LEADER,
        LEADER_IDLE,
        TURN,
        FOLLOW,
        FOLLOW_IDLE,
        ATTACK,
        FLEE,
        FLEE_IDLE,
        DEAD,
    }

	public void Init()
	{
        SetState(STATE.IDLE);
        _agent.speed = _moveSpeed;

        _rickFleePoint = UtilFunction.ResourceLoad(RICK_FLEE_POINT);
	}

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    public void SetCanSkipStep(bool b)
    {
        _isCanSkipStep = b;
    }

    public void SetTargetPoint(Vector3 v)
    {
        _curTargetPos = v;
    }

    public void OnHurt(float damage)
    {
        if (_isDead)
            return;

        _hurtCount++;
        if (_hurtCount >= 3)
        {
            _hurtCount = 0;
            float[] distArray = new float[_rickFleePoint.transform.childCount];
            float minDist = 0f;
            int index = 0;

            for (int i = 0; i < _rickFleePoint.transform.childCount; i++)
            {
                float dist = Vector3.Distance(transform.position, _rickFleePoint.transform.GetChild(i).position);
                distArray[i] = dist;
            }
            minDist = distArray[0]; //¸³Öµ,minDist³õÊ¼Öµ²»ÄÜÎª0,·ñÔòÕÒ²»³ö±È0¸üÐ¡µÄ¾àÀë
            for (int i = 0; i < distArray.Length; i++)
            {
                if (distArray[i] < minDist)
                {
                    minDist = distArray[i];
                    index = i;
                }
            }

            SetTargetPoint(_rickFleePoint.transform.GetChild(index).position);
            SetState(STATE.FLEE);
        }
        _curHp -= damage;
        if(_curHp <= 0)
        {
            _curHp = 0;
            _isDead = true;
            SetState(STATE.DEAD);
        }
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
            case STATE.EMPTY:
                _agent.enabled = false;
                _anim.SetBool("isIdleBool", true);
                break;
            case STATE.IDLE:
                _agent.enabled = false;
                _anim.SetBool("isIdleBool", true);
                break;
            case STATE.WALK:
                _agent.speed = _moveSpeed;
                _anim.SetBool("isWalkBool", true);
                break;
            case STATE.WALK_BACK:
                _agent.speed = _moveSpeed;
                _anim.SetBool("isWalkBackBool", true);
                break;
            case STATE.WALK_TO:
                _anim.SetBool("isWalkBool", true);
                break;
            case STATE.RUN:
                _agent.speed = _runSpeed;
                _anim.SetBool("isRunBool", true);
                break;
            case STATE.LEADER:
                _agent.speed = _moveSpeed;
                _anim.SetBool("isWalkBool", true);
                break;
            case STATE.LEADER_IDLE:
                _agent.enabled = false;
                _anim.SetBool("isIdleBool", true);
                break;
            case STATE.TURN:
                break;
            case STATE.FOLLOW:
                _anim.SetBool("isWalkBool", true);
                break;
            case STATE.FOLLOW_IDLE:
                _agent.enabled = false;
                _anim.SetBool("isIdleBool", true);
                break;
            case STATE.ATTACK:
                _agent.enabled = false;
                _anim.SetBool("isAttackBool", true);
                break;
            case STATE.FLEE:
                _agent.speed = _runSpeed;
                _anim.SetBool("isRunBool", true);
                break;
            case STATE.FLEE_IDLE:
                _agent.enabled = false;
                _anim.SetBool("isIdleBool", true);
                break;
            case STATE.DEAD:
                _agent.enabled = false;
                _anim.SetBool("isDeadBool", true);
                break;
        }
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
            case STATE.WALK_BACK:
                DoWalkBack();
                break;
            case STATE.WALK_TO:
                DoWalkTo();
                break;
            case STATE.RUN:
                DoRun();
                break;
            case STATE.LEADER:
                DoLeader();
                break;
            case STATE.LEADER_IDLE:
                DoLeaderIdle();
                break;
            case STATE.TURN:
                DoTurn();
                break;
            case STATE.FOLLOW:
                DoFollow();
                break;
            case STATE.FOLLOW_IDLE:
                DoFollowIdle();
                break;
            case STATE.ATTACK:
                DoAttack();
                break;
            case STATE.FLEE:
                DoFlee();
                break;
            case STATE.FLEE_IDLE:
                DoFleeIdle();
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
        _agent.SetDestination(_curTargetPos);

        float dist = Vector3.Distance(transform.position, _curTargetPos);
        if (dist <= minArriveDistance)
        {
            if (_isCanSkipStep)
            {
                RickManager.Instance.FinishCurTaskImmediately();
            }
            else
            {
                SetState(STATE.EMPTY);
            }
        }
    }

    private void DoWalkBack()
    {
        _timer += Time.deltaTime;
        transform.Translate(Vector3.back * _moveSpeed * Time.deltaTime, Space.Self);
        if (_timer >= 2f)
        {
            _timer = 0f;
            SetState(STATE.TURN);
        }
    }

    private void DoWalkTo()
    {
        _agent.SetDestination(_curTargetPos);
        float dist = Vector3.Distance(transform.position, _curTargetPos);
        if (dist <= arriveTargetDistance)
        {
            if (_isCanSkipStep)
            {
                RickManager.Instance.FinishCurTaskImmediately();
            }
            else
            {
                SetState(STATE.EMPTY);
            }
        }
    }

    private void DoRun()
    {
        _agent.SetDestination(_curTargetPos);

        float dist = Vector3.Distance(transform.position, _curTargetPos);
        if (dist <= minArriveDistance)
        {
            if (_isCanSkipStep)
            {
                RickManager.Instance.FinishCurTaskImmediately();
            }
            else
            {
                SetState(STATE.EMPTY);
            }
        }
    }

    private void DoLeader() //Áìµ¼Íæ¼Ò
    {
        _agent.SetDestination(_curTargetPos);
        float dist = Vector3.Distance(transform.position, _target.transform.position);
        if (dist >= 6)
        {
            SetState(STATE.LEADER_IDLE);
        }

        float distPos = Vector3.Distance(transform.position, _curTargetPos);
        if (distPos <= 0.2f)
        {
            if (_isCanSkipStep)
                RickManager.Instance.FinishCurTaskImmediately();
            else
                SetState(STATE.IDLE);
        }
    }

    private void DoLeaderIdle()
    {
        float dist = Vector3.Distance(transform.position, _target.transform.position);
        if (dist < 4)
        {
            SetState(STATE.LEADER);
        }
    }

    private void DoTurn()
    {
        SetState(STATE.WALK);
    }

    private void DoFollow()
    {
        _agent.SetDestination(_curTargetPos);
        Vector3 pos = _curTargetPos;
        pos.y = transform.position.y;
        float dist = Vector3.Distance(transform.position, pos);
        if (dist <= 1f)
        {
            SetState(STATE.FOLLOW_IDLE);
        }
    }

    private void DoFollowIdle()
    {
        Vector3 pos = _curTargetPos;
        pos.y = transform.position.y;
        float dist = Vector3.Distance(transform.position, pos);
        if (dist >= 2f)
        {
            SetState(STATE.FOLLOW);
        }
    }

    private void DoAttack()
    {

    }

    private void DoFlee()
    {
        _agent.SetDestination(_curTargetPos);
        Vector3 pos = _curTargetPos;
        pos.y = transform.position.y;
        float dist = Vector3.Distance(transform.position, pos);
        if (dist <= 0.3f)
        {
            SetState(STATE.FLEE_IDLE);
        }
    }

    private void DoFleeIdle()
    {
        float dist = Vector3.Distance(transform.position, _target.transform.position);
        if (dist <= 1.5f)
        {
            SetState(STATE.FOLLOW);
        }
    }

    private void ResetAnimator()
    {
        _anim.SetBool("isIdleBool", false);
        _anim.SetBool("isWalkBool", false);
        _anim.SetBool("isRunBool", false);
        _anim.SetBool("isDeadBool", false);
        _anim.SetBool("isWalkBackBool", false);
        _anim.SetBool("isAttackBool", false);
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
