//MelissaTaskProcess.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/23/2017 4:44 PM
//Description: Melissa任务流程管理
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelissaTaskProcess : MonoBehaviour 
{
    private MelissaTaskGroup _melissaTaskGroup;
    public MelissaTaskGroup melissaTaskGroup
    {
        get { return _melissaTaskGroup; }
    }

    private int _curTaskID = 0;
    public int curTaskID
    {
        get { return _curTaskID; }
    }

    public void Init()
    {
        _melissaTaskGroup = gameObject.AddComponent<MelissaTaskGroup>();
        _melissaTaskGroup.Load();
        _curTaskID = _melissaTaskGroup.taskList[0].taskId;
    }

    public void FinishCurTask()
    {
        SkipStep();
    }

    private void SkipStep()
    {
        _curTaskID++;
    }
}
