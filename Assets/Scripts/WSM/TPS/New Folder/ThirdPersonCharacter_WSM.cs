using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class ThirdPersonCharacter_WSM : MonoBehaviour
{

    [SerializeField] float m_MovingTurnSpeed = 360;
    [SerializeField] float m_StationaryTurnSpeed = 180;
    [SerializeField] float m_JumpPower = 12f;
    [Range(1f, 4f)] [SerializeField] float m_GravityMultiplier = 2f;
    [SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
    [SerializeField] float m_MoveSpeedMultiplier = 1f;
    [SerializeField] float m_AnimSpeedMultiplier = 1f;
    [SerializeField] float m_GroundCheckDistance = 0.1f;

    Rigidbody m_Rigidbody;
    Animator m_Animator;
    bool m_IsGrounded;
    float m_OrigGroundCheckDistance;
    const float k_Half = 0.5f;
    float m_TurnAmount;
    float m_ForwardAmount;
    Vector3 m_GroundNormal;
    float m_CapsuleHeight;
    Vector3 m_CapsuleCenter;
    CapsuleCollider m_Capsule;
    bool m_Crouching;

    public ThirdPersonUserControl_WSM a;

    public bool Rope_WSM = false;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Capsule = GetComponent<CapsuleCollider>();
        m_CapsuleHeight = m_Capsule.height;
        m_CapsuleCenter = m_Capsule.center;

        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        m_OrigGroundCheckDistance = m_GroundCheckDistance;


        a = GetComponent<ThirdPersonUserControl_WSM>();
    }

    private void Update()
    {
        UpdateGetUP();

        if (Rope_WSM)
        {
            transform.position += new Vector3(0, Input.GetAxis("Vertical") * Time.deltaTime * 2f, 0);

            transform.RotateAround(Level_20_Manager.Instance.Rope_1.transform.position, Vector3.up, Input.GetAxis("Horizontal") * Time.deltaTime * 45f);

            transform.LookAt(new Vector3(Level_20_Manager.Instance.Rope_1.transform.position.x,
                transform.position.y,
                Level_20_Manager.Instance.Rope_1.transform.position.z));

            if (Input.GetKeyDown(KeyCode.F))
            {
                GoDownRope();
                Dui();
            }

        }


    }

    public void Move(Vector3 move, bool crouch, bool jump)
    {
        if (!canMove)
        {

            move = Vector3.zero;
            jump = false;
        }
        // convert the world relative moveInput vector into a local-relative
        // turn amount and forward amount required to head in the desired
        // direction.
        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);
        CheckGroundStatus();
        move = Vector3.ProjectOnPlane(move, m_GroundNormal);
        m_TurnAmount = Mathf.Atan2(move.x, move.z);
        m_ForwardAmount = move.z;

        ApplyExtraTurnRotation();

        // control and velocity handling is different when grounded and airborne:
        if (m_IsGrounded)
        {
            HandleGroundedMovement(crouch, jump);
        }
        else
        {
            HandleAirborneMovement();
        }

        ScaleCapsuleForCrouching(crouch);
        PreventStandingInLowHeadroom();

        // send input and other state parameters to the animator
        UpdateAnimator(move);

        ////跳起来也可以移动  WSM
        //if (!m_IsGrounded)
        //{
        //    Vector3 a = new Vector3(move.x, 0, move.z);
        //    a = transform.TransformDirection(a);
        //    m_Rigidbody.AddForce(a.normalized * 100);
        //}
    }


    void ScaleCapsuleForCrouching(bool crouch)
    {
        if (m_IsGrounded && crouch)
        {
            if (m_Crouching) return;
            m_Capsule.height = m_Capsule.height / 5f;
            m_Capsule.center = m_Capsule.center / 4f;
            m_Crouching = true;
        }
        else
        {
            Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
            float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
            if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength, layerMask, QueryTriggerInteraction.Ignore))
            {
                //m_Crouching = true;
                return;
            }
            m_Capsule.height = m_CapsuleHeight;
            m_Capsule.center = m_CapsuleCenter;
            m_Crouching = false;
        }
    }

    void PreventStandingInLowHeadroom()
    {
        // prevent standing up in crouch-only zones
        if (!m_Crouching)
        {
            Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
            float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
            if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength, layerMask, QueryTriggerInteraction.Ignore))
            {
                m_Crouching = true;
            }
        }
    }


    void UpdateAnimator(Vector3 move)
    {
        // update the animator parameters
        m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
        m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
        m_Animator.SetBool("Crouch", m_Crouching);
        m_Animator.SetBool("OnGround", m_IsGrounded);
        if (!m_IsGrounded)
        {
            m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);
        }

        // calculate which leg is behind, so as to leave that leg trailing in the jump animation
        // (This code is reliant on the specific run cycle offset in our animations,
        // and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
        float runCycle =
            Mathf.Repeat(
                m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
        float jumpLeg = (runCycle < k_Half ? 1 : -1) * m_ForwardAmount;
        //if (m_IsGrounded)
        //{
        //    m_Animator.SetFloat("JumpLeg", jumpLeg);
        //}

        // the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
        // which affects the movement speed because of the root motion.
        if (m_IsGrounded && move.magnitude > 0)
        {
            m_Animator.speed = m_AnimSpeedMultiplier;
        }
        else
        {
            // don't use that while airborne
            m_Animator.speed = 1;
        }
    }


    void HandleAirborneMovement()
    {
        // apply extra gravity from multiplier:
        Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
        m_Rigidbody.AddForce(extraGravityForce);

        m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
    }


    void HandleGroundedMovement(bool crouch, bool jump)
    {
        // check whether conditions are right to allow a jump:
        if (jump && !crouch && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
        {
            m_Rigidbody.AddForce(Vector3.up * 100f);
            // jump!
            m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower, m_Rigidbody.velocity.z);
            m_IsGrounded = false;
            m_Animator.applyRootMotion = false;
            m_GroundCheckDistance = 0.1f;
        }
        else if (jump)  //WSM
        {
            m_Rigidbody.AddForce(Vector3.up * 100f);
        }
    }

    void ApplyExtraTurnRotation()
    {
        // help the character turn faster (this is in addition to root rotation in the animation)
        float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
        transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
    }


    public void OnAnimatorMove()
    {
        // we implement this function to override the default root motion.
        // this allows us to modify the positional speed before it's applied.
        if (m_IsGrounded && Time.deltaTime > 0)
        {
            Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

            // we preserve the existing y part of the current velocity.
            if (m_Rigidbody.useGravity)
            {
                v.y = m_Rigidbody.velocity.y;
            }
            m_Rigidbody.velocity = v;
        }
    }

    public LayerMask layerMask;
    void CheckGroundStatus()
    {

        if (canCheckGroundStatus)
        {
            RaycastHit hitInfo;
#if UNITY_EDITOR
            // helper to visualise the ground check ray in the scene view
            Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * 0.5f/*m_GroundCheckDistance*/));
