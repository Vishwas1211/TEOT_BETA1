using System.Collections;using System.Collections.Generic;
using UnityEngine;


public class InteractiveGoods : MonoBehaviour 
{


    #region---------外部变量----------

    public GoodsType m_GoodsType;

    #endregion

    #region---------内部变量----------



    public enum GoodsType
    {
        Door,    //碎片
        Broken,  //破碎
        MoveOut, //搬走
        Get,     //获取
    }

    #endregion

    #region---------调用方法----------

    public void Init()
	{
	}

    public virtual void Operation()  // 操作
    {
    }




 #endregion

 #region---------功能方法----------



 #endregion

 #region---------工具方法----------



 #endregion

 #region---------生命周期函数----------

	private  void Start ()
	{
		
		
	
	}

	
	
	
	void Update () 
	{
		
		
	
	}



 #endregion

}
