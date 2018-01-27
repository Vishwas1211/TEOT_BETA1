//
//  PlayerStatus.cs
//  TEOT_ONLINE
//
//  Created by 孙磊 on 8/7/2017 11:27 AM.
//
//
using UnityEngine;
using System.Collections;


public enum PlayerState
{
    none,
    move,
    stop,
    jump,
    climb,
    drop,
    dead,
    idle,
}

public class PlayerStatus : MonoBehaviour
{
    public static bool isMove = true;
    public static bool isLeftMenu = false;
    public static bool isRightMenu = false;
    public static bool isShowMenu = false;
    public static bool isCanMove = true;
    public static bool isTimeStart = false;
    public static bool isLeftHandFollow = true;
    public static bool isRightHandFollow = true;
    public static bool isJump = false;
    public static bool isClimb = false;
    public static bool isDRO = false;
    public PlayerState playerState = PlayerState.move;
    public PlayerHandItem payerHandItem;

    private void Update()
    {
        if (IsEqualPlayerState(PlayerState.climb) && !PlayerManager.Instance.leftHandController.GetComponent<PlayerHandController>().isG && !PlayerManager.Instance.rightHandController.GetComponent<PlayerHandController>().isG)
        {
            SetPlayerState(PlayerState.drop);
        }

        if (IsEqualPlayerState(PlayerState.drop))
        {
            PlayerManager.Instance.GetComponent<Rigidbody>().useGravity = true;
        }
        else
        {
            PlayerManager.Instance.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    public void SetPlayerState(PlayerState state)
    {
        if (state == playerState || playerState == PlayerState.jump) return;
        playerState = state;
    }

    public void SetPlayerStateStop()
    {
        playerState = PlayerState.stop;
    }

    public bool IsEqualPlayerState(PlayerState state)
    {
        return playerState == state ? true : false;
    }
}

