//AmyStoryProcess.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/25/2017 9:54 AM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmyStoryProcess : MonoBehaviour 
{
    private int _curStoryID;
    public int curStoryID
    {
        get { return _curStoryID; }
    }

    private AmyStoryGroup _amyStoryGroup;
    public AmyStoryGroup amyStoryGroup
    {
        get { return _amyStoryGroup; }
    }
	
	public void Init ()
	{
        _amyStoryGroup = gameObject.AddComponent<AmyStoryGroup>();
        _amyStoryGroup.Init();

        _curStoryID = _amyStoryGroup.storyList[0].taskId;
	}

    public void FinishCurTask()
    {
        SkipStep();
    }

    private void SkipStep()
    {
        _curStoryID++;
    }
}
