//TaskProgressLevel27.cs
//TEOT_ONLINE
//
//Create by WangJie on 8/15/2017 3:14 PM
//Description: µÚÁù½×¶Î
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskProgressLevel28 : TaskProgressBase
{
    override public void EnterTask(int taskId)
    {
        base.EnterTask(taskId);

        switch (taskId)
        {
            case 28001: // µ½´ïÁËÂ¥¶¥£¬µØ°åÌ®Ëú
                {
                    Debug.Log(28);
                }
                break;
            case 28002://Íæ¼Ò×ßµ½±ßÔµºó£¬ó¦Ð·¹Ö·ÉÉÏÀ´
                {
                    GameObject.Find("WarPlatform_Glow").GetComponent<test28Animation>().PlayAnimtion();
                    CrabManager.Instance.Init();
                }
                break;
            case 28003://´òËÀó¦Ð·¹Ö£¬³öÏÖ·É´¬
                {

                }
                break;
            case 28004://Íæ¼ÒÌøÉÏ·É´¬£¬·É´¬Æð·É
                {

                }
                break;
            case 28005://ÈÎÎñÍê³É
                {
                }
                break;
            case 28006://ÈÎÎñÍê³É
                {
                    //GameObject.Find("RoofDoor_Collision").GetComponent<Collider>().enabled = false ;
                }
                break;
            case 28007://ÈÎÎñÍê³É
                {
                }
                break;
            case 28008://ÈÎÎñÍê³É
                {
                }
                break;
            case 28009://ÈÎÎñÍê³É
                {
                    UtilFunction.ResourceLoad("Prefabs/SHP");
                }
                break;
            case 28010://ÈÎÎñÍê³É
                {
                    PlayerManager.Instance.playerCollider.GetComponent<test12>().Jump();
                }
                break;
        }
    }

    public override void UpdateTask(int taskId)
    {
        base.UpdateTask(taskId);
        switch (taskId)
        {
            case 28001:
                break;
            case 28009:
                UtilFunction.qwe(new Vector3(-4.701f,0f, 25.025f));
                break;
        }
    }

    public override void ExitTask(int taskId)
    {
        base.ExitTask(taskId);
    }
}
