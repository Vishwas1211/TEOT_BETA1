//EmilyStoryProcess.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/25/2017 6:15 PM
//Description: Emily流程管理
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmilyStoryProcess : MonoBehaviour 
{
    private int _curTaskID;
    public int curTaskID
    {
        get { return _curTaskID; }
    }

    private EmilyStoryGroup _emilyStoryGroup;
    public EmilyStoryGroup emilyStoryGroup
    {
        get { return _emilyStoryGroup; }
    }


    public void Init ()
	{
        _emilyStoryGroup = gameObject.AddComponent<EmilyStoryGroup>();
        _emilyStoryGroup.Init();
        _curTaskID = _emilyStoryGroup.storyList[0].taskId;
	}
	
	public void FinishCurTask()
    {
        SkipStep();
    }

    public void FinishTo(int id)
    {
        _curTaskID = id;
    }

    private void SkipStep()
    {
        _curTaskID++;
    }
}
