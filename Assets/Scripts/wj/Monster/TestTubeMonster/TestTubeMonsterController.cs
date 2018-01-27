//TestTubeMonsterController.cs
//TEOT_ONLINE
//
//Create by WangJie on 9/12/2017 6:06 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestTubeMonsterController : MonoBehaviour 
{
    private const float MAX_HP = 100;
    private float _curHp = MAX_HP;

    private bool _isDead;
    public bool isDead
    {
        get { return _isDead; }
    }

    private Animator _anim;
    [SerializeField]
    private float _moveSpeed = 2f;

    private NavMeshAgent _agent;

    public STATE curState;

    public enum STATE
    {
        IDLE,
        WALK,
        RUN,
    }
	
	public void Init ()
	{
        SetState(STATE.IDLE);
	}

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
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
                break;
            case STATE.RUN:
                break;
        }
    }

    public void OnHurt(float damage)
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
            case STATE.RUN:
                DoRun();
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

    void Update () 
	{
        UpdateState();
	}
}
