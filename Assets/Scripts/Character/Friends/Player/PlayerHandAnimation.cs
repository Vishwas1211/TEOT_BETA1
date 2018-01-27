//
//  PlayerHandAnimation.cs
//  TEOT_ONLINE
//
//  Created by 孙磊 on 8/12/2017 1:29 PM.
//
//

using UnityEngine;
using System.Collections;

public enum HandState
{
    Hand,
    Gun,
    Grip,
    Button,
    Anniu,
}

public class PlayerHandAnimation : MonoBehaviour
{
    public static bool[] playerHandState = new bool[3];
    public Animator handAnimator;
    HandState handState = new HandState();
    private void Start()
    {
        SetHandState(HandState.Hand);

    }

    public HandState GetHandState()
    {
        return handState;
    }

    public void SetHandState(int id)
    {
        for (int i = 0; i < playerHandState.Length; i++)
        {
            playerHandState[i] = false;
        }
        playerHandState[id] = true;
    }

    public void SetHandState(HandState state)
    {
        if (handState == state)
        {
            return;
        }
        handState = state;
        ReleasedHandAnimation();
    }

    public void TriggerPressed()
    {
        handAnimator.Play("grip");
    }

    public void TriggerReleased()
    {
        handAnimator.Play("daiji");
    }

    public void Gun()
    {
        handAnimator.Play("gun");
    }

    public void GunTrigger()
    {
        handAnimator.Play("gunTrigger");
    }

    public void HandButton()
    {
        handAnimator.Play("press");
    }

    public void HandAnniu()
    {
        handAnimator.Play("anniu");
    }

    public void PlayHandAnimation()
    {
        switch (handState)
        {
            case HandState.Hand:
                TriggerPressed();
                break;
            case HandState.Gun:
                GunTrigger();
                break;
            case HandState.Grip:
                TriggerPressed();
                break;
            case HandState.Button:
                HandButton();
                break;
            case HandState.Anniu:
                HandAnniu();
                break;
            default:
                break;
        }
    }

    public void ReleasedHandAnimation()
    {
        switch (handState)
        {
            case HandState.Hand:
            case HandState.Button:
                TriggerReleased();
                break;
            case HandState.Gun:
                Gun();
                break;
            case HandState.Grip:
                TriggerPressed();

                break;
            default:
                break;
        }
    }
}