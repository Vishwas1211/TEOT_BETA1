using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class testZMB : MonoBehaviour
{
    public Transform pos;
    public Animator ani;
    //public Vector3[] path = new Vector3[3];
    bool isb = true;
    bool isDeath = false;
    public string aniName;
    // Use this for initialization
    void Start()
    {
        ani.Play(aniName);
        PlayerInfo.PlayerAliveEvent += ewq;
        PlayerInfo.PlayerDeathEvent += qwe;
        //path[0] = new Vector3(transform.position.x,46.9f,transform.position.z);
    }

    void qwe() {
        Debug.Log("idle");
    }

    void ewq() {
        Debug.Log("attack");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        //if (transform.position.y > 46.9f && isb)
        //{
        //    isb = false;
        //    transform.GetComponent<Animator>().SetBool("isPa", true);
        //    transform.DOLocalPath(path, 2, PathType.CatmullRom);
        //}
    }

    private float _curHp = 5;
    public void ShoShang(float hp)
    {
        if (isDeath) return;

        _curHp -= hp;
        if (_curHp <= 0)
        {
            NoLockView_Camera.can_4_8 = true;
            transform.GetComponent<BoxCollider>().enabled = false;
            ani.SetBool("isDeath", true);
            isDeath = true;
        }
    }
}
