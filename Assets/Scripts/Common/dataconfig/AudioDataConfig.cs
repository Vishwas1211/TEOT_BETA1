//
//  AudioDataConfig.cs
//  TEOT_ONLINE
//
//  Created by EDSENSES_P2 on 8/2/2017 4:15 PM.
//
//

using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class AudioDataConfig
{
    public AudioDataBase[] audioDataBaseGroup;
}

[Serializable]
public class AudioDataBase
{
    public string DESCRIPTION;
    public int ID;
    public string RES_PATH;
    public float VOLUME;
}