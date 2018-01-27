//
//  DataAudioGroup.cs
//  TEOT_ONLINE
//
//  Created by EDSENSES_P2 on 8/2/2017 4:20 PM.
//
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataAudioGroup : MonoBehaviour {

    private Dictionary<int, DataAudio> _dataAudio;
    public Dictionary<int, DataAudio> dataAudio
    {
        get { return _dataAudio; }
    }

    private bool _isLoad = false;

    public DataAudio GetAudio(int id)
    {
        if (_dataAudio.ContainsKey(id))
        {
            return _dataAudio[id];
        }
        return null;
    }

    public AudioClip GetAudioClip(int id)
    {
        if(_dataAudio.ContainsKey(id))
            return Resources.Load(_dataAudio[id].path) as AudioClip;
        Debug.LogError(id + "声音片段不在数据表中，请查验:" + _dataAudio[id].desc);
        return null;
    }

    public List<DataAudio> GetAllAudio()
    {
        List<DataAudio> allAudio = new List<DataAudio>();
        foreach (DataAudio audio in _dataAudio.Values)
        {
            allAudio.Add(audio);
        }
        return allAudio;
    }

    public void Load(string path)
    {
        if (_isLoad)
        {
            return;
        }

        _isLoad = true;

        _dataAudio = new Dictionary<int, DataAudio>();

        AudioDataConfig audioData = LoadJson.LoadJsonAudioFromFile(path);

        foreach (AudioDataBase audioDataBase in audioData.audioDataBaseGroup)
        {
            DataAudio data = new DataAudio();
            data.Load(audioDataBase);

            _dataAudio.Add(data.id, data);
        }
    }
}