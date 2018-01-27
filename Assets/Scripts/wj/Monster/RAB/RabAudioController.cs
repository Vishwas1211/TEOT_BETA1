//RabAudioController.cs
//TEOT_ONLINE
//
//Create by WangJie on 9/18/2017 6:20 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RabAudioController : MonoBehaviour 
{
    private AudioSource[] _audioSource;
	
	void Awake ()
	{
        _audioSource = GetComponents<AudioSource>();
        //_audioSource[0].outputAudioMixerGroup = null;
	}
	
    public void PlayAudio(int id)
    {
        _audioSource[0].clip = DataManager.instance.audioGroup.GetAudioClip(id);
        _audioSource[0].Play();
    }

    public void PlayAudioLoop(int id)
    {
        _audioSource[1].clip = DataManager.instance.audioGroup.GetAudioClip(id);
        _audioSource[1].loop = true;
        _audioSource[1].Play();
    }

    public void StopAudioLoop()
    {
        _audioSource[1].Stop();
    }

	void Update () 
	{
		
	}
}
