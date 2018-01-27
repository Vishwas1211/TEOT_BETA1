//GeorgeStoryProcess.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/26/2017 12:33 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeorgeStoryProcess : MonoBehaviour 
{
    private int _curTaskID;
    public int curTaskID
    {
        get { return _curTaskID; }
    }

    private GeorgeStoryGroup _georgeStoryGroup;
    public GeorgeStoryGroup georgeStoryGroup
    {
        get { return _georgeStoryGroup; }
    }

    public void Init ()
	{
        _georgeStoryGroup = gameObject.AddComponent<GeorgeStoryGroup>();
        _georgeStoryGroup.Init();

        _curTaskID = _georgeStoryGroup.storyList[0].taskId;
	}
	
	public void FinishCurTask () 
	{
        SkipStep();
	}

    private void SkipStep()
    {
        _curTaskID++;
    }
}
