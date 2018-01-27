//GeorgeManager.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/26/2017 12:31 PM
//Description: 
//

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeorgeManager : SingletonMono<GeorgeManager> 
{
    private const string GEORGE_PATH = "Prefabs/Character/NPCs/George/George";

    private long _taskStartTime = 0;
    private float _audioWaitTime = 0f;
    private float _timer = 0f;

    private bool _isStartTimer;
    private bool _isStartDetection;
    private bool _isOpenDoor;

    private NPC_StoryConfig _curGeorgeStory;

    private GameObject _george;
    public GameObject george
    {
        get { return _george; }
    }

    private GeorgeStoryProcess _georgeStoryProcess;
    public GeorgeStoryProcess georgeStoryProcess
    {
        get { return _georgeStoryProcess; }
    }

    private GeorgeController _georgeController;
    public GeorgeController georgeController
    {
        get { return _georgeController; }
    }

    private NPC_AudioController _georgeAudioController;
    public NPC_AudioController georgeAudioController
    {
        get { return _georgeAudioController; }
    }

    public void Init ()
	{
        _george = UtilFunction.ResourceLoad(GEORGE_PATH);
        _georgeController = _george.GetComponent<GeorgeController>();
        _georgeController.Init();
        _georgeStoryProcess = gameObject.AddComponent<GeorgeStoryProcess>();
        _georgeStoryProcess.Init();
        _georgeAudioController = gameObject.AddComponent<NPC_AudioController>();
        _georgeAudioController.Init(george.GetComponent<AudioSource>());
        //TEST
        StartTask();
	}

    public void StartTask()
    {
        RunStory();
    }

    public void SetDoorOpen()
    {
        _isOpenDoor = true;
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
        FinishCurTaskImmedately();
    }

    public void FinishCurTaskImmedately()
    {
        _georgeStoryProcess.FinishCurTask();

        RunStory();
    }

    public void FinishCurTaskDelay(float time)
    {
        StartCoroutine(DelayFinishTask(time));
    }

    IEnumerator DelayFinishTask(float time)
    {
        yield return new WaitForSeconds(time);
        FinishCurTaskImmedately();
    }

    private void RunStory()
    {
        if (_georgeStoryProcess.georgeStoryGroup.storyDict.ContainsKey(_georgeStoryProcess.curTaskID))
            EnterStory(_georgeStoryProcess.georgeStoryGroup.storyDict[_georgeStoryProcess.curTaskID]);
        else
            Debug.Log("找不到当前任务ID:" + _georgeStoryProcess.curTaskID);
    }
	
	private void EnterStory(NPC_StoryConfig story)
    {
        Debug.Log("George--当前任务:" + _georgeStoryProcess.curTaskID + "---" + story.description);
        _curGeorgeStory = story;
        _georgeAudioController.PlaySound(story.audioId);

        _taskStartTime = TimeHelper.GetCurrentRealTimestamp();
        if (story.audioId > 0)
        {
            _audioWaitTime = _georgeAudioController.GetCurAudioLength(story.audioId);
        }
        else
        {
            _audioWaitTime = 0f;
        }

        switch (_georgeStoryProcess.curTaskID)
        {
            case 1001:  //出现
                //FinishCurTask();
                break;
            case 1002:  //说话：是不是外面有人？
                //FinishCurTask();
                _isStartTimer = true;
                break;
            case 1003:  //说话：求求你帮我们离开这里！
                //FinishCurTask();
                _isStartTimer = true;
                break;
            case 1004:  //说话：帮我们想办法把门打开。
                _isStartTimer = true;
                _isStartDetection = true;   //检测玩家到达监控室
                //FinishCurTask();    //test
                break;
            case 1005:  //说话：门打开后，我们要一起干倒这个士兵
                _isStartTimer = true;
                break;
            case 1006:  //把Emily推到墙角
                //FinishCurTask();
                FinishCurTaskImmedately();
                break;
            case 1007:  //说话：你现在要听我的，要不你站在我们这边
                _isStartTimer = true;
                break;
            case 1008:  //说话：哈哈哈哈…
                _isStartTimer = true;
                break;
            case 1009:  //把Emily整到屋里
                //TODO 拖着Emily走
                _isStartDetection = true;//TODO 判断玩家是否来到门前
                break;
            case 1010:  //说话：你们终于来了！但是这门好像被什么卡住了！
                //FinishCurTask();
                _isStartTimer = true;
                break;
            case 1011:  //说话：他走不了很远。他需要我们！
                _isStartTimer = true;  //说完跳到Amy 1012步
                _isStartDetection = true;//TODO 判断玩家是否来到门前
                
                //FinishCurTask();
                break;
            case 1012:  //说话：太好了，你把门锯开了！
                FinishCurTask();
                break;
            case 1013:  //打玩家
                _georgeController.SetTarget(story.target);
                _georgeController.SetState(GeorgeController.STATE.ATTACK_WALK);
                break;
            case 1014:
                _georgeController.SetState(GeorgeController.STATE.DEATH);
                break;
        }
    }

    private void Update()
    {
        UpdateTask();

        //test
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            _isOpenDoor = true;
        }
    }

    private void UpdateTask()
    {
        switch (_georgeStoryProcess.curTaskID)
        {
            case 1001:  //出现
                break;
            case 1002:  //说话：是不是外面有人？
                if (_isStartTimer)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _audioWaitTime + 0.2f)
                    {
                        _timer = 0f;
                        _isStartTimer = false;
                        LizzyManager.Instance.FinishCurTask();
                    }
                }
                break;
            case 1003:  //说话：求求你帮我们离开这里！
                if (_isStartTimer)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _audioWaitTime + 0.2f)
                    {
                        _timer = 0f;
                        _isStartTimer = false;
                        MelissaManager.Instance.FinishCurTask();
                    }
                }
                break;
            case 1004:  //说话：帮我们想办法把门打开。
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

                if (_isStartDetection)
                {
                    //float dist = Vector3.Distance(_curGeorgeStory.target.position, new Vector3(5.894f, 46.25f, 27.484f));
                    //if (dist <= 0.4f)
                    //{
                    //    _isStartDetection = false;
                    //    FinishCurTask();
                    //}
                    //等玩家输入完密码
                    if (Level_10_Manager.Instance.CompletePuzzle)
                    {
                        _isStartDetection = false;
                        FinishCurTask();
                    }
                }
                break;
            case 1005:  //说话：门打开后，我们要一起干倒这个士兵
                if (_isStartTimer)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _audioWaitTime + 0.2f)
                    {
                        _timer = 0f;
                        _isStartTimer = false;
                        EmilyManager.Instance.FinishCurTask();
                    }
                }
                break;
            case 1006:  //把Emily推到墙角
                break;
            case 1007:  //说话：你现在要听我的，要不你站在我们这边
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
                break;
            case 1008:  //说话：哈哈哈哈…
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
                break;
            case 1009:  //把Emily整到房间里
                if (_isStartDetection)
                {
                    float dist = Vector3.Distance(new Vector3(-22.67f, 46.219f, 41.05f), _curGeorgeStory.target.position);
                    if (dist <= 2f)
                    {
                        _isStartDetection = false;
                        FinishCurTask();
                    }
                }
                break;
            case 1010:  //说话：你们终于来了！但是这门好像被什么卡住了！ 
                if (_isStartTimer)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _audioWaitTime + 0.2f)
                    {
                        _timer = 0f;
                        _isStartTimer = false;
                        MelissaManager.Instance.FinishCurTask(); //跳到小女孩1025步
                    }
                }
                break; 
            case 1011:  //说话：他走不了很远。他需要我们！
                if (_isStartTimer)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _audioWaitTime + 0.2f)
                    {
                        _timer = 0f;
                        _isStartTimer = false;
                        AmyManager.Instance.FinishCurTask();  //说完跳到Amy 1012步
                    }
                }
                if (_isStartDetection)
                {
                    float dist = Vector3.Distance(new Vector3(-22.67f, 46.219f, 41.05f), _curGeorgeStory.target.position);
                    if (_isOpenDoor)
                    {
                        _isStartDetection = false;

                        FinishCurTaskImmedately();
                        AmyManager.Instance.FinishCurTaskImmediately(); //跳到Amy 1013步，与Melissa团聚
                        MelissaManager.Instance.FinishCurTaskImmediately(); //跳到小女孩1027步，与Amy团聚
                        LizzyManager.Instance.FinishCurTaskImmediately(); //跳到lizzy 1005，逃跑
                    }
                }
                break;
            case 1012:  //说话：太好了，你把门锯开了！
                break;
            case 1013:  //打玩家
                break;
        }
    }
}
