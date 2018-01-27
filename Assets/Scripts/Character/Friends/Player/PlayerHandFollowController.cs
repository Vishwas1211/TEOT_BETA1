//
//  PlayerHandFollowController.cs
//  TEOT_ONLINE
//
//  Created by 孙磊 on 8/12/2017 7:56 PM.
//
//

using UnityEngine;
using System.Collections;

public class PlayerHandFollowController : MonoBehaviour
{
    public bool isFollow = true;
    public GameObject hand;
    private Transform _pos;
    private PlayerHandItem _payerHandItem;
    public PlayerHandStatus playerHandStatus;

    private void Update()
    {
        if (PlayerHandController.isGrip && playerHandStatus.playerHandItem == PlayerHandItem.Door)
        {
            if (_pos == null) return;

            hand.transform.position = new Vector3(_pos.position.x, _pos.position.y, _pos.position.z);
        }
        else
        {
            if (isFollow)
            {
                hand.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }
        }
        hand.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    public void SetPos(Transform pos)
    {
        _pos = pos;
    }
}