//EmilyManager.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/25/2017 6:16 PM
//Description: 
//

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmilyManager : SingletonMono<EmilyManager> 
{
    private const string EMILY_PATH = "Prefabs/Character/NPCs/Emily/Emily";

    private long _taskStartTime = 0;
    private float _audioWaitTime = 0f;
    private float _timer = 0f;

    private bool _isStartTimer;
    private bool _isStartDetection;

    private NPC_StoryConfig _curEmilyTask;

    private GameObject _emily;
    public GameObject emily
    {
        get { return _emily; }
    }

    private EmilyController _emilyController;
    public EmilyController emilyController
    {
        get { return _emilyController; }
    }

    private EmilyStoryProcess _emilyStoryProcess;
    public EmilyStoryProcess emilyStoryProcess
    {
        get { return _emilyStoryProcess; }
    }

    private NPC_AudioController _emilyAudioController;
    public NPC_AudioController emilyAudioController
    {
        get { return _emilyAudioController; }
    }

    public void Init ()
	{
        _emily = UtilFunction.ResourceLoad(EMILY_PATH);
        _emilyController = _emily.GetComponent<EmilyController>();
        _emilyController.Init();

        _emilyStoryProcess = gameObject.AddComponent<EmilyStoryProcess>();
        _emilyStoryProcess.Init();
        _emilyAudioController = gameObject.AddComponent<NPC_AudioController>();
        _emilyAudioController.Init(emily.GetComponent<AudioSource>());

        //FinishTo(1013); //test直接出现在平民中间
        RunStory();
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
        _emilyStoryProcess.FinishCurTask();
        RunStory();
    }

    public void FinishTo(int id)
    {
        _emilyStoryProcess.FinishTo(id);
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

    private void EnterTask(NPC_StoryConfig task)
    {
        Debug.Log("Emily--当前剧情" + task.taskId + task.description);
        _curEmilyTask = task;
        _emilyAudioController.PlaySound(task.audioId);
        _emilyController.SetTargetPoint(task.targetPos);

        _taskStartTime = TimeHelper.GetCurrentRealTimestamp();
        if (task.audioId > 0)
        {
            _audioWaitTime = _emilyAudioController.GetCurAudioLength(task.audioId);
        }
        else
        {
            _audioWaitTime = 0f;
        }

        switch (_emilyStoryProcess.curTaskID)
        {
            case 1001: //出现
                break;
            case 1002:  //说话：等下，好像门口有人！
                _isStartTimer = true;
                _emilyController.SetState(EmilyController.STATE.IDLE);
                break;
            case 1003:  //说话：帮帮我们离开这里！
                _isStartTimer = true;
                _emilyController.SetState(EmilyController.STATE.IDLE);
                break;
            case 1004:  //说话：不，你们绝不能那么做！
                _isStartTimer = true;
                break;
            case 1005:  //说话：不…我不要这么做！
                _isStartTimer = true;
                break;
            case 1006:  //说话：Rick 你倒是说些什么呀？
                _isStartTimer = true;
                break;
            case 1007:  //说话：你怎么知道，他可能是好人！
                _isStartTimer = true;
                break;
            case 1008:  //说话：不…这是错误的做法，这么做会把我们都毁了…  等待玩家救援
                //TODO 等待救援
                _emily.transform.position = new Vector3(-25.76f, 46.25f, 29.8f);
                _isStartTimer = true;
                _isStartDetection = true;
                break;
            case 1009:  //说话：噢，谢谢你！我就知道会发生这样的事情！
                FinishCurTask();
                break;
            case 1010:  //说话：你看那边！你从这门走过去，就能看到电梯。
                FinishCurTask();
                break;
            case 1011:  //说话：我不跟你一起去了，我还有别的事要做
                FinishCurTask();
                break;
            case 1012:  //说话：对了，如果你找到Rick，救他一命。
                FinishCurTask();
                break;
            case 1013:  //消失
                //FinishCurTask();
                _emily.transform.position = new Vector3(-26.445f, 88.4924f, 33.424f);
                break;
            case 1014:  //出现在平民中间
                _isStartDetection = true;
                _emilyController.SetState(EmilyController.STATE.WALK_TO);
                //FinishCurTask();
                break;
            case 1015:  //走出来感谢玩家
                FinishCurTask();
                break;
            case 1016:  //说话：真是很谢谢你！
                FinishCurTask();
                break;
            case 1017:  //说话：这里的情况变得越来越糟糕，你们把电源切断后，
                _isStartDetection = true;
                _emilyController.SetCanSkip(false);
                _emilyController.SetState(EmilyController.STATE.WALK);
                //FinishCurTask();
                break;
            case 1018:  //说话：你可以通过这条路走到上一层
                FinishCurTask();
                break;
            case 1019:  //拿起重物，扔
                FinishCurTask();
                break;
            case 1020:  //如果玩家要求跳过去，然后摔死
                FinishCurTask();
                break;
            case 1021:  //说话：看，你现在明白了吧
                FinishCurTask();
                break;
            case 1022:  //说话：我们现在就要出发了！
                FinishCurTask();
                break;
            case 1023:  //说话：不管你是谁
                break;
        }
    }

    private void RunStory()
    {
        if (_emilyStoryProcess.emilyStoryGroup.storyDict.ContainsKey(_emilyStoryProcess.curTaskID))
            EnterTask(_emilyStoryProcess.emilyStoryGroup.storyDict[_emilyStoryProcess.curTaskID]);
        else
            Debug.Log("找不到当前剧情ID---" + _emilyStoryProcess.curTaskID);
    }

    private void Update()
    {
        UpdateTask();
    }

    private void UpdateTask()
    {
        switch (_emilyStoryProcess.curTaskID)
        {
            case 1001: //出现
                break;
            case 1002:  //说话：等下，好像门口有人！
                if (_isStartTimer)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _audioWaitTime + 1)
                    {
                        _timer = 0f;
                        _isStartTimer = false;
                        GeorgeManager.Instance.FinishCurTask();
                    }
                }
                break;
            case 1003:  //说话：帮帮我们离开这里！
                if (_isStartTimer)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _audioWaitTime + 1)
                    {
                        _timer = 0f;
                        _isStartTimer = false;
                        GeorgeManager.Instance.FinishCurTask();
                    }
                }
                break;
            case 1004:  //说话：不，你们绝不能那么做！
                if (_isStartTimer)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _audioWaitTime + 1)
                    {
                        _timer = 0f;
                        _isStartTimer = false;
                        GeorgeManager.Instance.FinishCurTask();
                    }
                }
                break;
            case 1005:  //说话：不…我不要这么做！
                if (_isStartTimer)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _audioWaitTime + 1)
                    {
                        _timer = 0f;
                        _isStartTimer = false;
                        LizzyManager.Instance.FinishCurTask();
                    }
                }
                break;
            case 1006:  //说话：Rick 你倒是说些什么呀？
                if (_isStartTimer)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _audioWaitTime + 1)
                    {
                        _timer = 0f;
                        _isStartTimer = false;
                        AmyManager.Instance.FinishCurTask();
                    }
                }
                break;
            case 1007:  //说话：你怎么知道，他可能是好人！
                if (_isStartTimer)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _audioWaitTime + 1)
                    {
                        _timer = 0f;
                        _isStartTimer = false;
                        GeorgeManager.Instance.FinishCurTask();
                    }
                }
                break;
            case 1008:  //说话：不…这是错误的做法，这么做会把我们都毁了…
                if (_isStartTimer)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _audioWaitTime + 1)
                    {
                        _timer = 0f;
                        _isStartTimer = false;
                        AmyManager.Instance.FinishCurTask();
                    }
                }

                if (_isStartDetection)
                {
                    float dist = Vector3.Distance(new Vector3(-25.76f, 46.25f, 29.8f), _curEmilyTask.target.position);
                    if (dist <= 0.4f)
                    {
                        _isStartDetection = false;
                        FinishCurTask();
                    }
                }
                break;
            case 1009:  //说话：噢，谢谢你！我就知道会发生这样的事情！
                break;
            case 1010:  //说话：你看那边！你从这门走过去，就能看到电梯。
                break;
            case 1011:  //说话：我不跟你一起去了，我还有别的事要做
                break;
            case 1012:  //说话：对了，如果你找到Rick，救他一命。
                break;
            case 1013:  //消失
                break;
            case 1014:  //出现在平民中间
                _emilyController.SetTargetPoint(_curEmilyTask.target.position);
                if (_isStartDetection)
                {
                    float dist = Vector3.Distance(_emily.transform.position, _curEmilyTask.target.position);
                    if (dist <= 1f)
                    {
                        _isStartDetection = false;
                        FinishCurTask();
                    }
                }
                break;
            case 1015:  //走出来感谢玩家
                break;
            case 1016:  //说话：真是很谢谢你！
                break;
            case 1017:  //说话：这里的情况变得越来越糟糕，你们把电源切断后，
                if (_isStartDetection)
                {
                    float dist = Vector3.Distance(new Vector3(-1.21f, 88.4924f, 37.94f), _curEmilyTask.target.position);
                    if (dist <= 1f)
                    {
                        _isStartDetection = false;
                        FinishCurTask();
                    }
                }
                break;
            case 1018:  //说话：你可以通过这条路走到上一层
                break;
            case 1019:  //拿起重物，扔
                break;
            case 1020:  //如果玩家要求跳过去，然后摔死
                break;
            case 1021:  //说话：看，你现在明白了吧
                break;
            case 1022:  //说话：我们现在就要出发了！
                break;
            case 1023:  //说话：不管你是谁
                break;
        }
    }
}
