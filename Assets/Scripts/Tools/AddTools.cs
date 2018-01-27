//
//  AddTools.cs
//  TEOT_ONLINE
//
//  Created by 孙磊 on 8/10/2017 12:22 PM.
//
//

using UnityEngine;
using System.Collections;

public class AddTools : MonoBehaviour
{

    #region---------公有变量----------
    public int id;
    #endregion

    #region---------私有变量----------
    #endregion

    #region---------生命周期函数----------  

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnEnable()
    {
        BackpackController.AddToolsEvent += new BackpackControllerEventHandler(CallBackAddTools);
    }
    #endregion   

    #region---------工具方法----------
    #endregion

    #region---------私有方法----------
    #endregion

    #region---------公有方法----------

    #endregion

    #region---------回调方法----------
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("PlayerCollider") || other.name.Contains("Camera (eye)"))
        {
            return;
        }
        Debug.Log(other.name);
        other.transform.Find("RadialMenu/RadialMenuUI/Panel").GetComponent<BackpackController>().AddTools(id);
        Destroy(this.gameObject);
    }

    private void CallBackAddTools(PlayerToolsBase p)
    {
        //Destroy(this.gameObject);
    }
    #endregion






}