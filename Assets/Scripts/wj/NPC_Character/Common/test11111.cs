using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test11111 : MonoBehaviour
{
    NPC_AudioController npc;
    public AudioSource audioSource;
    private float _audioWaitTime;
    private float _taskStartTime;

    private void Start()
    {
        npc = GetComponent<NPC_AudioController>();
        npc.Init(audioSource);
    }
    // Update is called once per frame
    void Update()
    {
        switch (TaskStepManagaer.Instance.GetCurTaskID())
        {

            case 1001:
                {
                    _taskStartTime = TimeHelper.GetCurrentRealTimestamp();
                    _audioWaitTime = npc.GetCurAudioLength(1001);

                    npc.PlaySound(1001);
                }
                break;
            case 1002:
                {
                    npc.PlaySound(1002);
                }
                break;
            case 1003:
                {
                    npc.PlaySound(1003);
                }
                break;
            //case 1001:
            //    {
            //        npc.PlaySound(TaskStepManagaer.Instance.GetCurTaskID());
            //    }
            //    break;
            //case 1001:
            //    {
            //        npc.PlaySound(TaskStepManagaer.Instance.GetCurTaskID());
            //    }
            //    break;
            //case 1001:
            //    {
            //        npc.PlaySound(TaskStepManagaer.Instance.GetCurTaskID());
            //    }
            //    break;
            //case 1001:
            //    {
            //        npc.PlaySound(TaskStepManagaer.Instance.GetCurTaskID());
            //    }
            //    break;
            //case 1001:
            //    {
            //        npc.PlaySound(TaskStepManagaer.Instance.GetCurTaskID());
            //    }
            //    break;
            //case 1001:
            //    {
            //        npc.PlaySound(TaskStepManagaer.Instance.GetCurTaskID());
            //    }
            //    break;
            //case 1001:
            //    {
            //        npc.PlaySound(TaskStepManagaer.Instance.GetCurTaskID());
            //    }
            //    break;
            //case 1001:
            //    {
            //        npc.PlaySound(TaskStepManagaer.Instance.GetCurTaskID());
            //    }
            //    break;
            //case 1001:
            //    {
            //        npc.PlaySound(TaskStepManagaer.Instance.GetCurTaskID());
            //    }
            //    break;
            //case 1001:
            //    {
            //        npc.PlaySound(TaskStepManagaer.Instance.GetCurTaskID());
            //    }
            //    break;
            //case 1001:
            //    {
            //        npc.PlaySound(TaskStepManagaer.Instance.GetCurTaskID());
            //    }
            //    break;
            //case 1001:
            //    {
            //        npc.PlaySound(TaskStepManagaer.Instance.GetCurTaskID());
            //    }
            //    break;
            //case 1001:
            //    {
            //        npc.PlaySound(TaskStepManagaer.Instance.GetCurTaskID());
            //    }
            //    break;
            default:
                break;
        }
    }

    public void FinishCurTask()
    {
        //完成一些事之后，在执行（播放声音）
        StartCoroutine(FinishCurTaskWait());
    }
    IEnumerator FinishCurTaskWait()
    {
        float taskElapseTime = (float)(TimeHelper.GetCurrentRealTimestamp() - _taskStartTime) / 1000;
        float taskWaitTime = Math.Max(_audioWaitTime - taskElapseTime, 0);
        Debug.Log("声音长度：" + _audioWaitTime + ",等待时间：" + taskWaitTime + ",过去时间：" + taskElapseTime);
        yield return new WaitForSeconds(taskWaitTime);
        TaskStepManagaer.Instance.FinishCurTaskImmediately();
    }

}
