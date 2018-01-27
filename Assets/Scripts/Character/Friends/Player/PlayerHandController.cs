//
//  PlayerHandController.cs
//  TEOT_ONLINE
//
//  Created by 孙磊 on 8/12/2017 1:22 PM.
//
//



using UnityEngine;
using System.Collections;
using VRTK;

public class PlayerHandController : MonoBehaviour
{
    private VRTK_ControllerEvents _events;
    private PlayerHandAnimation _playerHandAnimation;
    public PlayerToolsBase playerToolsBase;
    public PlayerHandStatus playerHandStatus;
    public static bool isTrigger = false;
    public static bool isTouchpad = false;
    public static bool isGrip = false;
    public static float front = 0;
    public static float up = 0;
    public static float left = 0;
    public static bool l;
    public static bool r;
    public bool isG = false;
    public GameObject dro_left;
    public GameObject dro_right;
    private GameObject _eye;
    private GameObject _player;
    public void Init()
    {
        _eye = PlayerManager.Instance.eye;
        _player = PlayerManager.Instance.gameObject;

        if (_events == null)
        {
            _events = GetComponent<VRTK_ControllerEvents>();
            _events.TriggerPressed += new ControllerInteractionEventHandler(DoTriggerPressed);
            _events.TriggerReleased += new ControllerInteractionEventHandler(DoTriggerReleased);
            _events.TouchpadAxisChanged += new ControllerInteractionEventHandler(DoTouchpadAxisChanged);
            _events.TouchpadPressed += new ControllerInteractionEventHandler(DoTouchpadPressed);
            _events.TouchpadReleased += new ControllerInteractionEventHandler(DoTouchpadReleased);
            _events.GripPressed += new ControllerInteractionEventHandler(DoGripPressed);
            _events.GripReleased += new ControllerInteractionEventHandler(DoGripReleased);
            _events.ButtonTwoPressed += new ControllerInteractionEventHandler(DoMenuPressed);
        }
        //BackpackController.ChangeTools += new BackpackControllerEventHandler(SetPlayerToolsBase);
        _playerHandAnimation = transform.GetComponent<PlayerHandAnimation>();
    }

    void SetPlayerToolsBase(PlayerToolsBase p)
    {
        playerToolsBase = p;
    }

    private void OnDisable()
    {
        if (_events == null)
        {
            return;
        }
        _events.TriggerPressed -= new ControllerInteractionEventHandler(DoTriggerPressed);
        _events.TriggerReleased -= new ControllerInteractionEventHandler(DoTriggerReleased);
        _events.TouchpadAxisChanged -= new ControllerInteractionEventHandler(DoTouchpadAxisChanged);
        _events.TouchpadPressed -= new ControllerInteractionEventHandler(DoTouchpadPressed);
        _events.TouchpadReleased -= new ControllerInteractionEventHandler(DoTouchpadReleased);
        _events.GripPressed -= new ControllerInteractionEventHandler(DoGripPressed);
        _events.GripReleased -= new ControllerInteractionEventHandler(DoGripReleased);
    }

    //扣动扳机
    private void DoTriggerPressed(object sender, ControllerInteractionEventArgs e)
    {
        if (playerToolsBase != null)
        {
            playerHandStatus.playerHandItem = PlayerHandItem.Tools;
            playerToolsBase.UseTheTools();
        }
        else
        {
            playerHandStatus.playerHandItem = PlayerHandItem.Empty;
            _playerHandAnimation.SetHandState(HandState.Button);
        }
        _playerHandAnimation.PlayHandAnimation();
        isTrigger = true;
    }

    //松开扳机
    private void DoTriggerReleased(object sender, ControllerInteractionEventArgs e)
    {
        if (playerToolsBase != null)
        {
            playerToolsBase.ReTheTools();
        }
        _playerHandAnimation.ReleasedHandAnimation();
        isTrigger = false;
        if (_events.name.Contains("Controller (left)"))
        {
            PlayerStatus.isLeftMenu = !PlayerStatus.isLeftMenu;
        }
        else
        {
            PlayerStatus.isRightMenu = !PlayerStatus.isRightMenu;
        }
        if (!PlayerStatus.isLeftMenu && !PlayerStatus.isRightMenu)
        {
            PlayerStatus.isShowMenu = false;
        }
        else
        {
            PlayerStatus.isShowMenu = true;
        }
    }

