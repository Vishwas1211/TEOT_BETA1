//
//  DecodingPanel.cs
//  TEOT_ONLINE
//
//  Created by 王颉 on 8/4/2017 12:06 PM.
//  解码面板
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DecodingPanel : MonoBehaviour {

    private bool _testBool;
    private float _timer;
    private float _succeedTime = 5f;

    private void UpdateCheck()
    {
        if (_testBool)
        {
            _timer += Time.deltaTime;
            if (_timer >= _succeedTime)
            {
                _timer = 0;
                Succeed();
            }
        }
        else
        {
            _timer = 0;
        }
    }

    private void Succeed()
    {
        //TODO 处理逻辑
        TaskStepManagaer.Instance.FinishCurTask();
    }

    #region---------生命周期函数----------  

    void Start () 
    {

    }

    void Update () 
    {
        UpdateCheck();
    }

    #endregion    

}