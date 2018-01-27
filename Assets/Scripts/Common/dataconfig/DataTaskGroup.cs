using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTaskGroup : MonoBehaviour {

    public Dictionary<int, DataTask> _dataTask;
    public Dictionary<int, DataTask> dataTask
    {
        get { return _dataTask; }
    }

    private bool _isLoad = false;

    public DataTask GetTask(int id)
    {
        if (_dataTask.ContainsKey(id))
        {
            return _dataTask[id];
        }

        return null;
    }

    public List<DataTask> GetAllTask()
    {
        List<DataTask> allTask = new List<DataTask>();

        foreach (DataTask task in _dataTask.Values)
        {
            allTask.Add(task);
        }

        return allTask;
    }

    public void Load(string path)
    {
        if (_isLoad)
        {
            return;
        }

        _isLoad = true;

        _dataTask = new Dictionary<int, DataTask>();
        TaskDataConfig taskData = LoadJson.LoadJsonTaskFromFile(path);

        foreach (TaskDataBase taskDataBase in taskData.taskDataBaseGroup)
        {
            DataTask data = new DataTask();
            data.Load(taskDataBase);

            _dataTask.Add(data.id, data);
        }
    }
}
