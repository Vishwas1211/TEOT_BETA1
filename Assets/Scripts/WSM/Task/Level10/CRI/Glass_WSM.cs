using System.Collections;using System.Collections.Generic;
using UnityEngine;


public class Glass_WSM : MonoBehaviour 
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

    public void PoSui()
    {
        //播放声音
        //播放破损特效
        //删除自己
        Destroy(this.gameObject);
    }

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
