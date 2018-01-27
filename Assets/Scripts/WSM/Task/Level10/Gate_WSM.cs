using System.Collections;using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gate_WSM : MonoBehaviour 
{



    #region---------外部变量----------



    #endregion

    #region---------内部变量----------
    public GameObject door_L;
    public GameObject door_R;

    private Vector3 L;
    private Vector3 R;

    private bool IsOpen = false;
    public bool canOpen = false;
    public bool canClose = false;

    private bool isComplete = true; //变化完成
 #endregion

    #region---------调用方法----------

    public void Init()
	{
        //door_L = transform.GetChild(0).gameObject;
        //door_R = transform.GetChild(1).gameObject;
        L = door_L.transform.position;
        R = door_R.transform.position;
    }

    public void CaoZui()
    {
        if (isComplete)
        {
            if (IsOpen)
            {
                CloseDoor();
            }
            else
            {
                OpenDoor();
            }
            IsOpen = !IsOpen;
        }
    }

    //开门
    public void OpenDoor()
    {
        isComplete = false;
        Tween t1 =  door_L.transform.DOMoveX(door_L.transform.position.x - 1f, 3);
        door_R.transform.DOMoveX(door_R.transform.position.x + 1f, 3);

        t1.OnComplete(OnComplete);

     
    }

  

    //关门
    public void CloseDoor()
    {

        isComplete = false;
        Tween t1 = door_L.transform.DOMoveX(door_L.transform.position.x + 1f, 3);
        door_R.transform.DOMoveX(door_R.transform.position.x - 1f, 3);
        t1.OnComplete(OnComplete);
    }

    #endregion

    #region---------功能方法----------

    // 完成变化时回调
    private void OnComplete()
    {
        isComplete = true;
    }

    public void UpdateOpenDoor()
    {
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
