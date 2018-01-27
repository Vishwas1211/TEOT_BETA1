//AmyAudioController.cs
//TEOT_ONLINE
//
//Create by WangJie on 9/6/2017 3:02 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_AudioController : MonoBehaviour 
{
    private AudioSource _audioSource;
	
	public void Init (AudioSource audioSource)
	{
        _audioSource = audioSource;
	}

    public void PlaySound(int id)
    {
        if (id < 0)
            return;
        if (DataManager.instance.audioGroup.dataAudio.ContainsKey(id))
        {
            _audioSource.clip = DataManager.instance.audioGroup.GetAudioClip(id);
            _audioSource.Play();
        }
    }

    public void PlaySoundFinish(int id)
    {
        if (!DataManager.instance.audioGroup.dataAudio.ContainsKey(id))
            return;

        if (_audioSource.isPlaying)
            return;
        _audioSource.clip = DataManager.instance.audioGroup.GetAudioClip(id);
        _audioSource.Play();
    }

    public float GetCurAudioLength(int audioID)
    {
        float length = DataManager.instance.audioGroup.GetAudioClip(audioID).length;
        return length;
    }
}
