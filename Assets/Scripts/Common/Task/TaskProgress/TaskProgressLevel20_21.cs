//TaskProgressLevel20_21.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/15/2017 3:01 PM
//Description: µÚËÄ½×¶Î
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskProgressLevel20_21 : TaskProgressBase
{
    override public void EnterTask(int taskId)
    {
        base.EnterTask(taskId);

        switch (taskId)
        {
            case 20001: // £¨Ñ°ÕÒÏßË÷£©µçÌÝÐèÒªÔ¿³×²Å¿ÉÒÔ¿ªÆô£¬Íæ¼ÒÒªÏë°ì·¨²»Í¨¹ýÕ½¶·À´ÏûÃð»úÆ÷ÈË£¬Ö»ÄÜµ½Õ¹ÀÀÊÒÑ°ÕÒÏßË÷
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 20002:// lizzy chu xian
                {
                    Destroy(GameObject.Find("L18key (1)"));
                    //TaskStepManagaer.Instance.FinishCurTask();
                    Debug.Log(20002);
                    LizzyManager.Instance.FinishCurTaskImmediately(1006);
                    TaskStepManagaer.Instance.FinishTaskTo(20004);
                }
                break;
            case 20003://find tool
                {
                    LizzyManager.Instance.lizzy.SetActive(false);
                    Debug.Log(20003);
                    PlaySoundController.Instance.PlaySoundEffect(this.gameObject, 10001);
                    //TaskStepManagaer.Instance.FinishCurTask();
                    TaskStepManagaer.Instance.FinishTaskTo(25003);

                }
                break;
            case 20004://Íæ¼Ò½øÈë·¿¼ä
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                    //TaskStepManagaer.Instance.FinishTaskTo(25001);
                }
                break;
            case 20005://ËùÓÐ»úÆ÷ÈË¶¼ËÀµô
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 20006://´ò¿ªµçÌÝÃÅ£¬ÃÅ¿ªµÄÒ»É²ÄÇ£¬Ò»¸ö½©Ê¬µô³öÀ´ ÏÅËÀÍæ¼Ò
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 20007://Íæ¼ÒÐèÒªÆÆ½âÍ¼ÐÎÂë²ÅÄÜÊ¹µçÌÝÆô¶¯
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                    TaskStepManagaer.Instance.FinishTaskTo(25001);
                }
                break;
            case 21001://µçÌÝÆô¶¯ºó£¬ÉãÏñ»úÉÁºìÉ«£¬µçÌÝÍ£Ö¹¿ªÊ¼Õð¶¯£¬Íæ¼ÒÐèÒª´ò¿ªµçÌÝ¶¥²¿µÄÃÅãÅ  
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 21002://´ò¿ªÃÅãÅÖ®ºó£¬ÐèÒªÐÞÏß
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 21003://ÐÞºÃµçÌÝÖ®ºó£¬µ½´ï12²ã£¬Í£Ö¹
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 21004://Íæ¼Ò×ßÏò×ßÀÈ
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 21005://Ìýµ½Ò»Éù¼â½Ð£¬Íæ¼ÒÒªÕÒµ½lizzy 
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
        }
    }

    public override void UpdateTask(int taskId)
    {
        base.UpdateTask(taskId);
        switch (taskId)
        {
            case 20001:
                if (UtilFunction.ewq(new Vector3(-5.791f, 80.669f, 39.338f)))
                {
                    TaskStepManagaer.Instance.FinishTaskTo(20002);
                }
                if (UtilFunction.ewq(new Vector3(-9.515f, 80.669f, 39.338f)))
                {
                    TaskStepManagaer.Instance.FinishTaskTo(20003);
                }
                break;
        }
    }

    public override void ExitTask(int taskId)
    {
        base.ExitTask(taskId);
    }
}
