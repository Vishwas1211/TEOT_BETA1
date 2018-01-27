//RabController.cs
//TEOT_ONLINE
//
//Create by WangJie on 9/18/2017 5:51 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class RabController : MonoBehaviour
{
    private const string PATROL_POINT_PATH = "Prefabs/Character/Enemy/RAB/RabPatrolPath";
    private const string RAY_POINT = "RayPoint";
    private const string CATCH_POINT = "Rab/RAB_SpineJ_1/RAB_SpineJ_2/RAB_SpineJ_3/RAB_SpineJ_4/RAB_SpineJ_5/RAB_SpineJ_6/" +
        "RAB_SpineJ_7/RAB_BackRJ1/RAB_BackRJ2/RAB_BackRJ3/RAB_ArmRJ1/RAB_ArmRJ2/RAB_ArmRJ3/RAB_PalmRJ1/CatchPlayerPoint";
    private const string CLIMB_ROTATE_PATH = "Prefabs/Character/Enemy/RAB/RabClimbRotatePoints";
    private const string LOST_PLAYER_POINT_PATH = "Prefabs/Character/Enemy/RAB/RabPlayerLostPoint";
    private const string WALL_JUMP_POINT = "Prefabs/Character/Enemy/RAB/RabWallJumpPoints";
    private const string WALL_TO_GROUND = "Prefabs/Character/Enemy/RAB/RabWallToGroundPoints";

    private const float MAX_HP = 100;
    private float _curHp = MAX_HP;
    private float _moveSpeed = 3;
    private float _runSpeed = 7f;
    private float _timer = 0f;

    private int _curTargetPointIndex = 0;

    private int _jumpWallIndex = 0;

    private bool _isDead;
    private bool _isCanAttract;

    private GameObject _lostPlayerPoint;
    private GameObject _catchPoint;
    private GameObject _rayPoint;
    private GameObject _target;
    private GameObject _patrolPointPrefab;
    private GameObject[] _patrolPoints;
    private GameObject _rabClimbRotatePointsPrefab;
    /// <summary>
    /// 向墙上跳的数组
    /// </summary>
    private GameObject[] _rabClimbRotatePoints;

    private GameObject _wallJumpPrefab;
    /// <summary>
    /// 从一面墙向另一面墙跳数组
    /// </summary>
    private GameObject[] _wallJumpPoints;

    private GameObject _wallToGroundPrefab;
    //
    // 摘要:
    //     ///
    //     The tag of this game object.
    //     ///
    private GameObject[] _wallToGroundPoints;

    #region RabClimbRotatePoints
    private Vector3[] _rabRotatePoints0 = new Vector3[8];
    private Vector3[] _rabRotatePoints1 = new Vector3[8];
    private Vector3[] _rabRotatePoints2 = new Vector3[8];
    private Vector3[] _rabRotatePoints3 = new Vector3[8];

    private Vector3[] _wallJumpPoint1;
    private Vector3[] _wallJumpPoint2;
    private Vector3[] _wallJumpPoint3;
    private Vector3[] _wallJumpPoint4;

    private Vector3[] _wallToGroundPoint1;
    private Vector3[] _wallToGroundPoint2;
    private Vector3[] _wallToGroundPoint3;
    private Vector3[] _wallToGroundPoint4;
    #endregion

    private Vector3 _curTargetPoint;
    private Vector3 _LostTrackPoint;

    private RabCatchTrigger _rabCatchTrigger;
    private RabBulletManager _rabBulletManager;

    [Range(0.1f, 1.5f)]
    public float minArriveDistance = 0.2f;
    private Animator _anim;
    private NavMeshAgent _agent;

    public RAB_STATE curState;

    public enum RAB_STATE
    {
        IDLE,
        WALK,
        RUN,
        CHASE_TO_POINT, //追到点
        WALK_TO_ROTATE,
        JUMP_TO_WALL,
        WALL_TO_WALL,
        WALK_ON_WALL,
        WALL_TO_GROUND,
        WALK_TO_FIASH,
        ATTRACT_IDLE,       //吸引待机
        CLIMB,
        PATROL,
        JUMP,
        SHOOT,
        JET,
        HIT,
        CRUSH,
        CATCH,
        FOLLOW,
        ABLEPSIA,
        DEATH,
        RENORN,
    }

    public void Init()
    {
        _rabBulletManager = gameObject.AddComponent<RabBulletManager>();
        _rabBulletManager.Init();
        _lostPlayerPoint = UtilFunction.ResourceLoad(LOST_PLAYER_POINT_PATH);
        _catchPoint = transform.Find(CATCH_POINT).gameObject;
        _rabCatchTrigger = _catchPoint.AddComponent<RabCatchTrigger>();
        _rayPoint = transform.Find(RAY_POINT).gameObject;
        _patrolPointPrefab = UtilFunction.ResourceLoad(PATROL_POINT_PATH);
        _patrolPoints = new GameObject[_patrolPointPrefab.transform.childCount];
        _target = GameObject.Find("ThirdPersonController");

        for (int i = 0; i < _patrolPointPrefab.transform.childCount; i++)
        {
            _patrolPoints[i] = _patrolPointPrefab.transform.GetChild(i).gameObject;
        }
        SetTargetPoint(_patrolPoints[0].transform.position);


        InitPoint();
        LoadWallJumpPoints();

        _agent.speed = _moveSpeed;
        SetState(RAB_STATE.PATROL);
    }

    private void InitPoint()
    {
        _rabClimbRotatePointsPrefab = UtilFunction.ResourceLoad(CLIMB_ROTATE_PATH);
        _rabClimbRotatePoints = new GameObject[_rabClimbRotatePointsPrefab.transform.childCount];
        for (int i = 0; i < _rabClimbRotatePointsPrefab.transform.childCount; i++)
        {
            _rabClimbRotatePoints[i] = _rabClimbRotatePointsPrefab.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < _rabClimbRotatePoints[0].transform.childCount; i++)
        {
            _rabRotatePoints0[i] = _rabClimbRotatePoints[0].transform.GetChild(i).position;
            _rabRotatePoints1[i] = _rabClimbRotatePoints[1].transform.GetChild(i).position;
            _rabRotatePoints2[i] = _rabClimbRotatePoints[2].transform.GetChild(i).position;
            _rabRotatePoints3[i] = _rabClimbRotatePoints[3].transform.GetChild(i).position;
        }


    }

    private void LoadWallJumpPoints()
    {
        _wallJumpPrefab = UtilFunction.ResourceLoad(WALL_JUMP_POINT);
        _wallJumpPoints = new GameObject[_wallJumpPrefab.transform.childCount];

        for (int i = 0; i < _wallJumpPrefab.transform.childCount; i++)
        {
            _wallJumpPoints[i] = _wallJumpPrefab.transform.GetChild(i).gameObject;
        }

        _wallJumpPoint1 = new Vector3[_wallJumpPoints[0].transform.childCount];
        _wallJumpPoint2 = new Vector3[_wallJumpPoints[1].transform.childCount];
        _wallJumpPoint3 = new Vector3[_wallJumpPoints[2].transform.childCount];
        _wallJumpPoint4 = new Vector3[_wallJumpPoints[3].transform.childCount];

        for (int i = 0; i < _wallJumpPoint1.Length; i++)
        {
            _wallJumpPoint1[i] = _wallJumpPoints[0].transform.GetChild(i).position;
            _wallJumpPoint2[i] = _wallJumpPoints[1].transform.GetChild(i).position;
            _wallJumpPoint3[i] = _wallJumpPoints[2].transform.GetChild(i).position;
            _wallJumpPoint4[i] = _wallJumpPoints[3].transform.GetChild(i).position;
        }

        _wallToGroundPrefab = UtilFunction.ResourceLoad(WALL_TO_GROUND);
        _wallToGroundPoints = new GameObject[_wallToGroundPrefab.transform.childCount];
        for (int i = 0; i < _wallToGroundPrefab.transform.childCount; i++)
        {
            _wallToGroundPoints[i] = _wallToGroundPrefab.transform.GetChild(i).gameObject;
        }

        _wallToGroundPoint1 = new Vector3[_wallToGroundPoints[0].transform.childCount];
        _wallToGroundPoint2 = new Vector3[_wallToGroundPoints[1].transform.childCount];
        _wallToGroundPoint3 = new Vector3[_wallToGroundPoints[2].transform.childCount];
        _wallToGroundPoint4 = new Vector3[_wallToGroundPoints[3].transform.childCount];

        for (int i = 0; i < _wallToGroundPoints[0].transform.childCount; i++)
        {
            _wallToGroundPoint1[i] = _wallToGroundPoints[0].transform.GetChild(i).position;
            _wallToGroundPoint2[i] = _wallToGroundPoints[1].transform.GetChild(i).position;
            _wallToGroundPoint3[i] = _wallToGroundPoints[2].transform.GetChild(i).position;
            _wallToGroundPoint4[i] = _wallToGroundPoints[3].transform.GetChild(i).position;
        }
    }

    public void SetCanAttract()
    {
        _isCanAttract = true;
    }

    public void OnAblepsia() //收到闪光弹攻击
    {
        SetState(RAB_STATE.ABLEPSIA);
    }

    public void SetTargetPoint(Vector3 v)
    {
        if (v == Vector3.zero)
            return;
        _curTargetPoint = v;
    }

    public void OnHurt(float damage)
    {
        if (_isDead)
            return;
        _curHp -= damage;
        if (_curHp <= 0)
        {
            _curHp = 0f;
            _isDead = true;
            SetState(RAB_STATE.DEATH);
        }
    }

    public void SetState(RAB_STATE state)
    {
        if (state == curState)
            return;
        curState = state;

        _agent.enabled = true;
        ResetAnimator();

        switch (curState)
        {
            case RAB_STATE.IDLE:
                _agent.enabled = false;
                _anim.SetBool("isIdleBool", true);
                break;
            case RAB_STATE.WALK:
                _agent.speed = _moveSpeed;
                _anim.SetBool("isWalkBool", true);
                break;
            case RAB_STATE.RUN:
                _agent.speed = _runSpeed;
                _anim.SetBool("isRunBool", true);
                break;
            case RAB_STATE.CHASE_TO_POINT:
                _anim.SetBool("isRunBool", true);
                break;
            case RAB_STATE.JUMP_TO_WALL:
                _agent.enabled = false;
                _anim.SetBool("isIdleBool", true);

                switch (_jumpWallIndex)
                {
                    case 0:
                        transform.DOLocalPath(_rabRotatePoints0, 1, PathType.Linear, PathMode.Full3D);
                        transform.DOLocalRotateQuaternion(_rabClimbRotatePoints[0].transform.GetChild(7).rotation, 1);
                        break;
                    case 1:
                        transform.DOLocalPath(_rabRotatePoints1, 1, PathType.Linear, PathMode.Full3D);
                        transform.DOLocalRotateQuaternion(_rabClimbRotatePoints[1].transform.GetChild(7).rotation, 1);
                        break;
                    case 2:
                        transform.DOLocalPath(_rabRotatePoints2, 1, PathType.Linear, PathMode.Full3D);
                        transform.DOLocalRotateQuaternion(_rabClimbRotatePoints[2].transform.GetChild(7).rotation, 1);
                        break;
                    case 3:
                        transform.DOLocalPath(_rabRotatePoints3, 1, PathType.Linear, PathMode.Full3D);
                        transform.DOLocalRotateQuaternion(_rabClimbRotatePoints[3].transform.GetChild(7).rotation, 1);
                        break;
                }
                break;
            case RAB_STATE.WALL_TO_WALL:
                _agent.enabled = false;
                _anim.SetBool("isIdleBool", true);

                switch (_jumpWallIndex)
                {
                    case 0:
                        transform.DOLocalPath(_wallJumpPoint1, 1, PathType.Linear, PathMode.Full3D);
                        transform.DOLocalRotateQuaternion(_wallJumpPoints[0].transform.GetChild(7).rotation, 1);
                        break;
                    case 1:
                        transform.DOLocalPath(_wallJumpPoint2, 1, PathType.Linear, PathMode.Full3D);
                        transform.DOLocalRotateQuaternion(_wallJumpPoints[1].transform.GetChild(7).rotation, 1);
                        break;
                    case 2:
                        transform.DOLocalPath(_wallJumpPoint3, 1, PathType.Linear, PathMode.Full3D);
                        transform.DOLocalRotateQuaternion(_wallJumpPoints[2].transform.GetChild(7).rotation, 1);
                        break;
                    case 3:
                        transform.DOLocalPath(_wallJumpPoint4, 1, PathType.Linear, PathMode.Full3D);
                        transform.DOLocalRotateQuaternion(_wallJumpPoints[3].transform.GetChild(7).rotation, 1);
                        break;
                }
                break;
            case RAB_STATE.WALK_ON_WALL:
                _agent.enabled = false;
                _anim.SetBool("isWalkBool", true);
                break;
            case RAB_STATE.WALL_TO_GROUND:
                _agent.enabled = false;
                _anim.SetBool("isIdleBool", true);

                switch (_jumpWallIndex)
                {
                    case 0:
                        transform.DOLocalPath(_wallToGroundPoint1, 1, PathType.Linear, PathMode.Full3D);
                        transform.DOLocalRotateQuaternion(_wallToGroundPoints[0].transform.GetChild(7).rotation, 1);
                        break;
                    case 1:
                        transform.DOLocalPath(_wallToGroundPoint2, 1, PathType.Linear, PathMode.Full3D);
                        transform.DOLocalRotateQuaternion(_wallToGroundPoints[1].transform.GetChild(7).rotation, 1);
                        break;
                    case 2:
                        transform.DOLocalPath(_wallToGroundPoint3, 1, PathType.Linear, PathMode.Full3D);
                        transform.DOLocalRotateQuaternion(_wallToGroundPoints[2].transform.GetChild(7).rotation, 1);
                        break;
                    case 3:
                        transform.DOLocalPath(_wallToGroundPoint4, 1, PathType.Linear, PathMode.Full3D);
                        transform.DOLocalRotateQuaternion(_wallToGroundPoints[3].transform.GetChild(7).rotation, 1);
                        break;
                }
                break;
            case RAB_STATE.WALK_TO_FIASH:
                _anim.SetBool("isWalkBool", true);
                break;
            case RAB_STATE.ATTRACT_IDLE:
                _agent.enabled = false;
                _anim.SetBool("isIdleBool", true);
                break;
            case RAB_STATE.PATROL:
                _anim.SetBool("isWalkBool", true);
                break;
            case RAB_STATE.JUMP:
                _agent.enabled = false;
                _anim.SetBool("isJumpBool", true);
                break;
            case RAB_STATE.SHOOT:
                _agent.enabled = false;
                Vector3 posShoot = _target.transform.position;
                posShoot.y = transform.position.y;
                transform.DOLookAt(posShoot, 0.5f);
                _anim.SetTrigger("isShootTrigger");
                _rabBulletManager.PlayEffect(RabBulletManager.INST_EFFECT_TYPE.SHOOT, _target.transform.position + new Vector3(0, 0.4f, 0), 1);
                break;
            case RAB_STATE.JET:
                _agent.enabled = false;
                Vector3 posJet = _target.transform.position;
                posJet.y = transform.position.y;
                transform.DOLookAt(posJet, 0.5f);
                _anim.SetTrigger("isJetTrigger");
                break;
            case RAB_STATE.HIT:
                _agent.enabled = false;
                Vector3 posHit = _target.transform.position;
                posHit.y = transform.position.y;
                transform.DOLookAt(posHit, 0.5f);
                _anim.SetTrigger("isHitTrigger");
                break;
            case RAB_STATE.CRUSH:
                _agent.enabled = false;
                Vector3 posCrush = _target.transform.position;
                posCrush.y = transform.position.y;
                transform.DOLookAt(posCrush, 0.5f);
                _anim.SetTrigger("isCrushTrigger");
                break;
            case RAB_STATE.CATCH:
                _agent.enabled = false;
                Vector3 posCatch = _target.transform.position;
                posCatch.y = transform.position.y;
                transform.DOLookAt(posCatch, 0.5f);
                _anim.SetTrigger("isCatchTrigger");
                break;
            case RAB_STATE.FOLLOW:
                break;
            case RAB_STATE.ABLEPSIA: //失明
                _anim.SetBool("isIdleBool", true);
                break;
            case RAB_STATE.DEATH:
                int ranIndex = Random.Range(0, 8);
                switch (ranIndex)
                {
                    case 0:
                        _anim.SetTrigger("isDeath3RightTrigger");
                        break;
                    case 1:
                        _anim.SetTrigger("isDeath1FrontTrigger");
                        break;
                    case 2:
                        _anim.SetTrigger("isDeath1BackTrigger");
                        break;
                    case 3:
                        _anim.SetTrigger("isDeath1LeftTrigger");
                        break;
                    case 4:
                        _anim.SetTrigger("isDeath1RightTrigger");
                        break;
                    case 5:
                        _anim.SetTrigger("isDeath3BackTrigger");
                        break;
                    case 6:
                        _anim.SetTrigger("isDeath3FrontTrigger");
                        break;
                    case 7:
                        _anim.SetTrigger("isDeath3LeftTrigger");
                        break;
                }
                break;
            case RAB_STATE.RENORN:
                _anim.SetBool("isRebornBool", true);
                break;
        }
    }

    private void UpdateState()
    {
        if (_isDead)
            return;
        switch (curState)
        {
            case RAB_STATE.IDLE:
                DoIdle();
                break;
            case RAB_STATE.WALK:
                DoWalk();
                break;
            case RAB_STATE.RUN:
                DoRun();
                break;
            case RAB_STATE.CHASE_TO_POINT:
                DoChaseToPoint();
                break;
            case RAB_STATE.JUMP_TO_WALL:
                DoJumpToWall();
                break;
            case RAB_STATE.WALL_TO_WALL:
                DoWallToWall();
                break;
            case RAB_STATE.WALK_ON_WALL:
                WalkOnWall();
                break;
            case RAB_STATE.WALL_TO_GROUND:
                DoWallToGround();
                break;
            case RAB_STATE.WALK_TO_FIASH:
                DoWalkToFlash();
                break;
            case RAB_STATE.ATTRACT_IDLE:
                DoAttractIdle();
                break;
            case RAB_STATE.PATROL:
                DoPatrol();
                break;
            case RAB_STATE.JUMP:
                DoJump();
                break;
            case RAB_STATE.SHOOT:
                DoShoot();
                break;
            case RAB_STATE.JET:
                DoJet();
                break;
            case RAB_STATE.HIT:
                DoHit();
                break;
            case RAB_STATE.CRUSH:
                DoCrush();
                break;
            case RAB_STATE.CATCH:
                DoCatch();
                break;
            case RAB_STATE.FOLLOW:
                DoFollow();
                break;
            case RAB_STATE.ABLEPSIA:
                DoAblepsia();
                break;
            case RAB_STATE.DEATH:
                DoDeath();
                break;
        }
    }

    private void DoIdle()
    {
        CheckFlashAttract(); //检测有没有被吸引

        //SetState(RAB_STATE.PATROL);
        _timer += Time.deltaTime;
        if (_timer <= 0.3f)
            return;
        _timer = 0f;

        //if (!CheckTarget())
        //{
        //    _LostTrackPoint = _target.transform.position;
        //    SetState(RAB_STATE.CHASE_TO_POINT);
        //    return;
        //}

        float distTarget = Vector3.Distance(transform.position, _target.transform.position);
        if (distTarget >= 7)
        {
            SetState(RAB_STATE.RUN);
        }
        else if (distTarget >= 2)
        {
            SetState(RAB_STATE.WALK);
        }
        else
        {
            int skillIndex = Random.Range(0, 3);
            switch (skillIndex)
            {
                case 0:
                    SetState(RAB_STATE.JET);
                    break;
                case 1:
                    SetState(RAB_STATE.HIT);
                    break;
                case 2:
                    SetState(RAB_STATE.CATCH);
                    break;
            }
        }
    }

    private void DoWalk()
    {
        CheckFlashAttract(); //检测有没有被吸引

        _timer += Time.deltaTime;
        if (_timer >= 3)
        {
            _timer = 0f;
            SetState(RAB_STATE.RUN);
        }
        _agent.SetDestination(_target.transform.position);
        float distTarget = Vector3.Distance(transform.position, _target.transform.position);
        if (distTarget <= 1)
        {
            _timer = 0f;
            SetState(RAB_STATE.IDLE);
        }
    }

    private float _lostTimer = 0f;
    private void DoRun()
    {
        CheckFlashAttract(); //检测有没有被吸引

        _agent.SetDestination(_target.transform.position);

        CheckTakeOffPoint();

        float dist = Vector3.Distance(transform.position, _target.transform.position);
        if (dist <= 2)
        {
            for (int i = 0; i < _lostPlayerPoint.transform.childCount; i++)
            {
                float distTarget = Vector3.Distance(_target.transform.position, _lostPlayerPoint.transform.GetChild(i).position);
                if (distTarget <= 4f)
                {
                    SetState(RAB_STATE.HIT);
                    return;
                }
            }
            _timer = 0f;
            SetState(RAB_STATE.SHOOT);
            return;
        }

        CheckTargetLost();
        if (!CheckTarget())
        {
            _lostTimer += Time.deltaTime;
            if (_lostTimer >= 1)
            {
                _lostTimer = 0f;
                _timer = 0f;
                SetState(RAB_STATE.CHASE_TO_POINT);
            }
        }
        _timer += Time.deltaTime;
        if (_timer >= 3)
        {
            _timer = 0f;
            SetState(RAB_STATE.SHOOT);
        }
    }

    private void CheckTakeOffPoint()
    {
        for (int i = 0; i < _rabClimbRotatePoints.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, _rabClimbRotatePoints[i].transform.position);
            float distTarget = Vector3.Distance(transform.position, _target.transform.position);
            if (dist <= 1 /*&& distTarget <= 10*/)
            {
                float angle = Vector3.Angle(transform.forward, _rabClimbRotatePoints[i].transform.forward);
                if (angle <= 90)
                {
                    _jumpWallIndex = i;
                    SetState(RAB_STATE.JUMP_TO_WALL);
                }
            }
        }
    }

    private void DoChaseToPoint()
    {
        CheckFlashAttract(); //检测有没有被吸引

        _agent.SetDestination(_LostTrackPoint);
        float distTarget = Vector3.Distance(transform.position, _LostTrackPoint);
        if (distTarget <= 2)
        {
            SetState(RAB_STATE.PATROL);
        }
    }

    private void DoJumpToWall()
    {
        _timer += Time.deltaTime;
        if (_timer >= 1)
        {
            _timer = 0f;
            SetState(RAB_STATE.WALK_ON_WALL);
        }
    }

    private void DoWallToWall()
    {
        float dist = Vector3.Distance(transform.position, _wallJumpPoints[_jumpWallIndex].transform.GetChild(7).position);
        if (dist <= 0.2f)
        {
            SetState(RAB_STATE.WALL_TO_GROUND);
        }
    }

    private void WalkOnWall()
    {
        transform.Translate(Vector3.forward * _moveSpeed * 1.5f * Time.deltaTime, Space.Self);

        float dist = Vector3.Distance(transform.position, _wallJumpPoints[_jumpWallIndex].transform.position);
        if (dist <= 0.2f)
        {
            SetState(RAB_STATE.WALL_TO_WALL);
        }
    }

    private void DoWallToGround()
    {
        //transform.Translate(Vector3.forward * _moveSpeed * 1.5f * Time.deltaTime, Space.Self);

        float dist = Vector3.Distance(transform.position, _wallToGroundPoints[_jumpWallIndex].transform.GetChild(7).position);
        if (dist <= 0.2f)
        {
            SetState(RAB_STATE.IDLE);
        }
    }

    private void DoWalkToFlash()
    {
        _agent.SetDestination(new Vector3(-24.9f, 92.60792f, 36.81f));
        float dist = Vector3.Distance(transform.position, new Vector3(-24.9f, 92.60792f, 36.81f));
        if (dist <= 0.5f)
        {
            SetState(RAB_STATE.ATTRACT_IDLE);
        }
    }

    private void DoAttractIdle()
    {
        //被吸引之后的逻辑
    }

    private void DoPatrol()
    {
        CheckFlashAttract(); //检测有没有被吸引

        _agent.SetDestination(_patrolPoints[_curTargetPointIndex].transform.position);
        float dist = Vector3.Distance(transform.position, _patrolPoints[_curTargetPointIndex].transform.position);
        if (dist <= minArriveDistance)
        {
            ChangePointIndex();
        }

        if (CheckTarget())
        {
            SetState(RAB_STATE.RUN);
        }
    }

    private void DoJump()
    {

    }

    private void DoShoot()
    {
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("RAB_attack_shoot") && info.normalizedTime >= 1)
        {
            SetState(RAB_STATE.IDLE);
        }
    }

    private void DoJet()
    {
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("RAB_attack_jet") && info.normalizedTime >= 1)
        {
            SetState(RAB_STATE.IDLE);
        }
    }

    private void DoHit()
    {
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("RAB_attack_hit") && info.normalizedTime >= 1)
        {
            SetState(RAB_STATE.IDLE);
        }
    }

    private void DoCrush()
    {
        _timer += Time.deltaTime;
        if (_timer <= 1)
        {
            transform.Translate(Vector3.forward * _moveSpeed * 1.5f * Time.deltaTime, Space.Self);
        }
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("RAB_attack_crush") && info.normalizedTime >= 1)
        {
            _timer = 0f;
            SetState(RAB_STATE.IDLE);
        }
    }

    private void DoCatch()
    {
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("RAB_attack_catch") && info.normalizedTime >= 1)
        {
            //if (_target.transform.parent != null)
            {
                _target.transform.parent = null;
                _target.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            SetState(RAB_STATE.IDLE);
            return;
        }

        if (_rabCatchTrigger.isCatchPlayer)
        {
            if (_target.transform.parent == null)
            {
                _target.transform.parent = _catchPoint.transform;
            }
        }
    }

    private void DoFollow()
    {

    }

    private void DoAblepsia()
    {
        _timer += Time.deltaTime;
        if (_timer >= 30f)
        {
            _timer = 0f;
            SetState(RAB_STATE.IDLE);
        }
    }

    private void DoDeath()
    {

    }

    private void ChangePointIndex()
    {
        if (_curTargetPointIndex < _patrolPoints.Length - 1)
        {
            _curTargetPointIndex++;
        }
        else
        {
            _curTargetPointIndex = 0;
        }
    }

    private bool CheckTarget()
    {
        RaycastHit hit;
        Ray ray = new Ray(_rayPoint.transform.position, _target.transform.position - _rayPoint.transform.position);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag.Equals("Player"))
            {
                //SetState(RAB_STATE.RUN);
                return true;
            }
        }
        return false;
    }

    private float _checkTimer = 0f;
    private void CheckTargetLost()
    {
        _checkTimer += Time.deltaTime;
        if (_checkTimer <= 0.5f)
            return;
        _checkTimer = 0f;

        for (int i = 0; i < _lostPlayerPoint.transform.childCount; i++)
        {
            float dist = Vector3.Distance(_target.transform.position, _lostPlayerPoint.transform.GetChild(i).transform.position);
            if (dist <= 2)
            {
                _LostTrackPoint = _lostPlayerPoint.transform.GetChild(i).transform.position;
            }
        }
    }

    private void CheckFlashAttract()
    {
        if (!_isCanAttract)
            return;

        float dist = Vector3.Distance(transform.position, new Vector3(-24.9f, 92.60792f, 36.81f));
        if (dist <= 10f)
        {
            _timer = 0f;
            SetState(RAB_STATE.WALK_TO_FIASH);
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
        //Init();
    }

    void Update () 
	{
        UpdateState();
	}

    private void ResetAnimator()
    {
        _anim.SetBool("isIdleBool", false);
        _anim.SetBool("isWalkBool", false);
        _anim.SetBool("isRunBool", false);
        _anim.SetBool("isWalkBool", false);
        _anim.SetBool("isJumpBool", false);
        _anim.SetBool("isRebornBool", false);
    }
}
