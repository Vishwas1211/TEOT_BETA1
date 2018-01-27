//EmissionBullet.cs
//TEOT_ONLINE
//
//Create by WangJie on 10/26/2017 5:11 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionBullet : MonoBehaviour 
{
    private const string EFFECT_PATH = "Prefabs/Character/Enemy/Crab/Effects/EmissionExplodeEffects";

    public GameObject _target;
    public float moveSpeed = 3f;

    private bool _startShootBullet = true;

    private Transform sunrise;
    private Vector3 sunset;
    private Vector3 center;

    private float _ramCenterX;
    private float _ramCenterY;
    private float _ramCenterZ;

    public float _centerOffset = 0.46f;

    private float _progressTime = 0.0f;

    public float _smoothMove = 0.01f;

    public void SetTarget(GameObject go)
    {
        _target = go;
    }
    
    void Start()
    {
        if (_target != null)
        {
            _ramCenterX = Random.Range(0f, 3f);
            _ramCenterY = Random.Range(1f, 4f);
            _ramCenterZ = Random.Range(0f, 3f);

            sunrise = transform;
            Vector3 pos = _target.transform.position;
            sunset = pos;
        }
        else
        {
            _ramCenterX = Random.Range(0f, 3f);
            _ramCenterY = Random.Range(1f, 4f);
            _ramCenterZ = Random.Range(0f, 3f);

            sunrise = transform;
            Vector3 pos = new Vector3(0f, 0f, 0f);
            sunset = pos;
        }
    }

    void Update()
    {
        ShootBullet();
    }


    private void ShootBullet()
    {
        if (_startShootBullet)
        {
            //          for (int i = 0; i < 6; i++)
            {
                if (sunrise.position != null)
                {
                    center = (sunrise.position + sunset) * _centerOffset;
                    Vector3 centorProject = Vector3.Project(center, sunrise.position - sunset); // 中心点在两点之间的投影  
                    center -= new Vector3(_ramCenterX, _ramCenterY, _ramCenterZ);// Vector3.MoveTowards(center, centorProject, 5f); // 沿着投影方向移动移动距离（距离越大弧度越小）                  

                    Vector3 riseRelCenter = sunrise.position - center;  //相对于中心在弧线上插值  
                    Vector3 setRelCenter = sunset - center;

                    _progressTime += 0.05f * Time.deltaTime;

                    transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, _progressTime);
                    transform.position += center;
                    //              _emissionEffects [i].transform.Find ("tuowei_zidan").Translate (Vector3.forward * _bulletSpeed * Time.deltaTime, Space.Self);
                }
            }
        }
        else
        {
            _progressTime = 0.0f;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("TestPlayer"))
        {
            Destroy(gameObject);
            //GameObject effect = UtilFunction.ResourceLoadOnPosition(EFFECT_PATH, transform.position, Quaternion.identity);
            //Destroy(effect, 1f);
            //玩家受伤
        }
        else if (other.name.Contains("FPSController"))
        {
            Destroy(gameObject);
            GameObject effect = UtilFunction.ResourceLoadOnPosition(EFFECT_PATH, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            //玩家受伤
        }
        else if (other.name.Contains("ControlPanel"))
        {
            Destroy(gameObject);
            GameObject effect = UtilFunction.ResourceLoadOnPosition(EFFECT_PATH, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }

}
