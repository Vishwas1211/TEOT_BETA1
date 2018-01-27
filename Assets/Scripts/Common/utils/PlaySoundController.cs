using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundController : SingletonMono<PlaySoundController>
{
    private AudioSource _audioSource;

    private string GetSoundPath(int id)
    {
        return DataManager.instance.audioGroup.GetAudio(id).path;
    }

    /// <summary>
    /// 播放NPC声音的方法
    /// </summary>
    /// <param name="go">需要播放声音的物体</param>
    /// <param name="id">播放声音的id</param>
    public void PlaySoundWithNPC(AudioSource audioSource, int startId, int endId)
    {
        //设置为3d音效
        audioSource.spatialBlend = 1.0f;
        StartCoroutine(PlayNpcSound(audioSource, startId, endId));
    }

    IEnumerator PlayNpcSound(AudioSource audioSource, int startId, int endId)
    {
        AudioClip clip = (AudioClip)Resources.Load(GetSoundPath(startId));
        audioSource.clip = clip;
        audioSource.Play();
        yield return new WaitForSeconds(clip.length);
        startId++;
        if (startId <= endId)
        {
            if (test7.isOnce)
            {
                startId++;
            }
            StartCoroutine(PlayNpcSound(audioSource, startId, endId));
        }
        else
        {
            StopCoroutine("PlayNpcSound");
        }
    }

    /// <summary>
    /// 播放音效的方法
    /// </summary>
    /// <param name="go">需要播放声音的物体</param>
    /// <param name="id">播放声音的id</param>
    /// <param name="distance">声音传播的距离</param>
    public void PlaySoundEffect(GameObject go, int id, float distance = 1)
    {
        if (go.GetComponent<AudioSource>() == null)
        {
            _audioSource = go.AddComponent<AudioSource>();
        }else
        {
            _audioSource = go.GetComponent<AudioSource>();
        }
        _audioSource.spatialBlend = 1.0f;
        AudioClip clip = (AudioClip)Resources.Load(GetSoundPath(id));
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    /// <summary>
    /// 播放循环声音方法
    /// </summary>
    /// <param name="go">需要播放声音的物体</param>
    /// <param name="id">播放声音的id</param>

    public void PlaySoundLoop(GameObject go, int id)
    {
        AudioClip clip = (AudioClip)Resources.Load(GetSoundPath(id));
        if (go.GetComponent<AudioSource>() == null)
        {
            _audioSource = go.AddComponent<AudioSource>();
        }
        _audioSource.spatialBlend = 1.0f;
        _audioSource.loop = true;
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    /// <summary>
    /// 停止播放声音方法
    /// </summary>
    public void StopSoundLoop()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }
}
