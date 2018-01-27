using System.Collections;using System.Collections.Generic;
using UnityEngine;


public class GuanCha_WSM : MonoBehaviour 
{


    #region---------外部变量----------

    public Transform [] wuPin;

    #endregion

    #region---------内部变量----------
    private int geShu = 0;
    public bool isShow = false;

    private float x;
    private float y;
    private float s = 1;
    #endregion

    #region---------调用方法----------

    public void Init()
	{
        wuPin = new Transform[transform.childCount];
        for (int i = 0; i < wuPin.Length; i++)
        {
            wuPin[i] =transform.GetChild(i);
            wuPin[i].gameObject.SetActive(false);
        }


    }

    public void KanShiGuanCha( int a)
    {
        //关掉相机上的脚本
        Level_10_Manager.Instance.GuanBiXiangJi(false);

        geShu = a;
        wuPin[geShu].gameObject.SetActive(true);
        wuPin[geShu].transform.localPosition = new Vector3(0, 0, 0.36f);
        wuPin[geShu].transform.localRotation = Quaternion.Euler(0, 0, 0);
        wuPin[geShu].transform.localScale = Vector3.one;
        isShow = true;
    }

    public void WanCheng()  //完成
    {
        isShow = false;
        YinCang();

        Level_10_Manager.Instance.GuanBiXiangJi(true);
    }

    public void YinCang()   // 隐藏
    {
        for (int i = 0; i < wuPin.Length; i++)
        {
            wuPin[i].gameObject.SetActive(false);
        }
    }
    #endregion

    #region---------功能方法----------



    #endregion

    #region---------工具方法----------



    #endregion

    #region---------生命周期函数----------

    private void Start()
    {
        Init();
    }


    // Update is called once per frame
    void Update()
    {
        if (isShow)
        {
            //transform.rotation = Quaternion.Euler(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
            //transform.Rotate(Input.GetAxis("Mouse Y"), 0, -Input.GetAxis("Mouse X"));
            x += Input.GetAxis("Mouse Y");
            y += Input.GetAxis("Mouse X");
            s += Input.GetAxis("Mouse ScrollWheel");
            wuPin[geShu]. transform.localRotation = Quaternion.Euler(x, -y, 0);
            wuPin[geShu]. transform.localScale = new Vector3(s, s, s);
            if (Input.GetMouseButtonDown(1))
            {
                WanCheng();


            }
        }
    }

  



    #endregion

}
