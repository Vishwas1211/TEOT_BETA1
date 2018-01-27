using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using qwe;

public class TaskStepManagaer : MonoBehaviour {

    private bool _isStart = false;

    private List<TaskStep> _taskList = new List<TaskStep>();

    private int _curTaskIndex = 0;
    public int curTaskIndex
    {
        get { return _curTaskIndex; }
        set { _curTaskIndex = value; }
    }

    public int[] arrTaskIndexLevel = new int[28];

    private TaskStep _curTaskStep;

    public int curTaskId
    {
        get
        {
            if (_curTaskStep != null)
                return _curTaskStep.taskId;
            else
                return -1;
        }
    }

    public TaskProgressManager taskProgressManager;

    public bool _isFirstTask = false;

    public void StartTask()
    {
        _isStart = true;
    }

    public void Load()
    {
        LoadData();
        taskProgressManager = gameObject.AddComponent<TaskProgressManager>();
        taskProgressManager.Init();
    }

    private void LoadData()
    {
        List<DataTask> listTask = DataManager.instance.taskGroup.GetAllTask();
        int taskCount = listTask.Count;

        for (int i = 0; i < taskCount; ++i)
        {
            int taskId = listTask[i].id;

            string desc = listTask[i].desc;

            DataTask dataTask = listTask[i];
            TaskStep taskBase = TaskStep.Create(this.gameObject, taskId, dataTask);
            _taskList.Add(taskBase);

            switch (taskId)
            {
                case 1001:
                    arrTaskIndexLevel[0] = i;
                    break;
                case 2001:
                    arrTaskIndexLevel[1] = i;
                    break;
                case 3001:
                    arrTaskIndexLevel[2] = i;
                    break;
                case 4001:
                    arrTaskIndexLevel[3] = i;
                    break;
                case 5001:
                    arrTaskIndexLevel[4] = i;
                    break;
                case 6001:
                    arrTaskIndexLevel[5] = i;
                    break;
                case 7001:
                    break;
                case 8001:
                    break;
                case 9001:
                    break;
                case 10001:
                    break;
                case 11001:
                    break;
                case 12001:
                    break;
                case 13001:
                    break;
                case 14001:
                    break;
                case 15001:
                    break;
                case 16001:
                    break;
                case 17001:
                    break;
                case 18001:
                    break;
                case 19001:
                    break;
                case 20001:
                    break;
                case 21001:
                    break;
                case 22001:
                    break;
                case 23001:
                    break;
                case 24001:
                    break;
                case 25001:
                    break;
                case 26001:
                    break;
                case 27001:
                    break;
                case 28001:
                    break;
            }
        }
    }


    private void Update()
    {
        if (_isStart)
        {
            TaskLoop();
        }
    }

    private void TaskLoop()
    {
        if (_curTaskIndex < _taskList.Count)
        {
            if (!_isFirstTask)
            {
                _curTaskStep = _taskList[_curTaskIndex];
                _curTaskStep.InitTask();
                _isFirstTask = true;
            }

            if (_curTaskStep.IsFinish())
            {
                _curTaskStep = _taskList[_curTaskIndex];
                _curTaskStep.InitTask();
            }
            else
            {
                _curTaskStep.StartTask();
            }
        }
        else
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// 跳到第xx步
    /// </summary>
    public void FinishTaskTo(int taskId)
    {
        for (int i = 0; i < _taskList.Count; i++)
        {
            if(_taskList[i].taskId.Equals(taskId))
            {
                _curTaskStep.FinishTaskTo(i);
            }
        }
    }

    /// <summary>
    /// 默认0.3秒之后跳步
    /// </summary>
    public void FinishCurTask()
    {
        if (_curTaskStep != null)
        {
            _curTaskStep.FinishTask();
        }
    }

    public void FinishCurTaskImmediately()
    {
        if (_curTaskStep != null)
        {
            _curTaskStep.FinishTaskImmediately();
        }
    }

    public bool IsEqualTaskId(int taskId)
    {
        if (_curTaskStep != null)
        {
            return _curTaskStep.taskId == taskId;
        }

        return false;
    }

    public void DelayTimeFinishTask(float delayTime)
    {
        StartCoroutine(DelayTimeFinishTaskCallBack(delayTime));
    }

    IEnumerator DelayTimeFinishTaskCallBack(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        FinishCurTask();
    }

    public void SetTaskIndex(int index)
    {
        if (_curTaskStep != null)
        {
            _curTaskStep.FinishTaskImmediately();
        }

        _curTaskIndex = index;
    }

    public int GetCurTaskID()
    {
        TaskStep taskStep = _taskList[_curTaskIndex];
        return taskStep.dataTask.id;
    }

    public DataTask GetCurDataTask()
    {
        TaskStep taskStep = _taskList[_curTaskIndex];
        return taskStep.dataTask;
    }

    private static TaskStepManagaer _instance;
    public static TaskStepManagaer Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("TaskStepManager");
                DontDestroyOnLoad(go);
                _instance = go.AddComponent<TaskStepManagaer>();
            }
            return _instance;
        }
    }
}
