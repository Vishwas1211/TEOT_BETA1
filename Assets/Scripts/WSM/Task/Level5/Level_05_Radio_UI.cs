using System.Collections;using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class Level_05_Radio_UI : MonoBehaviour, IPointerDownHandler
{


    #region---------外部变量----------

    public UI_Type m_type;
    public int m_ButtonNumber;

    public float ZhengQue = 5;

    public AudioClip XinXi;

    #endregion

    #region---------内部变量----------

    public enum UI_Type
    {
       Button,
       BoChang,
       YiLiang
    }

 #endregion

 #region---------调用方法----------

	public void Init()
	{
	}

    //被按下
    public void OnPointerDown(PointerEventData eventData)
    {
        //Level_05_Manager.Instance.Radio_Script.CurrentUI = this;
        if (m_type == UI_Type.Button)
        {
            Level_05_Manager.Instance.Radio_Script.Radio1.clip = XinXi;
            Level_05_Manager.Instance.Radio_Script.ZhengQue = ZhengQue;
            Level_05_Manager.Instance.Radio_Script.BianHua();
        }
    }

    #endregion

    #region---------功能方法----------



    #endregion

    #region---------工具方法----------

    //高亮显示


    #endregion

    #region---------生命周期函数----------

    private void Start ()
	{
		
		
	
	}

	
	
	
	void Update () 
	{
		
		
	
	}



 #endregion

}
