//
//  TaskProgressBase.cs
//  TEOT_ONLINE
//
//  Created by EDSENSES_P2 on 8/2/2017 5:55 PM.
//
//

using UnityEngine;
using System.Collections;
using qwe;

public class TaskProgressBase : MonoBehaviour {
    protected DataTask _dataTask;

    protected TaskProgressManager _taskProgressManager;
    public TaskProgressManager taskProgressManager
    {
        set { _taskProgressManager = value; }
    }

    virtual public void EnterTask(int taskId)
    {
        _dataTask = DataManager.instance.taskGroup.GetTask(taskId);
        Debug.Log("<color=red>" + " 当前任务 " + "</color>" + taskId + ":" + _dataTask.stageDesc + "--" + _dataTask.desc);
        //TestReadJson.Instance.text.text = "当前任务 " + taskId + ":" + _dataTask.stageDesc + "--" + _dataTask.desc;
    }

    virtual public void UpdateTask(int taskId)
    {

    }

    virtual public void ExitTask(int taskId)
    {
    }

}