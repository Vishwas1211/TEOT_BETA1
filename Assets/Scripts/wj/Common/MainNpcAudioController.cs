//MainNpcAudioController.cs
//TEOT_ONLINE
//
//Create by WangJie on 12/18/2017 12:33 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainNpcAudioController : SingletonMono<MainNpcAudioController> 
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound(int id)
    {
        if (id <= 0)
            return;
        if (DataManager.instance.audioGroup.dataAudio.ContainsKey(id))
        {
            _audioSource.clip = DataManager.instance.audioGroup.GetAudioClip(id);
            _audioSource.Play();
        }
    }
}
