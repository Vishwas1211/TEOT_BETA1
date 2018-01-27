//
//  GameStart.cs
//  TEOT_ONLINE
//
//  Created by EDSENSES-P1 on 8/2/2017 10:15 AM.
//
//

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UIFramework;

public class GameStart : MonoBehaviour
{
    public int id;
    private bool isb;
    private void Start()
    {
        StartCoroutine(StartGame());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            TaskStepManagaer.Instance.FinishCurTaskImmediately();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            TaskStepManagaer.Instance.FinishTaskTo(id);
        }
        //if (!isb && UtilFunction.ewq(new Vector3(1.084f, 80.669f, 36.487f)))
        //{
        //    isb = true;
        //    animator.Play("Take 001");
        //    go.SetActive(false);
        //}
        
    }

    IEnumerator StartGame() {
        yield return new WaitForSeconds(1);
        DataManager.instance.LoadData();
        TaskStepManagaer.Instance.Load();
        TaskStepManagaer.Instance.StartTask();
        PlayerManager.Instance.Init();
        //Backpack.instance.Init();

        DoorManager.Instance.Init();
        Level_01_Manager.Instance.Init();
        Level_04_Manager.Instance.Init();
        Level_05_Manager.Instance.Init();   //µÚÎå²ã
        Level_10_Manager.Instance.Init();   //µÚÊ®²ã
        Level_20_Manager.Instance.Init();   //µÚ¶þÊ®²ã
        Level_21_Manager.Instance.Init();
        LizzyManager.Instance.Init();
        //JayceeManager.instance.Init();    //Á÷³ÌÀïµ÷ÓÃµÄ

        RickManager.Instance.Init();

        RabManager.Instance.Init();
        UIManager.Instance.OpenPage(AppConst.UIPATH_MianUI);
    }
}