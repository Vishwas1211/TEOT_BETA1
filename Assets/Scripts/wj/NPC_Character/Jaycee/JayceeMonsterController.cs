//JayceeMonsterController.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/10/2017 9:26 AM
//Description: Jaycee怪形态控制

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JayceeMonsterController : MonoBehaviour {
    private float _maxHp;
    private float _curHp;
    private float _moveSpeed;

    private float _timer = 0f;

    private bool _isDead;

    private NavMeshAgent _agent;
    private Animator _anim;

    public CONTROL_TYPE curType;
    public JAYCEE_M_STATE curState;

    public enum CONTROL_TYPE
    {
        COMMON,
        STORY
    }

    public enum JAYCEE_M_STATE
    {
        IDLE,
        WALK,
        RUN,
        JUMP,
        TALK_ABOUT,
        ATTACK,
        HURT,
        DEATH,
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();

        _curHp = 10f;
    }

    public void Init()
	{
		
	}

    public void SetType(CONTROL_TYPE type)
    {
        if (type == curType)
            return;
        curType = type;

        switch (curType)
        {
            case CONTROL_TYPE.COMMON:
                break;
            case CONTROL_TYPE.STORY:
                _agent.isStopped = true;
                break;
        }
    }

    private void SetState(JAYCEE_M_STATE state)
    {
        if (state == curState)
            return;
        curState = state;

        ResetAnimator();

        switch (curState)
        {
            case JAYCEE_M_STATE.IDLE:
                //_anim.SetBool("", true);
                break;
            case JAYCEE_M_STATE.WALK:
                //_anim.SetBool("", true);
                break;
            case JAYCEE_M_STATE.RUN:
                //_anim.SetBool("", true);
                break;
            case JAYCEE_M_STATE.JUMP:
                //_anim.SetBool("", true);
                break;
            case JAYCEE_M_STATE.TALK_ABOUT:
                //_anim.SetBool("", true);
                break;
            case JAYCEE_M_STATE.ATTACK:
                //_anim.SetBool("", true);
                break;
            case JAYCEE_M_STATE.HURT:
                //_anim.SetBool("", true);
                break;
            case JAYCEE_M_STATE.DEATH:
                //_anim.SetBool("", true);
                break;
        }
    }

    private void UpdateState()
    {
        if (_isDead)
            return;
        switch (curState)
        {
            case JAYCEE_M_STATE.IDLE:
                DoIdle();
                break;
            case JAYCEE_M_STATE.WALK:
                DoWalk();
                break;
            case JAYCEE_M_STATE.RUN:
                DoRun();
                break;
            case JAYCEE_M_STATE.JUMP:
                DoJump();
                break;
            case JAYCEE_M_STATE.TALK_ABOUT:
                DoTalkAbout();
                break;
            case JAYCEE_M_STATE.ATTACK:
                DoAttack();
                break;
            case JAYCEE_M_STATE.HURT:
                DoHurt();
                break;
            case JAYCEE_M_STATE.DEATH:
                DoDeath();
                break;
        }
    }

    public void OnHurt(float damage) //外界调用
    {
        if (_isDead)
            return;

        _curHp -= damage;
        if (_curHp <= 0)
        {
            _curHp = 0;
            _isDead = true;
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

    private void DoJump()
    {

    }

    private void DoTalkAbout()
    {

    }

    private void DoAttack()
    {

    }

    private void DoHurt()
    {

    }

    private void DoDeath()
    {

    }

    private void ResetAnimator()
    {
        //_anim.SetBool("", false);
    }

    void Update () 
	{
        UpdateState();
	}


    public void ShoShang(float hp)
    {
        _curHp-=hp;
        if (_curHp <= 0)
        {
            TaskStepManagaer.Instance.FinishCurTask();
            Destroy(this.gameObject);
        }
    }

}
