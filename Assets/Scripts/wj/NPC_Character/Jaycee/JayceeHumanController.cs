//JayceeHumanController.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/10/2017 9:24 AM
//Description: JayceeÈËÐÎÌ¬¿ØÖÆ

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JayceeHumanController : MonoBehaviour {
    private const string SAOBA_PATH = "Jaycee (1)/Jaycee_Hips/Jaycee_Spine/Jaycee_Spine1/Jaycee_Spine2/" +
        "Jaycee_Spine3/Jaycee_Spine4/Jaycee_Neck/Jaycee_LeftShoulder/Jaycee_LeftArm/Jaycee_LeftForeArm/" +
        "Jaycee_LeftHand/saoba";

    private const float _maxHp = 100;
    private float _curHp = _maxHp;
    private float _moveSpeed = 2;
    private float _runSpeed = 4;
    private float _angleSpeed = 1000f;
    private float _timer = 0f; //¼ÆÊ±Æ÷


    private bool _isDead;
    private bool _isStartApear;
    private bool _isCanSkipStep;
    public bool isCanRobBesom;

    private Vector3 _targetPoint;
    private GameObject _saoBa;
    private GameObject _target;

    private NavMeshAgent _agent;
    private Animator _anim;

    public GameObject[] paths;
    private Vector3[] runPaths = {
        new Vector3(-26.17f, 20.48f, 35.738f),
        new Vector3(-18.342f, 20.48f, 38.126f),
        new Vector3(-9.71f, 20.48f, 38.126f)
    };

    public CONTROL_TYPE curType;
    public JAYCEE_H_STATE curState;
    
    public enum CONTROL_TYPE
    {
        STORY,
        COMMON
    }

    public enum JAYCEE_H_STATE
    {
        IDLE,
        WALK,
        WALK_BACK,
        WALK_PATH,
        RUN,
        LEADER,
        LEADER_IDLE,
        JUMP,
        CROUCH,
        TALK_ABOUT,
        BACK_LOOK,
        ATTACK,
        HURT,
        DEATH,
    }

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _saoBa = transform.Find(SAOBA_PATH).gameObject;
        _saoBa.SetActive(false);
        _agent.speed = _moveSpeed;
    }

    private void Start()
    {
        //Test
        Init();
        //StartAppear();
        DoorTrigger.OpenJayceeEvent += new OpenDoorEventHandler(StartAttack);
    }

    public void Init ()
	{
        SetState(JAYCEE_H_STATE.IDLE);
        SetCanSkipStep(true);
	}

    public void StartAppear()
    {
        _isStartApear = true;
    }

   public void RobBesom()
    {
        if (!isCanRobBesom)
            return;
        Destroy(_saoBa);
        isCanRobBesom = false;
    }

    public void SetTarget(GameObject target)
    {
        _target = target;
    }

    public void SetCanSkipStep(bool b)
    {
        _isCanSkipStep = b;
    }

    public void SetType(CONTROL_TYPE type)
    {
        if (type == curType)
            return;
        curType = type;

        switch (curType)
        {
            case CONTROL_TYPE.STORY:
                _agent.isStopped = true;
                break;
            case CONTROL_TYPE.COMMON:
                break;
        }
    }

    public void SetState(JAYCEE_H_STATE state)
    {
        if (state == curState)
            return;
        curState = state;
        ResetAnimator();

        _agent.angularSpeed = _angleSpeed;
        _agent.enabled = true;

        switch (curState)
        {
            case JAYCEE_H_STATE.IDLE:
                _agent.enabled = false;
                _anim.SetBool("isIdleBool", true);
                break;
            case JAYCEE_H_STATE.WALK:
                _agent.speed = _moveSpeed;
                _anim.SetBool("isWalkBool", true);
                break;
            case JAYCEE_H_STATE.WALK_BACK:
                _agent.angularSpeed = 0;
                _agent.speed = _moveSpeed;
                _anim.SetBool("isWalkBackBool", true);
                break;
            case JAYCEE_H_STATE.WALK_PATH:
                _anim.SetBool("isRunBool", true);
                break;
            case JAYCEE_H_STATE.RUN:
                _agent.speed = _runSpeed;
                _anim.SetBool("isRunBool", true);
                break;
            case JAYCEE_H_STATE.LEADER:
                _agent.speed = _moveSpeed;
                _anim.SetBool("isWalkBool", true);
                break;
            case JAYCEE_H_STATE.LEADER_IDLE:
                _agent.enabled = false;
                _anim.SetBool("isIdleBool", true);
                break;
            case JAYCEE_H_STATE.JUMP:
                _agent.enabled = false;
                _anim.SetBool("isJumpBool", true);
                break;
            case JAYCEE_H_STATE.CROUCH:
                _agent.enabled = false;
                _anim.SetBool("isCrouchBool", true);
                break;
            case JAYCEE_H_STATE.TALK_ABOUT:
                _agent.enabled = false;
                //_anim.SetBool("", true);
                break;
            case JAYCEE_H_STATE.BACK_LOOK:
                _agent.enabled = false;
                _anim.SetBool("isBackLookBool", true);
                break;
            case JAYCEE_H_STATE.ATTACK:
                _agent.enabled = false;
                _anim.SetBool("isAttackBool", true);
                break;
            case JAYCEE_H_STATE.HURT:
                _agent.enabled = false;
                _anim.SetBool("isHurtBool", true);
                break;
            case JAYCEE_H_STATE.DEATH:
                _agent.enabled = false;
                _anim.SetBool("isDeathBool", true);
                break;
        }
    }

    private void UpdateState()
    {
        if (_isDead)
            return;
        
        switch (curState)
            {
            case JAYCEE_H_STATE.IDLE:
                DoIdle();
                break;
            case JAYCEE_H_STATE.WALK:
                DoWalk();
                break;
            case JAYCEE_H_STATE.WALK_BACK:
                DoWalkBack();
                break;
            case JAYCEE_H_STATE.WALK_PATH:
                DoWalkPath();
                break;
            case JAYCEE_H_STATE.RUN:
                DoRun();
                break;
            case JAYCEE_H_STATE.LEADER:
                DoLeader();
                break;
            case JAYCEE_H_STATE.LEADER_IDLE:
                DoLeaderIdle();
                break;
            case JAYCEE_H_STATE.JUMP:
                DoJump();
                break;
            case JAYCEE_H_STATE.CROUCH:
                DoCrouch();
                break;
            case JAYCEE_H_STATE.TALK_ABOUT:
                DoTalkAbout();
                break;
            case JAYCEE_H_STATE.BACK_LOOK:
                DoBackLook();
                break;
            case JAYCEE_H_STATE.ATTACK:
                DoAttack();
                break;
            case JAYCEE_H_STATE.HURT:
                DoHurt();
                break;
            case JAYCEE_H_STATE.DEATH:
                DoDeath();
                break;
        }
    }

    public void SetActiveBesom(bool display)
    {
        _saoBa.SetActive(display);
    }

    public void OnHurt(float damage) //Íâ½çµ÷ÓÃ
    {
        if (_isDead)
            return;

        _curHp -= damage;
        if(_curHp <= 0)
        {
            _curHp = 0;
            _isDead = true;
        }
    }

    private void DoIdle()
    {
        //if (Vector3.Distance(paths[1].transform.position, transform.position) <= 0.3f)
        //{
        //    return;
        //}
        //float distance = Vector3.Distance(paths[0].transform.position, transform.position);
        //if (distance <= 0.3f)
        //{
        //    SetState(JAYCEE_H_STATE.BACK_LOOK);
        //    return;
        //}
        //SetTargetPoint(paths[0].transform.position);
        //SetState(JAYCEE_H_STATE.RUN);
    }

    private void DoWalk()
    {
        _agent.SetDestination(_targetPoint);

        float distance = Vector3.Distance(_targetPoint, transform.position);
        if (distance <= 0.2f)
        {
            if (_isCanSkipStep)
            {
                SetState(JAYCEE_H_STATE.IDLE);
                JayceeManager.Instance.FinishCurTaskImmediately();
            }
            else
                SetState(JAYCEE_H_STATE.IDLE);
        }
    }

    private void DoWalkBack()
    {
        _agent.SetDestination(_targetPoint);

        float distance = Vector3.Distance(_targetPoint, transform.position);
        if (distance <= 0.2f)
        {
            if (_isCanSkipStep)
            {
                JayceeManager.Instance.FinishCurTaskImmediately();
                SetState(JAYCEE_H_STATE.IDLE);
            }
            else
                SetState(JAYCEE_H_STATE.IDLE);
        }
    }

    private int indexPath = 0;
    private void DoWalkPath()
    {
        SetTargetPoint(runPaths[indexPath]);
        _agent.SetDestination(_targetPoint);

        float distance = Vector3.Distance(_targetPoint, transform.position);
        if (distance <= 0.3f)
        {
            if (indexPath <= 1)
            {
                indexPath++;
            }
            else
            {
                SetTargetPoint(new Vector3(-6.77f, 20.48f, 26.12f));
                SetState(JAYCEE_H_STATE.RUN);
            }
        }
    }

    private void DoRun()
    {
        _agent.SetDestination(_targetPoint);

        float distance = Vector3.Distance(_targetPoint, transform.position);
        if (distance <= 0.2f)
        {
            //if (_targetPoint == paths[1].transform.position)
            //{
            //    _saoBa.SetActive(true);
            //}
            //SetState(JAYCEE_H_STATE.IDLE);
            if (_isCanSkipStep)
            {
                JayceeManager.Instance.FinishCurTaskImmediately();
                SetState(JAYCEE_H_STATE.IDLE);
            }
            else
                SetState(JAYCEE_H_STATE.IDLE);
        }
    }

    private void DoLeader() //Áìµ¼Íæ¼Ò
    {
        _agent.SetDestination(_targetPoint);
        float dist = Vector3.Distance(transform.position, _target.transform.position);
        if (dist >= 6)
        {
            SetState(JAYCEE_H_STATE.LEADER_IDLE);
        }

        float distPos = Vector3.Distance(transform.position, _targetPoint);
        if (distPos <= 0.2f)
        {
            if (_isCanSkipStep)
            {
                SetState(JAYCEE_H_STATE.IDLE);
                JayceeManager.Instance.FinishCurTaskImmediately();
            }
            else
                SetState(JAYCEE_H_STATE.IDLE);
        }
    }

    private void DoLeaderIdle()
    {
        float dist = Vector3.Distance(transform.position, _target.transform.position);
        if (dist < 4)
        {
            SetState(JAYCEE_H_STATE.LEADER);
        }
    }

    private void DoJump()
    {
    }

    private void DoCrouch()
    {
    }

    private void DoTalkAbout()
    {
    }

    private void DoBackLook()
    {
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("houkan") && info.normalizedTime >= 1)
        {
            SetState(JAYCEE_H_STATE.WALK_PATH);
            JayceeManager.Instance.FinishCurTask();
        }
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

    public void SetTargetPoint(Vector3 v)
    {
        _targetPoint = v;
    }

    private void ResetAnimator()
    {
        _anim.SetBool("isIdleBool", false);
        _anim.SetBool("isWalkBool", false);
        _anim.SetBool("isRunBool", false);
        _anim.SetBool("isJumpBool", false);
        _anim.SetBool("isCrouchBool", false);
        _anim.SetBool("isAttackBool", false);
        _anim.SetBool("isHurtBool", false);
        _anim.SetBool("isDeathBool", false);
        _anim.SetBool("isBackLookBool", false);
    }

    private void StartAttack()
    {
        Vector3 v = GameObject.Find("Player").transform.position;
        v.y = transform.position.y;
        transform.LookAt(v);
        SetState(JAYCEE_H_STATE.ATTACK);
    }

    void Update () 
	{
        if (!_isStartApear)
        {
            return;
        }
            UpdateState();
	}
}
