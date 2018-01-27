//CiviliansStoryProcess.cs
//TEOT_ONLINE
//
//Create by WangJie on 9/15/2017 10:28 AM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CiviliansStoryProcess : MonoBehaviour 
{
    private int _curTaskID;
    public int curTaskID
    {
        get { return _curTaskID; }
    }

    private CiviliansStoryGroup _civiliansStoryGroup;
    public CiviliansStoryGroup civiliansStoryGroup
    {
        get { return _civiliansStoryGroup; }
    }


    public void Init()
    {
        _civiliansStoryGroup = gameObject.AddComponent<CiviliansStoryGroup>();
        _civiliansStoryGroup.Init();
        _curTaskID = _civiliansStoryGroup.storyList[0].taskId;
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
