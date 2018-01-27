//RickStoryProcess.cs
//TEOT_ONLINE
//
//Create by WangJie on 9/13/2017 10:13 AM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RickStoryProcess : MonoBehaviour 
{
    private int _curTaskID;
    public int curTaskID
    {
        get { return _curTaskID; }
    }

    private RickStoryGroup _rickStoryGroup;
    public RickStoryGroup rickStoryGroup
    {
        get { return _rickStoryGroup; }
    }

    public void Init()
    {
        _rickStoryGroup = gameObject.AddComponent<RickStoryGroup>();
        _rickStoryGroup.Init();

        _curTaskID = _rickStoryGroup.storyList[0].taskId;
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
