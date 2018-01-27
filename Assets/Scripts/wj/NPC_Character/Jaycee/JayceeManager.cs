//
//  DecodingPanel.cs
//  TEOT_ONLINE
//
//  Created by 王颉 on 8/9/2017 11:06 AM.
//  Jaycee管理
//

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JayceeManager : SingletonMono<JayceeManager>
{
    private const string SAOBA_PARENT_PATH = "Jaycee/ZMB_SpineJ_1/ZMB_SpineJ_2/ZMB_SpineJ_3/ZMB_SpineJ_4/" +
        "ZMB_SpineJ_5/ZMB_SpineJ_6/ZMB_SpineJ_7/ZMB_SpineJ_8/ZMB_RShoulderPlateJ_1/ZMB_RShoulderJ_1/" +
        "ZMB_RForeArmJ_1/ZMB_RForeArmJ_2/ZMB_RWristJ_1/ZMB_RWristJ_1_ctrl/" +
        "ZMB_RMidFinBaseJ_1_sd/ZMB_RMidFinBaseJ_1_ctrl/ZMB_RMidFinJ_1_sd";
    private const string HUMAN_JAYCEE_PATH = "Prefabs/Character/NPCs/Jaycee/Jaycee";
    private const string MONSTER_JAYCEE_PATH = "Prefabs/Character/NPCs/Jaycee/JayceeMonster";

    private long _taskStartTime = 0;
    private float _audioWaitTime = 0f;
    private float _timer = 0f;

    private bool _isStartTimer;
    private bool _isCanTalk;
    public bool isCanTalk
    {
        set { _isCanTalk = value; }
    }

    public bool isHumanJayceeDead;
    public bool isMonsterJayceeDead;
    private bool _isStartDetection;

    public NPC_StoryConfig _curStory;

    public GameObject humanState { get;set; } //Jaycee人形态

    public GameObject monsterState { get; set; } //Jaycee怪形态

    private JayceeHumanController _jayceeHumanController;
    public JayceeHumanController jayceeHumanController
    {   
        get { return _jayceeHumanController; }
    }

    private JayceeMonsterController _jayceeMonsterController;
    public JayceeMonsterController jayceeMonsterController
    {
        get { return _jayceeMonsterController; }
    }

    public JayceeStoryProcess jayceeStoryProcess
    {
        get;set;
    }

    private NPC_AudioController _jayceeAudioController;
    public NPC_AudioController jayceeAudioController
    {
        get { return _jayceeAudioController; }
    }
    
	public void Init ()
    {
        humanState = UtilFunction.ResourceLoad(HUMAN_JAYCEE_PATH);
        //monsterState = UtilFunction.ResourceLoad(MONSTER_JAYCEE_PATH);
        //jayceeMonsterController = monsterState.GetComponent<JayceeMonsterController>();
        _jayceeHumanController = humanState.GetComponent<JayceeHumanController>();
        jayceeStoryProcess = gameObject.AddComponent<JayceeStoryProcess>();
        jayceeStoryProcess.Init();
        _jayceeAudioController = gameObject.AddComponent<NPC_AudioController>();
        _jayceeAudioController.Init(humanState.GetComponent<AudioSource>());

        RunTask();
    }

    public void SkipStepTo(int id)
    {
        jayceeStoryProcess.SkipStepTo(id);
        
    }

    public void FinishCurTask()
    {
        //完成一些事之后，在执行（播放声音）
        StartCoroutine(FinishCurTaskWait());
    }

    IEnumerator FinishCurTaskWait()
    {
        float taskElapseTime = (float)(TimeHelper.GetCurrentRealTimestamp() - _taskStartTime) / 1000;
        float taskWaitTime = Math.Max(_audioWaitTime - taskElapseTime, 0);
        Debug.Log("声音长度：" + _audioWaitTime + ",等待时间：" + taskWaitTime + ",过去时间：" + taskElapseTime);
        yield return new WaitForSeconds(taskWaitTime);
        FinishCurTaskImmediately();
    }

    public void FinishCurTaskImmediately()
    {
        jayceeStoryProcess.FinishCurTaskImmediately();
        RunTask();
    }

    public void FinishCurTaskDelay(float time)
    {
        StartCoroutine(DelayFinishTask(time));
    }

    IEnumerator DelayFinishTask(float time)
    {
        yield return new WaitForSeconds(time);
        FinishCurTaskImmediately();
    }

    public void EnterTask(NPC_StoryConfig task)
    {
        Debug.Log("Jaycee--当前任务ID" + task.taskId + "---" + task.description);
        _curStory = task;
        _jayceeAudioController.PlaySound(task.audioId);

        _taskStartTime = TimeHelper.GetCurrentRealTimestamp();
        if (task.audioId > 0)
        {
            _audioWaitTime = _jayceeAudioController.GetCurAudioLength(task.audioId);
        }
        else
        {
            _audioWaitTime = 0f;
        }

        switch (jayceeStoryProcess.curTaskID)
        {
            case 1001://Jaycee出现
                _jayceeHumanController.StartAppear();
                FinishCurTaskDelay(0.5f);
                break;
            case 1002://走向门口
                //_jayceeHumanController.SetTargetPoint(task.targetPos);
                //_jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.RUN);
                FinishCurTask();
                break;
            case 1003://回头看
                FinishCurTaskImmediately();
                break;
            case 1004://向厕所跑
                _jayceeHumanController.SetTargetPoint(task.targetPos);
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.RUN);
                break;
            case 1005://在厕所等待玩家,拿起扫把
                _isStartDetection = true;
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.IDLE);
                humanState.transform.Rotate(new Vector3(0, 180, 0));
                _jayceeHumanController.SetActiveBesom(true);
                _jayceeHumanController.isCanRobBesom = true;
                TaskStepManagaer.Instance.FinishCurTaskImmediately();
                break;
            case 1006://玩家从大厅进门后，说话
                _isStartDetection = true;
                break;
            case 1007://玩家到达门口，说话
                _isStartDetection = true;
                break;
            case 1008: //进门，打玩家
                _isStartDetection = true;
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.ATTACK);
                break;
            case 1009://抢完了扫把，蹲在地上说话
                _isStartDetection = true;
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.CROUCH);
                break;
            case 1010://玩家出去之后，到达某一点，跑出去，等玩家
                _isStartDetection = true;
                _jayceeHumanController.SetCanSkipStep(false);
                _jayceeHumanController.SetTargetPoint(task.targetPos);
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.RUN);
                break;
            case 1011://跑向房间
                _isStartDetection = true;
                _jayceeHumanController.SetCanSkipStep(false);
                _jayceeHumanController.SetTargetPoint(task.targetPos);
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.RUN);
                //TaskStepManagaer.Instance.FinishCurTaskImmediately();
                break;
            case 1012://玩家到了另一个房间，跑出来
                _jayceeHumanController.SetCanSkipStep(true);
                _jayceeHumanController.SetTargetPoint(task.targetPos);
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.RUN);
                break;
            case 1013://跑出来之后，骂玩家
                _isStartDetection = true;
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.IDLE);
                TaskStepManagaer.Instance.FinishCurTaskImmediately();
                break;
            case 1014://玩家走近之后，带领玩家走
                FinishCurTaskImmediately();
                break;
            case 1015://带玩家走
                _jayceeHumanController.SetTarget(_curStory.target.gameObject);
                _jayceeHumanController.SetCanSkipStep(true);
                _jayceeHumanController.SetTargetPoint(task.targetPos);
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.LEADER);
                TaskStepManagaer.Instance.FinishTaskTo(7001);
                break;
            case 1016://楼塌，跳过去
                _jayceeHumanController.SetCanSkipStep(false);
                _isStartDetection = true;
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.WALK);
                _jayceeHumanController.SetTargetPoint(task.targetPos);
                break;
            case 1017://往角落跑
                _jayceeHumanController.SetCanSkipStep(true);
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.WALK);
                _jayceeHumanController.SetTargetPoint(task.targetPos);
                break;
            case 1018://跑向房间
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.WALK);
                _jayceeHumanController.SetTargetPoint(task.targetPos);
                break;
            case 1019://跑向一个房间，关门,等待玩家
                _isStartDetection = true;
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.IDLE);
                //FinishCurTaskDelay(0.5f);//test
                break;
            case 1020://再次见到玩家，说话
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.IDLE);
                FinishCurTask();
                break;
            case 1021:  //往房间走
                _jayceeHumanController.SetTargetPoint(task.targetPos);
                _jayceeHumanController.SetTarget(_curStory.target.gameObject);
                _jayceeHumanController.SetCanSkipStep(true);
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.LEADER);
                break;   
            case 1022:  //被怪物攻击
                //_isStartDetection = true;
                break;
            case 1023:  //怪物被打死之后，继续向房间走
                _jayceeHumanController.SetTargetPoint(task.targetPos);
                _jayceeHumanController.SetTarget(_curStory.target.gameObject);
                _jayceeHumanController.SetCanSkipStep(true);
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.LEADER);
                break;   
            case 1024:  //被怪物攻击
                //_isStartDetection = true;
                break;
            case 1025:  //怪物被打死，带领玩家走到柜子旁边
                _isStartDetection = true;
                _jayceeHumanController.SetTarget(_curStory.target.gameObject);
                _jayceeHumanController.SetTargetPoint(task.targetPos);
                _jayceeHumanController.SetCanSkipStep(false);
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.LEADER);
                break;
            case 1026://打开柜子
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.IDLE);
                FinishCurTaskDelay(0.5f);
                break;
            case 1027://说话
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.IDLE);
                FinishCurTask();
                break;
            case 1028://走到另一边
                _jayceeHumanController.SetCanSkipStep(true);
                _jayceeHumanController.SetTargetPoint(task.targetPos);
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.WALK);
                break;
            case 1029://说话
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.IDLE);
                FinishCurTask();
                break;
            case 1030://边走边说，等待玩家拿起注射器
                _isStartDetection = true;
                _jayceeHumanController.SetCanSkipStep(false);
                _jayceeHumanController.SetTargetPoint(task.targetPos);
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.WALK);
                //FinishCurTaskImmediately();//test
                break;
            case 1031://玩家拿起了注射器，然后外面发起尖叫,跑出房间，边跑边说
                //_isStartDetection = true;
                _jayceeHumanController.SetCanSkipStep(true);
                _jayceeHumanController.SetTargetPoint(task.targetPos);
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.RUN);
                break;
            case 1032://玩家到达，抱起女孩，说话
                //_isStartDetection = true;
                //_jayceeHumanController.SetCanSkipStep(true);
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.IDLE);
                FinishCurTask();
                break;
            case 1033://跑向房间
                _jayceeHumanController.SetCanSkipStep(false);
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.RUN);
                _jayceeHumanController.SetTargetPoint(task.targetPos);
                break;
            case 1034://后退，撞墙
                _jayceeHumanController.SetCanSkipStep(true);
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.WALK_BACK);
                _jayceeHumanController.SetTargetPoint(task.targetPos);
                //FinishCurTaskDelay(3f);//WSM
                break;
            case 1035://摔倒
                _jayceeHumanController.SetState(JayceeHumanController.JAYCEE_H_STATE.CROUCH);
                FinishCurTaskDelay(0.5f);
                break;
            case 1036://说话
                FinishCurTask();
                break;
            case 1037://身体抖动，说话
                FinishCurTask();
                break;
            case 1038://剧烈抖动，脸变色，说话
                FinishCurTask();
                break;
            case 1039://变身
                humanState.SetActive(false);
                monsterState = UtilFunction.ResourceLoad(MONSTER_JAYCEE_PATH);
                _jayceeMonsterController = monsterState.GetComponent<JayceeMonsterController>();
                break;
            case 1040://起身
                FinishCurTask();
                break;
            case 1041://打玩家
                break;
            case 1042://变boss
                break;
            case 1043://追玩家
                break;
        }
    }

    private void RunTask()
    {
        if (jayceeStoryProcess.jayceeTaskGroup.taskDict.ContainsKey(jayceeStoryProcess.curTaskID))
        {
            EnterTask(jayceeStoryProcess.jayceeTaskGroup.taskDict[jayceeStoryProcess.curTaskID]);
        }
    }

    private void Start()
    {
        //test
        //Init();
    }

    private void Update ()
    {
        UpdateTask();
	}

    private void UpdateTask()
    {
        switch (jayceeStoryProcess.curTaskID)
        {
            case 1001://Jaycee出现
                break;
            case 1002://走向门口
                break;
            case 1003://回头看
                break;
            case 1004://向厕所跑
                break;
            case 1005://在厕所等待玩家,拿起扫把
                if (_isStartDetection)
                {
                    float dist = Vector3.Distance(_curStory.target.position, new Vector3(-28.06f, 20.48f, 36.56f));//用来判断玩家是否到达大厅
                    if (dist <= 1f)
                    {
                        _isStartDetection = false;
                        FinishCurTask();
                    }
                }
                break;
            case 1006://玩家从大厅进门后，说话
                if (_isStartDetection)
                {
                    float dist = Vector3.Distance(_curStory.target.position, new Vector3(-24.12f, 20.48f, 38.02f)); //用来判断玩家是否到达门口
                    if (dist <= 1f && _isCanTalk)
                    {
                        _isStartDetection = false;
                        FinishCurTask();
                    }
                }
                break;
            case 1007://玩家到达门口，说话
                if (_isStartDetection)
                {
                    float dist = Vector3.Distance(_curStory.target.position, new Vector3(-23.13f, 20.48f, 40.46f)); //用来判断玩家是否进门
                    if (dist <= 1f)
                    {
                        _isStartDetection = false;
                        FinishCurTask();
                    }
                }
                break;
            case 1008://进门，打玩家
                if (_isStartDetection)
                {
                    GameObject saobaParent = humanState.transform.Find(SAOBA_PARENT_PATH).gameObject;
                    if (saobaParent.transform.childCount <= 1) //判断手里还有没有扫把（玩家是否抢走了扫把）
                    {
                        _isStartDetection = false;
                        FinishCurTask();
                    }
                }
                break;
            case 1009://抢完了扫把，蹲在地上说话
                if (_isStartDetection)
                {
                    float dist = Vector3.Distance(_curStory.target.position, new Vector3(-23.508f, 20.48f, 38.634f));
                    if (dist <= 1f)
                    {
                        _isStartDetection = false;
                        FinishCurTaskImmediately();
                    }
                }
                break;
            case 1010://玩家出去之后，到达某一点，跑出去，等玩家
                if (_isStartDetection)
                {
                    float dist = Vector3.Distance(_curStory.target.position, new Vector3(-10.63f, 20.48f, 38.18f));
                    if (dist <= 2f)
                    {
                        _isStartDetection = false;
                        FinishCurTask();
                    }
                }
                break;
            case 1011://跑向房间
                if (_isStartDetection)
                {
                    float dist = Vector3.Distance(_curStory.target.position, new Vector3(-1.83f, 20.48f, 27.15f));//判断玩家是否到了另一个房间
                    if (dist <= 0.5f)
                    {
                        _isStartDetection = false;
                        FinishCurTaskImmediately();
                    }
                }
                break;
            case 1012://玩家到了另一个房间，跑出来
                break;
            case 1013://跑出来之后，骂玩家
                if (_isStartDetection)
                {
                    float dist = Vector3.Distance(_curStory.target.position, new Vector3(-8.548f, 20.48f, 30.82f));//判断玩家是否走近
                    if (dist <= 0.5f)
                    {
                        _isStartDetection = false;
                        FinishCurTaskImmediately();
                    }
                }

                break;
            case 1014://玩家走近之后，带领玩家走
                break;
            case 1015://带玩家走
                break;
            case 1016://楼塌，跳过去
                if (_isStartDetection)
                {
                    float dist = Vector3.Distance(_curStory.target.position, Level_05_Manager.Instance.PlayerPositions[9].position);
                    if (dist <= 1f)
                    {
                        _isStartDetection = false;
                        FinishCurTask();
                    }
                }
                break;
            case 1017://往角落跑
                break;
            case 1018://跑向房间
                break;
            case 1019://跑向一个房间，关门,等待玩家
                if (_isStartDetection)
                {
                    float dist = Vector3.Distance(_curStory.target.position, new Vector3(-0.98f, 26.45631f, 34.81f));
                    float dist_2 = Vector3.Distance(_curStory.target.position, new Vector3(-6.0f, 26.4f, 37.8f));
                    if (dist <= 1f || dist_2<=1f)
                    {
                        _isStartDetection = false;
                        FinishCurTask();
                    }
                }
                break;
            case 1020://再次见到玩家，说话
                break;
            case 1021:  //往房间走
                break;
            case 1022:  //被怪物攻击
                if (_isStartDetection)
                {
                    //需要打死怪物
                        _isStartDetection = false;
                    FinishCurTask();
                }
                break;
            case 1023:  //怪物被打死之后，继续向房间走
                break;
            case 1024:  //被怪物攻击
                if (_isStartDetection)
                {
                    //需要打死怪物
                        _isStartDetection = false;
                    FinishCurTask();
                }
                break;
            case 1025://怪物被打死，走到柜子旁边
                if (_isStartDetection)  //需要拿起注射器 test
                {
                    float dist = Vector3.Distance(_curStory.target.position, new Vector3(-24.952f, 26.411f, 39.895f));
                    if (dist <= 1f)
                    {
                        _isStartDetection = false;
                        FinishCurTask();
                    }
                }
                break;
            case 1026://打开柜子
                break;
            case 1027://说话
                break;
            case 1028://走到另一边
                break;
            case 1029://说话
                break;
            case 1030://边走边说，等待玩家拿起注射器
                if (_isStartDetection)  //需要拿起注射器 test
                {
                    float dist = Vector3.Distance(_curStory.target.position, new Vector3(-24.27f, 26.411f, 26.41f));
                    if (dist <= 1f)
                    {
                        _isStartDetection = false;
                        FinishCurTask();
                    }
                }
                break;
            case 1031://玩家拿起了注射器，然后外面发起尖叫,跑出房间，边跑边说
                if (_isStartDetection)
                {
                    float dist = Vector3.Distance(_curStory.target.position, new Vector3(-20f, 26.411f, 29.71f));
                    if (dist <= 0.4f)
                    {
                        _isStartDetection = false;
                        FinishCurTask();
                    }
                }
                break;
            case 1032://玩家到达，抱起女孩，说话
                break;
            case 1033://跑向房间
                break;
            case 1034://后退，撞墙
                break;
            case 1035://摔倒
                break;
            case 1036://说话
                break;
            case 1037://身体抖动，说话
                break;
            case 1038://剧烈抖动，脸变色，说话
                break;
            case 1039://变身
                break;
            case 1040://起身
                break;
            case 1041://打玩家
                break;
            case 1042://变boss
                break;
            case 1043://追玩家
                break;
        }
    }
}
