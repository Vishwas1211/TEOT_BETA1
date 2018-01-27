//
//  DataAudio.cs
//  TEOT_ONLINE
//
//  Created by EDSENSES_P2 on 8/2/2017 4:22 PM.
//
//

using UnityEngine;
using System.Collections;

public class DataAudio
{

    public int id;
    public string desc;
    public string path;
    public float volume;

    public void Load(AudioDataBase audioDataBase)
    {
        id = audioDataBase.ID;
        desc = audioDataBase.DESCRIPTION;
        path = audioDataBase.RES_PATH;
        volume = audioDataBase.VOLUME;
    }
}