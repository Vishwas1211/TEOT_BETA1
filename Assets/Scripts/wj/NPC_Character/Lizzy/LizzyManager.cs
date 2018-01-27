//LizzyManager.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/26/2017 9:58 AM
//Description: 
//

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizzyManager : SingletonMono<LizzyManager>
{
    private const string LIZZY_PATH = "Prefabs/Character/NPCs/Lizzy/Lizzy";

    private long _taskStartTime = 0;
    private float _audioWaitTime = 0f;
    private float _timer = 0f;

    private bool _isStartTimer;
    public bool _isStartDetection;
    private bool _isRescued;
    private bool _isLeave;

    private NPC_StoryConfig _curLizzyTask;

    private GameObject _lizzy;
    public GameObject lizzy
    {
        get { return _lizzy; }
    }

    private LizzyStoryProcess _lizzyStoryProcess;
    public LizzyStoryProcess lizzyStoryProcess
    {
        get { return _lizzyStoryProcess; }
    }

    private LizzyController _lizzyController;
    public LizzyController lizzyController
    {
        get { return _lizzyController; }
    }

    private NPC_AudioController _lizzyAudioController;
    public NPC_AudioController lizzyAudioController
    {
        get { return _lizzyAudioController; }
    }

    public void Init()
    {
        _lizzyStoryProcess = gameObject.AddComponent<LizzyStoryProcess>();
        _lizzyStoryProcess.Init();
        _lizzy = UtilFunction.ResourceLoad(LIZZY_PATH);
        _lizzyController = _lizzy.GetComponent<LizzyController>();
        _lizzyController.Init();
        _lizzyAudioController = gameObject.AddComponent<NPC_AudioController>();
        _lizzyAudioController.Init(lizzy.GetComponent<AudioSource>());

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
        _lizzyStoryProcess.FinishCurTask();

        RunStory();
    }


    public void FinishCurTaskImmediately(int i)
    {
        _lizzyStoryProcess.FinishCurTask(i);

        RunStory();
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

    private void RunStory()
    {
        if (_lizzyStoryProcess.lizzyStoryGroup.storyDict.ContainsKey(_lizzyStoryProcess.curTaskID))
            EnterStory(_lizzyStoryProcess.lizzyStoryGroup.storyDict[_lizzyStoryProcess.curTaskID]);
        else
            Debug.Log("找不到当前任务ID" + _lizzyStoryProcess.curTaskID);
    }

    private void EnterStory(NPC_StoryConfig story)
    {
        Debug.Log("Lizzy--当前任务ID" + story.taskId + "---" + story.description);
        _curLizzyTask = story;
        _lizzyAudioController.PlaySound(story.audioId);
        _lizzyController.SetTargetPoint(story.targetPos);

        _taskStartTime = TimeHelper.GetCurrentRealTimestamp();
        if (story.audioId > 0)
        {
            _audioWaitTime = _lizzyAudioController.GetCurAudioLength(story.audioId);
        }
        else
        {
            _audioWaitTime = 0f;
        }

        switch (_lizzyStoryProcess.curTaskID)
        {
            case 1001://出现
                //FinishCurTaskImmediately(1007);   //test
                //_isStartDetection = true;
                //_lizzyController.SetCanSkipStep(false);
                //_lizzyController.SetState(LizzyController.STATE.RUN);
                //FinishCurTask();    //test
                break;
            case 1002://说话：快帮帮我们！我们需要帮助！
                _isStartTimer = true;
                //FinishCurTask();    //test
                break;
            case 1003://说话：你到底在想些什么？
                _isStartTimer = true;
                //FinishCurTask();    //test
                break;
            case 1004://说话：好我知道！
                _isStartTimer = true;
                //FinishCurTask();    //test
                break;
            case 1005://逃跑
                _lizzy.transform.position = new Vector3(-0.74f, 80.61f, 36.32f);
                //_lizzyController.SetCanSkipStep(false);
                _isStartDetection = true;
                _lizzyController.SetState(LizzyController.STATE.IDLE);
                //FinishCurTask();    //test
                break;
            case 1006://陷入困境
                _isStartDetection = true;
                _lizzyController.SetCanSkipStep(false);
                _lizzyController.SetState(LizzyController.STATE.RUN);
                break;
            case 1007://看到玩家后，说话：啊呃呃，救我…救我出这里！
                FinishCurTask();
                break;
            case 1008://说话：谁..你是谁？…噢是你！对不起不是我的错。George...！
                FinishCurTask();
                break;
            case 1009://（哭泣道）…我不想死…至少不要在今天…
                TaskStepManagaer.Instance.FinishCurTaskImmediately();
                FinishCurTask();
                //_isStartDetection = true;

                break;
            case 1010://如果玩家走开不救，：不…不要…不要留我在这里等死！求求你！！！（Lizzy剧情结束)
                _isStartDetection = true;
                _lizzyController.SetCanSkipStep(false);
                _lizzyController.SetState(LizzyController.STATE.RUN);
                //if (_isRescued) //救了就立即跳到下一步
                //{
                //    FinishCurTaskImmediately();
                //}
                break;
            case 1011://如果被救，:谢谢你救我！对于之前发生的事情，我很抱歉，因为我太害怕了！
                _lizzyController.SetState(LizzyController.STATE.DEATH);
                TaskStepManagaer.Instance.FinishCurTaskImmediately();
                //FinishCurTask();
                break;
            case 1012://如果被救，:谢谢你救我！对于之前发生的事情，我很抱歉，因为我太害怕了！
                FinishCurTask();
                break;
            case 1013://：不管怎么样…我知道有条路可以带你到顶楼。跟我来！
                FinishCurTask();
                break;
            case 1014://带领玩家走
                _lizzyController.SetCanSkipStep(true);
                _lizzyController.SetState(LizzyController.STATE.WALK);
                TaskStepManagaer.Instance.FinishCurTaskImmediately();
                //FinishCurTask();
                break;
            case 1015://搬橱柜，玩家帮忙
                _isStartDetection = true;
                _lizzyController.SetState(LizzyController.STATE.IDLE);
                //FinishCurTask();
                break;
            case 1016://“这个通道直达那边的楼梯，通过楼梯到上一层，那里有...”
                FinishCurTask();
                break;
            case 1017://被攻击，“阿呃呃”
                _lizzyController.SetState(LizzyController.STATE.DEATH);
                break;
            case 1018://技能动作，被攻击
                FinishCurTask();
                break;
            case 1019://躺在地上，奄奄一息
                //_lizzyController.SetState(LizzyController.STATE.LAY_DOWN);
                FinishCurTask();
                break;
            case 1020://打败敌人后，“咳咳…我想我活不了了。救救Rick…”
                FinishCurTask();
                break;
            case 1021://咳嗽，吐血
                FinishCurTask();
                break;
            case 1022://“谢谢你…还有..对不…起”
                FinishCurTask();
                break;
            case 1023://极乐
                _lizzyController.SetState(LizzyController.STATE.DEATH);
                break;
        }
    }

    private void Update()
    {
        UpdateTask();
    }

    private void UpdateTask()
    {
        switch (_lizzyStoryProcess.curTaskID)
        {
            case 1001://出现
                if (_isStartDetection)
                {
                    float dist = Vector3.Distance(PlayerManager.Instance.playerCollider.gameObject.transform.position, new Vector3(-24.514f, 80.575f, 28.629f));
                    if (dist <= 1f)
                    {
                        _isStartDetection = false;
                        FinishCurTaskImmediately();
                    }
                }
                break;
            case 1002://说话：快帮帮我们！我们需要帮助！
                //if (_isStartDetection)
                //{
                //    float dist = Vector3.Distance(PlayerManager.Instance.playerCollider.gameObject.transform.position, new Vector3(-24.514f, 80.575f, 28.629f));
                //    if (dist <= 0.5f)
                //    {
                //        _isStartDetection = false;
                //        FinishCurTaskImmediately();
                //    }
                //}
                if (_isStartTimer)
                {
                    _timer += Time.deltaTime;
                    if (_timer >= _audioWaitTime + 0.2f)
                    {
                        _timer = 0f;
                        _isStartTimer = false;
                        GeorgeManager.Instance.FinishCurTask();
                        //FinishCurTask();    //test
                    }
                }
                break;
            case 1003://说话：你到底在想些什么？
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
            case 1004://说话：好我知道！
                break;
            case 1005://逃跑
                if (_isStartDetection)
                {
                    //float dist = Vector3.Distance(_curLizzyTask.target.position, new Vector3(-7.65f, 80.582f, 39.49f));
                    //if (dist <= 1f)
                    //{
                    //    _isStartDetection = false;
                    //    FinishCurTaskImmediately();
                    //}
                }
                break;
            case 1006://陷入困境
                if (_isStartDetection)
                {
                    float dist = Vector3.Distance(_curLizzyTask.target.position, new Vector3(-22.63f, 80.582f, 30.02f));
                    if (dist <= 1f)
                    {
                        _isStartDetection = false;
                        FinishCurTaskImmediately();
                    }
                }
                break;
            case 1007://看到玩家后，说话：啊呃呃，救我…救我出这里！
                break;
            case 1008://说话：谁..你是谁？…噢是你！对不起不是我的错。George...！
                break;
            case 1009://（哭泣道）…我不想死…至少不要在今天…

                break;
            case 1010://如果玩家走开不救，：不…不要…不要留我在这里等死！求求你！！！
                if (_isStartDetection)
                {
                    //判断如果玩家没救
                    //float dist = Vector3.Distance(_curLizzyTask.target.position, new Vector3(-23.43399f, 80.697f, 35.03268f));
                    //if (dist <= 0.5f)
                    //{
                    //    TaskStepManagaer.Instance.FinishTaskTo(20005);
                    //    _isStartDetection = false;
                    //    FinishCurTask();
                    //}
                    float dist1 = Vector3.Distance(_curLizzyTask.target.position, new Vector3(-13.587f, 80.582f, 28.399f));
                    if (dist1 <= 1f)
                    {
                        TaskStepManagaer.Instance.FinishTaskTo(20006);
                        _isStartDetection = false;
                        FinishCurTaskImmediately(1012);
                    }
                }
                break;

            case 1011://如果被救，:谢谢你救我！对于之前发生的事情，我很抱歉，因为我太害怕了！
                break;
            case 1012://如果被救，:谢谢你救我！对于之前发生的事情，我很抱歉，因为我太害怕了！
                break;
            case 1013://：不管怎么样…我知道有条路可以带你到顶楼。跟我来！
                break;
            case 1014://带领玩家走
                break;
            case 1015://搬橱柜，玩家帮忙
                if (_isStartDetection)
                {
                    //如果柜子被搬开
                    float dist = Vector3.Distance(_curLizzyTask.target.position, _lizzy.transform.position);
                    if (dist <= 0.5f)
                    {
                        _isStartDetection = false;
                        FinishCurTaskImmediately();
                    }
                }
                break;
            case 1016://“这个通道直达那边的楼梯，通过楼梯到上一层，那里有...”
                break;
            case 1017://被攻击，“阿呃呃”
                break;
            case 1018://技能动作，被攻击
                break;
            case 1019://躺在地上，奄奄一息
                break;
            case 1020://打败敌人后，“咳咳…我想我活不了了。救救Rick…”
                break;
            case 1021://咳嗽，出血
                break;
            case 1022://“谢谢你…还有...对不…起”
                break;
            case 1023://极乐
                break;
        }
    }
}
