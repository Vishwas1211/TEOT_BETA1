//TaskProgressLevel27.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/15/2017 3:14 PM
//Description: 第六阶段
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskProgressLevel27 : TaskProgressBase
{
    override public void EnterTask(int taskId)
    {
        base.EnterTask(taskId);

        switch (taskId)
        {
            case 27001: //叙述计划
                {
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 27002://启动计划
                {

                   //兔子里跳步
                }
                break;
            case 27003://开启电梯
                {
                    TaskStepManagaer.Instance.FinishCurTask();

                }
                break;





            case 27004://剧情分支三
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                    //打死Boss跳步

                    //Level20_Manager.Instance.Level20_Door_13_Script.CanOpen = true;
                    //Level20_Manager.Instance.Level20_Door_11_Script.CanOpen = true;
                    //Level20_Manager.Instance.Level20_Door_9_Script.CanOpen = true;
                    //Level20_Manager.Instance.Level20_Door_8_Script.CanOpen = true;
                    //Level20_Manager.Instance.Level20_Door_19_Script.CanOpen = true;
                    //Level20_Manager.Instance.Level20_Door_7_Script.CanOpen = true;
                    //Level20_Manager.Instance.Level20_Door_15_Script.CanOpen = true;

                    //Level20_Manager.Instance.Level20_Door_2_Script.CanOpen = false;
                    //Level20_Manager.Instance.Level20_Door_1_Script.CanOpen = false;
                    //Level20_Manager.Instance.Level20_Door_10_Script.CanOpen = false;
                    //Level20_Manager.Instance.Level20_Door_12_Script.CanOpen = false;
                    //Level20_Manager.Instance.Level20_Door_17_Script.CanOpen = false;
                    //Level20_Manager.Instance.Level21_Door_A00_Script.CanOpen = false;
                    //Level20_Manager.Instance.Level21_Door_B04_Script.CanOpen = false;
                    //Level20_Manager.Instance.Level21_Door_B14_Script.CanOpen = false;
                    //Level20_Manager.Instance.Level21_Door_B07_Script.CanOpen = false;


                    //Level20_Manager.Instance.Level20_Door_12_Script.CanOpen = false;
                    //Level20_Manager.Instance.Level20_Door_0_Script.CanOpen = false;

                    //Boss死跳步
                }
                break;
            case 27005://
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                    //据开门
                }
                break;
            case 27006://找R
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                    //延时时间跳步
                }
                break;
            case 27007://返回修电梯
                {
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 27008://打死Boss
                {

                   

                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
        }
    }

    public override void UpdateTask(int taskId)
    {
        base.UpdateTask(taskId);
    }

    public override void ExitTask(int taskId)
    {
        base.ExitTask(taskId);
    }
}
