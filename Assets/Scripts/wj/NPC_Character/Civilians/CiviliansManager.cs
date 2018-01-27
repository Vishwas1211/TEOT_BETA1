//CiviliansManager.cs
//TEOT_ONLINE
//
//Create by WangJie on 9/15/2017 10:26 AM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CiviliansManager : SingletonMono<CiviliansManager> 
{
    private const string CIVILIANS_MAN_PATH = "Prefabs/Character/NPCs/Civilians/Civilians_Man";
    private const string CIVILIANS_WOMAN_PATH = "Prefabs/Character/NPCs/Civilians/Civilians_Woman";

    private bool _isStartDetection;
    private bool _isFamilyRescued;
    private bool _isDeliverance;


    private NPC_StoryConfig _curCiviliansTask;

    private GameObject _civiliansMan;
    public GameObject civiliansMan
    {
        get { return _civiliansMan; }
    }

    private GameObject _civiliansWoman;
    public GameObject civiliansWoman
    {
        get { return _civiliansWoman; }
    }

    private CiviliansController _civiliansController;
    public CiviliansController civiliansController
    {
        get { return _civiliansController; }
    }

    private CiviliansWomanController _civiliansWomanController;
    public CiviliansWomanController civiliansWomanControlle
    {
        get { return _civiliansWomanController; }
    }
    

    private CiviliansStoryProcess _civiliansStoryProcess;
    public CiviliansStoryProcess civiliansStoryProcess
    {
        get { return _civiliansStoryProcess; }
    }

    private NPC_AudioController _civiliansAudioController;
    public NPC_AudioController civiliansAudioController
    {
        get { return _civiliansAudioController; }
    }

    public void Init()
    {
        _civiliansMan = UtilFunction.ResourceLoad(CIVILIANS_MAN_PATH);
        _civiliansController = _civiliansMan.GetComponent<CiviliansController>();
        _civiliansController.Init();

        //_civiliansWoman = UtilFunction.ResourceLoad(CIVILIANS_WOMAN_PATH);
        //_civiliansWomanController = _civiliansWoman.GetComponent<CiviliansWomanController>();
        //_civiliansWomanController.Init();

        _civiliansStoryProcess = gameObject.AddComponent<CiviliansStoryProcess>();
        _civiliansStoryProcess.Init();
        _civiliansAudioController = gameObject.AddComponent<NPC_AudioController>();
        _civiliansAudioController.Init(_civiliansMan.GetComponent<AudioSource>());

        RunStory();
    }

    public void FinishCurTask()
    {
        FinishCurTaskImmediately();
    }

    public void FinishCurTaskImmediately()
    {
        _civiliansStoryProcess.FinishCurTask();
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

    private void EnterTask(NPC_StoryConfig task)
    {
        _curCiviliansTask = task;
        Debug.Log("Civilians--当前剧情" + task.taskId + task.description);
        _civiliansController.SetTargetPoint(task.targetPos);
        _civiliansAudioController.PlaySound(task.audioId);
        switch (_civiliansStoryProcess.curTaskID)
        {
            case 1001:  //出现
                _civiliansController.SetState(CiviliansController.STATE.CROUCH);
                FinishCurTaskDelay(0.5f);
                break;
            case 1002:  //被僵尸追杀
                _civiliansController.SetState(CiviliansController.STATE.CROUCH);
                FinishCurTaskDelay(0.5f);
                break;
            case 1003:  //说话(男)：去死，你们这群怪物！(等待玩家救助)
                _civiliansController.SetState(CiviliansController.STATE.CROUCH);
                //_isStartDetection = true;
                break;
            case 1004:  //获救,向玩家身边走

                TaskStepManagaer.Instance.FinishTaskTo(26008);  //主流程跳步

                _isStartDetection = true;
                _civiliansController.SetState(CiviliansController.STATE.WALK_TO);
                break;
            case 1005:  //感谢玩家，需要说话（没有语音）
                _civiliansController.SetState(CiviliansController.STATE.TALK);
                FinishCurTaskDelay(3f);
                break;
            case 1006:  //走到一边(随便藏在某处)
                EmilyManager.Instance.FinishCurTask();// 让Emily的流程跳到1015

                //_civiliansController.SetState(CiviliansController.STATE.WALK);
                //transform.position = new Vector3(2.289f, 89.904f, 27.439f);
                break;
            case 1007:  //走到楼梯上
                _civiliansController.SetState(CiviliansController.STATE.WALK);
                //_isStartDetection = true; //判断条件是否达成
                break;
            case 1008:  //说话：快，来这，到这来！
                _isStartDetection = true;
                _civiliansController.SetState(CiviliansController.STATE.TALK);
                //FinishCurTaskDelay(0.5f);
                break;
            case 1009:  //玩家到旁边之后，说话：不要出声，等他们都走了！
                _isStartDetection = true;
                //FinishCurTaskDelay(0.5f);
                break;
            case 1010:  //如果玩家救了他的家庭，跳到下一步，如果没有，跳两步
                FinishCurTaskDelay(0.5f);
                break;
            case 1011:  //救了:Ok!他们都走了！我想现在我们都相互救了各自一命，这是电梯的钥匙，希望能帮上你。
                _isStartDetection = true;
                //FinishCurTaskDelay(0.5f);
                break;
            case 1012:  //没救：OK！他们都走了。我想现在我们都平了。
                _isStartDetection = true;
                break;
            case 1013:  //等待玩家离开后，重新躲起来
                break;
        }
    }

    private void RunStory()
    {
        if (_civiliansStoryProcess.civiliansStoryGroup.storyDict.ContainsKey(_civiliansStoryProcess.curTaskID))
            EnterTask(_civiliansStoryProcess.civiliansStoryGroup.storyDict[_civiliansStoryProcess.curTaskID]);
        else
            Debug.Log("找不到当前剧情ID---" + _civiliansStoryProcess.curTaskID);
    }

    private void Update()
    {
        UpdateTask();

        //test
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    _isDeliverance = true;
        //}
    }

    private void UpdateTask()
    {
        switch (_civiliansStoryProcess.curTaskID)
        {
            case 1001:  //出现
                break;
            case 1002:  //被僵尸追杀
                break;
            case 1003:  //说话(男)：去死，你们这群怪物！(等待玩家救助)
                //if (_isStartDetection)
                //{
                //    //if (_isDeliverance) //获得救助
                //    {
                //        _isStartDetection = false;
                //        FinishCurTask();
                //    }
                //}
                break;
            case 1004:  //获救,向玩家身边走
                _civiliansController.SetTargetPoint(_curCiviliansTask.target.position);
                if (_isStartDetection)
                {
                    float dist = Vector3.Distance(_civiliansMan.transform.position, _curCiviliansTask.target.position);
                    if (dist <= 1.5f)
                    {
                        _isStartDetection = false;
                        FinishCurTask();
                    }
                }
                break;
            case 1005:  //感谢玩家
                break;
            case 1006:  //走到一边(随便藏在某处)
                break;
            case 1007:  //走到楼梯上
                if (_isStartDetection)
                {
                    //TODO 判断玩家是否触发条件
                    if (true)
                    {
                        _isStartDetection = false;
                        FinishCurTask();
                    }
                }
                break;
            case 1008:  //说话：快，来这，到这来！
                if (_isStartDetection)
                {
                    //TODO 判断玩家来到身边
                    float dist = Vector3.Distance(_civiliansMan.transform.position, _curCiviliansTask.target.position);
                    if (dist <= 2)
                    {
                        _isStartDetection = false;
                        FinishCurTask();
                    }
                }
                break;
            case 1009:  //玩家到旁边之后，说话：不要出声，等他们都走了！
                if (_isStartDetection)
                {
                    //TODO 检测敌人是不是都走了
                }
                break;
            case 1010:  //如果玩家救了他的家庭，跳到下一步，如果没有，跳两步
                break;
            case 1011:  //救了:Ok!他们都走了！我想现在我们都相互救了各自一命，这是电梯的钥匙，希望能帮上你。
                //TODO 送出电梯的钥匙 检测玩家是否离开
                //给钥匙
                if (_isStartDetection)
                {
                    if (_isFamilyRescued)
                    {
                        float dist = Vector3.Distance(_civiliansMan.transform.position, _curCiviliansTask.target.position);
                        if (dist >= 5)
                        {
                            _isStartDetection = false;
                            FinishCurTask();
                        }
                    }
                    else
                    {
                        _isStartDetection = false;
                        FinishCurTaskImmediately();
                    }
                }
                break;
            case 1012:  //没救：OK！他们都走了。我想现在我们都平了。
                //TODO 检测玩家是否离开
                if (_isStartDetection)
                {
                    float dist = Vector3.Distance(_civiliansMan.transform.position, _curCiviliansTask.target.position);
                    if (dist >= 5)
                    {
                        _isStartDetection = false;
                        FinishCurTask();
                    }
                }
                break;
            case 1013:  //等待玩家离开后，重新躲起来
                break;
        }
    }
}
