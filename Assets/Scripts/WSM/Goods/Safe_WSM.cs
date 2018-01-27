using System.Collections;using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Safe_WSM : InteractiveGoods 
{


    #region---------外部变量----------


    public GameObject ZhuanNiou_D;
    public GameObject ZhuanNiou_X;
    public GameObject Suo;
    public GameObject Door;

 //声音

    #endregion

    #region---------内部变量----------

    private bool isZhuan_D = false;
    private bool isZhuan_X = false;

    private bool X_OK = false;

    private float _Zhuan_D =3f;
    private float __Zhuan_D = 0;

    private float _Zhuan_X = 3f;
    private float __Zhuan_X = 0;

    #endregion

    #region---------调用方法----------

    public void Init()
	{
        //ZhuanNiou_D = transform.Find("Men/DaSuo").gameObject;
        //ZhuanNiou_X = transform.Find("Man/XiaoSuo").gameObject;
        //Suo = transform.Find("Man/Tie").gameObject;
        //Door = transform.Find("Man").gameObject;
    }


    public void XuanZhuan_D()
    {
        if (X_OK)
        {
            isZhuan_D = true;
            __Zhuan_D = 0;
        }
    }

    public void XuanZhuan_X()
    {
        isZhuan_X = true;
        __Zhuan_X = 0;
    }

    #endregion

    #region---------功能方法----------

    public void UpdateXuanZhuan_D()
    {
        if (isZhuan_D)
        {
            if (_Zhuan_D < __Zhuan_D)
            {
                isZhuan_D = false;
                Tween t = Suo.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 1f); //锁缩小
                t.OnComplete(OpenDoor);// 完成变化时回调
            }
            ZhuanNiou_D.transform.Rotate(Vector3.forward);
            __Zhuan_D += Time.deltaTime;
        }
    }

    public void UpdateXuanZhuan_X()
    {
        if (isZhuan_X)
        {
            if (_Zhuan_X < __Zhuan_X)
            {
                isZhuan_X = false;
                X_OK = true;
            }
            ZhuanNiou_X.transform.Rotate(Vector3.forward);
            __Zhuan_X += Time.deltaTime;
        }
    }

    private void OpenDoor()
    {
        Door.transform.DOLocalRotateQuaternion(Quaternion.Euler(0,90,0),3f);
    }

    #endregion

    #region---------工具方法----------



    #endregion

    #region---------生命周期函数----------

    private  void Start ()
	{
        Init();
    }

	
	
	
	void Update () 
	{
     

        UpdateXuanZhuan_D();
        UpdateXuanZhuan_X();
    }



 #endregion

}
