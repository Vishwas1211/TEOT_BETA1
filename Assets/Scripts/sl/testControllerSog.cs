using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testControllerSog : MonoBehaviour
{
    float h;
    float v;
    float x;
    float y;
    public float speed;
    public Animator _anim;
    public BEHAVIOR_SATE _curState;
    public static bool isDeath;
    public enum BEHAVIOR_SATE
    {
        IDLE,
        WALK,
        RUN,
        JUMP,
        CROUCH_L,
        CROUCH_R,
        WALK_SHOOT,
        CROUCH_SHOOT_L,
        CROUCH_SHOOT_R,
        GRAB,//×¥
        HAMMER,//ÔÒµØ
        BRANDISH,//ÂÖ¸ì²²
        LIFT_UP,//ÏÆÆð
        DUOBI,
        HURT,
        DEATH,
        LEFT,
        RIGHT,
        BACK,
    }

    public void SetState(BEHAVIOR_SATE state)
    {
        if (state == _curState)
            return;
        _curState = state;

        ResetAnimator();
        switch (_curState)
        {
            case BEHAVIOR_SATE.IDLE:
                _anim.SetBool("isIdleBool", true);
                break;
            case BEHAVIOR_SATE.WALK:
                _anim.SetBool("isWalkBool", true);
                break;
            case BEHAVIOR_SATE.RUN:
                _anim.SetBool("isRunBool", true);
                break;
            case BEHAVIOR_SATE.JUMP:
                _anim.SetBool("isJumpBool", true);
                break;
            case BEHAVIOR_SATE.CROUCH_L:
                _anim.SetBool("isCrouchLBool", true);
                break;
            case BEHAVIOR_SATE.CROUCH_R:
                _anim.SetBool("isCrouchRBool", true);
                break;
            case BEHAVIOR_SATE.WALK_SHOOT:
                _anim.SetBool("isWalkShootBool", true);
                break;
            case BEHAVIOR_SATE.CROUCH_SHOOT_L:
                _anim.SetTrigger("isCrouchShootLTrigger");
                break;
            case BEHAVIOR_SATE.CROUCH_SHOOT_R:
                _anim.SetTrigger("isCrouchShootRTrigger");
                break;
            case BEHAVIOR_SATE.GRAB:
                _anim.SetTrigger("isGrabTrigger");
                break;
            case BEHAVIOR_SATE.HAMMER:
                _anim.SetTrigger("isHammerTrigger");
                break;
            case BEHAVIOR_SATE.BRANDISH:
                _anim.SetTrigger("isBrandishTrigger");
                break;
            case BEHAVIOR_SATE.LIFT_UP:
                _anim.SetTrigger("isLiftUpTrigger");
                break;
            case BEHAVIOR_SATE.DUOBI:
                _anim.SetTrigger("isDuoBiTrigger");
                break;
            case BEHAVIOR_SATE.HURT:
                _anim.SetTrigger("isHurtTrigger");
                break;
            case BEHAVIOR_SATE.DEATH:
                _anim.SetTrigger("isDeathTrigger");
                break;
            case BEHAVIOR_SATE.LEFT:
                _anim.SetBool("isLeftLoop", true);
                break;
            case BEHAVIOR_SATE.RIGHT:
                _anim.SetBool("isRightLoop", true);
                break;
            case BEHAVIOR_SATE.BACK:
                _anim.SetBool("isWalkBack", true);
                break;
        }
    }

    private void ResetAnimator()
    {
        _anim.SetBool("isIdleBool", false);
        _anim.SetBool("isWalkBool", false);
        _anim.SetBool("isRunBool", false);
        _anim.SetBool("isCrouchLBool", false);
        _anim.SetBool("isCrouchRBool", false);
        _anim.SetBool("isJumpBool", false);
        _anim.SetBool("isWalkShootBool", false);
        _anim.SetBool("isLeftLoop", false);
        _anim.SetBool("isRightLoop", false);
        _anim.SetBool("isWalkBack", false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
        //h = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        //v = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        //x = Input.GetAxis("Mouse X") * Time.deltaTime * speed * 100;
        //y = Input.GetAxis("Mouse Y") * Time.deltaTime * speed * 10;
        //transform.Translate(h, 0, v);
        //transform.Rotate(0, x, 0);

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    SetState(BEHAVIOR_SATE.WALK);
        //}
        //if (Input.GetKeyUp(KeyCode.W))
        //{
        //    SetState(BEHAVIOR_SATE.IDLE);
        //}

        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    SetState(BEHAVIOR_SATE.LEFT);
        //}
        //if (Input.GetKeyUp(KeyCode.A))
        //{
        //    SetState(BEHAVIOR_SATE.IDLE);
        //}

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    SetState(BEHAVIOR_SATE.BACK);
        //}
        //if (Input.GetKeyUp(KeyCode.S))
        //{
        //    SetState(BEHAVIOR_SATE.IDLE);
        //}

        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    SetState(BEHAVIOR_SATE.RIGHT);
        //}
        //if (Input.GetKeyUp(KeyCode.D))
        //{
        //    SetState(BEHAVIOR_SATE.IDLE);
        //}
    }

    private void UpdateState()
    {
        //if (_isDead)
        //    return;
        //switch (_curState)
        //{
        //    case BEHAVIOR_SATE.IDLE:
        //        DoIdle();
        //        break;
        //    case BEHAVIOR_SATE.WALK:
        //        DoWalk();
        //        break;
        //    case BEHAVIOR_SATE.RUN:
        //        DoRun();
        //        break;
        //    case BEHAVIOR_SATE.JUMP:
        //        DoJump();
        //        break;
        //    case BEHAVIOR_SATE.CROUCH_L:
        //        DoCrouchL();
        //        break;
        //    case BEHAVIOR_SATE.CROUCH_R:
        //        DoCrouchR();
        //        break;
        //    case BEHAVIOR_SATE.WALK_SHOOT:
        //        DoWalkShoot();
        //        break;
        //    case BEHAVIOR_SATE.CROUCH_SHOOT_L:
        //        DoCrouchShootL();
        //        break;
        //    case BEHAVIOR_SATE.CROUCH_SHOOT_R:
        //        DoCrouchShootR();
        //        break;
        //    case BEHAVIOR_SATE.GRAB:
        //        DoGrab();
        //        break;
        //    case BEHAVIOR_SATE.HAMMER:
        //        DoHammer();
        //        break;
        //    case BEHAVIOR_SATE.BRANDISH:
        //        DoBrandish();
        //        break;
        //    case BEHAVIOR_SATE.LIFT_UP:
        //        DoLiftUp();
        //        break;
        //    case BEHAVIOR_SATE.DUOBI:
        //        DoDuoBi();
        //        break;
        //    case BEHAVIOR_SATE.HURT:
        //        DoHurt();
        //        break;
        //}
    }

}
