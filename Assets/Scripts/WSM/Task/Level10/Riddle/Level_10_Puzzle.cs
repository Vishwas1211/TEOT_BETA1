using System.Collections;using System.Collections.Generic;
using UnityEngine;


public class Level_10_Puzzle : MonoBehaviour 
{
    public Level_10_MakeRiddle m_DaZu;

    public Level_10_PuzzleDigitalChange[] c0;

    public string targetN_0;
    public string targetN_1;
    public string targetN_2;

    public string intpuString;

    private float bianHuaShiJian = 0.5f;  //延迟时间

    #region---------外部变量----------



    #endregion

    #region---------内部变量----------



    #endregion

    #region---------调用方法----------

    public void Init(Level_10_MakeRiddle dazu)
	{
        c0 = new Level_10_PuzzleDigitalChange[transform.childCount];
        m_DaZu = dazu;
        for (int i = 0; i < transform.childCount; i++)
        {
            c0[i] = transform.GetChild(i).GetComponent<Level_10_PuzzleDigitalChange>();
            c0[i].Init(bianHuaShiJian);
        }
        XiaoShi();
    }

    public void XiaoShi()
    {
        gameObject.SetActive(false);
    }

    public void PanDuan(string input)  //判断是否正确
    {
        string a = input.Replace(" ", ""); //去除所有的空格
        string b = targetN_0 + targetN_1 + targetN_2;
        if (a == b)
        {
            ChengGong();
        }
        else
        {
            ShiBai();
        }
    }

    public void KanShiSuiJi()      //开始随机
    {
        gameObject.SetActive(true);
        foreach (var item in c0)
        {
            item.LuanBeng();

            StartCoroutine(CloseDoor_defer());
        }
    }

    public void GuanBi()        //关闭这个谜题
    {
        gameObject.SetActive(false);
    }

    private IEnumerator CloseDoor_defer()
    {
        for (int i = 0; i < c0.Length; i++)
        {
            yield return new WaitForSeconds(bianHuaShiJian);
            c0[i].WanCheng();

        }
       
    }



    #endregion

    #region---------功能方法----------

    public void ChengGong()   //输入正确
    {
        m_DaZu.ChengGong();
    }

    public void ShiBai() //输入错误
    {
        m_DaZu.ShiBai();
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
