//ZmbController.cs
//TEOT_ONLINE
//
//Create by WangJie on 1/10/2018 10:39 AM
//Description: 
//

using RootMotion.Dynamics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZmbController : MonoBehaviour 
{
    private const float MAX_HP = 50;
    private float _curHp =MAX_HP;
    private float _walkSpeed = 2f;
    private float _runSpeed = 4f;

    private float _timer = 0f;

    private bool _isDead;
    private bool _isFirstCreep;

    private Vector3 _initialPos;
    private Vector3 _targetPos;

    private GameObject _target;
    private GameObject _rayPoint;

    private Animator _anim;
    private NavMeshAgent _agent;

    //public GameObject cube;

    public STATE curState;
    public ZMB_TYPE zmbType;
    public BEHAVIOR_STATE lastBehaviorState = BEHAVIOR_STATE.EMPTY;

    public enum ZMB_TYPE
    {
        KNIFE,//刀
        HAMMER,//锤子
        GENERAL, //普通
    }

    public enum BEHAVIOR_STATE
    {
        EMPTY,
        IDLE,
        WALK,
        RUN,
        SKILL,
    }

    public enum STATE
    {
        IDLE,
        WALK,
        RUN,
        JUMP,                             //跳
        CREEP,                            //爬
        PATROL,                          //巡逻
        CREEP_IDLE,                    //趴着待机
        CROSS_JUMP,                   //跳跃障碍
        LOOK_BACK,                    //后看
        LOOK_AROUND,               //左右看
        OBSERVE,                        //观察

        SMELL,                           //闻东西
        BITE,                              //咬
        JUMP_BITE,                     //跳咬
        CREEP_GRAB,                  //趴着抓
        CREEP_GRAB_FORCE,       //趴着用力抓
        GRAB,                             //抓
        GRAB_FAST,                     //快速抓
        WRESTLING,                    //摔

        ARM1,
        ARM2,
        ARM3,
        ARM4,
        ARM5,
        ARM6,
        ARM7,

        KNIFE1,
        KNIFE2,
        KNIFE3,
        KNIFE4,
        KNIFE5,

        HAMMER1,
        HAMMER2,
        HAMMER3,
        HAMMER4,
        HAMMER5,
        HAMMER6,
        HAMMER7,
        HAMMER8,
        HAMMER9,

        BEATEN_WALK_SPASTICITY,
        BEATEN_RUN_SPASTICITY,
        BEATEN_ATTACK_SPASTICITY,
        BEATEN_LEFT,                  //受伤左 
        BENTEN_RIGHT,               //受伤右
        PLAY_DEAD,                    //装死
        DEATH,
    }

    public void Init()
    {
        _initialPos = transform.position;
        _target = GameObject.Find("ThirdPersonController");
        _rayPoint = transform.Find("ZMB_New/RayPoint").gameObject;
        transform.root.Find("PuppetMaster").GetComponent<PuppetMaster>().mode = PuppetMaster.Mode.Kinematic;
    }

    public void SetTargetPos(Vector3 v)
    {
        if (v != _targetPos)
            _targetPos = v;
    }

    public void OnHurt(float damage)
    {
        if (_isDead)
            return;

        _curHp -= damage;
        Debug.Log(_curHp);
        //if (zmbType == ZMB_TYPE.GENERAL && _curHp <= 20)
        //{
        //    if (!_isFirstCreep)
        //    {
        //        _isFirstCreep = true;
        //        SetState(STATE.PLAY_DEAD);
        //    }
        //}

        //switch (lastBehaviorState)
        //{
        //    case BEHAVIOR_STATE.EMPTY:
        //        break;
        //    case BEHAVIOR_STATE.IDLE:
        //        SetState(STATE.BEATEN_LEFT);
        //        break;
        //    case BEHAVIOR_STATE.WALK:
        //        SetState(STATE.BEATEN_LEFT);
        //        //Vector3 posWalk = transform.position + transform.forward * -2;
        //        //SetTargetPos(posWalk);
        //        //SetState(STATE.BEATEN_WALK_SPASTICITY);
        //        break;
        //    case BEHAVIOR_STATE.RUN:
        //        SetState(STATE.BEATEN_LEFT);
        //        //Vector3 posRun = transform.position + transform.forward * -2;
        //        //SetTargetPos(posRun);
        //        //SetState(STATE.BEATEN_RUN_SPASTICITY);
        //        break;
        //    case BEHAVIOR_STATE.SKILL:
        //        SetState(STATE.BEATEN_LEFT);
        //        //Vector3 posSkill = transform.position + transform.forward * -2;
        //        //SetTargetPos(posSkill);
        //        //SetState(STATE.BEATEN_ATTACK_SPASTICITY);
        //        break;
        //}

        //lastBehaviorState = BEHAVIOR_STATE.EMPTY;

        if (_curHp <= 0)
        {
            _curHp = 0;
            _isDead = true;
            SetState(STATE.DEATH);
            transform.root.Find("PuppetMaster").GetComponent<PuppetMaster>().mode = PuppetMaster.Mode.Active;
        }
    }

    public void SetState(STATE state)
    {
        if (state == curState)
            return;
        curState = state;

        _agent.enabled = true;
        _agent.angularSpeed = 1000;
        RestAnimator();

        //transform.parent = null;

        switch (curState)
        {
            case STATE.IDLE:
                lastBehaviorState = BEHAVIOR_STATE.IDLE;
                _agent.enabled = false;
                _anim.SetBool("isIdleBool", true);
                break;
            case STATE.WALK:
                lastBehaviorState = BEHAVIOR_STATE.RUN;
                _agent.speed = _walkSpeed;
                _anim.SetBool("isWalkBool", true);
                break;
            case STATE.RUN:
                lastBehaviorState = BEHAVIOR_STATE.RUN;
                _agent.speed = _runSpeed;
                _anim.SetBool("isRunBool", true);
                break;
            case STATE.JUMP:
                _agent.enabled = false;
                _anim.SetBool("isJumpBool", true);
                break;
            case STATE.PATROL:
                lastBehaviorState = BEHAVIOR_STATE.WALK;
                _agent.speed = _walkSpeed;
                _anim.SetBool("isWalkBool", true);
                break;
            case STATE.CREEP:
                _agent.speed = 1f;
                _anim.SetBool("isCreepBool", true);
                break;
            case STATE.CREEP_IDLE:
                _agent.enabled = false;
                _anim.SetBool("isCreepBool", true);
                break;
            case STATE.CROSS_JUMP:
                _agent.enabled = false;
                _anim.SetBool("isCrossJumpBool", true);
                break;
            case STATE.LOOK_BACK:
                lastBehaviorState = BEHAVIOR_STATE.IDLE;
                _agent.enabled = false;
                _anim.SetBool("isLookBackBool", true);
                break;
            case STATE.LOOK_AROUND:
                lastBehaviorState = BEHAVIOR_STATE.IDLE;
                _agent.enabled = false;
                _anim.SetBool("isLookAroundBool", true);
                break;
            case STATE.OBSERVE:
                lastBehaviorState = BEHAVIOR_STATE.WALK;
                _agent.speed = _walkSpeed;
                _anim.SetBool("isWalkBool", true);
                break;
            case STATE.SMELL:
                lastBehaviorState = BEHAVIOR_STATE.IDLE;
                _agent.enabled = false;
                _anim.SetBool("isSmellBool", true);
                break;
            //=============================空手
            case STATE.BITE:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetTrigger("isBiteTrigger");
                break;
            case STATE.JUMP_BITE:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetTrigger("isJumpBiteTrigger");
                break;
            case STATE.CREEP_GRAB:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetTrigger("isCreepGrabTrigger");
                break;
            case STATE.CREEP_GRAB_FORCE:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetTrigger("isCreepGrabForceTrigger");
                break;
            case STATE.GRAB:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetTrigger("isGrabTrigger");
                break;
            case STATE.GRAB_FAST:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetTrigger("isGrabFastTrigger");
                break;
            case STATE.WRESTLING:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetTrigger("isWrestlingTrigger");
                break;
            case STATE.ARM1:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetTrigger("isArm1Trigger");
                break;
            case STATE.ARM2:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetTrigger("isArm2Trigger");
                break;
            case STATE.ARM3:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetTrigger("isArm3Trigger");
                break;
            case STATE.ARM4:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetTrigger("isArm4Trigger");
                break;
            case STATE.ARM5:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetTrigger("isArm5Trigger");
                break;
            case STATE.ARM6:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetTrigger("isArm6Trigger");
                break;
            case STATE.ARM7:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetTrigger("isArm7Trigger");
                break;
                //=============================拿刀
            case STATE.KNIFE1:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetTrigger("isKnife1Trigger");
                break;
            case STATE.KNIFE2:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetTrigger("isKnife2Trigger");
                break;
            case STATE.KNIFE3:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetTrigger("isKnife3Trigger");
                break;
            case STATE.KNIFE4:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetTrigger("isKnife4Trigger");
                break;
            case STATE.KNIFE5:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetTrigger("isKnife5Trigger");
                break;
            //=============================锤子
            case STATE.HAMMER1:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetBool("isHammer1Bool", true);
                break;
            case STATE.HAMMER2:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetBool("isHammer2Bool",true);
                break;
            case STATE.HAMMER3:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetBool("isHammer3Bool", true);
                break;
            case STATE.HAMMER4:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetBool("isHammer4Bool", true);
                break;
            case STATE.HAMMER5:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetBool("isHammer5Bool", true);
                break;
            case STATE.HAMMER6:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetBool("isHammer6Bool", true);
                break;
            case STATE.HAMMER7:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetBool("isHammer7Bool", true);
                break;
            case STATE.HAMMER8:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetBool("isHammer8Bool", true);
                break;
            case STATE.HAMMER9:
                lastBehaviorState = BEHAVIOR_STATE.SKILL;
                _agent.enabled = false;
                _anim.SetBool("isHammer9Bool", true);
                break;

            case STATE.BEATEN_WALK_SPASTICITY:
                _agent.speed = 1f;
                _agent.angularSpeed = 0;
                _anim.SetTrigger("isBeatenWalkSpasticityTrigger");
                break;
            case STATE.BEATEN_RUN_SPASTICITY:
                _agent.speed = 1f;
                _agent.angularSpeed = 0;
                _anim.SetTrigger("isBeatenRunSpasticityTrigger");
                break;
            case STATE.BEATEN_ATTACK_SPASTICITY:
                _agent.speed = 1f;
                _agent.angularSpeed = 0;
                _anim.SetTrigger("isBeatenAttackSpasticityTrigger");
                break;
            case STATE.BEATEN_LEFT:
                _agent.enabled = false;
                _anim.SetTrigger("isBeatenLeftTrigger");
                break;
            case STATE.BENTEN_RIGHT:
                _agent.enabled = false;
                _anim.SetTrigger("isBeatenRightTrigger");
                break;
            case STATE.PLAY_DEAD:
                _agent.enabled = false;
                _anim.SetTrigger("isPlayDeadTrigger");
                break;
            case STATE.DEATH:
                _agent.enabled = false;
                if (zmbType == ZMB_TYPE.GENERAL)
                {
                    _anim.SetTrigger("isDeath1Front");
                }
                else
                {
                    _anim.SetTrigger("isDeathTrigger");
                }
                //_anim.SetTrigger("isDeath1BackATrigger");
                break;
        }
    }

    private void RestAnimator()
    {
        _anim.SetBool("isIdleBool", false);
        _anim.SetBool("isWalkBool", false);
        _anim.SetBool("isRunBool", false);
        _anim.SetBool("isJumpBool", false);
        _anim.SetBool("isCreepBool", false);
        _anim.SetBool("isCreepIdleBool", false);
        _anim.SetBool("isCrossJumpBool", false);
        _anim.SetBool("isLookBackBool", false);
        _anim.SetBool("isLookAroundBool", false);
        _anim.SetBool("isSmellBool", false);

        _anim.SetBool("isHammer1Bool", false);
        _anim.SetBool("isHammer2Bool", false);
        _anim.SetBool("isHammer3Bool", false);
        _anim.SetBool("isHammer4Bool", false);
        _anim.SetBool("isHammer5Bool", false);
        _anim.SetBool("isHammer6Bool", false);
        _anim.SetBool("isHammer7Bool", false);
        _anim.SetBool("isHammer8Bool", false);
        _anim.SetBool("isHammer9Bool", false);
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
            case STATE.JUMP:
                DoJump();
                break;
            case STATE.PATROL:
                DoPatrol();
                break;
            case STATE.CREEP:
                DoCreep();
                break;
            case STATE.CREEP_IDLE:
                DoCreepIdle();
                break;
            case STATE.CROSS_JUMP:
                DoCrossJump();
                break;
            case STATE.LOOK_BACK:
                DoLookBack();
                break;
            case STATE.LOOK_AROUND:
                DoLookAround();
                break;
            case STATE.OBSERVE:
                DoObserve();
                break;
            case STATE.SMELL:
                DoSmell();
                break;
                //==========================空手
            case STATE.BITE:
                DoBite();
                break;
            case STATE.JUMP_BITE:
                DoJumpBite();
                break;
            case STATE.CREEP_GRAB:
                DoCreepGrab();
                break;
            case STATE.CREEP_GRAB_FORCE:
                DoCreepGrabFroce();
                break;
            case STATE.GRAB:
                DoSkill("grab");
                break;
            case STATE.GRAB_FAST:
                DoSkill("grabfast");
                break;
            case STATE.WRESTLING:
                DoWrestling();
                break;
            case STATE.ARM1:
                DoSkill("attack_arm1");
                break;
            case STATE.ARM2:
                DoSkill("attack_arm2");
                break;
            case STATE.ARM3:
                DoSkill("attack_arm3");
                break;
            case STATE.ARM4:
                DoSkill("attack_arm4");
                break;
            case STATE.ARM5:
                DoSkill("attack_arm5");
                break;
            case STATE.ARM6:
                DoSkill("attack_arm6");
                break;
            case STATE.ARM7:
                DoSkill("attack_arm7");
                break;
            //==========================拿刀
            case STATE.KNIFE1:
                DoSkill("attack_knife1");
                break;
            case STATE.KNIFE2:
                DoSkill("attack_knife2");
                break;
            case STATE.KNIFE3:
                DoSkill("attack_knife3");
                break;
            case STATE.KNIFE4:
                DoSkill("attack_knife4");
                break;
            case STATE.KNIFE5:
                DoSkill("attack_knife5");
                break;
            //==========================锤子
            case STATE.HAMMER1:
                DoSkill("attack_hammer1");
                break;
            case STATE.HAMMER2:
                DoSkill("attack_hammer2");
                break;
            case STATE.HAMMER3:
                DoSkill("attack_hammer3");
                break;
            case STATE.HAMMER4:
                DoSkill("attack_hammer4");
                break;
            case STATE.HAMMER5:
                DoSkill("attack_hammer5");
                break;
            case STATE.HAMMER6:
                DoSkill("attack_hammer6");
                break;
            case STATE.HAMMER7:
                DoSkill("attack_hammer7");
                break;
            case STATE.HAMMER8:
                DoSkill("attack_hammer8");
                break;
            case STATE.HAMMER9:
                DoSkill("attack_hammer9");
                break;

            case STATE.BEATEN_WALK_SPASTICITY:
                //DoSkill("BeatenWalkSpasticity");
                DoBeatenWalkSpasticity();
                break;
            case STATE.BEATEN_RUN_SPASTICITY:
                //DoSkill("BeatenRunSpasticity");
                DoBeatenRunSpasticity();
                break;
            case STATE.BEATEN_ATTACK_SPASTICITY:
                //DoSkill("BeatenAttackSpasticity");
                DoBeatenAttackSpasticity();
                break;
            case STATE.BEATEN_LEFT:
                break;
            case STATE.BENTEN_RIGHT:
                break;
            case STATE.PLAY_DEAD:
                DoPlayDead();
                break;
            case STATE.DEATH:
                break;
        }
    }

    private void DoIdle()
    {
        _timer += Time.deltaTime;
        if (_timer <= 1f)
            return;
        _timer = 0f;


        float dist = Vector3.Distance(transform.position, _target.transform.position);
        if (dist <= 2)
        {
            Vector3 pos = _target.transform.position + _target.transform.right * -1f;
            SetTargetPos(pos);
            SetState(STATE.OBSERVE);
            
        }
        else if (dist <= 4)
        {
            SetState(STATE.WALK);
        }
        else if(dist <= 10)
        {
            SetState(STATE.RUN);
        }
    }

    private void DoWalk()
    {
        _agent.SetDestination(_target.transform.position);
        float dist = Vector3.Distance(transform.position, _target.transform.position);

        if (!_agent.isOnNavMesh)
        {
            SetState(STATE.PATROL);
            return;
        }

        if (dist >= 5)
        {
            SetState(STATE.RUN);
        }

        switch (zmbType)
        {
            case ZMB_TYPE.KNIFE:
                if (dist <= 1.5f)
                    SetState(STATE.KNIFE2);
                break;
            case ZMB_TYPE.HAMMER:
                if (dist <= 1.8f)
                    SetState(STATE.HAMMER5);
                break;
            case ZMB_TYPE.GENERAL:
                if (dist <= 1f)
                    SetState(STATE.ARM1);
                break;
        }
    }

    private void DoRun()
    {
        _agent.SetDestination(_target.transform.position);

        if (!_agent.isOnNavMesh)
        {
            SetState(STATE.PATROL);
            return;
        }

        float dist = Vector3.Distance(transform.position, _target.transform.position);
        switch (zmbType)
        {
            case ZMB_TYPE.KNIFE:
                if (dist <= 1.5)
                    SetState(STATE.KNIFE3);
                break;
            case ZMB_TYPE.HAMMER:
                if (dist <= 1.8f)
                    SetState(STATE.HAMMER7);
                break;
            case ZMB_TYPE.GENERAL:
                //if (dist <= 2)
                //    SetState(STATE.JUMP_BITE);
                if(dist <= 1f)
                    SetState(STATE.ARM4);
                break;
        }
    }

    private void DoJump()
    {

    }

    private void DoPatrol()
    {
        _agent.SetDestination(_targetPos);

        CheckTarget();

        if (!_agent.hasPath)
        {
            RandomPatrolPos();
            //Debug.Log("过不去!");
        }

        float dist = Vector3.Distance(transform.position, _targetPos);
        if (dist <= 0.2f)
        {
            RandomPatrolPos();
            SetState(STATE.LOOK_AROUND);
        }
    }
    
    private void RandomPatrolPos()
    {
        SetTargetPos(_initialPos + new Vector3(UnityEngine.Random.Range(-5, 5), 0, UnityEngine.Random.Range(-5, 5)));
    }

    private void DoCreep()
    {
        _agent.SetDestination(_target.transform.position);
        float dist = Vector3.Distance(transform.position, _target.transform.position);
        if (dist <= 1)
        {
            SetState(STATE.CREEP_IDLE);
        }

        if (dist >= 3)
        {
            _agent.speed = 4;
        }
        else
        {
            _agent.speed = 2;
        }
    }

    private void DoCreepIdle()
    {
        _timer += Time.deltaTime;
        if (_timer <= 0.2f)
            return;
        _timer = 0;
        float dist = Vector3.Distance(transform.position, _target.transform.position);
        if (dist >= 1.5f)
        {
            SetState(STATE.CREEP);
        }
        else
        {
            int skillRandom = UnityEngine.Random.Range(0, 2);
            switch (skillRandom)
            {
                case 0:
                    SetState(STATE.CREEP_GRAB);
                    break;
                case 1:
                    SetState(STATE.CREEP_GRAB_FORCE);
                    break;
            }
        }
    }

    private void DoCrossJump()
    {

    }

    private void DoLookBack()
    {

    }

    private void DoLookAround()
    {
        CheckTarget();

        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("rest_zuoyou") && info.normalizedTime >= 1)
        {
            SetState(STATE.PATROL);
        }
    }

    private void DoObserve()
    {
        _agent.SetDestination(_targetPos);
        transform.LookAt(_target.transform);
        if (!_agent.isOnNavMesh)
        {
            SetState(STATE.PATROL);
            return;
        }

        float dist = Vector3.Distance(transform.position, _targetPos);
        if (dist <= 0.2f)
        {
            SelectedSkill();
        }
    }

    private void SelectedSkill()
    {
        switch (zmbType)
        {
            case ZMB_TYPE.KNIFE:
                int skillKnifeRandom = UnityEngine.Random.Range(0, 5);
                switch (skillKnifeRandom)
                {
                    case 0:
                        SetState(STATE.KNIFE1);
                        break;
                    case 1:
                        SetState(STATE.KNIFE2);
                        break;
                    case 2:
                        SetState(STATE.KNIFE3);
                        break;
                    case 3:
                        SetState(STATE.KNIFE4);
                        break;
                    case 4:
                        SetState(STATE.KNIFE5);
                        break;
                }
                break;
            case ZMB_TYPE.HAMMER:
                int skillHammerRandom = UnityEngine.Random.Range(0, 9);
                switch (skillHammerRandom)
                {
                    case 0:
                        SetState(STATE.HAMMER1);
                        break;
                    case 1:
                        SetState(STATE.HAMMER2);
                        break;
                    case 2:
                        SetState(STATE.HAMMER3);
                        break;
                    case 3:
                        SetState(STATE.HAMMER4);
                        break;
                    case 4:
                        SetState(STATE.HAMMER5);
                        break;
                    case 5:
                        SetState(STATE.HAMMER6);
                        break;
                    case 6:
                        SetState(STATE.HAMMER7);
                        break;
                    case 7:
                        SetState(STATE.HAMMER8);
                        break;
                    case 8:
                        SetState(STATE.HAMMER9);
                        break;
                }
                break;
            case ZMB_TYPE.GENERAL:
                int skillRandom = UnityEngine.Random.Range(0, 7);
                switch (skillRandom)
                {
                    case 0:
                        SetState(STATE.GRAB);
                        break;
                    case 1:
                        SetState(STATE.GRAB_FAST);
                        break;
                    case 2:
                        SetState(STATE.WRESTLING);
                        break;
                    case 3:
                        SetState(STATE.ARM1);
                        break;
                    case 4:
                        SetState(STATE.ARM7);
                        break;
                    case 5:
                        SetState(STATE.ARM3);
                        break;
                    case 6:
                        SetState(STATE.ARM4);
                        break;
                    case 7:
                        SetState(STATE.ARM2);
                        break;
                    case 8:
                        SetState(STATE.ARM5);
                        break;
                    case 9:
                        SetState(STATE.ARM6);
                        break;
                }
                break;
        }
    }

    private void DoSmell()
    {

    }

    private void DoBite()
    {
        Vector3 pos = _target.transform.position;
        pos.y = transform.position.y;
        transform.LookAt(pos);

        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("attackBite1") && info.normalizedTime >= 1)
        {
            transform.parent = null;
            SetState(STATE.IDLE);
            return;
        }

        transform.SetParent(_target.transform);
    }

    private void DoJumpBite()
    {
        Vector3 pos = _target.transform.position;
        pos.y = transform.position.y;
        transform.LookAt(pos);

        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("attackBite2") && info.normalizedTime >= 1)
        {
            transform.parent = null;
            SetState(STATE.IDLE);
            return;
        }

        float dist = Vector3.Distance(transform.position, pos);
        if (dist <= 0.5f)
        {
            //让僵尸骑在玩家身上，让僵尸成为玩家的子物体
            transform.SetParent(_target.transform);
            return;
        }
        transform.Translate(Vector3.forward * 4 * Time.deltaTime, Space.Self);
    }

    private void DoWrestling()
    {
        //把玩家搬到身后去

        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("guojianshuai") && info.normalizedTime >= 1)
        {
            SetState(STATE.IDLE);
        }
    }

    private void DoPlayDead()
    {
        _timer += Time.deltaTime;

        if (_timer >= 2)
        {
            _timer = 0;
            SetState(STATE.CREEP);
        }
    }

    private void DoCreepGrab()
    {
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("pa_attack_01") && info.normalizedTime >= 1)
        {
            SetState(STATE.CREEP_IDLE);
        }
    }

    private void DoCreepGrabFroce()
    {
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("pa_attack_02") && info.normalizedTime >= 1)
        {
            SetState(STATE.CREEP_IDLE);
        }
    }

    private void DoBeatenWalkSpasticity()
    {
        _agent.SetDestination(_targetPos);

        if (!_agent.hasPath)
        {
            SetState(STATE.IDLE);
            return;
        }

        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("BeatenWalkSpasticity") && info.normalizedTime  > 1)
        {
            SetState(STATE.IDLE);
        }
    }

    private void DoBeatenRunSpasticity()
    {
        _agent.SetDestination(_targetPos);

        if (!_agent.hasPath)
        {
            SetState(STATE.IDLE);
            return;
        }

        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("BeatenRunSpasticity") && info.normalizedTime > 1)
        {
            SetState(STATE.IDLE);
        }
    }

    private void DoBeatenAttackSpasticity()
    {
        _agent.SetDestination(_targetPos);

        if (!_agent.hasPath)
        {
            SetState(STATE.IDLE);
            return;
        }

        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("BeatenAttackSpasticity") && info.normalizedTime > 1)
        {
            SetState(STATE.IDLE);
        }
    }

    private void DoSkill(string name)
    {
        Vector3 pos = _target.transform.position;
        pos.y = transform.position.y;
        transform.LookAt(pos);

        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName(name) && info.normalizedTime >= 0.9f)
        {
            SetState(STATE.IDLE);
        }
    }

    private void CheckTarget()
    {

        RaycastHit hit;
        Ray ray = new Ray(_rayPoint.transform.position, (_target.transform.position + new Vector3(0, 1f, 0)) - _rayPoint.transform.position);
        if (Physics.Raycast(ray, out hit, 10))
        {
        //Debug.DrawRay(_rayPoint.transform.position, (_target.transform.position + new Vector3(0, 1f, 0)) - _rayPoint.transform.position, Color.blue, 0.5f);
            //Debug.Log(hit.transform.name);
            if (hit.transform.name == "ThirdPersonController")
            {
                //float dist = Vector3.Distance(transform.position, _target.transform.position);
                //if (dist <= 6)
                //{
                    SetState(STATE.WALK);
                //}
            }

        }
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }

    void Start ()
	{
        //test
        Init();
        SetState(STATE.PATROL);
	}
	
	void Update () 
	{
        UpdateState();

        if (Input.GetKeyDown(KeyCode.K))
        {
            OnHurt(2);
        }
	}
}
