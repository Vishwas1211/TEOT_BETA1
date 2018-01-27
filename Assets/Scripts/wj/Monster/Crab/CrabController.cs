//CrabController.cs
//TEOT_ONLINE
//
//Create by WangJie on 9/26/2017 12:37 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrabController : MonoBehaviour 
{
    private const string JUMP_BACK_PATH = "Prefabs/Character/Enemy/Crab/Path/CrabJumpBackPath";
    private const float MAX_HP = 100;
    [SerializeField]
    private float _curHp;
    [SerializeField]
    private float _lastStageHp;

    private float _moveSpeed = 3f;
    private float _runSpeed = 5f;
    private float _angleSpeed = 1000;
    private float _timer = 0f;
    private float _timerShield = 0f;
    private float _timerObserve = 20f;
    private float _minAtkDist = 5f;
    private float _farRange = 10f;      //远程攻击距离
    private float _skillCD = 0f;
    private float _percentHp = 100f;
    private float posDelta = 15;

    private int _pathIndex = 0;

    private Vector3 _targetPoint;
    private Vector3 _randFlyPoint;

    public GameObject _target;
    private GameObject _controlPanel;   //控制台
    private List<GameObject> _playerList = new List<GameObject>();
    private GameObject _crabJumpBackPrefab;
    private GameObject[] _crabJumpBackPath;

    private bool _isDead;
    private bool _isStruggle;
    private bool _isHaveInterference;
    private bool _isCanImmediatelyChange; //当玩家近身的时候，是否可以立即释放近战攻击
    private bool _isStart;

    private AudioSource _audioSource;
    private NavMeshAgent _agent;
    private Animator _anim;
    //AnimatorStateInfo info;

    private CrabBulletManager _crabBulletManager;
    public CrabBulletManager crabBulletManager { get { return _crabBulletManager; } }

    public GameObject targetDoor;

    private CRAB_STATE lastCrabState = CRAB_STATE.EMPTY;
    public CRAB_STATE curCrabState;
    public HATRED_LEVEL curHatredLevel;
    public CRAB_STAGE _curCrabStage;

    //public GameObject targetTip;    //显示目标位置标识

    public enum CRAB_STATE
    {
        EMPTY,                  //空状态
        IDLE,                    //待机
        WALK_LEFT,           //横向左走
        WALK_RIGHT,         //横向右走
        WALK_FRONT,         //向前走
        WALK_BACK,           //后退
        TURN_LEFT,            //向左转
        TURN_RIGHT,          //向右转
        RUN,                      //跑
        JUMP,                     //跳
        JUMP_FRONT,         //向前跳
        JUMP_BACK,           //想后跳
        HAMMER,                //砸地
        CYCLON,                //原地旋转
        GRAVEL,                 //左挖地
        GRAVER,                 //右挖地
        DRILL_L,                //左手钻
        DRILL_R,                //右手钻
        STRICK_R,               //右手抓
        STRICK_L,               //左手抓
        AIR_SHOOT,            //空中射击
        POWER,                 //空中能量球
        MACHINE,               //机枪
        EMISSION,              //双肩发炮
        ARTILLERY_COMBO,//火炮连击
        DODGE_L,              //左躲避
        DODGE_R,              //右躲避
        SHAKE,                  //抖动（玩家站到boss身上时）
        GROUND_SHAKE,    //地面振动
        SHIELD,                 //护盾
        CHANGE,               //变换
        ROAR,                   //咆哮
        HURT,                    //受伤
        STRUGGLE,           //死前挣扎
        DEATH,                  //死亡
    }

    public enum HATRED_LEVEL
    {
        MEDIUM_RANGE,
        SHORT_RANGE,
        INTERFERENCE,//干扰器
        LONGE_RANGE,
    }

    public enum CRAB_STAGE
    {
        LOW_STAGE,
        MEDIUM_STAGE,
        //ADVANCED_STAGE,
    }

    public void Init()
    {
        //info = _anim.GetCurrentAnimatorStateInfo(0);

        _crabBulletManager = gameObject.AddComponent<CrabBulletManager>();
        _crabBulletManager.Init();
        _controlPanel = GameObject.Find("ControlPanel");
        _playerList.Add(GameObject.Find("ThirdPersonController"));
        _crabJumpBackPrefab = UtilFunction.ResourceLoad(JUMP_BACK_PATH);
        int countOfChild = _crabJumpBackPrefab.transform.childCount;

        _crabJumpBackPath = new GameObject[countOfChild];
        for (int i = 0; i < countOfChild; i++)
        {
            _crabJumpBackPath[i] = _crabJumpBackPrefab.transform.GetChild(i).gameObject;
        }
        targetDoor = GameObject.Find("Exterior_5C3_Door");
        //SetCrabStage(CRAB_STAGE.LOW_STAGE);
        //targetTip = GameObject.Find("targetTip");
        SetState(CRAB_STATE.IDLE);
    }

    public void SetCrabStage(CRAB_STAGE crabStage)//crab阶段
    {
        if (crabStage == _curCrabStage)
            return;
        _curCrabStage = crabStage;
    }

    public void Appear()
    {
        _isStart = true;
        SetState(CRAB_STATE.IDLE);
    }

    public  void SetTarget(GameObject t)
    {
        _target = t;
    }

    public void SetTargetPoint(Vector3 v)
    {
        if ( v == Vector3.zero)
            return;
        _targetPoint = v;
        //targetTip.transform.position = v;
    }

    public void OnHurt(float damage)
    {
        if (!_isStart)
            return;
        if (_isStruggle)
            return;
        _curHp -= damage;
        _percentHp = (_curHp / MAX_HP) * 100;

        if (_curHp <= 0)
        {
            _curHp = 0;
            _isStruggle = true;
            SetState(CRAB_STATE.STRUGGLE);
        }

        //if (_percentHp <= 30)
        //{
        //    SetCrabStage(CRAB_STAGE.ADVANCED_STAGE);
        //}
        //else 
        if (_percentHp <= 65)
        {
            SetCrabStage(CRAB_STAGE.MEDIUM_STAGE);
        }
    }   

    public void AttackBehaviour()
    {
        //判断向左转还是向右转
        Vector3 targetDirection = _target.transform.position - transform.position;

        float angle = Vector3.Angle(transform.forward, targetDirection);
        float angleQH = Vector3.Dot(transform.forward, targetDirection);
        float angleZY = Vector3.Cross(transform.forward, targetDirection).y;
        //Debug.Log("---+++---+--++--++-++-+-+-+--+++前后:" + angleQH);
        //Debug.Log("---+++---+--++--++-++-+-+-+--+++左右:" + angleZY);
        Debug.Log("---+++---+--++--++-++-+-+-+--+++前后:" + angle);
        if (angle >= 10)
        {
            AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
            if (angleZY < 0)
            {
                SetState(CRAB_STATE.TURN_LEFT);
            }
            else if (angleZY > 0)
            {
                SetState(CRAB_STATE.TURN_RIGHT);
            }
            else
            {
                JudgeLevelSelectSkill();
            }
        }
        else
        {
            JudgeLevelSelectSkill();
        }
    }

    public void SetHatredLevel(HATRED_LEVEL level)
    {
        if (level == curHatredLevel)
            return;
        curHatredLevel = level;
    }

    public void SetState(CRAB_STATE state)
    {
        if (state == curCrabState)
            return;
        curCrabState = state;

        ResetAnimator();
        _agent.enabled = true;
        _isCanImmediatelyChange = true;

        switch (curCrabState)
        {
            case CRAB_STATE.IDLE:
                //_agent.isStopped = true;
                _agent.enabled = false;
                _anim.SetBool("isIdleBool", true);
                break;
            case CRAB_STATE.WALK_LEFT:
                _agent.isStopped = false;
                _agent.speed = 2;
                _agent.angularSpeed = 0;
                _anim.SetBool("isWalkLeftBool", true);
                break;
            case CRAB_STATE.WALK_RIGHT:
                _agent.isStopped = false;
                _agent.speed = 2;
                _agent.angularSpeed = 0;
                _anim.SetBool("isWalkRightBool", true);
                break;
            case CRAB_STATE.WALK_FRONT:
                _agent.isStopped = false;
                _agent.speed = _moveSpeed;
                _agent.angularSpeed = _angleSpeed;
                _anim.SetBool("isWalkFrontBool", true);
                break;
            case CRAB_STATE.WALK_BACK:
                _agent.isStopped = false;
                _agent.angularSpeed = _angleSpeed;
                _anim.SetBool("isWalkBackBool", true);
                break;
            case CRAB_STATE.TURN_LEFT:
                _agent.isStopped = true;
                _anim.SetBool("isTurnLeftBool", true);
                break;
            case CRAB_STATE.TURN_RIGHT:
                _agent.isStopped = true;
                _anim.SetBool("isTurnRightBool", true);
                break;
            case CRAB_STATE.RUN:
                _agent.speed = _runSpeed;
                _agent.isStopped = false;
                _agent.angularSpeed = _angleSpeed;
                _anim.SetBool("isRunBool", true);
                break;
            case CRAB_STATE.JUMP:
                _agent.isStopped = true;
                _anim.SetBool("isJumpBool", true);
                break;
            case CRAB_STATE.JUMP_FRONT:
                _agent.isStopped = true;
                _anim.SetBool("isJumpFrontBool", true);
                break;
            case CRAB_STATE.JUMP_BACK:
                _agent.enabled = false;
                _anim.SetBool("isJumpBackBool", true);
                break;
            case CRAB_STATE.HAMMER:
                _isCanImmediatelyChange = false; //不能在未释放完技能的时候跳出
                lastCrabState = CRAB_STATE.HAMMER; //记录上一个状态
                //_agent.isStopped = true;
                _agent.enabled = false;
                _anim.SetTrigger("isHammerTrigger");
                _isHaveInterference = false;
                break;
            case CRAB_STATE.CYCLON:
                _isCanImmediatelyChange = false; //不能在未释放完技能的时候跳出
                _agent.isStopped = true;
                _anim.SetTrigger("isCyclonTrigger");
                CheckPlayerHurt();
                break;
            case CRAB_STATE.GRAVEL:
                _agent.isStopped = true;
                _anim.SetTrigger("isGravelLeftTrigger");
                break;
            case CRAB_STATE.GRAVER:
                _agent.isStopped = true;
                _anim.SetTrigger("isGravelRightTrigger");
                break;
            case CRAB_STATE.DRILL_L:
                _agent.isStopped = true;
                _anim.SetTrigger("isDrillLTrigger");
                break;
            case CRAB_STATE.DRILL_R:
                _agent.isStopped = true;
                _anim.SetTrigger("isDrillRTrigger");
                break;
            case CRAB_STATE.STRICK_R:
                _isCanImmediatelyChange = false; //不能在未释放完技能的时候跳出
                lastCrabState = CRAB_STATE.STRICK_R; //记录上一个状态
                _agent.isStopped = true;
                _anim.SetTrigger("isStrickTrigger");
                break;
            case CRAB_STATE.AIR_SHOOT:
                _isCanImmediatelyChange = false; //不能在未释放完技能的时候跳出
                _agent.enabled = false;
                //_agent.isStopped = true;
                _anim.SetTrigger("isAirShootTrigger");
                break;
            case CRAB_STATE.POWER:
                _isCanImmediatelyChange = false; //不能在未释放完技能的时候跳出
                _agent.enabled = false;
                //_agent.isStopped = true;
                _anim.SetTrigger("isPowerTrigger");
                break;
            case CRAB_STATE.MACHINE:
                //_agent.isStopped = true;
                _anim.SetTrigger("isMachineTrigger");
                _crabBulletManager.PlaySkill(CrabBulletManager.SKILL_TYPE.MACHINE, _target, 1f);
                break;
            case CRAB_STATE.EMISSION:
                _agent.enabled = false;
                //_agent.isStopped = true;
                _anim.SetTrigger("isEmissionTrigger");
                Vector3 posLook = _target.transform.position;
                posLook.y = transform.position.y;
                transform.LookAt(posLook);
                _crabBulletManager.PlaySkill(CrabBulletManager.SKILL_TYPE.EMISSION, _target, 1.2f);
                break;
            case CRAB_STATE.ARTILLERY_COMBO:
                _isCanImmediatelyChange = false; //不能在未释放完技能的时候跳出
                _agent.enabled = false;
                //_agent.isStopped = true;
                _anim.SetTrigger("isArtilleryComboTrigger");
                _crabBulletManager.PlaySkill(CrabBulletManager.SKILL_TYPE.ARTILLERY_COMBO, _target);
                break;
            case CRAB_STATE.DODGE_L:
                break;
            case CRAB_STATE.DODGE_R:
                break;
            case CRAB_STATE.SHAKE:
                break;
            case CRAB_STATE.GROUND_SHAKE:
                _isCanImmediatelyChange = false; //不能在未释放完技能的时候跳出
                _agent.isStopped = true;
                _anim.SetTrigger("isGroundShakeTrigger");
                break;
            case CRAB_STATE.SHIELD:
                _agent.isStopped = true;
                //_anim.SetBool("isShieldBool",true);
                _anim.SetTrigger("isShieldTrigger");
                _crabBulletManager.EffectsControl(CrabBulletManager.CRAB_EFFECT.SHIELD, true, 0.5f);
                _crabBulletManager.EffectsControl(CrabBulletManager.CRAB_EFFECT.SHIELD, false, 3.5f);
                break;
            case CRAB_STATE.CHANGE:
                _agent.isStopped = true;
                _anim.SetTrigger("isChangeTrigger");
                break;
            case CRAB_STATE.ROAR:
                _agent.isStopped = true;
                _anim.SetBool("isRoarBool", true);
                break;
            case CRAB_STATE.HURT:
                _agent.isStopped = true;
                _anim.SetTrigger("isHurtTrigger");
                break;
            case CRAB_STATE.STRUGGLE:
                Vector3 pos = targetDoor.transform.position;
                pos.y = transform.position.y;
                transform.LookAt(pos);
                _agent.isStopped = true;
                _anim.SetTrigger("isMachineTrigger");
                _crabBulletManager.PlaySkill(CrabBulletManager.SKILL_TYPE.STRUGGLE, targetDoor, 1f);
                break;
            case CRAB_STATE.DEATH:
                TaskStepManagaer.Instance.FinishCurTaskImmediately();
                _isDead = true;
                _agent.isStopped = true;
                _anim.SetTrigger("isDeathTrigger");
                break;
        }
    }

    private void ResetAnimator()
    {
        _anim.SetBool("isIdleBool", false);//
        _anim.SetBool("isWalkLeftBool", false);
        _anim.SetBool("isWalkRightBool", false);
        _anim.SetBool("isWalkFrontBool", false);
        _anim.SetBool("isWalkBackBool", false);
        _anim.SetBool("isRunBool", false);
        _anim.SetBool("isJumpBool", false);
        _anim.SetBool("isJumpFrontBool", false);
        _anim.SetBool("isTurnLeftBool", false);
        _anim.SetBool("isTurnRightBool", false);
        _anim.SetBool("isJumpBackBool", false);
        _anim.SetBool("isRoarBool", false);
        //_anim.SetBool("isShieldBool", false);
    }

    private void UpdateCrabState()
    {
        if (_isDead)
            return;

        switch (curCrabState)
        {
            case CRAB_STATE.IDLE:
                DoIdle();
                break;
            case CRAB_STATE.WALK_LEFT:
                DoWalkLeft();
                break;
            case CRAB_STATE.WALK_RIGHT:
                DoWalkRight();
                break;
            case CRAB_STATE.WALK_FRONT:
                DoWalkFront();
                break;
            case CRAB_STATE.WALK_BACK:
                DoWalkBack();
                break;
            case CRAB_STATE.TURN_LEFT:
                DoTurnLeft();
                break;
            case CRAB_STATE.TURN_RIGHT:
                DoTurnRight();
                break;
            case CRAB_STATE.RUN:
                DoRun();
                break;
            case CRAB_STATE.JUMP:
                DoJump();
                break;
            case CRAB_STATE.JUMP_FRONT:
                DoJumpFront();
                break;
            case CRAB_STATE.JUMP_BACK:
                DoJumpBack();
                break;
            case CRAB_STATE.HAMMER:
                DoSkill("atk2_dbhammer");
                break;
            case CRAB_STATE.CYCLON:
                DoSkill("atk5_cyclon");
                break;
            case CRAB_STATE.GRAVEL:
                DoSkill("atk13_gravel_R");
                break;
            case CRAB_STATE.GRAVER:
                DoSkill("atk13_gravel_L");
                break;
            case CRAB_STATE.DRILL_L:
                DoSkill("atk12_drill_L");
                break;
            case CRAB_STATE.DRILL_R:
                DoSkill("atk12_drill_R");
                break;
            case CRAB_STATE.STRICK_R:
                DoSkill("atk1_strick_R");
                break;
            case CRAB_STATE.AIR_SHOOT:
                DoSkill("atk6_airShoot");
                break;
            case CRAB_STATE.POWER:
                DoSkill("atk11_exPower");
                break;
            case CRAB_STATE.MACHINE:
                DoSkill("atk4_machineGun");
                break;
            case CRAB_STATE.EMISSION:
                DoSkill("atk3_exEmission");
                break;
            case CRAB_STATE.ARTILLERY_COMBO:
                DoSkill("atk3_single_R_end");
                break;
            case CRAB_STATE.DODGE_L:
                break;
            case CRAB_STATE.DODGE_R:
                break;
            case CRAB_STATE.SHAKE:
                break;
            case CRAB_STATE.GROUND_SHAKE:
                DoSkill("atk8_groundshake");
                break;
            case CRAB_STATE.SHIELD:
                DoSkill("atk10_shield");
                break;
            case CRAB_STATE.CHANGE:
                DoSkill("bianshen");
                break;
            case CRAB_STATE.ROAR:
                DoSkill("paoxiao");
                break;
            case CRAB_STATE.HURT:
                DoSkill("beaten");
                break;
            case CRAB_STATE.STRUGGLE:
                DoStruggle();
                break;
            case CRAB_STATE.DEATH:
                break;
        }

    }

    private bool _isOnceAdvanced = true;
    private float _flyStartSpeed = 0f;
    private void AdvancedStage()
    {
        //持续在天上飞
        if (_isOnceAdvanced)
        {
            if (_flyStartSpeed <= 3)
            {
                _flyStartSpeed += 0.01f;
            }
            transform.position = Vector3.Lerp(transform.position, new Vector3(-5.6f, 130f, 48.57f), _flyStartSpeed * Time.deltaTime);
            //这里最好需要一个临时物体来代替new出来的新vector3位置，避免没帧都会new一个新的vector3
            float dist = Vector3.Distance(transform.position, new Vector3(-5.6f, 130f, 48.57f));
            if (dist <= 0.2f)
            {
                _flyStartSpeed = 0f;
                RandomPosition();
                _isOnceAdvanced = false;
            }
        }
        else
        {
            if (_flyStartSpeed <= 10)
            {
                _flyStartSpeed += 0.1f;
            }
            //TODO 随机移动到一个点
            transform.position = Vector3.MoveTowards(transform.position, _randFlyPoint, _flyStartSpeed * Time.deltaTime);
            //这里最好需要一个临时物体来代替new出来的新vector3位置，避免没帧都会new一个新的vector3
            float dist = Vector3.Distance(transform.position, _randFlyPoint);
            if (dist <= 0.2f)
            {
                _flyStartSpeed = 0f;
                RandomPosition();
                JudgeLevelSelectSkill();
            }
        }

    }

    private void RandomPosition()
    {
        _randFlyPoint = new Vector3(-5.6f, 130f, 48.57f) + new Vector3(Random.Range(-1f, 1f) * 10f, 0, Random.Range(1f, 2f) * 10f);
        //targetTip.transform.position = _randFlyPoint;
    }

    private bool _isCanOnceCheck = true;
    private void DoIdle()
    {
        //if (_curCrabStage == CRAB_STAGE.ADVANCED_STAGE)  //起飞用
        //{
        //    AdvancedStage();
        //}
        //else
        {
            if (lastCrabState == CRAB_STATE.HAMMER || lastCrabState == CRAB_STATE.STRICK_R) //两个需要释放完后退几步的技能
            {
                lastCrabState = CRAB_STATE.EMPTY;
                SetState(CRAB_STATE.WALK_BACK);
                return;
            }
            _skillCD += Time.deltaTime;
            if (_skillCD >= 0.5f)
            {
                _skillCD = 0f;
                for (int i = 0; i < _playerList.Count; i++)
                {
                    float dist = Vector3.Distance(transform.position, _playerList[i].transform.position);
                    //if (_percentHp <= 99 && _isCanOnceCheck)
                    //{
                    //    _isCanOnceCheck = false;
                    //    SetState(CRAB_STATE.ROAR);
                    //}
                    //else if (Vector3.Distance(transform.position, _playerList[i].transform.position) <= 6)
                    //{
                    //    SetTarget(_playerList[i]);
                    //    SetState(CRAB_STATE.CYCLON);
                    //}
                    //else
                    //{
                        SetTarget(_playerList[i]);
                        //if (_timerObserve >= 20f)
                        //{
                        //    _timerObserve = 0;
                        //    Observe();
                        //}
                        //else
                        //{
                        AttackBehaviour();
                        //}

                    //}
                }
            }
        }
    }

    private void Observe() //观察
    {
        int rand = Random.Range(0, 2);
        switch (rand)
        {
            case 0:
                FindPos(0);
                SetState(CRAB_STATE.WALK_LEFT);
                break;
            case 1:
                FindPos(1);
                SetState(CRAB_STATE.WALK_RIGHT);
                break;
        }
    }

    private void FindPos(int i)
    {
        Vector3 pos = Vector3.zero;
        switch (i)
        {
            case 0://Left
                pos = transform.position + (posDelta * transform.right);
                pos.y = transform.position.y;
                SetTargetPoint(pos);
                break;
            case 1://Right
                pos = transform.position - (posDelta * transform.right);
                pos.y = transform.position.y;
                SetTargetPoint(pos);
                break;
        }
    }

    private void DoWalkLeft()
    {
        _timer += Time.deltaTime;//开始计时
        if(_timer >= 5f)
        {
            _timer = 0f;
            SetState(CRAB_STATE.IDLE);
        }

        if (Vector3.Distance(transform.position, new Vector3(-5.6f, 120.14f, 48.57f)) >= 7.5f)
        {
            //重置一下随机的左右移动距离
            float randNum = Random.Range(5, 15.0f);
            posDelta = randNum;
            //
            SetTargetPoint(new Vector3(-5.6f, 120.14f, 48.57f));
            _timer = 0f;
            SetState(CRAB_STATE.WALK_FRONT);
        }

        Vector3 posTarget = _target.transform.position;
        posTarget.y = transform.position.y;
        transform.LookAt(posTarget);
        float targetDist = Vector3.Distance(_targetPoint, new Vector3(-5.6f, 120.14f, 48.57f));
        if (targetDist >= 8)
        {
            posDelta -= 0.2f;
            FindPos(0);
        }
        else
        {
            _agent.SetDestination(_targetPoint);
        }

        float dist = Vector3.Distance(transform.position, _targetPoint);
        if (dist <= _agent.stoppingDistance + 0.2f)
        {
            float randNum = Random.Range(5, 15.0f); 
            posDelta = randNum;
            FindPos(1);
            SetState(CRAB_STATE.WALK_RIGHT);
        }
    }

    private void DoWalkRight()
    {
        _timer += Time.deltaTime;//开始计时
        if (_timer >= 5f)
        {
            _timer = 0f;
            SetState(CRAB_STATE.IDLE);
        }

        if (Vector3.Distance(transform.position, new Vector3(-5.6f, 120.14f, 48.57f)) >= 7.5f)
        {
            //重置一下随机的左右移动距离
            float randNum = Random.Range(5, 15.0f);
            posDelta = randNum;
            //
            SetTargetPoint(new Vector3(-5.6f, 120.14f, 48.57f));
            _timer = 0f;
            SetState(CRAB_STATE.WALK_FRONT);
        }

        Vector3 posTarget = _target.transform.position;
        posTarget.y = transform.position.y;
        transform.LookAt(posTarget);
        float targetDist = Vector3.Distance(_targetPoint, new Vector3(-5.6f, 120.14f, 48.57f));
        if (targetDist >= 8)
        {
            posDelta -= 0.2f;
            FindPos(1);
        }
        else
        {
            _agent.SetDestination(_targetPoint);
        }

        float dist = Vector3.Distance(transform.position, _targetPoint);
        if (dist <= _agent.stoppingDistance + 0.2f)
        {
            float randNum = Random.Range(5, 15.0f);
            posDelta = randNum;
            FindPos(0);
            SetState(CRAB_STATE.WALK_LEFT);
        }
    }

    private void DoWalkFront()
    {
        float dist = Vector3.Distance(transform.position, _targetPoint);
        if (dist <= _agent.stoppingDistance + 0.2f)
        {
            Observe();
        }
    }

    private void DoWalkBack()
    {
        _timer += Time.deltaTime;
        transform.Translate(Vector3.back * _moveSpeed * Time.deltaTime, Space.Self);
        if (_timer >= 1)
        {
            _timer = 0f;
            SetState(CRAB_STATE.IDLE);
        }
    }


    private void DoTurnLeft()
    {
        _timer += Time.deltaTime;
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.LookRotation(_target.transform.position - transform.position), info.length * Time.deltaTime);

        if (_timer >= info.length) 
        {
            _timer = 0f;
            JudgeLevelSelectSkill();
        }
    }

    private void DoTurnRight()
    {
        _timer += Time.deltaTime;
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.LookRotation(_target.transform.position - transform.position), info.length * Time.deltaTime);

        //if (/*info.IsName("ban_zhaunshen_R") &&*/ info.normalizedTime >= 1)
        if(_timer >= info.length)
        {
            _timer = 0f;
            JudgeLevelSelectSkill();
        }
    }

    private void DoRun()
    {
        _agent.SetDestination(_target.transform.position);
        float dist = Vector3.Distance(transform.position, _target.transform.position);
        if (dist <= 5)
        {
            int i = Random.Range(0, 2);
            switch (i)
            {
                case 0:
                    SetState(CRAB_STATE.STRICK_R);
                    break;
                case 1:
                    SetState(CRAB_STATE.HAMMER);
                    break;
            }
        }
    }

    private void DoJump()
    {

    }

    private void DoJumpFront()
    {

    }
    private float _jumpSpeed = 4;
    private void DoJumpBack()
    {
        _timer += Time.deltaTime;
        if (_timer <= 0.35f)
        {
            return;
        }
        //transform.position = Vector3.Lerp(transform.position, _crabJumpBackPath[_pathIndex].transform.position, 4 * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, _crabJumpBackPath[0].transform.position, _jumpSpeed * 1.5f * Time.deltaTime);

        float dist = Vector3.Distance(transform.position, _crabJumpBackPath[_pathIndex].transform.position);
        if (dist <= 0.1f)
        {
            _timer = 0f;
            SetState(CRAB_STATE.IDLE);
        }
    }

    private void DoStruggle()
    {
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("atk4_machineGun") && info.normalizedTime >= 1)
        {
            SetState(CRAB_STATE.DEATH);
        }
    }

    private void DoSkill(string skillName)
    {
        AnimatorStateInfo info = _anim.GetCurrentAnimatorStateInfo(0);
        if (Vector3.Distance(transform.position, _playerList[0].transform.position) <= 6 && _isCanImmediatelyChange)
        {
            SetTarget(_playerList[0]);
            SetState(CRAB_STATE.CYCLON);
            return; //条件符合立即跳出
        }
        else if (info.IsName(skillName) && info.normalizedTime >= 1)
        {
            if (skillName == "paoxiao")
                SetState(CRAB_STATE.ARTILLERY_COMBO);
            else
                SetState(CRAB_STATE.IDLE);
        }

    }

    private void JudgeLevelSelectSkill()
    {
        switch (_curCrabStage)
        {
            case CRAB_STAGE.LOW_STAGE:
                int i = Random.Range(0, 4);
                switch (i)
                {
                    case 0:
                        SetState(CRAB_STATE.MACHINE);
                        break;
                    case 1:
                        SetState(CRAB_STATE.GRAVEL);
                        break;
                    case 2:
                        SetState(CRAB_STATE.RUN);
                        break;
                    case 3:
                        SetState(CRAB_STATE.GRAVER);
                        break;
                }
                break;
            case CRAB_STAGE.MEDIUM_STAGE:
                int j = Random.Range(0, 5);
                switch (j)
                {
                    case 0:
                        SetState(CRAB_STATE.AIR_SHOOT);
                        break;
                    case 1:
                        SetState(CRAB_STATE.POWER);
                        break;
                    case 2:
                        SetState(CRAB_STATE.RUN);
                        break;
                    case 3:
                        SetState(CRAB_STATE.GROUND_SHAKE);
                        break;
                    case 4:
                        SetState(CRAB_STATE.ARTILLERY_COMBO);
                        break;
                    case 5:
                        SetState(CRAB_STATE.EMISSION);
                        break;
                }
                break;
            //case CRAB_STAGE.ADVANCED_STAGE:
            //    int skillAdvanced = Random.Range(0, 4);
            //    switch (skillAdvanced)
            //    {
            //        case 0:
            //            SetState(CRAB_STATE.AIR_SHOOT);
            //            break;
            //        case 1:
            //            SetState(CRAB_STATE.POWER);
            //            break;
            //        case 2:
            //            SetState(CRAB_STATE.EMISSION);
            //            break;
            //        case 3:
            //            SetState(CRAB_STATE.ARTILLERY_COMBO);
            //            break;
            //    }
                //break;
        }
        //switch (curHatredLevel)
        //{
        //case HATRED_LEVEL.SHORT_RANGE:
        //    SetState(CRAB_STATE.CYCLON);    //近身攻击
        //    break;
        //case HATRED_LEVEL.MEDIUM_RANGE:
        //    {

        //}
        //break;
        //    case HATRED_LEVEL.INTERFERENCE: //有干扰器
        //        SetState(CRAB_STATE.RUN);
        //        break;
        //    case HATRED_LEVEL.LONGE_RANGE:
        //        SetState(CRAB_STATE.EMISSION);
        //        break;
        //}
    }


    //判断是否释放护盾
    private void UpdateOpenShield()
    {
        if (_isDead)
            return;

        //_timerShield += Time.deltaTime;
        //if (_timerShield >= 3)
        //{
        //    _timerShield = 0;
        //    _lastStageHp = _curHp;
        //}

        //if (_curCrabStage == CRAB_STAGE.MEDIUM_STAGE) //只有在高级阶段的时候才能出现护盾
        //{
        //    if (_lastStageHp - _curHp >= 5)
        //    {
        //        _lastStageHp = _curHp;  //重置一下，防止出现无线切换到护盾状态的bug
        //        SetState(CRAB_STATE.SHIELD);
        //    }
        //}
    }

    private void RandomTargetPoint()
    {
        Vector3 pos = _target.transform.position;
        pos.y = transform.position.y;

        SetTargetPoint(pos);
    }


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();

        _agent.speed = _moveSpeed;
        _agent.angularSpeed = _angleSpeed;
    }

    void Start ()
	{
        SetState(CRAB_STATE.IDLE);
        //_curHp = MAX_HP;
        _lastStageHp = MAX_HP;
        Appear();
    }
	
	void Update () 
	{
        if (!_isStart)
            return;

        UpdateCrabState();
        UpdateOpenShield();
        _timerObserve += Time.deltaTime;

        //test
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    _isHaveInterference = true;
        //}
        if (Input.GetKeyDown(KeyCode.P))
        {
            OnHurt(51);
        }
    }

    private void CheckPlayerHurt()
    {
        StartCoroutine(DelayAttackPlayer());
    }

    IEnumerator DelayAttackPlayer()
    {
        yield return new WaitForSeconds(1.2f);
        float dist = Vector3.Distance(transform.position, _target.transform.position);
        if (dist <= 6)
        {
            //Vector3 temp = _target.transform.position;
            //temp.y = transform.position.y;
            //Vector3 direction = (temp - transform.position).normalized;
            //_target.transform.position += direction * 5f;
            //_target.GetComponent<TestPlayer>().OnHurt();
        }
    }
}