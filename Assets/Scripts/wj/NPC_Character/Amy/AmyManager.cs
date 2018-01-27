//AmyManager.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/25/2017 10:03 AM
//Description: Amy管理
//

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmyManager : SingletonMono<AmyManager>
{
    private const string AMY_PATH = "Prefabs/Character/NPCs/Amy/Amy";

    private long _taskStartTime = 0;
    private float _audioWaitTime = 0f;
    private float _timer = 0f;

    private bool _isStartTimer;

    private NPC_StoryConfig _curStory;

    private AmyController _amyController;
    public AmyController amyController
    {
        get { return _amyController; }
    }

    private AmyStoryProcess _amyStoryProcess;
    public AmyStoryProcess amyStoryProcess
    {
        get { return _amyStoryProcess; }
    }

    private NPC_AudioController _amyAudioController;
    public NPC_AudioController amyAudioController
    {
        get { return _amyAudioController; }
    }

    private GameObject _amy;
    public GameObject amy
    {
        get { return _amy; }
    }

    public void Init()
    {
        _amy = UtilFunction.ResourceLoad(AMY_PATH);
        _amyController = _amy.GetComponent<AmyController>();
        _amyStoryProcess = gameObject.AddComponent<AmyStoryProcess>();
        _amyStoryProcess.Init();
        _amyAudioController = gameObject.AddComponent<NPC_AudioController>();
        _amyAudioController.Init(_amy.GetComponent<AudioSource>());

        EnterTask(_amyStoryProcess.amyStoryGroup.storyDict[_amyStoryProcess.curStoryID]);
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
        Debug.Log("声音长度：" + _audioWaitTime + ",等待时间：" +  taskWaitTime + ",过去时间：" + taskElapseTime);
        yield return new WaitForSeconds(taskWaitTime);
        FinishCurTaskImmediately();
    }

    public void FinishCurTaskImmediately()
    {
        if (_amyStoryProcess.curStoryID == _amyStoryProcess.amyStoryGroup.storyList[_amyStoryProcess.amyStoryGroup.storyList.Count - 1].taskId)
            return;

        _amyStoryProcess.FinishCurTask();

        if (_amyStoryProcess.amyStoryGroup.storyDict.ContainsKey(_amyStoryProcess.curStoryID))
            EnterTask(_amyStoryProcess.amyStoryGroup.storyDict[_amyStoryProcess.curStoryID]);
        else
            Debug.LogError("找不到当前任务 --- " + _amyStoryProcess.curStoryID);
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
        Debug.Log("Amy--当前剧情" + task.taskId + "---" + task.description);
        _amyAudioController.PlaySound(task.audioId);
        _taskStartTime = TimeHelper.GetCurrentRealTimestamp();
        if (task.audioId > 0)
        {
            _audioWaitTime = _amyAudioController.GetCurAudioLength(task.audioId);
        }
        else
        {
            _audioWaitTime = 0f;
        }
        switch (_amyStoryProcess.curStoryID)
        {
            case 1000://出现
                FinishCurTask();
                break;
            case 1001://RICK 你快滚开
                _isStartTimer = true;
                //FinishCurTask();
                break;
            case 1002://是你吗，宝贝你是怎么逃出来的
                _isStartTimer = true;
                //FinishCurTask();
                break;
            case 1003://外面的先生，不管你是谁，请不要伤害 我的女儿
                _isStartTimer = true;
                //FinishCurTask();
                break;
            case 1004://Melissa可以帮你，麻烦你照顾好她
                _isStartTimer = true;
                //FinishCurTask();  //test
                break;
            case 1005://Amy走过去，平息George
                //FinishCurTask();
                FinishCurTaskImmediately();
                break;
            case 1006://Emily，他说得也有道理…难道你不想离开这里？
                _isStartTimer = true;
                break;
            case 1007://你愿意牺牲你自己，我也可以
                _isStartTimer = true;
                break;
            case 1008://你曾经有个儿子，你忘了？
                 _isStartTimer = true;
                break;
            case 1009://好 现在让我们等！
                //FinishCurTask();
                break;
            case 1010://Melissa?宝贝，我在！
                _isStartTimer = true; //开始计时
                //FinishCurTask();
                break;
            case 1011://咦，Rick呢！George，Rick不见了！
                _isStartTimer = true;  //说完跳到小女孩1010步
                //FinishCurTask();
                break;
            case 1012://Liz你觉得我们要去哪里？
                _isStartTimer = true;  //说完跳到lizzy 1004步
                Level_10_Manager.Instance.CanChainsaw = true;
                //FinishCurTask();
                break;
            case 1013://与Melissa团聚
                FinishCurTask();
                break;
            case 1014://拿起激光锯打玩家
                _amyController.SetTarget(task.target);
                _amyController.SetState(AmyController.STATE.ATTACK_WALK);
                break;
            case 1015://身死道消
                _amyController.SetState(AmyController.STATE.DEAD);
                break;
        }
    }

    private void Update()
    {
        UpdateTask();
    }

    private void TimerFinish()
    {
        _timer += Time.deltaTime;
        if (_timer >= _audioWaitTime + 1)
        {
            _timer = 0f;
            _isStartTimer = false;
        }
    }

    private void UpdateTask()
    {
        
        switch (_amyStoryProcess.curStoryID)
        {
            case 1000://出现
                break;
            case 1001://RICK 你快滚开
                if (_isStartTimer)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _audioWaitTime + 0.2f)
                    {
                        _timer = 0f;
                        EmilyManager.Instance.FinishCurTask();
                        _isStartTimer = false;
                    }
                }
                break;
            case 1002://是你吗，宝贝你是怎么逃出来的
                if (_isStartTimer)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _audioWaitTime + 0.2f)
                    {
                        _timer = 0f;
                        MelissaManager.Instance.FinishCurTask();
                        _isStartTimer = false;
                    }
                }
                break;
            case 1003://外面的先生，不管你是谁，请不要伤害 我的女儿
                if (_isStartTimer)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _audioWaitTime + 0.2f)
                    {
                        _timer = 0f;
                        EmilyManager.Instance.FinishCurTask();
                        _isStartTimer = false;
                    }
                }
                break;
            case 1004://Melissa可以帮你，麻烦你照顾好她
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
            case 1005://Amy走过去，平息George
                break;
            case 1006://Emily，他说得也有道理…难道你不想离开这里？
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
            case 1007://你愿意牺牲你自己，我也可以
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
            case 1008://你曾经有个儿子，你忘了？
                if (_isStartTimer)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _audioWaitTime + 0.2f)
                    {
                        _timer = 0f;
                        _isStartTimer = false;
                        EmilyManager.Instance.FinishCurTask();
                        GeorgeManager.Instance.FinishCurTask();
                    }
                }
                break;
            case 1009://好 现在让我们等！
                break;
            case 1010://Melissa?宝贝，我在！
                if (_isStartTimer)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _audioWaitTime + 0.2f)
                    {
                        _timer = 0f;
                        MelissaManager.Instance.FinishCurTask();  //说完跳到小女孩1026步
                        _isStartTimer = false;
                    }
                }
                break;
            case 1011://咦，Rick呢！George，Rick不见了！
                if (_isStartTimer)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _audioWaitTime + 0.2f)
                    {
                        _timer = 0f;
                        GeorgeManager.Instance.FinishCurTask();  //说完跳到George 1010步
                        _isStartTimer = false;
                    }
                }
                break;
            case 1012://Liz你觉得我们要去哪里？
                if (_isStartTimer)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _audioWaitTime + 0.2f)
                    {
                        _timer = 0f;
                        LizzyManager.Instance.FinishCurTask();  //说完跳到lizzy 1004步
                        _isStartTimer = false;
                    }
                }
                break;
            case 1013://与Melissa团聚
                break;
            case 1014://拿起激光锯打玩家
                break;
            case 1015://身死道消
                break;
        }
    }
}
