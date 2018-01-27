using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataToolsGroup : MonoBehaviour {
    public Dictionary<int, DataTools> _dataTools;
    public Dictionary<int, DataTools> dataTools
    {
        get { return _dataTools; }
    }

    private bool _isLoad = false;

    public DataTools GetTools(int id)
    {
        if (_dataTools.ContainsKey(id))
        {
            return _dataTools[id];
        }

        return null;
    }

    public GameObject GetToolsWithId(int id) {
        if (_dataTools.ContainsKey(id))
            return Resources.Load(_dataTools[id].path) as GameObject;
        Debug.LogError(id + "物品不在数据表中，请查验:" + _dataTools[id].desc);
        return null;
    }

    public List<DataTools> GetAllTools()
    {
        List<DataTools> allTools = new List<DataTools>();
        foreach (DataTools tool in _dataTools.Values)
        {
            allTools.Add(tool);
        }
        return allTools;
    }

    public void Load(string path)
    {
        if (_isLoad)
        {
            return;
        }

        _isLoad = true;

        _dataTools = new Dictionary<int, DataTools>();
        ToolsDataConfig taskData = LoadJson.LoadJsonToolsFromFile(path);

        foreach (ToolsDataBase taskDataBase in taskData.toolsDataBaseGroup)
        {
            DataTools data = new DataTools();
            data.Load(taskDataBase);

            _dataTools.Add(data.id, data);
        }
    }
}
