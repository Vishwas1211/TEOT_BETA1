using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMove : MonoBehaviour
{
    private enum PlayerStep
    {
        setp0 = DataPlayerHeight.STEP0,
        setp1 = DataPlayerHeight.STEP1,
        setp2 = DataPlayerHeight.STEP2,
        setp3 = DataPlayerHeight.STEP3,
    }

    private GameObject _playerCollider;
    private GameObject _eye;
    private GameObject _player;
    private PlayerStep _playerCurrentStep = new PlayerStep();
    private float _playerHeight = 0;

    Vector3 v3;
    bool isWalk = false;
    public float max = 0.3f;
    public float min = 0.1f;
    private LayerMask layerMask = ~(1 << 8);
    public GameObject go1;
    public GameObject go2;
    public GameObject go3;
    public GameObject go4;

    public static bool f;
    public static bool b;
    public static bool l;
    public static bool r;

    public void Init()
    {
        _playerCollider = PlayerManager.Instance.playerCollider;
        _eye = PlayerManager.Instance.eye;
        _player = PlayerManager.Instance.gameObject;
        SetPlayerStep(PlayerStep.setp1);
    }

    void Update()
    {
        //transform.position = _eye.transform.position;
        //transform.rotation = Quaternion.Euler(0, _eye.transform.rotation.y, 0);

        //Vector3 v3 = go1.transform.position - _player.transform.position;
        //v3.y = 0;
        //Vector3 vectorForward = _eye.transform.forward;
        //float dotValue = Vector3.Dot(vectorForward.normalized, v3.normalized);
        //float angle = Mathf.Acos(dotValue) * Mathf.Rad2Deg;
        //Vector3 crossValue = Vector3.Cross(vectorForward, v3);
        //if (crossValue.y > 0 && angle > 80 && angle < 100)
        //{
        //    go1.tag = "Right";
        //    go2.tag = "Left";
        //    go3.tag = "Back";
        //    go4.tag = "Front";
        //}
        //if (crossValue.y < 0 && angle > 80 && angle < 100)
        //{
        //    go1.tag = "Left";
        //    go2.tag = "Right";
        //    go3.tag = "Front";
        //    go4.tag = "Back";
        //}
        //if (angle > 130 && angle < 180)
        //{
        //    go1.tag = "Back";
        //    go2.tag = "Front";
        //    go3.tag = "Left";
        //    go4.tag = "Right";
        //}
        //if (angle > 15 && angle < 30)
        //{
        //    go1.tag = "Front";
        //    go2.tag = "Back";
        //    go3.tag = "Right";
        //    go4.tag = "Left";
        //}
    }

    float Angle_360()
    {
        float angle = Mathf.Atan2(v3.z, v3.x) * Mathf.Rad2Deg;
        angle = 90.0f - angle;
        if (angle < 0)
        {
            angle += 360.0f;
        }
        return angle;
    }

    private void LateUpdate()
    {
        //v3 = (_eye.transform.position - _player.transform.position);
        //v3.y = 0;
        //if (PlayerManager.Instance.playerStatus.IsEqualPlayerState(PlayerState.stop) && v3.magnitude > max)
        //{
        //    PlayerManager.Instance.playerStatus.SetPlayerState(PlayerState.move);
        //}
        //if (PlayerManager.Instance.playerStatus.IsEqualPlayerState(PlayerState.move) && (v3.magnitude < min))
        //{
        //    PlayerManager.Instance.playerStatus.SetPlayerState(PlayerState.stop);
        //}
        //if (PlayerManager.Instance.playerStatus.IsEqualPlayerState(PlayerState.move))
        {
            //if (Angle_360() < 45 || Angle_360() > 315)
            if (f)
            {
                _player.transform.Translate(_eye.transform.forward * 1 * Time.deltaTime);
            }
            //if (Angle_360() > 45 && Angle_360() < 135)
            if (r)
            {
                _player.transform.Translate(_eye.transform.right * 1 * Time.deltaTime);
            }
            //if (Angle_360() > 135 && Angle_360() < 225)
            if (b)
            {
                _player.transform.Translate(-_eye.transform.forward * 1 * Time.deltaTime);
            }
            //if (Angle_360() > 225 && Angle_360() < 315)
            if (l)
            {
                _player.transform.Translate(-_eye.transform.right * 1 * Time.deltaTime);
            }
        }
        _player.transform.position = new Vector3(_player.transform.position.x, _playerHeight, _player.transform.position.z);

        if (PlayerManager.Instance.playerStatus.IsEqualPlayerState(PlayerState.jump))
        {
            PlayerManager.Instance.eye.GetComponent<Collider>().enabled = false;
            _player.transform.position = new Vector3(_player.transform.position.x, _playerHeight + 0.5f, _player.transform.position.z);
        }
    }

    float distance = 1.0f;

    public void UseDodge(int direction)
    {
        RaycastHit hit;
        if (PlayerManager.Instance.playerStatus.IsEqualPlayerState(PlayerState.move))
        {
            switch (direction)
            {
                case 0:
                    if (Physics.Raycast(_player.transform.position, _player.transform.forward, out hit, 100, layerMask))
                    {
                        if (Vector3.Distance(_player.transform.position, hit.point) >= distance)
                        {
                            _player.transform.position = new Vector3(_player.transform.position.x, _playerHeight, _player.transform.position.z) + _eye.transform.forward;
                        }
                    }
                    break;
                case 1:
                    if (Physics.Raycast(_player.transform.position, _player.transform.right, out hit, 100, layerMask))
                    {
                        if (Vector3.Distance(_player.transform.position, hit.point) >= distance)
                        {
                            _player.transform.position = new Vector3(_player.transform.position.x, _playerHeight, _player.transform.position.z) + _eye.transform.right;
                        }
                    }
                    break;
                case 2:
                    if (Physics.Raycast(_player.transform.position, -_player.transform.forward, out hit, 100, layerMask))
                    {
                        if (Vector3.Distance(_player.transform.position, hit.point) >= distance)
                        {
                            _player.transform.position = new Vector3(_player.transform.position.x, _playerHeight, _player.transform.position.z) + -_eye.transform.forward;
                        }
                    }
                    break;
                case 3:
                    if (Physics.Raycast(_player.transform.position, -_player.transform.right, out hit, 100, layerMask))
                    {
                        if (Vector3.Distance(_player.transform.position, hit.point) >= distance)
                        {
                            _player.transform.position = new Vector3(_player.transform.position.x, _playerHeight, _player.transform.position.z) + -_eye.transform.right;
                        }
                    }
                    break;
            }
        }
    }

    private void SetPlayerStep(PlayerStep step)
    {
        if (_playerCurrentStep == step)
        {
            return;
        }
        _playerCurrentStep = step;
        _playerHeight = (int)step / 100.0f;
    }

    public void SetPlayerHeight(float h)
    {
        _playerHeight = h;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Front":
                f = true;
                break;
            case "Back":
                b = true;
                break;
            case "Left":
                l = true;
                break;
            case "Right":
                r = true;
                break;
            default:
                break;
        }

        PlayerManager.Instance.playerStatus.SetPlayerState(PlayerState.move);
    }

    private void OnTriggerExit(Collider other)
    {
        f = false;
        b = false;
        l = false;
        r = false;
        PlayerManager.Instance.playerStatus.SetPlayerState(PlayerState.stop);
    }
}
