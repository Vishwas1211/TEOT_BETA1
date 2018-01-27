//SogBossController.cs
//TEOT_ONLINE
//
//Create by WangJie on 12/11/2017 3:56 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class SogBossController : MonoBehaviour 
{
    private const float MAX_HP = 100f;
    private float _curHp = MAX_HP;
    private float _walkSpeed = 2f;
    private float _runSpeed = 4f;
    private float _timer = 0f;

    private bool _isDead;
    private bool _isFirstWalk = true;
    private bool _isCanHurt;
    private bool _isFirstBrandish = true;
    private bool _isOnceRun = true;

    private int index = 0;
    private Vector3 posJumpStart = Vector3.zero;

    public GameObject _player;

    private Vector3 _point1 = new Vector3(2.816f, 0.68f, 23.6f);
    private Vector3 _point2 = new Vector3(-1.255f, 0.68f, 23.914f);
    //private Vector3 _jumpToPoint1 = new Vector3(-2.608f, 0.68f, 23.914f);
    //private Vector3 _jumpToPoint2 = new Vector3(-3.392f, 0.68f, 23.914f);
    private Vector3[] _jumpToPoint = { new Vector3(-2.608f, 2.02f, 23.914f), new Vector3(-3.392f, 0.68f, 23.914f) };

    private Animator _anim;
    private NavMeshAgent _agent;

    public BEHAVIOR_SATE _curState;
    public TYPE type = TYPE.FIRST_FLOOR;

    public enum TYPE
    {
        FIRST_FLOOR,
        OTHER,
    }

    public enum BEHAVIOR_SATE
    {
        IDLE,
        WALK,
        RUN,
        JUMP,
        CROUCH_L,
        CROUCH_R,
        WALK_SHOOT,
        CROUCH_SHOOT_L,
        CROUCH_SHOOT_R,
        GRAB,//×¥
        HAMMER,//ÔÒµØ
        BRANDISH,//ÂÖ¸ì²²
        LIFT_UP,//ÏÆÆð
        DUOBI,
        HURT,
        DEATH,
    }

    public void Init()
    {
        _agent.speed = _walkSpeed;
        _agent.angularSpeed = 1000;

        SetState(BEHAVIOR_SATE.RUN);
        _player = PlayerManager.Instance.playerCollider;
    }

    public void OnHurt(float damage)
    {
        if (_isDead)
            return;
        if (!_isCanHurt)
            return;

        _curHp -= damage;
        if (_curHp <= 0)
        {
            _curHp = 0f;
            _isDead = true;
            SetState(BEHAVIOR_SATE.DEATH);
            TaskStepManagaer.Instance.FinishCurTaskImmediately();
        }
        else
        {
            int random = Random.Range(0, 10);
            if (random == 1)
                SetState(BEHAVIOR_SATE.HURT);
            else if (random > 7)
                SetState(BEHAVIOR_SATE.DUOBI);
        }
    }

    public void SetState(BEHAVIOR_SATE state)
    {
        if (state == _curState)
            return;
        _curState = state;

        ResetAnimator();
        _agent.enabled = true;
        switch (_curState)
        {
            case BEHAVIOR_SATE.IDLE:
                _anim.SetBool("isIdleBool", true);
                break;
            case BEHAVIOR_SATE.WALK:
                _agent.speed = _walkSpeed;
                _anim.SetBool("isWalkBool", true);
                break;
            case BEHAVIOR_SATE.RUN:
                _agent.speed = _runSpeed;
                _anim.SetBool("isRunBool", true);
                break;
            case BEHAVIOR_SATE.JUMP:
                _agent.enabled = false;
                _anim.SetBool("isJumpBool", true);
                break;
            case BEHAVIOR_SATE.CROUCH_L:
                _anim.SetBool("isCrouchLBool", true);
                break;
            case BEHAVIOR_SATE.CROUCH_R:
                _anim.SetBool("isCrouchRBool", true);
                break;
            case BEHAVIOR_SATE.WALK_SHOOT:
                _anim.SetBool("isWalkShootBool", true);
                break;
            case BEHAVIOR_SATE.CROUCH_SHOOT_L:
                _anim.SetTrigger("isCrouchShootLTrigger");
                break;
            case BEHAVIOR_SATE.CROUCH_SHOOT_R:
                _anim.SetTrigger("isCrouchShootRTrigger");
                break;
            case BEHAVIOR_SATE.GRAB:
                _agent.enabled = false;
                _anim.SetTrigger("isGrabTrigger");
                break;
            case BEHAVIOR_SATE.HAMMER:
                _agent.enabled = false;
                _anim.SetTrigger("isHammerTrigger");
                break;
            case BEHAVIOR_SATE.BRANDISH:
                _agent.enabled = false;
                _anim.SetTrigger("isBrandishTrigger");
                break;
            case BEHAVIOR_SATE.LIFT_UP:
                _agent.enabled = false;
                _anim.SetTrigger("isLiftUpTrigger");
                break;
            case BEHAVIOR_SATE.DUOBI:
                _agent.enabled = false;
                _anim.SetTrigger("isDuoBiTrigger");
                break;
            case BEHAVIOR_SATE.HURT:
                _agent.enabled = false;
                _anim.SetTrigger("isHurtTrigger");
                break;
            case BEHAVIOR_SATE.DEATH:
                _agent.enabled = false;
                _anim.SetTrigger("isDeathTrigger");
                break;
        }
    }

    private void ResetAnimator()
    {
        _anim.SetBool("isIdleBool", false);
        _anim.SetBool("isWalkBool", false);
        _anim.SetBool("isRunBool", false);
        _anim.SetBool("isCrouchLBool", false);
        _anim.SetBool("isCrouchRBool", false);
        _anim.SetBool("isJumpBool", false);
        _anim.SetBool("isWalkShootBool", false);
    }

    private void UpdateState()
    {
        if (_isDead)
            return;
        switch (_curState)
        {
            case BEHAVIOR_SATE.IDLE:
                DoIdle();
                break;
            case BEHAVIOR_SATE.WALK:
                DoWalk();
                break;
            case BEHAVIOR_SATE.RUN:
                DoRun();
                break;
            case BEHAVIOR_SATE.JUMP:
                DoJump();
                break;
            case BEHAVIOR_SATE.CROUCH_L:
                DoCrouchL();
                break;
            case BEHAVIOR_SATE.CROUCH_R:
                DoCrouchR();
                break;
            case BEHAVIOR_SATE.WALK_SHOOT:
                DoWalkShoot();
                break;
            case BEHAVIOR_SATE.CROUCH_SHOOT_L:
                DoCrouchShootL();
                break;
            case BEHAVIOR_SATE.CROUCH_SHOOT_R:
                DoCrouchShootR();
                break;
            case BEHAVIOR_SATE.GRAB:
                DoGrab();
                break;
            case BEHAVIOR_SATE.HAMMER:
                DoHammer();
                break;
            case BEHAVIOR_SATE.BRANDISH:
                DoBrandish();
                break;
            case BEHAVIOR_SATE.LIFT_UP:
                DoLiftUp();
                break;
            case BEHAVIOR_SATE.DUOBI:
                DoDuoBi();
                break;
            case BEHAVIOR_SATE.HURT:
                DoHurt();
                break;
        }
    }

    private void DoIdle()
    {
        _timer += Time.deltaTime;
        if (_timer <= 0.5f)
            return;
        _timer = 0f;
        
        float dist = Vector3.Distance(transform.position, _player.transform.position);
        if (dist <= 3f)
        {
            Vector3 pos = _player.transform.position;
            pos.y = transform.position.y;
            transform.DOLookAt(pos, 0.5f);
            int randomValue = Random.Range(0, 3);
            switch (randomValue)
            {
                case 0:
                    SetState(BEHAVIOR_SATE.HAMMER);
                    break;
                case 1:
                    SetState(BEHAVIOR_SATE.GRAB);
                    break;
                case 2:
                    SetState(BEHAVIOR_SATE.BRANDISH);
                    break;
            }
        }
        else if (dist >= 6)
        {
            int randomValue = Random.Range(0, 3);
            switch (randomValue)
            {
                case 0:
                    SetState(BEHAVIOR_SATE.CROUCH_SHOOT_L);
                    break;
                case 1:
                    SetState(BEHAVIOR_SATE.CROUCH_SHOOT_R);
                    break;
                case 2:
                    SetState(BEHAVIOR_SATE.RUN);
                    break;
            }
        }
        else
        {
            SetState(BEHAVIOR_SATE.WALK);
        }
    }

    private void DoWalk() //-9.898558 0.04775382
    {
        if (_isFirstWalk && type == TYPE.FIRST_FLOOR)
        {
            _agent.SetDestination(_point2);
            float dist = Vector3.Distance(transform.position, _point2);
            _agent.stoppingDistance = 0.5f;
            if (dist <= 0.5f)
            {
                _isFirstWalk = false;
                SetState(BEHAVIOR_SATE.LIFT_UP);
            }
        }
        else
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position + new Vector3(0, 0.5f,0), transform.forward);
            Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), transform.forward, Color.red, 2);
            if (Physics.Raycast(ray, out hit, 1))
            {
                if (hit.transform.tag != "Player")
                {
                    _agent.stoppingDistance = 2f;
                    Debug.Log(hit.transform.name);
                    SetState(BEHAVIOR_SATE.HAMMER);
                    return;
                }
            }
            _agent.SetDestination(_player.transform.position);
            float dist = Vector3.Distance(transform.position, _player.transform.position);
            _agent.stoppingDistance = 2f;
            if (dist <= 2)
            {
                SetState(BEHAVIOR_SATE.IDLE);
            }
        }
    }

    private void DoRun() //
    {
        if (_isOnceRun && type == TYPE.FIRST_FLOOR)
        {
            _agent.SetDestination(_point1);
            float dist = Vector3.Distance(transform.position, _point1);
            _agent.stoppingDistance = 0.5f;
            if (dist <= 0.5f)
            {
                _isOnceRun = false;
                SetState(BEHAVIOR_SATE.BRANDISH);
            }
        }
        else
        {
            _agent.SetDestination(_player.transform.position);
            float dist = Vector3.Distance(transform.position, _player.transform.position);
            _agent.stoppingDistance = 2f;
            if (dist <= 2)
            {
                SetState(BEHAVIOR_SATE.IDLE);
            }
        }
    }

    private void DoJump()
    {
        _timer += Time.deltaTime;
        if (_timer >= 1.14f)
        {
            _timer = 0f;
            _isCanHurt = true;
            SetState(BEHAVIOR_SATE.IDLE);
        }
    }

    private void DoCrouchL()
    {

    }

    private void DoCrouchR()
    {

    }

    private void DoWalkShoot()
    {
        _agent.SetDestination(_player.transform.position);
        float dist = Vector3.Distance(transform.position, _player.transform.position);
        _agent.stoppingDistance = 2f;
        if (dist <= 2)
        {
            SetState(BEHAVIOR_SATE.IDLE);
        }
    }

    private void DoCrouchShootR()
    {
        Vector3 pos = _player.transform.position;
        pos.y = transform.position.y;
        transform.LookAt(pos);
        _timer += Time.deltaTime;
        if (_timer >= 4)
        {
            _timer = 0f;
            SetState(BEHAVIOR_SATE.IDLE);
        }
    }

    private void DoCrouchShootL()
    {
        Vector3 pos = _player.transform.position;
        pos.y = transform.position.y;
        transform.LookAt(pos);
        _timer += Time.deltaTime;
        if (_timer >= 4)
        {
            _timer = 0f;
            SetState(BEHAVIOR_SATE.IDLE);
        }
    }

    private void DoGrab()
    {
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("attack_hit1") && info.normalizedTime >= 1)
        {
            SetState(BEHAVIOR_SATE.IDLE);
        }
    }

    private void DoHammer()
    {
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("attack_hit2") && info.normalizedTime >= 1)
        {
            SetState(BEHAVIOR_SATE.IDLE);
        }
    }

    private void DoBrandish()
    {
        if (_isFirstBrandish)
        {
            AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
            if (info.IsName("attack_hit3") && info.normalizedTime >= 1)
            {
                _isFirstBrandish = false;
                SetState(BEHAVIOR_SATE.WALK);
            }
        }
        else
        {
            AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
            if (info.IsName("attack_hit3") && info.normalizedTime >= 1)
            {
                SetState(BEHAVIOR_SATE.IDLE);
            }
        }
    }

    private void DoLiftUp()
    {
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("attack_hit4") && info.normalizedTime >= 1)
        {
            SetState(BEHAVIOR_SATE.JUMP);
            transform.DOJump(_jumpToPoint[1], 0.5f, 0, 1.14f);
        }
    }

    private void DoDuoBi()
    {
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("DUOBI1") && info.normalizedTime >= 1)
        {
            SetState(BEHAVIOR_SATE.IDLE);
        }
    }

    private void DoHurt()
    {
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("beaten_left") && info.normalizedTime >= 1)
        {
            SetState(BEHAVIOR_SATE.IDLE);
        }
    }

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        //test
        Init();
    }

    private void Update()
    {
        UpdateState();
    }
}