    //按下触摸面板
    private void DoTouchpadPressed(object sender, ControllerInteractionEventArgs e)
    {
        if (PlayerStatus.isShowMenu)
        {
            return;
        }
        if (e.touchpadAngle < 45 || e.touchpadAngle > 315)
        {
            if (PlayerStatus.isDRO)
            {
                if (_events.name.Contains("Controller (left)"))
                {
                    //上升
                    DROManager.Instance.Lunch(true, e.touchpadAxis.y);
                    l = true;
                }
                if (_events.name.Contains("Controller (right)"))
                {
                    //前进
                    DROManager.Instance.MoveForward(e.touchpadAxis.y);
                    r = true;
                }
            }
            else
            {
                //PlayerMove.f = true;
                //PlayerManager.Instance.playerMove.UseDodge(0);
            }
        }
        if (e.touchpadAngle > 45 && e.touchpadAngle < 135)
        {
            if (PlayerStatus.isDRO)
            {
                if (_events.name.Contains("Controller (left)"))
                {
                    //顺时针
                    DROManager.Instance.SetRight(e.touchpadAxis.x);
                }
                if (_events.name.Contains("Controller (right)"))
                {
                    //向右
                    DROManager.Instance.TurnRight(e.touchpadAxis.x);
                }
            }
            else
            {
                PlayerMove.r = true;
                //PlayerManager.Instance.playerMove.UseDodge(1);
            }
        }
        if (e.touchpadAngle > 135 && e.touchpadAngle < 225)
        {
            if (PlayerStatus.isDRO)
            {
                if (_events.name.Contains("Controller (left)"))
                {
                    //下降
                    DROManager.Instance.Decline(true, Mathf.Abs(e.touchpadAxis.y) <= 0.2f ? 0 : Mathf.Abs(e.touchpadAxis.y));
                }
                if (_events.name.Contains("Controller (right)"))
                {
                    //后退
                    DROManager.Instance.MoveBack(e.touchpadAxis.y);
                }
            }
            else
            {
                PlayerMove.b = true;
                //PlayerManager.Instance.playerMove.UseDodge(2);
            }
        }
        if (e.touchpadAngle > 225 && e.touchpadAngle < 315)
        {
            if (PlayerStatus.isDRO)
            {
                if (_events.name.Contains("Controller (left)"))
                {
                    //逆时针
                    DROManager.Instance.SetLeft(e.touchpadAxis.x);
                }
                if (_events.name.Contains("Controller (right)"))
                {
                    //向左
                    DROManager.Instance.TurnLeft(e.touchpadAxis.x);
                }
            }
            else
            {
                PlayerMove.l = true;
                //PlayerManager.Instance.playerMove.UseDodge(3);
            }
        }
        isTouchpad = true;
    }
    //松开触摸面板
    private void DoTouchpadReleased(object sender, ControllerInteractionEventArgs e)
    {
        PlayerMove.l = false;
        PlayerMove.r = false;
        PlayerMove.b = false;
        PlayerMove.f = false;
        if (PlayerStatus.isDRO)
        {
            if (_events.name.Contains("Controller (left)"))
            {
                DROManager.Instance.Lunch(false, e.touchpadAxis.y);
            }
            if (_events.name.Contains("Controller (right)"))
            {
                DROManager.Instance.SetIdle();
            }
        }
        isTouchpad = false;
    }
    //按下握柄键
    private void DoGripPressed(object sender, ControllerInteractionEventArgs e)
    {
        if (_playerHandAnimation.GetHandState() == HandState.Hand || _playerHandAnimation.GetHandState() == HandState.Anniu)
        {
            _playerHandAnimation.PlayHandAnimation();
            //if (transform.GetComponent<Collider>().isTrigger)
            //{
            //    transform.GetComponent<Collider>().isTrigger = true;
            //}
        }
        //_playerHandAnimation.SetHandState(HandState.Hand);
        isGrip = true;
        isG = true;
    }
    //松开握柄键
    private void DoGripReleased(object sender, ControllerInteractionEventArgs e)
    {
        if (_playerHandAnimation.GetHandState() == HandState.Hand || _playerHandAnimation.GetHandState() == HandState.Anniu)
        {
            _playerHandAnimation.ReleasedHandAnimation();
        }
        StartCoroutine(qq());
        //playerToolsBase.rrrr();
        isGrip = false;
        isG = false;
        //		var device = SteamVR_Controller.Input ((int)this.GetComponent<SteamVR_TrackedObject> ().index);
        //		//触发手柄震动
        //		device.TriggerHapticPulse (2500);
        //		currentCatch.GetComponent<Rigidbody> ().velocity = device.velocity*5;
        //		currentCatch.GetComponent<Rigidbody> ().angularVelocity = device.angularVelocity;
        //		Destroy (currentCatch.GetComponent<FixedJoint>());
        //		currentCatch = null;
    }

