//
//  QWE.cs
//  TEOT_ONLINE
//
//  Created by 孙磊 on 8/12/2017 3:43 PM.
//
//

using UnityEngine;
using System.Collections;

public class PlayerTriggerDetection : MonoBehaviour
{
    private LayerMask layerMask = 0 << 8 | 1 << 9;
    private void Update()
    {
        RaycastHit _hit;
        Ray ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(ray, out _hit, 100, layerMask))
        {
            PlayerManager.Instance.playerMove.SetPlayerHeight(_hit.point.y);
            Debug.DrawLine(ray.origin, _hit.point, Color.red);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name.Contains("Cube (5)"))
        {
            Time.timeScale = 0.1f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name.Contains("Cube (5)"))
        {
            Time.timeScale = 1f;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.name.Contains("Cube (5)"))
    //    {
    //        Time.timeScale = 0.1f;
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.transform.name.Contains("Cube (5)"))
    //    {
    //        Time.timeScale = 1f;
    //    }
    //}
}