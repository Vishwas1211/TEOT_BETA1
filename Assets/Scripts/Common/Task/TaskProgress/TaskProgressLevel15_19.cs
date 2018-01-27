//TaskProgressLevel15_19.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/15/2017 10:14 AM
//Description: µÚÈý½×¶Î
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TaskProgressLevel15_19 : TaskProgressBase
{
    override public void EnterTask(int taskId)
    {
        base.EnterTask(taskId);

        switch (taskId)
        {
            case 15001://µÐÈË¶¼±»ÏûÃðºó£¬Íæ¼ÒÕÒÃÜÂëÓÃÀ´¹Ø±ÕµçÔ´
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 15002://¹Ø±ÕµçÔ´Ö®ºó£¬Íæ¼ÒÊÔÍ¼×ß½øÁíÒ»¸ö·¿¼ä¡£Ð¡Å®º¢½¨ÒéÓÃÍ¨·çµÀ
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 15003://µ±Íæ¼Òµ½´ïÍ¨·çµÀÓÒ±ß£¬Ð¡Å®º¢»áÌáÊ¾Íæ¼ÒÏòÍâ±ß¿´£¬·¢ÏÖµÐÈË
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 16001://É±ËÀµÐÈË£¬ÃÅ´ò¿ª
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 16002://ÃÅ´ò¿ªºóÒ»¸ö¶«Î÷µôÏÂÀ´£¬¹ý¼¸ÃëÒ»¸ö½©Ê¬³öÏÖ
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 16003://´òËÀ½©Ê¬ºóºÜ¶à±ãÇ©µôÂä£¬Ð¡Å®º¢Ëµ»°£¬Íæ¼ÒÐèÒª½øÄÚÎÝ
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 16004://½øµ½ÄÚÎÝÖ®ºó£¬ÓÐºÜ¶àÒÇÆ÷£¬Íæ¼ÒÐèÒª´òÆÆ²£Á§
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 16005://´òÆÆÁË²£Á§ºó£¬Ð¡Å®º¢¶ãÆðÀ´
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 16006://Íæ¼Ò½øÈëÁËÊµÑéÊÒ
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 16007://Íæ¼Ò×ß¹ýÊÔ¹ÜÊ±£¬ÏµÍ³ÌáÊ¾¹ÊÕÏ£¬¹ÖÎï±»¾ªÐÑ£¬·¢³ö¾¯±¨£¬ÑÓ³ÙÌø²½
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 16008://ÊÔ¹ÜÆÆÁÑ£¬¹ÖÎïËÕÐÑÅÜµô£¬Íæ¼ÒÐèÒªÈ¥¼à¿ØÊÒ¹Ø±ÕµçÔ´
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 17001://Íæ¼ÒÅÜµ½¼à¿ØÊÒºó£¬¿´µ½¼àÊÓÆ÷ÉÏÊÓÆµ£¬Íæ¼ÒÐèÒªÇÐ¶ÏµçÔ´£¬Ïú»Ù¾¯±¨Æ÷
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 17002://¹Ø±Õºó£¬»úÆ÷ËµÒ»¾ä»°£¬Õû¸öÃÅ×Ô¶¯¿ªÆô£¬³ýÁËÐÒ´æÕßµÄÃÅ¡£
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 17003://Íæ¼ÒÀ´µ½ÃÅ¿ÚÊ±£¬¿ÆÑ§¼ÒËµ»°£¬NPC¶Ô»°£¬Íæ¼ÒÐèÒªÈ¥ÕÒ¼¤¹â¾â°ÑÃÅ´ò¿ª
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 17004://µ½´ï¹¤¾ßÊÒºó½øÐÐÏÂÒ»²½
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 17005://¿Õ²½
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 18001://³öÏÖÁË¹ÖÎï£¬Íæ¼ÒÐèÒª»Øµ½ÐÒ´æÕß·¿¼ä
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 18002://Íæ¼Òµ½´ïÐÒ´æÕß·¿¼äÃÅÍâ£¬George»áÖ¸µ¼Íæ¼ÒÊ¹ÓÃ¼¤¹â¾â
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 18003://Íæ¼Ò´ò¿ªÃÅºó£¬ÐÒ´æÕß¸ÐÐ»Íæ¼Ò£¬²¢¸øÍæ¼ÒÁËÒ»¸ö¼ÙÏûÏ¢
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 18004://NPC³ÃÍæ¼Ò²»×¢Òâ£¬¿ªÊ¼ÍµÏ®Íæ¼Ò
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 18005://Íæ¼Ò°ÑGeorgeºÍAmy´òËÀ£¬Ð¡Å®º¢ºÞËÀÍæ¼Ò£¬Ë¤ÃÅ¶ø³ö
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 18006://¿Õ²½
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 18007://¿Õ²½
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 18008://¿Õ²½
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 18009://¿Õ²½
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 18010://¿Õ²½
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 19001://Íæ¼Ò´ò¿ªÁËÐÒ´æÕßÍ£ÁôµÄ·¿¼äÀïÃæµÄÒ»ÉÈÃÅ£¬¿´µ½EmilyÔÚÀïÃæµÈ×Å
                {
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 19002://È»ºóEmilyß¶ß¶Ò»Ð©»°£¬Íæ¼Ò´ÓÖÐÖªµÀÁËÍ¨ÍùÂ¥ÌÝµÄµÀÂ·
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 19003://¿ªÃÅºó£¬ÏòÂ¥ÌÝ×ß
                {
                    PlaySoundController.Instance.PlaySoundEffect(this.gameObject, 10001);
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 19004://电源重启完毕
                {
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 19005://Íæ¼Òµ½´ïÕ¹Ê¾Ìüºó£¬¿´µ½Ò»¸öºÜ´óµÄ»úÆ÷ÈËÊØ»¤×ÅÕâÀï
                {
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 19006://Íæ¼Òµ½´ïµçÌÝºó£¬·¢ÏÖµçÌÝÃ»ÓÐµç£¬Íæ¼ÒÐèÒªÖØÐÂÆô¶¯µçÔ´£¬Â·ÉÏÓÐÒ»Ð©µÐÈË
                {
                    GameObject.Find("DianTi_B_KeyHole").transform.GetComponent<testOpenElevator>().CloseElevator();
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 19007://第一个灯
                {
                    StartCoroutine(GameObject.Find("riddle").GetComponent<TargetSymbolNew>().MakePuzzle(2));
                }
                //TaskStepManagaer.Instance.FinishCurTask();
                break;
            case 19008://第二个灯
                {
                    StartCoroutine(GameObject.Find("riddle").GetComponent<TargetSymbolNew>().MakePuzzle(2));
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 19009://电梯上升
                {
                    Destroy(GameObject.Find("riddle").gameObject);
                    GameObject.Find("Level13GameObjectManager/Camera").gameObject.SetActive(false);
                    PlayerManager.Instance.eye.gameObject.SetActive(true);
                    PlayerManager.Instance.eye.transform.root.GetComponent<FreeLookCam>().enabled = true;
                    PlayerManager.Instance.playerCollider.transform.parent = GameObject.Find("DianTi_B_Shang").transform;
                    GameObject.Find("DianTi_B_Shang").transform.DOLocalMoveY(2.464f, 10).OnComplete(TaskStepManagaer.Instance.FinishCurTask);
                    //TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
            case 19010://完成解密
                {
                    PlayerManager.Instance.playerCollider.transform.parent = null;
                    GameObject.Find("DianTi_B_KeyHole").transform.GetComponent<testOpenElevator>().OpenElevator();
                    TaskStepManagaer.Instance.FinishCurTask();
                }
                break;
        }
    }

    public override void UpdateTask(int taskId)
    {
        base.UpdateTask(taskId);

        switch (taskId)
        {
            case 19005:
                UtilFunction.qwe(new Vector3(-20.253f, 0, 24.617f));
                break;
        }
    }

    public override void ExitTask(int taskId)
    {
        base.ExitTask(taskId);
    }
}
