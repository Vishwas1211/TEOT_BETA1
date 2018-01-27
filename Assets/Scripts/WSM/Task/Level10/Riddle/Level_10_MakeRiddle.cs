using System.Collections;using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_10_MakeRiddle : MonoBehaviour 
{


    #region---------外部变量----------

    private Level_10_Puzzle[] XiaoZu;

    private int MiTiBianHao = 0;//谜题编号

    private GameObject CuoWu;

    private bool CanInput = false;

    private Animator m_Animator;

    private InputField m_InputField;

    #endregion

    #region---------内部变量----------



    #endregion

    #region---------调用方法----------

    public void Init()
    {
        XiaoZu = new Level_10_Puzzle[transform.Find("XiaoZu").childCount];
        for (int i = 0; i < XiaoZu.Length; i++)
        {
            XiaoZu[i] = transform.Find("XiaoZu").GetChild(i).GetComponent<Level_10_Puzzle>();
            XiaoZu[i].Init(this);
        }
        CuoWu = transform.Find("TiShi/WRONG CODE (1)").gameObject;  //输入错误的提示
        CuoWu.SetActive(false);

        m_Animator = transform.root.Find("Ex_5C_F10/TV").GetComponent<Animator>();

        m_InputField = GetComponent<InputField>();
    }

    public void ShengChengMiTi()    //随机谜题
    {
        for (int i = 0; i < XiaoZu.Length; i++) //关闭所有谜题
        {
            XiaoZu[i].GuanBi();
        }
        CanInput = true;
        MiTiBianHao = Random.Range(0, transform.Find("XiaoZu").childCount - 1);
        XiaoZu[MiTiBianHao].KanShiSuiJi();   //打开一个
    }


    public void ShuRu_0(string s)
    {
        if (!CanInput)
        {
            s = null;
            //TODO 播一个警告声音之类的
            return;
        }
        string a = s.Replace(" ",""); //去除所有的空格
        if (a.Length <= 2 && a[a.Length - 1] == '1')
        {
            s += "  ";
        }
        if (a.Length <=2)
        {
            s += " ";
        }

    }

    public void XiaoShi()
    {
        for (int i = 0; i < XiaoZu.Length; i++)
        {
            XiaoZu[i].XiaoShi();
        }
        m_InputField .text = "";
}

    public void WanCheng(string s)  //玩家输入完成
    {
        //Debug.Log("玩家输入完成");
        if (CanInput)
        {
            s = m_InputField.textComponent.text;
            if (s.Length >= 3)
            {
                CanInput = false;
                XiaoZu[MiTiBianHao].PanDuan(s);
            }
        }
    }

    public void ChengGong()   //输入正确
    {
        m_Animator.SetBool("5_6",true);
        StartCoroutine(YanShiWanCheng());

        XiaoShi();
    }

    public void ShiBai() //输入错误
    {
        CuoWu.SetActive(true);
        //TODO 警告声音
        StartCoroutine(CloseDoor_defer());
    }

    public IEnumerator CloseDoor_defer()
    {
        yield return new WaitForSeconds(2);
        CuoWu.SetActive(false);
        ShengChengMiTi();
    }

    public IEnumerator YanShiWanCheng()
    {
        yield return new WaitForSeconds(5);
        Level_10_Manager.Instance.CompletePuzzle = true;
        Level_10_Manager.Instance.KaiQiZhong = false;
        Level_10_Manager.Instance.TuiChuTeXie(true);
        Level_10_Manager.Instance.Door_2_Script.canOpen = true;
        Level_10_Manager.Instance.JianShiQi.SetActive(true);
        Level_10_Manager.Instance.Show_dianshiji_videofeed(false);
    }

    #endregion

    #region---------功能方法----------



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

    

    }



    #endregion

}
