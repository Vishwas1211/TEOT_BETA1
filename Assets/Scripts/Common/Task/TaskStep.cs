using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskStep : MonoBehaviour {

    public int taskId;

    private DataTask _dataTask;
    public DataTask dataTask
    {
        get { return _dataTask; }
    }

    private bool _isTaskFinish;
    private bool _isEnterRunOnce;
    private bool _isFinishRunOnce;

    private bool _isUpdateTask;

    private long _taskStartTime;
    private float _npcSoundTime;
    private float _npcMoveOrRotateTime;

    public bool IsFinish()
    {
        return _isTaskFinish;
    }

    public void InitTask()
    {
        _isTaskFinish = false;
        _isEnterRunOnce = false;
        _isFinishRunOnce = false;
        _isUpdateTask = false;
    }

    public void StartTask()
    {
        if (!_isEnterRunOnce)
        {
            _isEnterRunOnce = true;
            EnterTask();
        }
    }

    private float GetCurAudioLength(int audioID)
    {
        float length = DataManager.instance.audioGroup.GetAudioClip(audioID).length;
        return length;
    }

    public void EnterTask()
    {
        _isUpdateTask = true;

        _taskStartTime = TimeHelper.GetCurrentRealTimestamp();

        MainNpcAudioController.Instance.PlaySound(_dataTask.audioId); //播放npc提示音

        if (_dataTask.audioId > 0)
        {
            _npcSoundTime = GetCurAudioLength(_dataTask.audioId);
        }
        else
        {
            _npcSoundTime = 0f;
        }
        TaskStepManagaer.Instance.taskProgressManager.EnterTask(_dataTask.id);
    }

    public void FinishTaskTo(int taskId)
    {
        _isUpdateTask = false;

        TaskStepManagaer.Instance.taskProgressManager.ExitTask(_dataTask.id);
        _isTaskFinish = true;
        TaskStepManagaer.Instance.curTaskIndex = taskId;
    }

    public void FinishTaskImmediately()
    {
        _isUpdateTask = false;

        TaskStepManagaer.Instance.taskProgressManager.ExitTask(_dataTask.id);
        _isTaskFinish = true;
        TaskStepManagaer.Instance.curTaskIndex++;
    }

    public void FinishTask()
    {
        if (!_isFinishRunOnce)
        {
            _isFinishRunOnce = true;
            StartCoroutine(FinishTaskDelay());
        }
    }

    IEnumerator FinishTaskDelay()
    {
        _isUpdateTask = false;
        TaskStepManagaer.Instance.taskProgressManager.ExitTask(_dataTask.id);

        float taskElapseTime = (float)(TimeHelper.GetCurrentRealTimestamp() - _taskStartTime) / 1000;
        float taskWaitTime = Math.Max(_npcSoundTime - taskElapseTime, 0);
        Debug.Log("主流程声音长度：" + _npcSoundTime + ",等待时间：" + taskWaitTime + ",过去时间：" + taskElapseTime);
        yield return new WaitForSeconds(taskWaitTime);

        _isTaskFinish = true;
        TaskStepManagaer.Instance.curTaskIndex++;
    }

    private void UpdateTask()
    {
        if (_isUpdateTask)
        {
            TaskStepManagaer.Instance.taskProgressManager.UpdateTask(_dataTask.id);
        }
    }

    private void Update()
    {
        UpdateTask();
    }

    public void LoadData(DataTask data)
    {
        taskId = data.id;
        _dataTask = data;
    }

    public static TaskStep Create(GameObject parent, int name, DataTask dataTask)
    {
        GameObject go = new GameObject("taskbase" + name);
        go.transform.parent = parent.transform;
        TaskStep taskBase = go.AddComponent<TaskStep>();
        taskBase.LoadData(dataTask);

        return taskBase;
    }
}
