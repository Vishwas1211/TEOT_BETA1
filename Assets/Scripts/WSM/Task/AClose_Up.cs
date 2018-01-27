using System.Collections;using System.Collections.Generic;
using UnityEngine;


public class AClose_Up : MonoBehaviour 
{


    #region---------外部变量----------

    private Transform Poarent;
    private Vector3 pos;
    private Quaternion rot;

    public float speed = 1f;

    #endregion

    #region---------内部变量----------
    private bool isTeXie = false;
    private bool isHuanYuan = false;
    private bool ZhuangTie = false;//是否十特写状态

    private Transform OPoarent;
    private Vector3 Opos;
    private Quaternion Orot;

 #endregion

 #region---------调用方法----------

	public void Init()
	{
        OPoarent = transform.parent;
        Opos = transform.localPosition;
        Orot = transform.localRotation;
    }

    public void TeXie(Vector3 pos,Quaternion rot)
    {
        transform.parent = null;
        this.pos = pos;
        this.rot = rot;

        transform.position = pos;
        transform.rotation = rot;

        //isTeXie = true;
        //isHuanYuan = false;
        ZhuangTie = true;
    }

    public void HuanYuan()    //还原
    {
        transform.parent = OPoarent;
        transform.localPosition = Opos;
        transform.localRotation = Orot;
        Level_10_Manager.Instance.WanCheng();

        //isTeXie = false;
        //isHuanYuan = true;
        ZhuangTie = false;
    }

    #endregion

    #region---------功能方法----------

    public void UpdateTeXie()
    {
        if (!isTeXie)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, pos, speed);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, speed);

        if (Vector3.Distance(transform.position, pos) < 0.1f && Quaternion.Angle(transform.rotation, rot) < 1f)
        {
            isTeXie = false;
        }

    }

    public void UpdateHuanYuan()
    {
        if (!isHuanYuan)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.localPosition, Opos, speed);
        transform.rotation = Quaternion.Lerp(transform.localRotation, Orot, speed);

        if (Vector3.Distance(transform.localPosition, Opos) < 0.1f && Quaternion.Angle(transform.localRotation, Orot) < 1f)
        {
            isHuanYuan = false;

            transform.localPosition = Opos;
            transform.localRotation = Orot;
        }

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
        //UpdateTeXie();
        //UpdateHuanYuan();

        if (ZhuangTie && Input.GetKeyDown(KeyCode.Escape))
        {
            HuanYuan();
        }
    }



 #endregion

}
