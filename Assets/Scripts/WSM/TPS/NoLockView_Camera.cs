using UnityEngine;
using System.Collections;
using UnityEditor;
using DG.Tweening;

public class NoLockView_Camera : MonoBehaviour
{
    public float z;
    //观察目标
    public Transform Target;
    //观察距离
    public float Distance = 5F;
    //旋转速度
    private float SpeedX = 240;
    private float SpeedY = 120;
    //角度限制
    private float MinLimitY = -5;
    private float MaxLimitY = 180;

    //旋转角度
    private float mX = 0.0F;
    private float mY = 0.0F;

    //鼠标缩放距离最值
    private float MaxDistance = 10;
    private float MinDistance = 1.5F;
    //鼠标缩放速率
    private float ZoomSpeed = 2F;

    //是否启用差值
    public bool isNeedDamping = true;
    //速度
    public float Damping = 2.5F;

    public GameObject go;

    void Start()
    {
        //初始化旋转角度
        mX = transform.eulerAngles.x;
        mY = transform.eulerAngles.y;
    }

    //void LateUpdate()
    //{
    //    //鼠标右键旋转
    //    if (Target != null && Input.GetMouseButton(1))
    //    {
    //        //获取鼠标输入
    //        mX += Input.GetAxis("Mouse X") * SpeedX * 0.02F;
    //        mY -= Input.GetAxis("Mouse Y") * SpeedY * 0.02F;
    //        //范围限制
    //        mY = ClampAngle(mY, MinLimitY, MaxLimitY);
    //    }

    //    //鼠标滚轮缩放

    //    Distance -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
    //    Distance = Mathf.Clamp(Distance, MinDistance, MaxDistance);

    //    //重新计算位置和角度
    //    Quaternion mRotation = Quaternion.Euler(mY, mX, 0);
    //    Vector3 mPosition = mRotation * new Vector3(0.0F, 0.0F, -Distance) + Target.position;

