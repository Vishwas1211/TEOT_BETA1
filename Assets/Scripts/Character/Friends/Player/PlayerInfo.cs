using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void PlayerInfoDelegate();

public class PlayerInfo : MonoBehaviour
{
    public static event PlayerInfoDelegate PlayerDeathEvent;
    public static event PlayerInfoDelegate PlayerAliveEvent;
    public static void DoDeathEvent()
    {
        PlayerDeathEvent();
    }

    public static void DoAlivehEvent()
    {
        PlayerAliveEvent();
    }

    public enum BEHAVIOR_SATE
    {
        ALIVE,
        REVIVE,
        DEATH,
    }
    public BEHAVIOR_SATE _curState;
    [HideInInspector]
    public const float MAX_HP = 100;
    [HideInInspector]
    public float _curHp = MAX_HP;
    private float _time;









    // Use this for initialization
    void Start()
    {
        //UptateHealth();
    }

    // Update is called once per frame
    void Update()
    {     
        if (Input.GetKeyDown(KeyCode.B))
        {
            OnHurt(10);
        }
        UpdateState();
    }



    //private void UptateHealth()
    //{
    //    if (GlobalEvent.updatePlayerHealth != null)
    //    {
    //        GlobalEvent.updatePlayerHealth.Invoke(this._curHp, PlayerInfo.MAX_HP);
    //    }
    //}



    private void UpdateState()
    {
        switch (_curState)
        {
            case BEHAVIOR_SATE.ALIVE:
                break;
            case BEHAVIOR_SATE.REVIVE:
                _time += Time.deltaTime;
                if (_time > 10)
                {
                    Revive();
                }
                break;
            case BEHAVIOR_SATE.DEATH:
                break;
            default:
                break;
        }
    }

    public void SetState(BEHAVIOR_SATE state)
    {
        if (state == _curState) return;
        _curState = state;
        switch (_curState)
        {
            case BEHAVIOR_SATE.ALIVE:
                break;
            case BEHAVIOR_SATE.REVIVE:
                Debug.Log("player is reviveing");
                break;
            case BEHAVIOR_SATE.DEATH:
                Debug.Log("player is death");
                PlayerManager.Instance.animator.enabled = false;
                //PlayerManager.Instance.motionController.IsEnabled = false;
                GameObject.Find("ThirdPersonController").GetComponent<ThirdPersonCharacter_WSM>().canMove = false;
                SetState(BEHAVIOR_SATE.REVIVE);
                PlayerDeathEvent();
                break;
            default:
                break;
        }
    }

    public void OnHurt(int i)
    {
        if (_curState == BEHAVIOR_SATE.DEATH) return;
        _curHp -= i;
        if (_curHp <= 0)
        {
            SetState(BEHAVIOR_SATE.DEATH);
        }
        //UptateHealth();
    }

    public void Revive()
    {
        _time = 0;
        _curHp = MAX_HP;
        PlayerManager.Instance.animator.enabled = true;
        //PlayerManager.Instance.motionController.IsEnabled = true;
        GameObject.Find("ThirdPersonController").GetComponent<ThirdPersonCharacter_WSM>().canMove = true;
        Debug.Log("player is alive");
        SetState(BEHAVIOR_SATE.ALIVE);
        DoAlivehEvent();
    }


}
