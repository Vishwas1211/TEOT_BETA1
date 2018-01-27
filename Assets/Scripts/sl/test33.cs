//
//  test33.cs
//  TEOT_ONLINE
//
//  Created by 孙磊 on 8/14/2017 2:27 PM.
//
//

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class test33 : MonoBehaviour
{
    AudioSource audioSource;
    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Step0();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Step1();
        }
    }


    public void Step0() {
        PlaySoundController.Instance.PlaySoundWithNPC(audioSource, 10001,10002);
        //全系地图

        //
    }

    public void Step1()
    {
        PlaySoundController.Instance.PlaySoundWithNPC(audioSource, 10003, 10004);
    }
    public void Step2()
    {
        PlaySoundController.Instance.PlaySoundWithNPC(audioSource, 10005, 10007);
    }
}