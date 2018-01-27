using System.Collections;using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JayceeMonster_WSM : MonoBehaviour 
{

	
 #region---------外部变量----------



 #endregion

 #region---------内部变量----------



 #endregion

 #region---------调用方法----------

	public void Init()
	{
	}


    #endregion

    #region---------功能方法----------



    #endregion

    #region---------工具方法----------



    #endregion

    #region---------生命周期函数----------

    public float time_1 = 1;
    private float time_2 = 0;

    public bool _Stop = true;

	private  void Start ()
	{
        _Stop = true;

    }

	
	
	
	void Update () 
	{

        if (_Stop && time_1 < time_2)
        {
            time_2 = 0;
            transform.DOMoveY(transform.position.y+0.6f, 0.4f);
            Level_05_Manager.Instance.Shock_TiZi_F5_25(1f);
        }
        time_2 += Time.deltaTime;

        if (transform.position.y > 48.04)
        {
            PlayerArrive();
        }
       

        if (false) //TODO 追上玩家
        {
            //游戏结束
            _Stop = true;
        }
	}

    public void PlayerArrive()
    {
        _Stop = false;
        //Destroy(this.gameObject);
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }


 #endregion

}
