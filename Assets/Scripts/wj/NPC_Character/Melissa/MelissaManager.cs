//MelissaManager.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/23/2017 3:55 PM
//Description: Mellissa管理
//

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MelissaManager : SingletonMono<MelissaManager>
{
    private const string MELISSA_PATH = "Prefabs/Character/NPCs/Melissa/Melissa";

    private long _taskStartTime = 0;
    private float _audioWaitTime = 0f;
    private float _timer = 0f;

    private bool _isStartTimer;
    private bool _isStartDetection;

    private bool canUpdate = false;

    private NPC_StoryConfig _curMelissaTask;

    private GameObject _melissa;
    public GameObject melissa
    {
        get { return _melissa; }
    }

    private MelissaController _melissaController;
    public MelissaController melissaController
    {
        get { return _melissaController; }
    }

    private MelissaTaskProcess _melissaTaskProcess;
    public MelissaTaskProcess melissaTaskProcess
    {
        get { return _melissaTaskProcess; }
    }

    private NPC_AudioController _melissaAudioController;
    public NPC_AudioController melissaAudioController
    {
        get { return _melissaAudioController; }
    }

    public void Init()
    {
        _melissa = UtilFunction.ResourceLoad(MELISSA_PATH);
        _melissa.name = "melissa";
        _melissaController = _melissa.GetComponent<MelissaController>();
        _melissaController.Init();

        _melissaTaskProcess = gameObject.AddComponent<MelissaTaskProcess>();
        _melissaTaskProcess.Init();
        _melissaAudioController = gameObject.AddComponent<NPC_AudioController>();
        _melissaAudioController.Init(melissa.GetComponent<AudioSource>());

        RunTask();

        canUpdate = true;
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
        if (melissaTaskProcess.curTaskID == melissaTaskProcess.melissaTaskGroup.taskList[melissaTaskProcess.melissaTaskGroup.taskList.Count - 1].taskId)
            return;

        _melissaTaskProcess.FinishCurTask();
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

    //public bool IsHasCurTask()
    //{

    //}

    private void EnterTask(NPC_StoryConfig melissaTask)
    {
        Debug.Log("Melissa--当前剧情" + melissaTask.taskId + "---" + melissaTask.description);
        //_melissaAudioController.PlaySound(melissaTask.audioId);
        _curMelissaTask = melissaTask;
        _melissaController.SetTargetPoint(melissaTask.targetPos);
        _melissaAudioController.PlaySound(melissaTask.audioId);

        _taskStartTime = TimeHelper.GetCurrentRealTimestamp();
        if (melissaTask.audioId > 0)
        {
            _audioWaitTime = _melissaAudioController.GetCurAudioLength(melissaTask.audioId);
        }
        else
        {
            _audioWaitTime = 0f;
        }

        switch (melissaTaskProcess.curTaskID)
        {
            case 1000:  //出现
                //_melissaController.SetCanSkipStep(true); //test
                _melissaController.SetState(MelissaController.STATE.RUN);
                break;
            case 1001://妈妈
                _isStartTimer = true;//可以开始计时
                //FinishCurTaskImmediately();//test
                break;
            case 1002://妈妈，门口有人
                _melissaController.SetState(MelissaController.STATE.TALK);
                _isStartTimer = true;//可以开始计时
                //FinishCurTaskImmediately();//test
                break;
            case 1003://跟我来
                FinishCurTask();
                break;
            case 1004://走向走廊
                _melissaController.SetCanSkipStep(true);
                _melissaController.SetState(MelissaController.STATE.WALK);
                break;
            case 1005://走向房间门口
                _melissaController.SetCanSkipStep(true);
                _melissaController.SetState(MelissaController.STATE.WALK);
                break;
            case 1006://偷瞄一眼
                _melissaController.SetState(MelissaController.STATE.IDLE);
                FinishCurTaskDelay(0.5f);
                break;
            case 1007://开门
                _melissaController.SetState(MelissaController.STATE.IDLE);
                FinishCurTaskDelay(0.5f);
                break;
            case 1008://跑进去
                _melissaController.SetState(MelissaController.STATE.RUN);
                break;
            case 1009://躲在桌子下面，等待玩家进入房间
                _isStartDetection = true;
                _melissaController.SetState(MelissaController.STATE.CROUCH);
                //FinishCurTaskImmediately();//test
                break;
            case 1010://你不能使用枪械，否则整个地方都会炸开，看那里
                _melissaController.SetState(MelissaController.STATE.CROUCH);
                FinishCurTask();
                break;
            case 1011://跟着玩家走
                _melissaController.SetState(MelissaController.STATE.FOLLOW);
                FinishCurTaskDelay(0.5f);
                break;
            case 1012://如果玩家想硬闯，跟随或者跑掉
                _melissaController.SetState(MelissaController.STATE.IDLE);
                FinishCurTaskDelay(0.5f);
                break;
            case 1013://跟玩家建议用通风管道，等待玩家打开通风管道
                _melissaController.SetState(MelissaController.STATE.WALK);
                break;
            case 1014://张开双手，等待玩家将自己放进管道
                _isStartDetection = true;
                _melissaController._isCanCreep = true;
                _melissaController.SetState(MelissaController.STATE.OPEN_HAND);
                //FinishCurTaskImmediately();//test
                break;
            case 1015://爬向出口
                _melissa.transform.position = new Vector3(-17.478f, 49.518f, 27.481f);//test
                _melissaController.SetState(MelissaController.STATE.CREEP);
                break;
            case 1016://提示玩家向通风口外看
                _melissaController.SetState(MelissaController.STATE.IDLE);
                FinishCurTaskDelay(0.5f);
                break;
            case 1017://走出通风口
                _isStartDetection = true;
                _melissaController.SetCanSkipStep(false);
                _melissaController.SetState(MelissaController.STATE.WALK);
                break;
            case 1018://跟随玩家
                _isStartDetection = true;
                _melissaController.SetState(MelissaController.STATE.FOLLOW);
                //FinishCurTaskDelay(0.5f);
                break;
            case 1019://玩家进门时，说话
                FinishCurTask();
                break;
            case 1020://跟随玩家
                _isStartDetection = true;
                _melissaController.SetState(MelissaController.STATE.FOLLOW);
                //FinishCurTaskDelay(0.5f);
                break;
            case 1021://到达实验室，说话
                _melissaController.SetState(MelissaController.STATE.TALK);
                FinishCurTask();
                break;
            case 1022://走向桌子
                _melissaController.SetCanSkipStep(true);
                _melissaController.SetState(MelissaController.STATE.WALK);
                break;
            case 1023://玩家砸玻璃，躲到桌子下
                _isStartDetection = true;
                _melissaController.SetState(MelissaController.STATE.CROUCH);
               
                break;
            case 1024://玩家打死 僵尸之后，跟随玩家
                _isStartDetection = true;
                _melissaController.SetState(MelissaController.STATE.FOLLOW);
                //_melissaController.SetState(MelissaController.STATE.WALK);
                //FinishCurTaskDelay(0.5f);
                break;
            case 1025://妈妈，你在吗
                _isStartTimer = true; //开始计算说话时间
                _melissaController.SetState(MelissaController.STATE.TALK);
                //FinishCurTask();
                break;
            case 1026://好的，妈妈！我会守在这等你。
                _isStartTimer = true;
                _melissaController.SetState(MelissaController.STATE.TALK);
                //FinishCurTask();  
                //TODO等待玩家锯开门
                break;
            case 1027://与Amy团聚
                _melissaController.SetState(MelissaController.STATE.IDLE);
                FinishCurTaskDelay(0.5f);
                break;
            case 1028://Amy死了，Melissa恨死玩家，摔门而去
                _melissaController.SetState(MelissaController.STATE.RUN);
                break;
        }
    }

    private void RunTask()
    {
        if (_melissaTaskProcess.melissaTaskGroup.taskDict.ContainsKey(_melissaTaskProcess.curTaskID))
            EnterTask(_melissaTaskProcess.melissaTaskGroup.taskDict[_melissaTaskProcess.curTaskID]);
        else
            Debug.LogError("找不到当前Melissa剧情---" + _melissaTaskProcess.curTaskID);
    }

    private void AmyStepContorller()
    {
        if (_isStartTimer)
        {
            _timer += Time.deltaTime;
            if (_timer >= _audioWaitTime + 0.2f)
            {
                _timer = 0f;
                _isStartTimer = false;
                AmyManager.Instance.FinishCurTask();
            }
        }
    }

    private void UpdateTask()
    {
        switch (melissaTaskProcess.curTaskID)
        {
            case 1000:  //出现
                break;
            case 1001://妈妈
                AmyStepContorller();
                break;
            case 1002://妈妈，门口有人
                AmyStepContorller();
                break;
            case 1003://跟我来
                break;
            case 1004://走向走廊
                break;
            case 1005://走向房间门口
                break;
            case 1006://偷瞄一眼
                break;
            case 1007://开门
                break;
            case 1008://跑进去
                break;
            case 1009://躲在桌子下面，等待玩家进入房间
                if (_isStartDetection)
                {
                    float dist = Vector3.Distance(_curMelissaTask.target.position, Level_10_Manager.Instance.MelissaPosition[1].position);
                    if (dist <= 0.5f)
                    {
                        _isStartDetection = false;
                        FinishCurTaskImmediately();
                    }
                }
                break;
            case 1010://你不能使用枪械，否则整个地方都会炸开，看那里
                break;
            case 1011://跟着玩家走
                break;
            case 1012://如果玩家想硬闯，跟随或者跑掉
                break;
            case 1013://跟玩家建议用通风管道，等待玩家打开通风管道
                break;
            case 1014://张开双手，等待玩家将自己放进管道
                if (_isStartDetection)
                {
                    if (_melissa.transform.position.y >= 47.3f)
                    {
                        _isStartDetection = false;
                        FinishCurTaskImmediately();
                    }
                }
                break;
            case 1015://爬向出口
                break;
            case 1016://提示玩家向通风口外看
                break;
            case 1017://走出通风口
                if (_isStartDetection)
                {
                    float dist = Vector3.Distance(Level_10_Manager.Instance.MelissaPosition[0].position, _curMelissaTask.target.position);
                    if (dist <= 0.4f)
                    {
                        _isStartDetection = false;
                        FinishCurTaskImmediately();
                    }
                }
                break;
            case 1018://跟随玩家
                _melissaController.SetTargetPoint(_curMelissaTask.target.position);
                if (_isStartDetection)
                {
                    float dist = Vector3.Distance(_curMelissaTask.target.position, new Vector3(-12.222f, 46.25f, 37.255f));
                    if (dist <= 0.5f)
                    {
                        _isStartDetection = false;
                        FinishCurTaskImmediately();
                    }
                }
                break;
            case 1019://玩家进门时，说话
                break;
            case 1020://跟随玩家
                _melissaController.SetTargetPoint(_curMelissaTask.target.position);
                if (_isStartDetection)
                {
                    float dist = Vector3.Distance(_melissa.transform.position, new Vector3(-10.202f, 46.25439f, 36.21f));
                    if (dist <= 0.4f)
                    {
                        _isStartDetection = false;
                        FinishCurTaskImmediately();
                    }
                }
                break;
            case 1021://到达实验室，说话
                break;
            case 1022://走向桌子
                break;
            case 1023://玩家砸玻璃，躲到桌子下
                if (_isStartDetection)
                {
                    if (Level_10_Manager.Instance.firstKO_SF) //玩家打死僵尸
                    {
                        _isStartDetection = false;
                        FinishCurTaskImmediately();
                    }
                }
                break;
            case 1024://玩家打死 僵尸之后，跟随玩家
                _melissaController.SetTargetPoint(_curMelissaTask.target.position);
                if (_isStartDetection)
                {
                    float dist = Vector3.Distance(_melissa.transform.position, _curMelissaTask.targetPos);
                    if (dist <= 0.5f)
                    {
                        _isStartDetection = false;
                        //FinishCurTaskImmediately();
                    }
                }
                break;
            case 1025://妈妈，你在吗
                AmyStepContorller();  //说完跳到Amy 1010步
                break;
            case 1026://好的，妈妈！我会守在这等你。
                AmyStepContorller();  //说完跳到Amy 1011步
                break;
            case 1027://与Amy团聚
                break;
            case 1028://Amy死了，Melissa恨死玩家，摔门而去
                break;
        }
    }

    private void Update()
    {
        if (canUpdate)
        {
        UpdateTask();
        }
    }
}
