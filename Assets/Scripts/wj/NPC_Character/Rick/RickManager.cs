//RickManager.cs
//TEOT_ONLINE
//
//Create by WangJie on 9/13/2017 3:36 PM
//Description: 
//

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RickManager : SingletonMono<RickManager> 
{
    private const string PREFAB_PATH = "Prefabs/Character/NPCs/Rick/Rick";

    private long _taskStartTime = 0;
    private float _audioWaitTime = 0f;

    private bool _isStartDetection;
    private bool _isGetCaught;

    private NPC_StoryConfig _curRickStory;

    private GameObject _rick;
    public GameObject rick
    {
        get { return _rick; }
    }

    private RickController _rickController;
    public RickController rickController
    {
        get { return _rickController; }
    }

    private RickStoryProcess _rickStoryRrocess;
    public RickStoryProcess rickStoryProcess
    {
        get { return _rickStoryRrocess; }
    }

    private NPC_AudioController _rickAudioController;
    public NPC_AudioController rickAudioController
    {
        get { return _rickAudioController; }
    }

    public void Init()
    {
        _rick = UtilFunction.ResourceLoad(PREFAB_PATH);
        _rickController = _rick.GetComponent<RickController>();
        _rickController.Init();

        _rickStoryRrocess = gameObject.AddComponent<RickStoryProcess>();
        _rickStoryRrocess.Init();

        _rickAudioController = gameObject.AddComponent<NPC_AudioController>();
        _rickAudioController.Init(_rick.GetComponent<AudioSource>());

        RunTask();
    }

    public void FinishCurTask()
    {
        //播放完成声音
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
        if (_rickStoryRrocess.curTaskID == _rickStoryRrocess.rickStoryGroup.storyList[_rickStoryRrocess.rickStoryGroup.storyList.Count - 1].taskId)
            return;

        _rickStoryRrocess.FinishCurTask();
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

    private void EnterTask(NPC_StoryConfig rickTask)
    {
        Debug.Log("Rick--当前剧情" + rickTask.taskId + "---" + rickTask.description);
        _curRickStory = rickTask;
        _rickAudioController.PlaySound(rickTask.audioId);
        _rickController.SetTargetPoint(rickTask.targetPos);

        _taskStartTime = TimeHelper.GetCurrentRealTimestamp();
        if (rickTask.audioId > 0)
        {
            _audioWaitTime = _rickAudioController.GetCurAudioLength(rickTask.audioId);
        }
        else
        {
            _audioWaitTime = 0f;
        }

        switch (_rickStoryRrocess.curTaskID)
        {
            case 1001://出现
                FinishCurTaskDelay(0.5f);
                break;
            case 1002://在屋里面，进行剧情
                FinishCurTaskDelay(0.5f);
                break;
            case 1003://躲到一边
                _rickController.SetCanSkipStep(true);
                _rickController.SetState(RickController.STATE.WALK);
                break;
            case 1004://逃跑
                _rickController.SetState(RickController.STATE.IDLE);
                _rick.transform.position = rickTask.targetPos;
                FinishCurTask();
                break;
            case 1005://再次出现
                //再次看到玩家
                _isStartDetection = true;
                break;
            case 1006://打玩家
                //FinishCurTaskDelay(0.5f);
                _isStartDetection = true;
                _rickController.SetState(RickController.STATE.ATTACK);
                break;
            case 1007://"说话：不！你没法活捉我！滚开！"
                FinishCurTask();
                //_isStartDetection = true;
                break;
            case 1008://被玩家捉住之后，说话：不要…不要杀我！我能帮到你…
                _rickController.SetState(RickController.STATE.IDLE);
                FinishCurTask();
                break;
            case 1009://说话：你来这里是为了救…我？但是为什么呢…?
                FinishCurTask();
                break;
            case 1010://说话：好吧，那让我离开这该死的洞吧！首先，我们需要去武器室！
                FinishCurTask();
                break;
            case 1011://带领玩家去找武器
                _rickController.SetCanSkipStep(true);
                _rickController.SetTarget(_curRickStory.target.gameObject);
                _rickController.SetState(RickController.STATE.LEADER);
                break;
            case 1012://打开一个抽屉
                //播放一个打开抽屉的动画
                _isStartDetection = true;
                break;
            case 1013://向楼上走
                _rickController.SetCanSkipStep(true);
                _rickController.SetTarget(_curRickStory.target.gameObject);
                _rickController.SetState(RickController.STATE.LEADER);
                break;
            case 1014://向玩家介绍rab
                FinishCurTask();
                break;
            case 1015://开门
                FinishCurTaskDelay(2);
                break;
            case 1016://带玩家走,去武器库
                _isStartDetection = true;
                _rickController.SetCanSkipStep(false);
                _rickController.SetTarget(_curRickStory.target.gameObject);
                _rickController.SetState(RickController.STATE.LEADER);
                break;
            case 1017://跟随玩家走
                _isStartDetection = true;
                _rickController.SetState(RickController.STATE.FOLLOW);
                break;
            case 1018://说话：用光弹吧，闪瞎它。你有1分钟的时间，1分钟后，
                FinishCurTaskImmediately();
                break;
            case 1019://向后退，远远的看着
                      //_isStartDetection = true;
                      //_rickController.SetState(RickController.STATE.WALK); //向后退一段时间，然后转身，向后方走
                FinishCurTaskImmediately();
                break;
            case 1020://玩家拿到钥匙之后，跑向玩家
                _rickController.SetCanSkipStep(true);
                _rickController.SetState(RickController.STATE.WALK_TO);
                break;
            case 1021://跑到玩家跟前，跟随
                //_isStartDetection = true;
                _rickController.SetState(RickController.STATE.FOLLOW);
                break;
            //case 1022://
            //    break;
            //case 1023://
            //    FinishCurTaskDelay(0.5f);
            //    break;
            //case 1024:// 
            //          //_melissaController.SetState(MelissaController.STATE.WALK);
            //    FinishCurTaskDelay(0.5f);
            //    break;
            //case 1025://
            //    FinishCurTaskDelay(0.5f);
            //    break;
            //case 1026://
            //    FinishCurTaskDelay(0.5f);
            //    break;
            //case 1027://
            //    FinishCurTaskDelay(0.5f);
            //    break;
            //case 1028://
            //    //FinishCurTaskDelay(0.5f);
            //    break;
        }
    }

    private void Update()
    {
        UpdateTask();
    }

    private void UpdateTask()
    {
        switch (_rickStoryRrocess.curTaskID)
        {
            case 1001://出现
                break;
            case 1002://在屋里面，进行剧情
                break;
            case 1003://躲到一边
                break;
            case 1004://逃跑
                break;
            case 1005://再次出现
                //再次看到玩家
                if (_isStartDetection)
                {
                    float dist = Vector3.Distance(_curRickStory.target.position, new Vector3(-3.685572f, 88.53639f, 34.50282f)); //放一个位置

                    if (dist <= 1f)
                    {
                        _isStartDetection = false;
                        FinishCurTaskImmediately();
                    }
                }
                break;
            case 1006:// 打玩家
                break;
            case 1007://说话：不！你没法活捉我！滚开！
                      //准备被玩家抓住
                //if (_isStartDetection)
                //{
                //    if (_isGetCaught)
                //    {
                //        _isStartDetection = false;
                //        FinishCurTaskImmediately();
                //    }
                //}
                break;
            case 1008://被玩家捉住之后，说话：不要…不要杀我！我能帮到你…
                break;
            case 1009://说话：你来这里是为了救…我？但是为什么呢…?
                break;
            case 1010://说话：好吧，那让我离开这该死的洞吧！首先，我们需要去武器室！
                break;
            case 1011://带领玩家去武器室
                break;
            case 1012://打开一个抽屉
                if (_isStartDetection)
                {
                    if (Level_20_Manager.Instance.playerHaveWuQi)       //玩家拿起了武器
                    {
                        _isStartDetection = false;
                        FinishCurTaskImmediately();
                    }
                }
                break;
            case 1013://向楼上走
                break;
            case 1014://向玩家介绍rab
                break;
            case 1015://开门
                break;
            case 1016://带玩家走,去武器库
                if (_isStartDetection)
                {
                    float dist = Vector3.Distance(_curRickStory.target.position, new Vector3(-12.68f, 92.607f, 52.17f)); //当玩家和怪物离近了
                    if (dist <= 1.5f)
                    {
                        _isStartDetection = false;
                        FinishCurTaskImmediately();
                    }
                }
                break;
            case 1017://跟随玩家走
                if (_isStartDetection)
                {
                    if (true)/*拿到钥匙*/
                    {
                        _isStartDetection = false;
                        FinishCurTaskImmediately();
                    }
                }
                break;
            case 1018://说话：用光弹吧，闪瞎它。你有1分钟的时间，1分钟后，
                break;
            case 1019://向后退，远远的看着
                break;
            case 1020://玩家拿到钥匙之后，
                    _rickController.SetTargetPoint(_curRickStory.target.position);
                break;
            case 1021://跑到玩家跟前，跟随
                _rickController.SetTargetPoint(_curRickStory.target.position);
                //if (_isStartDetection)
                //{
                //    float dist = Vector3.Distance(_curRickStory.target.position, new Vector3(-12.222f, 46.25f, 37.255f));
                //    if (dist <= 0.5f)
                //    {
                //        _isStartDetection = false;
                //        FinishCurTaskImmediately();
                //    }
                //}
                break;
        }
    }

    private void RunTask()
    {
        if (_rickStoryRrocess.rickStoryGroup.storyDict.ContainsKey(_rickStoryRrocess.curTaskID))
            EnterTask(_rickStoryRrocess.rickStoryGroup.storyDict[_rickStoryRrocess.curTaskID]);
        else
            Debug.LogError("找不到当前Rick剧情---" + _rickStoryRrocess.curTaskID);
    }
}
