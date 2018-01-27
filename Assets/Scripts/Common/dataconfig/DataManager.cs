using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataManager {

	private static DataManager _instance = null;
	public static DataManager instance
	{
		get
		{
			if (_instance == null) {
				_instance = new DataManager();
			}
			return _instance;
		}
	}

	private bool _isLoaded = false;

	public void LoadData()
	{
		if (!_isLoaded) 
		{
            //_dataAudioGroup.Load(AppConfig.FOLDER_DATACONFIG + "audio_config.txt");
            _taskGroup.Load("/Resources/DataConfig/task_config.txt");
            _audioGroup.Load("/Resources/DataConfig/audio_config.txt");
            _toolsGroup.Load("/Resources/DataConfig/tools_config.txt");

            _isLoaded = true;
		}
	}

    private DataTaskGroup _taskGroup = new DataTaskGroup();
    public DataTaskGroup taskGroup
    {
        get { return _taskGroup; }
    }

    private DataAudioGroup _audioGroup = new DataAudioGroup();
    public DataAudioGroup audioGroup
    {
        get { return _audioGroup; }
    }

    private DataToolsGroup _toolsGroup = new DataToolsGroup();
    public DataToolsGroup toolsGroup
    {
        get { return _toolsGroup; }
    }




    public Dictionary<int, Item> ItemDic = new Dictionary<int, Item>();





}