    //    //设置相机的角度和位置
    //    if (isNeedDamping)
    //    {
    //        //球形插值
    //        transform.rotation = Quaternion.Lerp(transform.rotation, mRotation, Time.deltaTime * Damping);
    //        //线性插值
    //        transform.position = Vector3.Lerp(transform.position, mPosition, Time.deltaTime * Damping);
    //    }
    //    else
    //    {
    //        transform.rotation = mRotation;
    //        transform.position = mPosition;
    //    }
    //    //将玩家转到和相机对应的位置上
    //    //if (Target.GetComponent<NoLockiVew_Player>().State == NoLockiVew_Player.PlayerState.Walk)
    //    //{
    //    //    Target.eulerAngles = new Vector3(0, mX, 0);
    //    //}
    //}

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }


    //[System.Runtime.InteropServices.DllImport("user32.dll")] //引入dll
    //public static extern int SetCursorPos(int x, int y);

    void Update()
    {
        //SetMouseToAnyOfScreenPosition();
        //TargetingRaycast();

        UpdateFrontRay();

    }


    //void SetMouseToAnyOfScreenPosition()
    //{
    //    SetCursorPos(Screen.width / 2, Screen.height / 2);//放在update中，每帧调用，强制设置坐标
    //    Cursor.visible = false;//隐藏鼠标
    //}



    Ray ray;
    private bool can_1_6 = false;
    public static bool can_1_7 = false;
    public static bool can_4_1 = false;
    public static bool can_4_4;
    public static bool can_4_7;
    public static bool can_4_8;
    public static bool can_4_9;
    public static bool can_13_1;
    public static bool can_18_4;
    public static bool can_18_5;
    public static bool can_21_2;
    public static bool can_21_3;
    public static bool can_21_4;
    public static bool can_21_5;
    public static bool can_21_6;
    public static bool is_21_5_0;
    public static bool is_21_5_1;
    public static bool is_21_5_2;
    public static bool is_21_5_3;
    public LayerMask rayLayer = 0 << 8;

    public void UpdateFrontRay()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (TaskStepManagaer.Instance.IsEqualTaskId(28005))
            {
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                go.transform.position = GameObject.Find("Cube1").transform.position;
                go.transform.localScale = new Vector3(2, 2, 2);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));//射线
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 100f, rayLayer))
            {
                //if (PlayerPickupDetection.isb)
                PickUp(hitInfo.transform);

                {
                    if (hitInfo.transform.name == "muyuanzhuoL1")
                    {
                        hitInfo.transform.position = new Vector3(7.157f, 1.054068f, 19f);
                    }
                    if (hitInfo.transform.name == "F4_1")
                    {
                        Level_04_Manager.Instance.HaveKey = true;
                        DoorManager.Instance.level_04_Door_Script[17].CanOpen = true;
                        Destroy(hitInfo.transform.gameObject);
                    }
                    if (hitInfo.transform.name == "F4_7")
                    {
                        can_4_7 = true;
                        DoorManager.Instance.level_04_Door_Script[7].CanOpen = true;
                        Destroy(hitInfo.transform.gameObject);
                    }
                    Debug.DrawRay(ray.origin, hitInfo.point, Color.yellow);
                    if (hitInfo.transform == null)
                    {
                        Destroy(this.gameObject);
                        return;
                    }
                    if (hitInfo.transform.name == "knapsack")
                    {
                        GameObject.Destroy(hitInfo.transform.gameObject);
                    }
                    if (hitInfo.transform.name == "Health_Box")
                    {
                        GameObject.Destroy(hitInfo.transform.gameObject);
                    }
                    if (hitInfo.transform.name == "Wood3")
                    {
                        hitInfo.transform.GetComponent<ChangeLayerToGroup>().ChangePos();
                    }
                    if (hitInfo.transform.name == "Grenade")
                    {
                        GameObject.Destroy(hitInfo.transform.gameObject);
                    }
                    if (hitInfo.transform.name == "StonePillars")
                    {
                        hitInfo.transform.GetComponent<test17>().qwe();
                    }
                    if (hitInfo.transform.name == "SY1_qiang4" && (TaskStepManagaer.Instance.IsEqualTaskId(3003) || TaskStepManagaer.Instance.IsEqualTaskId(3006)))
                    {
                        if (can_1_6)
                        {
                            hitInfo.transform.GetComponent<OpenL1Door>().OpenTheDoor();
                        }
                        else
                        {
                            hitInfo.transform.GetComponent<ClickNextTask>().NextTask();
                        }
                    }

                    if (hitInfo.transform.name == "bijiben21" && TaskStepManagaer.Instance.IsEqualTaskId(3004))
                    {
                        hitInfo.transform.GetComponent<test5>().qwe();
                    }
                    if (hitInfo.transform.name == "bijiben2")
                    {
                        hitInfo.transform.GetComponent<ClickNextTask>().NextTask();
                    }
                    if (hitInfo.transform.name == "DianTi_B_KaiGuan_B" && TaskStepManagaer.Instance.IsEqualTaskId(19006))
                    {
                        hitInfo.transform.GetComponent<testRiddle>().ShowRiddle();
                    }
                    if (hitInfo.transform.name == "panel" && TaskStepManagaer.Instance.IsEqualTaskId(3005))
                    {
                        hitInfo.transform.GetComponent<test5>().qwe();
                    }
                    if (hitInfo.transform.name == "key")
                    {
                        can_1_6 = true;
                        Destroy(hitInfo.transform.gameObject);
                    }
                    if (hitInfo.transform.name == "L13key(Clone)")
                    {
                        can_13_1 = true;
                        Destroy(hitInfo.transform.gameObject);
                    }
                    if (hitInfo.transform.name == "L18key(Clone)")
                    {
                        can_18_5 = true;
                        Destroy(GameObject.Find("18wall"));
                        Destroy(hitInfo.transform.gameObject);
                    }
                    if (hitInfo.transform.name == "L18key (1)")
                    {
                        can_18_4 = true;
                        Destroy(hitInfo.transform.gameObject);
                    }
                    if (hitInfo.transform.parent)
                    {
                        if (hitInfo.transform.parent.name == "hongseguizi2 (18)" && TaskStepManagaer.Instance.IsEqualTaskId(25001))
                        {
                            hitInfo.transform.parent.GetComponent<testGuiZiMove>().Move();
                        }
                        if (hitInfo.transform.parent.name == "TongXunTa" && can_21_6 && TaskStepManagaer.Instance.IsEqualTaskId(28007))
                        {
                            can_21_6 = false;
                            hitInfo.transform.parent.gameObject.SetActive(false);
                            UtilFunction.ResourceLoadOnPosition("Effects/BrokenFx/Scene_Model/Object/Scene2/TongXunTa_FX", hitInfo.transform.parent.position, hitInfo.transform.parent.rotation);
                            TaskStepManagaer.Instance.FinishCurTaskImmediately();
                        }
                    }
                    if (hitInfo.transform.name == "Bashou7" && TaskStepManagaer.Instance.IsEqualTaskId(28003))
                    {
                        hitInfo.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y + 0.1f, hitInfo.transform.position.z);
                        hitInfo.transform.parent.parent.parent.Find("KongZhiTai3").DOMoveY(-0.4f, 3);
                        TaskStepManagaer.Instance.FinishCurTaskImmediately();
                    }
                    if (hitInfo.transform.name == "Dial6" && TaskStepManagaer.Instance.IsEqualTaskId(28003))
                    {
                        hitInfo.transform.DORotate(new Vector3(0, -90, 0), 1);
                        hitInfo.transform.parent.parent.parent.Find("KongZhiTai3").DOLocalMoveY(0f, 5);
                        TaskStepManagaer.Instance.FinishCurTaskImmediately();
                    }


                    if (hitInfo.transform.name == "DianNao_71")
                    {
                        hitInfo.transform.GetComponent<testPWD>().QWE();
                    }
                    if (hitInfo.transform.name == "panel (1)" && TaskStepManagaer.Instance.IsEqualTaskId(4003))
                    {
                        hitInfo.transform.GetComponent<test5>().qwe();
                    }
                    if (hitInfo.transform.name == "DT_KaiGuan 8" && TaskStepManagaer.Instance.IsEqualTaskId(4005))
                    {
                        hitInfo.transform.GetComponent<ClickNextTask>().NextTask();
                    }
                    if (hitInfo.transform.name == "SY_3_JiSuanQi")
                    {
                        GameObject.Find("KongZhiTai/KongZhiTai_2").transform.DOLocalMoveY(0f, 5);
                        PlaySoundController.Instance.PlaySoundEffect(hitInfo.transform.gameObject, 10001);
                    }
                    if (hitInfo.transform.name == "Pao_Gun")
                    {
                        can_21_6 = true;
                        Destroy(hitInfo.transform.gameObject);
                    }
                    if (hitInfo.transform.name == "WarPlatform_D" && can_21_6 && TaskStepManagaer.Instance.IsEqualTaskId(28008))
                    {
                        can_21_6 = false;
                        UtilFunction.ResourceLoad("Effects/BrokenFx/WarPlatform_D_Collision");
                        Destroy(hitInfo.transform.gameObject);
                        TaskStepManagaer.Instance.FinishCurTaskImmediately();
                    }

                    if (hitInfo.transform.name == "bijiben28")
                    {
                        PlaySoundController.Instance.PlaySoundEffect(hitInfo.transform.gameObject, 10001);
                        hitInfo.transform.GetComponent<test5>().qwe();
                    }

                    if (hitInfo.transform.name.Contains("ElectricAntenna3"))
                    {
                        if (!hitInfo.transform.GetComponent<testElectricute>().isb)
                        {
                            hitInfo.transform.gameObject.SetActive(false);
                            testElectricute.qwe();
                        }
                    }
                    //if (TaskStepManagaer.Instance.IsEqualTaskId(28002) && is_21_5_0 && is_21_5_1 && is_21_5_2 && is_21_5_3 && can_21_4)
                    {
                        if (hitInfo.transform.name == "KongZhiTai_Button_1" && can_21_4)
                        {
                            hitInfo.transform.GetComponent<testBlue>().qwe();
                            GameObject.Find("curve_sin_back").GetComponent<CurveSin123>().setI(1);
                        }
                        if (hitInfo.transform.name == "KongZhiTai_Button_2" && can_21_4)
                        {
                            hitInfo.transform.GetComponent<testBlue>().qwe();
                            GameObject.Find("curve_sin_back").GetComponent<CurveSin123>().setI(2);
                        }
                        if (hitInfo.transform.name == "KongZhiTai_Button_3" && can_21_4)
                        {
                            hitInfo.transform.GetComponent<testBlue>().qwe();
                            GameObject.Find("curve_sin_back").GetComponent<CurveSin123>().setI(0);
                        }
                    }
                    if (hitInfo.transform.name == "KongZhiTai" && !can_21_2)
                    {
                        can_21_2 = true;
                        TaskStepManagaer.Instance.FinishCurTaskImmediately();
                    }
                    if (hitInfo.transform.name == "EX_4F_Door_Big_A1")
                    {
                        if (can_4_1)
                        {
                            hitInfo.transform.GetComponent<OpenDoor>().OpenTheSlidingDoor();
                        }
                        //hitInfo.transform.GetComponent<test16>().qwe();
                    }
                    if (hitInfo.transform.name == "ElectricAntenna5")
                    {
                        if (testElectricute.ist && !is_21_5_0)
                        {
                            is_21_5_0 = true;
                            testElectricute.ewq();
                            GameObject go1 = UtilFunction.ResourceLoadOnPosition("Prefabs/Tools/ElectricAntenna3", new Vector3(hitInfo.transform.parent.Find("ElectricAntenna2").position.x, hitInfo.transform.parent.Find("ElectricAntenna2").position.y + 2, hitInfo.transform.parent.Find("ElectricAntenna2").position.z), Quaternion.identity);
                            go1.transform.GetComponent<testElectricute>().setB(true);
                        }
                    }
                    if (hitInfo.transform.name == "Detonator")
                    {
                        can_21_3 = true;
                        Destroy(hitInfo.transform.gameObject);
                    }
                    if (hitInfo.transform.name == "ElectricAntenna6")
                    {
                        if (testElectricute.ist && !is_21_5_1)
                        {
                            is_21_5_1 = true;
                            testElectricute.ewq();
                            GameObject go1 = UtilFunction.ResourceLoadOnPosition("Prefabs/Tools/ElectricAntenna3", new Vector3(hitInfo.transform.parent.Find("ElectricAntenna2").position.x, hitInfo.transform.parent.Find("ElectricAntenna2").position.y + 2, hitInfo.transform.parent.Find("ElectricAntenna2").position.z), Quaternion.identity);
                            go1.transform.GetComponent<testElectricute>().setB(true);
                        }
                    }
                    if (hitInfo.transform.name == "ElectricAntenna7")
                    {
                        if (testElectricute.ist && !is_21_5_2)
                        {
                            is_21_5_2 = true;
                            testElectricute.ewq();
                            GameObject go1 = UtilFunction.ResourceLoadOnPosition("Prefabs/Tools/ElectricAntenna3", new Vector3(hitInfo.transform.parent.Find("ElectricAntenna2").position.x, hitInfo.transform.parent.Find("ElectricAntenna2").position.y + 2, hitInfo.transform.parent.Find("ElectricAntenna2").position.z), Quaternion.identity);
                            go1.transform.GetComponent<testElectricute>().setB(true);
                        }
                    }
                    if (hitInfo.transform.name == "ElectricAntenna8")
                    {
                        if (testElectricute.ist && !is_21_5_3)
                        {
                            is_21_5_3 = true;
                            testElectricute.ewq();
                            GameObject go1 = UtilFunction.ResourceLoadOnPosition("Prefabs/Tools/ElectricAntenna3", new Vector3(hitInfo.transform.parent.Find("ElectricAntenna2").position.x, hitInfo.transform.parent.Find("ElectricAntenna2").position.y + 2, hitInfo.transform.parent.Find("ElectricAntenna2").position.z), Quaternion.identity);
                            go1.transform.GetComponent<testElectricute>().setB(true);
                        }
                    }
                    if (hitInfo.transform.name == "Cube12" && !can_21_4 && is_21_5_0 && is_21_5_1 && is_21_5_2 && is_21_5_3)
                    {
                        can_21_4 = true;
                        UtilFunction.ResourceLoadOnPosition("Prefabs/Tools/XinHaoQi", hitInfo.point, Quaternion.identity);
                    }

                    if (hitInfo.transform.tag.Contains("Pickup"))
                    {
                        GameObject.Destroy(hitInfo.transform.gameObject);
                    }

                    if (hitInfo.transform.name.Contains("Notes"))
                    {
                        hitInfo.transform.GetComponent<testLookZiLiao>().qwe();
                    }
                    if (hitInfo.transform.name.Contains("Men_MianBan 1"))
                    {
                        PlaySoundController.Instance.PlaySoundEffect(hitInfo.transform.gameObject, 10001);
                        can_4_1 = true;
                    }
                    if (hitInfo.transform.name == "EX_4F_Door_Big_B1")
                    {
                        hitInfo.transform.GetComponent<OpenDoor>().OpenTheSlidingDoor();
                        //hitInfo.transform.GetComponent<test16>().qwe();
                    }
                    if (hitInfo.transform.name == "ID_Key")
                    {
                        can_4_4 = true;
                        GameObject.Destroy(hitInfo.transform.gameObject);
                    }
                    if (hitInfo.transform.name == "Computer_B_Keyboard_1")
                    {
                        if (TaskStepManagaer.Instance.IsEqualTaskId(19003))
                        {
                            //hitInfo.transform.GetComponent<testL10Pwd>().OpenTV("Power0");
                            Level_10_Manager.Instance.ShowTV();

                        }
                        //if (TaskStepManagaer.Instance.IsEqualTaskId(19004))
                        //{
                        //    TaskStepManagaer.Instance.FinishCurTaskImmediately();
                        //    hitInfo.transform.GetComponent<testL10Pwd>().OpenTV("Power4");
                        //}
                    }
                    if (hitInfo.transform.name == "reshuihu (106)" && TaskStepManagaer.Instance.IsEqualTaskId(19005))
                    {
                        if (!testControllerSog.isDeath)
                        {
                            hitInfo.transform.GetComponent<testChangePlayer>().qwe();
                        }
                    }
                    if (hitInfo.transform.name == "DianTi_B_KeyHole")
                    {
                        hitInfo.transform.GetComponent<testOpenElevator>().OpenElevator();
                    }


                    if (hitInfo.transform.name == "EX_5A_Double_Door_L")
                    {
                        switch (hitInfo.transform.GetComponent<OpenDoor>().lockDoor)
                        {
                            case OpenDoor.Lock.none:
                                {
                                    hitInfo.transform.GetComponent<OpenDoor>().islockDoor = false;
                                    hitInfo.transform.GetComponent<OpenDoor>().OpenTheDoubleDoorLeft();
                                }
                                break;
                            case OpenDoor.Lock.key1:
                                {
                                    if (can_4_4)
                                    {
                                        hitInfo.transform.GetComponent<OpenDoor>().islockDoor = false;
                                        hitInfo.transform.GetComponent<OpenDoor>().OpenTheDoubleDoorLeft();
                                    }
                                }
                                break;
                            case OpenDoor.Lock.key2:
                                {
                                    if (can_4_9)
                                    {
                                        hitInfo.transform.GetComponent<OpenDoor>().islockDoor = false;
                                        hitInfo.transform.GetComponent<OpenDoor>().OpenTheDoubleDoorLeft();
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    if (hitInfo.transform.name == "EX_5A_Double_Door_R")
                    {
                        switch (hitInfo.transform.GetComponent<OpenDoor>().lockDoor)
                        {
                            case OpenDoor.Lock.none:
                                {
                                    hitInfo.transform.GetComponent<OpenDoor>().OpenTheDoubleDoorRight();
                                }
                                break;
                            case OpenDoor.Lock.key1:
                                {
                                    if (can_4_4)
                                    {
                                        hitInfo.transform.GetComponent<OpenDoor>().islockDoor = false;
                                        hitInfo.transform.GetComponent<OpenDoor>().OpenTheDoubleDoorRight();
                                    }
                                }
                                break;
                            case OpenDoor.Lock.key2:
                                {
                                    if (can_4_9)
                                    {
                                        hitInfo.transform.GetComponent<OpenDoor>().islockDoor = false;
                                        hitInfo.transform.GetComponent<OpenDoor>().OpenTheDoubleDoorRight();
                                    }
                                }
                                break;
                            default:
                                break;
                        }

                    }



                    if (hitInfo.transform.name == "EX_5C_18F_Door_13" && TaskStepManagaer.Instance.IsEqualTaskId(20001))
                    {
                        TaskStepManagaer.Instance.FinishTaskTo(20002);
                    }

                    if (hitInfo.transform.name.Contains("EX_5A_Door"))
                    {
                        switch (hitInfo.transform.GetComponent<OpenDoor>().lockDoor)
                        {
                            case OpenDoor.Lock.none:
                                hitInfo.transform.GetComponent<OpenDoor>().OpenTheDoor();
                                break;
                            case OpenDoor.Lock.key1:
                                hitInfo.transform.GetComponent<OpenDoor>().islockDoor = false;
                                hitInfo.transform.GetComponent<OpenDoor>().OpenTheDoor();
                                break;
                            case OpenDoor.Lock.key2:
                                if (can_4_9)
                                {
                                    hitInfo.transform.GetComponent<OpenDoor>().islockDoor = false;
                                    hitInfo.transform.GetComponent<OpenDoor>().OpenTheDoor();
                                }
                                break;
                            case OpenDoor.Lock.key3:
                                {
                                    if (can_4_7)
                                    {
                                        hitInfo.transform.GetComponent<OpenDoor>().islockDoor = false;
                                        hitInfo.transform.GetComponent<OpenDoor>().OpenTheDoor();
                                    }
                                }
                                break;
                            case OpenDoor.Lock.key4:
                                {
                                    if (can_18_4)
                                    {
                                        hitInfo.transform.GetComponent<OpenDoor>().islockDoor = false;
                                        hitInfo.transform.GetComponent<OpenDoor>().OpenTheDoor();
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    if (hitInfo.transform.name.Contains("Standby"))
                    {
                        hitInfo.transform.GetComponent<SymbolNew>().SetIsTarget();
                    }

                }

                //if (hitInfo.transform.root.name == "SogBoss_1F Root(Clone)")
                //{
                //    hitInfo.transform.root.Find("SogBoss_1F").GetComponent<SogBossController>().OnHurt(10);
                //}
                //if (hitInfo.transform.name == "18SOG (1)")
                //{
                //    hitInfo.transform.GetComponent<testL18Boss>().ShouShang(1);
                //}
                //if (hitInfo.transform.name == "SOG")
                //{
                //    hitInfo.transform.GetComponent<testZSQ>().ShoShang(1);
                //}
                //if (hitInfo.transform.name.Contains("Jaycee"))
                //{
                //    JayceeManager.Instance.jayceeHumanController.RobBesom();
                //}
                //if (hitInfo.transform.name == "meidikongtiao")
                //{
                //    hitInfo.transform.GetComponent<tset>().flash();
                //}
                //if (hitInfo.transform.name == "ZMB_1")
                //{
                //    hitInfo.transform.GetComponent<testZMB>().ShoShang(1);
                //}
                //if (hitInfo.transform.root.name == "Crab")
                //{
                //    if (testCrab.isb || !TaskStepManagaer.Instance.IsEqualTaskId(28005))
                //    {
                //        return;
                //    }
                //    hitInfo.transform.root.GetComponent<CrabController>().OnHurt(1);
                //}

                if (hitInfo.transform.name == "Capsule123")
                {
                    if (can_1_7)
                    {
                        PlayerManager.Instance.playerCollider.GetComponent<test12>().UseTheTools1(hitInfo.transform.gameObject);
                    }
                    //hitInfo.transform.GetComponent<test16>().qwe();
                }
            }
        }
    }

    // Which layers targeting ray must hit (-1 = everything)
    public LayerMask targetingLayerMask = -1;

    // Targeting ray length
    private float targetingRayLength = Mathf.Infinity;

    // Camera component reference
    private Camera cam;

    // 
    private string info = @"Left Click - switch flashing for object under mouse cursor
Right Click - switch see-through mode for object under mouse cursor
'1' - fade in/out constant highlighting
'2' - turn on/off constant highlighting immediately
'3' - turn off all types of highlighting immediately
";

    // 
    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    // 
    public void TargetingRaycast()
    {
        // Current target object transform component
        Transform targetTransform = null;

        // If camera component is available
        if (cam != null)
        {
            RaycastHit hitInfo;

            // Targeting raycast
            if (Physics.Raycast(ray, out hitInfo, targetingRayLength, targetingLayerMask.value))
            {
                // Cache what we've hit
                targetTransform = hitInfo.collider.transform;
            }
        }

        // If we've hit an object during raycast
        if (targetTransform != null)
        {
            // And this object has HighlighterController component
            HighlighterController hc = targetTransform.GetComponentInParent<HighlighterController>();
            if (hc != null)
            {
                // Transfer input information to the found HighlighterController
                if (Input.GetButtonDown("Fire1")) { hc.Fire1(); }
                if (Input.GetButtonUp("Fire2")) { hc.Fire2(); }
                hc.MouseOver();
            }
        }
    }

    void PickUp(Transform transform)
    {
        ItemEntity entity = transform.GetComponent<ItemEntity>();
        if (entity == null)
        {
            Debug.Log(transform.gameObject.name + "不包含道具实体  ItemEntity");
            return;
        }
        BagModule.BagManager.Instance.PutInBag(entity.item);
    }
}



