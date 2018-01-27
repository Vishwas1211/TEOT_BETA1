//JayceeStoryProcess.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/19/2017 5:18 PM
//Description: Jaycee剧情流程
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JayceeStoryProcess : MonoBehaviour 
{
    private int _curTaskId;
    public int curTaskID
    {
        get { return _curTaskId; }
    }

    private JayceeTaskGroup _jayceeTaskGroup;
    public JayceeTaskGroup jayceeTaskGroup
    {
        get { return _jayceeTaskGroup; }
    }


    public void Init ()
	{
        _jayceeTaskGroup = gameObject.AddComponent<JayceeTaskGroup>();
        _jayceeTaskGroup.Load();
        _curTaskId = _jayceeTaskGroup.taskList[0].taskId;
	}

    public void FinishCurTask()
    {
        //之后加说完话再跳步
        SkipStep();
    }

    public void FinishCurTaskImmediately()
    {
        SkipStep();
    }

    public void SkipStep()
    {
        _curTaskId++;
    }

    public void SkipStepTo(int id)
    {
        if (id <= _curTaskId)
            return;
        _curTaskId = id;
    }
}
