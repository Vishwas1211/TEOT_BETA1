//TestRotate.cs
//TEOT_ONLINE
//
//Create by WangJie on 12/23/2017 10:16 AM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestRotate : MonoBehaviour 
{
    public bool move;
    public float rotDuration = 0.7f;
    public float moveDuration = 0.7f;
    public GameObject go;
    public GameObject rGo;

    public GameObject g;
    private Vector3[] points;

    public GameObject point;

    private void Start()
    {
        points = new Vector3[g.transform.childCount];
        for (int i = 0; i < g.transform.childCount; i++)
        {
            points[i] = g.transform.GetChild(i).position;
        }

        //transform.DOLocalRotateQuaternion(rGo.transform.rotation, rotDuration);
        //transform.DOPath(points, 5);
        if (move)
        {
            transform.DOLocalPath(points, moveDuration, PathType.Linear, PathMode.Full3D);
        }

        float angle = Vector3.Angle(transform.forward, point.transform.forward);
        Debug.Log(angle);
    }

    private void Update()
    {
        //go.transform.rotation = Quaternion.Lerp(go.transform.rotation, rGo.transform.rotation, .5f * Time.deltaTime);
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * 2 * Time.deltaTime, Space.Self);
        }
    }

}
