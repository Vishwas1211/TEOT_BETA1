//
//  Scanner.cs
//  TEOT_ONLINE
//
//  Created by 孙磊 on 8/16/2017 7:42 PM.
//
//

using UnityEngine;
using System.Collections;

public class Scanner : PlayerToolsBase
{
    private LayerMask ingoreEnemyLayer;
    private GameObject shootPos;
    public GameObject rayLine;

    public override void UseTheTools()
    {
        base.UseTheTools();
        Fire();
    }

    public override void ReTheTools()
    {
        base.ReTheTools();
        ReFier();
    }

    private void Start()
    {
        shootPos = transform.Find("ShootPos").gameObject;
        rayLine = transform.Find("bar").gameObject;
    }

    private void Update()
    {
        if (rayLine.activeSelf)
        {
            RaycastHit hit;
            if (Physics.Raycast(shootPos.transform.position, shootPos.transform.forward, out hit, 100))
            {
                rayLine.transform.localScale = new Vector3(rayLine.transform.localScale.x, rayLine.transform.localScale.y, hit.distance);
            }
        }
    }

    private void Fire()
    {
        rayLine.SetActive(true);

    }
    private void ReFier()
    {
        rayLine.SetActive(false);
    }
}