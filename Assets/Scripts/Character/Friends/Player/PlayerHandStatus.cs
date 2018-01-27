using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum PlayerHandItem
    {
        Empty,
        Tools,
        Varia,
        Door,
        Paper,
    }

    public enum PlayerHandedness
    {
        Left,
        Right,
    }

public class PlayerHandStatus : MonoBehaviour {
    public PlayerHandedness playerHandedness;
    public PlayerHandItem playerHandItem;
    public PlayerHandAnimation playerHandAnimation;
    public void SetHandState(PlayerHandItem state)
    {
        if (playerHandItem == state)
        {
            return;
        }
        if (state == PlayerHandItem.Paper)
        {
            playerHandAnimation.SetHandState(HandState.Anniu);
        }
        playerHandItem = state;
    }
}
