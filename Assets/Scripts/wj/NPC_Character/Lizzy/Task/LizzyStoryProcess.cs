//LizzyStoryProcess.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/26/2017 9:57 AM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizzyStoryProcess : MonoBehaviour 
{
    private int _curTaskID;
    public int curTaskID
    {
        get { return _curTaskID; }
    }

    private LizzyStoryGroup _lizzyStoryGroup;
    public LizzyStoryGroup lizzyStoryGroup
    {
        get { return _lizzyStoryGroup; }
    }
    

    public void Init ()
	{
        _lizzyStoryGroup = gameObject.AddComponent<LizzyStoryGroup>();
        _lizzyStoryGroup.Init();

        _curTaskID = _lizzyStoryGroup.storyList[0].taskId;
	}

    public void FinishCurTask()
    {
        SkipStep();
    }

    private void SkipStep()
    {
        _curTaskID++;
    }

    public void FinishCurTask(int i) {
        _curTaskID = i;
    }
}