#endif
            // 0.1f is a small offset to start the ray from inside the character
            // it is also good to note that the transform position in the sample assets is at the base of the character
            if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance, layerMask)
                || Physics.Raycast(transform.position + (new Vector3(0.2f, 0.1f, 0)), Vector3.down, out hitInfo, m_GroundCheckDistance, layerMask)
                || Physics.Raycast(transform.position + (new Vector3(-0.2f, 0.1f, 0)), Vector3.down, out hitInfo, m_GroundCheckDistance, layerMask)
                || Physics.Raycast(transform.position + (new Vector3(0, 0.1f, -0.2f)), Vector3.down, out hitInfo, m_GroundCheckDistance, layerMask)
                || Physics.Raycast(transform.position + (new Vector3(0, 0.1f, 0.2f)), Vector3.down, out hitInfo, m_GroundCheckDistance, layerMask)
                )
            {
                m_GroundNormal = hitInfo.normal;
                m_IsGrounded = true;
                m_Animator.applyRootMotion = true;
            }
            else
            {
                m_IsGrounded = false;
                m_GroundNormal = Vector3.up;
                m_Animator.applyRootMotion = false;
            }
            //WSM  前
            Debug.DrawLine(transform.position + (new Vector3(0.2f, 0.5f, 0)), transform.position + (new Vector3(0.2f, 0.5f, 0)) + (Vector3.down * 0.5f/*m_GroundCheckDistance*/));

            //后
            Debug.DrawLine(transform.position + (new Vector3(-0.2f, 0.5f, 0)), transform.position + (new Vector3(-0.2f, 0.5f, 0)) + (Vector3.down * 0.5f/*m_GroundCheckDistance*/));

            //左
            Debug.DrawLine(transform.position + (new Vector3(0, 0.5f, -0.2f)), transform.position + (new Vector3(0, 0.5f, -0.2f)) + (Vector3.down * 0.5f/*m_GroundCheckDistance*/));

            //右
            Debug.DrawLine(transform.position + (new Vector3(0, 0.5f, 0.2f)), transform.position + (new Vector3(0, 0.5f, 0.2f)) + (Vector3.down * 0.5f/*m_GroundCheckDistance*/));

        }
    }

    private bool canGetUP = false;
    private bool canCheckGroundStatus = true;
    public bool canMove = true;
    public Vector3 targetPos;
    public void GetUP(Vector3 target)
    {
        targetPos = target;
        //开启移动
        canGetUP = true;

        //关闭地面检测
        canCheckGroundStatus = false;

        //关闭移动
        canMove = false;

        //关闭物理
        m_Rigidbody.isKinematic = true;

    }

    private void UpdateGetUP()
    {
        if (!canGetUP)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime);

        //移动到指定位置结束
        if (Vector3.Distance(transform.position, targetPos) < 0.5f)
        {
            canGetUP = false;

            //开启地面检测
            canCheckGroundStatus = true;

            //开启玩家移动
            canMove = true;

            //开启重力
            m_Rigidbody.isKinematic = false;
        }
    }

    private float targetHigh;
    public void OnCollisionStay(Collision collision)
    {
        //if (collision.transform.gameObject.layer == 17 && Input.GetKey(KeyCode.F))
        //{
        //    targetHigh = collision.transform.position.y;

        //    transform.LookAt(new Vector3(collision.transform.position.x, transform.position.y, collision.transform.position.z));

        //    m_Animator.SetBool("a", true);
        //    m_Animator.SetBool("b", false);
        //    //关闭重力      //到达高度关闭
        //    m_Rigidbody.useGravity = false;

        //    //关闭移动
        //    canMove = false;

        //    m_Capsule.isTrigger = true;

        //    StartCoroutine(CloseDoor_defer2());  //检测是否到达指定高度
        //}
        //else if (collision.transform.gameObject.layer == 15 && Input.GetKeyUp(KeyCode.F))
        //{
        //    if (collision.transform.tag == "Respawn")
        //    {
        //        XiangShangPa2();
        //    }
        //    else
        //    {
        //    XiangShangPa();
        //    }
        //}

        if (collision.transform.gameObject.layer == 17 && Input.GetKey(KeyCode.F))
        {
            transform.position = collision.transform.position;

        }
        else if (collision.transform.gameObject.layer == 15 && Input.GetKeyUp(KeyCode.F))
        {

            if (collision.transform.tag == "Respawn")
            {
                transform.position = collision.transform.position;
            }
            else
            {
                transform.position = collision.transform.position;
                ProtectCameraFromWallClip.isC = true;
            }
        }

    }


    private void OnTriggerStay(Collider other)
    {


        if (other.transform.gameObject.layer == 17 && Input.GetKey(KeyCode.F))
        {
            transform.position = other.transform.position;

        }
        else if (other.transform.gameObject.layer == 15 && Input.GetKeyUp(KeyCode.F))
        {

            if (other.transform.tag == "Respawn")
            {
                transform.position = other.transform.position;
                Dui();
            }
            else
            {
                transform.position = other.transform.position;
            }
        }

    }

    public void GoUpRope()
    {
        {
            Rope_WSM = true;
            m_Rigidbody.isKinematic = true;
            m_Rigidbody.useGravity = false;
            canMove = false;
        }
    }

    public void GoDownRope()
    {
        {
            Rope_WSM = false;
            m_Rigidbody.isKinematic = false;
            m_Rigidbody.useGravity = true;
            canMove = true;
        }
    }

    private IEnumerator CloseDoor_defer2()
    {

        while (transform.position.y <= targetHigh)
        {
            Debug.Log(transform.position.y + "   " + targetHigh);

            yield return new WaitForSeconds(0.1f);

        }
        m_Animator.SetBool("b", true);
        m_Animator.SetBool("a", false);
        yield return new WaitForSeconds(1.28f);

        //开启移动
        canMove = true;

        m_Capsule.isTrigger = false;
        m_Rigidbody.useGravity = true;

    }


    private void XiangShangPa()
    {
        m_Capsule.isTrigger = true;
        m_Rigidbody.useGravity = false;
        StartCoroutine(defer_C());
        m_Animator.SetTrigger("c");
        //Dui();
    }

    private void XiangShangPa2()
    {
        m_Capsule.isTrigger = true;
        m_Rigidbody.useGravity = false;
        m_Animator.SetTrigger("c");
        StartCoroutine(defer_C());
        Dui();
    }

    public void Dui()
    {
        ProtectCameraFromWallClip.isC = true;
        a.crouch = true;
        Debug.Log(a.crouch);
    }

    private IEnumerator defer_C()
    {
        yield return new WaitForSeconds(1.28f);
        m_Rigidbody.useGravity = true;
        m_Capsule.isTrigger = false;
    }


}
