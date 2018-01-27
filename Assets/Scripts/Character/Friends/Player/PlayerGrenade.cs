using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrenade : MonoBehaviour
{

    public float time = 3;//代表从A点出发到B经过的时长
    public Transform pointA;//点A
    public Transform pointB;//点B
    public float g = -10;//重力加速度
    // Use this for initialization
    private Vector3 speed;//初速度向量
    private Vector3 Gravity;//重力向量
    void Start()
    {
    }
    private float dTime = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.M))
        {
            dTime = 0;
            Fire(pointA, pointB);
        }
            Gravity.y = g * (dTime += Time.fixedDeltaTime);//v=at
                                                           //模拟位移
            transform.Translate(speed * Time.fixedDeltaTime);
            transform.Translate(Gravity * Time.fixedDeltaTime);
    }

    public void Fire(Transform startPos, Transform endPos)
    {
        g = -(startPos.position - endPos.position).magnitude / 4;

        transform.position = startPos.transform.position;
        //transform.position = pointA.position;//将物体置于A点
        //通过一个式子计算初速度
        speed = new Vector3((endPos.position.x - startPos.position.x) / time,
            (endPos.position.y - startPos.position.y) / time - 0.5f * g * time, (endPos.position.z - startPos.position.z) / time);
        Gravity = Vector3.zero;//重力初始速度为0
    }
}
