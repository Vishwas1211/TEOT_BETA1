using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class testGuiZiMove : MonoBehaviour {

    public void Move()
    {
        transform.DOLocalMoveZ(-0.76f,1).OnComplete(setTure);
    }

    public void setTure() {
        GameObject.Find("18SOG (1)").GetComponent<testL18Boss>().GongJi();
        LizzyManager.Instance.FinishCurTaskImmediately(1017);
    }
}
