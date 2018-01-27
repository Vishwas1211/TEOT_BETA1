using System.Collections;using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_10_PuzzleDigitalChange : MonoBehaviour 
{


    #region---------外部变量----------

    public float shuZi_0;
    public float shuZi_1;
    public float shuZi_2;
    private float _shuZi_0 = 0;
    private float _shuZi_1 = 0;
    private float _shuZi_2 = 0;
    public Text m_text;
    private string m_string;
    private float bianHuaShiJian = 2f;  //延迟时间


    private bool can_0 = false;
    private bool can_1 = false;
    private bool can_2 = false;

    #endregion

    #region---------内部变量----------

    private bool isLuanBeng = false;

 #endregion

 #region---------调用方法----------

	public void Init(float  a)
	{
        bianHuaShiJian = a;

    }

    public void WanCheng()
    {
        StartCoroutine(CloseDoor_defer());
    }

    public void LuanBeng()
    {
        can_0 = true;
        can_1 = true;
        can_2 = true;
    }
    #endregion

    #region---------功能方法----------

    public void ShuZiLuanBeng_0()
    {
        if(can_0)
        _shuZi_0 =((int)Random.Range(2, 9));
    }
    public void ShuZiLuanBeng_1()
    {
        if (can_1)
            _shuZi_1 = ((int)Random.Range(2, 9));
    }
    public void ShuZiLuanBeng_2()
    {
        if (can_2)
            _shuZi_2 = ((int)Random.Range(2, 9));
    }

    #endregion

    #region---------工具方法----------



    #endregion

    #region---------生命周期函数----------

    private  void Start ()
	{

        m_text = GetComponent<Text>();
	
	}

	
	
	
	void Update () 
	{
        ShuZiLuanBeng_0();
        ShuZiLuanBeng_1();
        ShuZiLuanBeng_2();

        m_string = _shuZi_0.ToString()+ " " + _shuZi_1.ToString() + " " + _shuZi_2.ToString();
        m_text.text = m_string;

        if (Input.GetKeyDown(KeyCode.R))
        {
            LuanBeng();
        }
    }



    private IEnumerator CloseDoor_defer()
    {
        can_0 = true;
        can_1 = true;
        can_2 = true;
        yield return new WaitForSeconds(bianHuaShiJian);
        can_0 = false;
        _shuZi_0 = shuZi_0;
        yield return new WaitForSeconds(bianHuaShiJian);
        can_1 = false;
        _shuZi_1 = shuZi_1;
        yield return new WaitForSeconds(bianHuaShiJian);
        can_2 = false;
        _shuZi_2 = shuZi_2;
    }

    #endregion

}