    private void DoMenuPressed(object sender, ControllerInteractionEventArgs e)
    {
        if (_events.name.Contains("Controller (left)"))
        {
            PlayerStatus.isLeftMenu = !PlayerStatus.isLeftMenu;
        }
        else
        {
            PlayerStatus.isRightMenu = !PlayerStatus.isRightMenu;
        }
        if (!PlayerStatus.isLeftMenu && !PlayerStatus.isRightMenu)
        {
            PlayerStatus.isShowMenu = false;
        }
        else
        {
            PlayerStatus.isShowMenu = true;
        }
    }

    private void DoTouchpadAxisChanged(object sender, ControllerInteractionEventArgs e)
    {
        if (PlayerStatus.isDRO)
        {
            if (_events.name.Contains("Controller (left)"))
            {
                dro_left.transform.rotation = Quaternion.Euler(e.touchpadAxis.y * 45, 0, -e.touchpadAxis.x * 45);
            }
            if (_events.name.Contains("Controller (right)"))
            {
                dro_right.transform.rotation = Quaternion.Euler(e.touchpadAxis.y * 45, 0, -e.touchpadAxis.x * 45);
            }
        }
        if (e.touchpadAngle < 45 || e.touchpadAngle > 315)
        {
            if (PlayerStatus.isDRO)
            {
                if (_events.name.Contains("Controller (left)"))
                {
                    //上升
                    Debug.Log("y>0");
                    DROManager.Instance.Lunch(true, Mathf.Abs(e.touchpadAxis.y) <= 0.2f ? 0 : Mathf.Abs(e.touchpadAxis.y));
                    l = true;
                }
                if (_events.name.Contains("Controller (right)"))
                {
                    //前进
                    Debug.Log("y>0");
                    DROManager.Instance.MoveForward(Mathf.Abs(e.touchpadAxis.y) <= 0.2f ? 0 : Mathf.Abs(e.touchpadAxis.y));
                    r = true;
                }
            }
        }

        if (e.touchpadAngle > 45 && e.touchpadAngle < 135)
        {
            if (PlayerStatus.isDRO)
            {
                if (_events.name.Contains("Controller (left)"))
                {
                    //顺时针
                    Debug.Log("x>0");
                    DROManager.Instance.SetRight(Mathf.Abs(e.touchpadAxis.x) <= 0.2f ? 0 : Mathf.Abs(e.touchpadAxis.x));
                }
                if (_events.name.Contains("Controller (right)"))
                {
                    //向右
                    DROManager.Instance.TurnRight(Mathf.Abs(e.touchpadAxis.x) <= 0.2f ? 0 : Mathf.Abs(e.touchpadAxis.x));
                }
            }
            else
            {
                //PlayerManager.Instance.playerMove.UseDodge(1);
            }
        }

        if (e.touchpadAngle > 135 && e.touchpadAngle < 225)
        {
            if (PlayerStatus.isDRO)
            {
                if (_events.name.Contains("Controller (left)"))
                {
                    //下降
                    Debug.Log("y<0");
                    DROManager.Instance.Decline(true, Mathf.Abs(e.touchpadAxis.y) <= 0.2f ? 0 : Mathf.Abs(e.touchpadAxis.y));

                }
                if (_events.name.Contains("Controller (right)"))
                {
                    //后退
                    DROManager.Instance.MoveBack(Mathf.Abs(e.touchpadAxis.y) <= 0.2f ? 0 : Mathf.Abs(e.touchpadAxis.y));
                }
            }
            else
            {
                //PlayerManager.Instance.playerMove.UseDodge(2);
            }
        }

        if (e.touchpadAngle > 225 && e.touchpadAngle < 315)
        {
            if (PlayerStatus.isDRO)
            {
                if (_events.name.Contains("Controller (left)"))
                {
                    //逆时针
                    Debug.Log("x<0");
                    DROManager.Instance.SetLeft(Mathf.Abs(e.touchpadAxis.x) <= 0.2f ? 0 : Mathf.Abs(e.touchpadAxis.x));
                }
                if (_events.name.Contains("Controller (right)"))
                {
                    //向左
                    DROManager.Instance.TurnLeft(Mathf.Abs(e.touchpadAxis.x) <= 0.2f ? 0 : Mathf.Abs(e.touchpadAxis.x));
                }
            }
            else
            {
                //PlayerManager.Instance.playerMove.UseDodge(3);
            }
        }
    }



    IEnumerator qq()
    {
        yield return new WaitForSeconds(0.25f);
        var collider = this.transform.Find("[VRTK][AUTOGEN][Controller][CollidersContainer]/Head").GetComponent<Collider>();
        collider.isTrigger = false;

        playerHandStatus.playerHandItem = PlayerHandItem.Empty;
        this.GetComponent<PlayerHandFollowController>().isFollow = true;
    }
    private void Update()
    {
        if (_events)
        {
            front = _events.transform.localRotation.x;
            up = _events.transform.localRotation.y;
            left = -_events.transform.localRotation.z;
        }
    }
}
