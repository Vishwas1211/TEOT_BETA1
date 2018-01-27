//
//  test6.cs
//  TEOT_ONLINE
//
//  Created by 孙磊 on 8/12/2017 1:07 PM.
//
//

using UnityEngine;
using System.Collections;

public class test6 : MonoBehaviour {
    bool isb = true;
    private void FixedUpdate()
    {
        if (isb)
        {
            //transform.parent.Find("GameObject").position = new Vector3(PlayerManager.Instance.leftHandController.transform.position.x - 0.0359993f, PlayerManager.Instance.leftHandController.transform.position.y + 0.011f, PlayerManager.Instance.leftHandController.transform.position.z-0.1439972f);

            transform.parent.Find("GameObject").position = new Vector3(PlayerManager.Instance.leftHandController.transform.position.x, PlayerManager.Instance.leftHandController.transform.position.y, PlayerManager.Instance.leftHandController.transform.position.z);
        }
        transform.parent.Find("GameObject").rotation = Quaternion.Euler(PlayerManager.Instance.leftHandController.transform.rotation.eulerAngles.x, PlayerManager.Instance.leftHandController.transform.rotation.eulerAngles.y, PlayerManager.Instance.leftHandController.transform.rotation.eulerAngles.z);

        //RaycastHit _hit;
        //Ray ray = new Ray(transform.position, -transform.up);
        //Ray ray1 = new Ray(transform.position, transform.up);
        //Ray ray2 = new Ray(transform.position, -transform.forward);
        //Ray ray3 = new Ray(transform.position, transform.forward);
        //Ray ray4 = new Ray(transform.position, -transform.right);
        //Ray ray5 = new Ray(transform.position, transform.right);

        //if (Physics.Raycast(ray, out _hit, 0.1f))
        //{
        //    isb = false;
        //    transform.forward = _hit.point;
        //    Debug.Log(_hit.transform.name);
        //}
        //if (Physics.Raycast(ray1, out _hit, 0.5f))
        //{
        //    // 如果射线与平面碰撞，打印碰撞物体信息  
        //    // 在场景视图中绘制射线  
        //}
        //if (Physics.Raycast(ray2, out _hit, 0.5f))
        //{
        //    // 如果射线与平面碰撞，打印碰撞物体信息  
        //    // 在场景视图中绘制射线  
        //}
        //if (Physics.Raycast(ray3, out _hit, 0.5f))
        //{

        //}
        //if (Physics.Raycast(ray4, out _hit, 0.5f))
        //{
        //    // 如果射线与平面碰撞，打印碰撞物体信息  
        //    // 在场景视图中绘制射线  
        //}
        //if (Physics.Raycast(ray5, out _hit, 0.5f))
        //{
        //    // 如果射线与平面碰撞，打印碰撞物体信息  
        //    // 在场景视图中绘制射线  
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        isb = false;
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
    }
    private void OnCollisionExit(Collision collision)
    {
        isb = true;
    }
}