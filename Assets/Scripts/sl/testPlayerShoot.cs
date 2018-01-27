using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayerShoot : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private GameObject fx;
    public GameObject shootPos;
    public float StartDelay = 0;
    public float TimeDelayToReactivate = 0.5f;
    public testButtle buttle;
    public GameObject sod;
    Vector3 lookAtTarget;
    public LayerMask layerMask;
    private bool _isUseIK;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        fx = shootPos.transform.Find("MuzzleFlash7").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetBool("isShoot", true);
                _isUseIK = true;
                InvokeRepeating("Reactivate", StartDelay, TimeDelayToReactivate);
                //sod.transform.localRotation = Quaternion.AngleAxis(20, Vector3.up);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            _isUseIK = false;
            //sod.transform.localRotation = Quaternion.AngleAxis(0, Vector3.up);
            animator.SetBool("isShoot", false);
            fx.SetActive(false);
            CancelInvoke();
        }
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));//射线  

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, layerMask))//发射射线(射线，射线碰撞信息，射线长度，射线会检测的层级)  
        {
            lookAtTarget = hit.point;
        }
    }

    public void CreatFx()
    {
        Resources.Load("");
    }

    void Reactivate()
    {
        buttle.CreatButtle(shootPos.transform.position, shootPos.transform.rotation);
        fx.SetActive(false);
        fx.SetActive(true);
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (animator != null)
        {
            if (_isUseIK)
            {
                animator.SetLookAtWeight(1, 1, 1, 1);
                animator.SetLookAtPosition(lookAtTarget);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.name == "SogBoss_1F Root(Clone)")
        {
            other.transform.root.Find("SogBoss_1F").GetComponent<SogBossController>().OnHurt(10);
        }
        if (other.transform.name == "18SOG (1)")
        {
            other.transform.GetComponent<testL18Boss>().ShouShang(1);
        }
        if (other.transform.name == "SOG")
        {
            other.transform.GetComponent<testZSQ>().ShoShang(1);
        }
        if (other.transform.name.Contains("Jaycee"))
        {
            JayceeManager.Instance.jayceeHumanController.RobBesom();
        }
        if (other.transform.name == "meidikongtiao")
        {
            other.transform.GetComponent<tset>().flash();
        }
        if (other.transform.name == "ZMB_1")
        {
            other.transform.GetComponent<testZMB>().ShoShang(1);
        }
        if (other.transform.root.name == "Crab")
        {
            if (testCrab.isb || !TaskStepManagaer.Instance.IsEqualTaskId(28005))
            {
                return;
            }
            other.transform.root.GetComponent<CrabController>().OnHurt(1);
        }

    }

}
