//MachineBullet.cs
//TEOT_ONLINE
//
//Create by WangJie on 10/28/2017 11:10 AM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineBullet : MonoBehaviour
{
    private const string EFFECT_PATH = "Prefabs/Character/Enemy/Crab/Effects/EmissionExplodeEffects";

    private GameObject _target;
    private float moveSpeed = 30f;


    public void SetTarget(GameObject go)
    {
        _target = go;
    }

    void Start()
    {
        if (_target.name.Contains("Door"))
        {
            transform.LookAt(_target.transform.position + new Vector3(0, 0.5f, 0));
        }
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_target.transform.position - transform.position), 2 * Time.deltaTime);
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
            //GameObject effect = UtilFunction.ResourceLoadOnPosition(EFFECT_PATH, transform.position, Quaternion.identity);
            //Destroy(effect, 1f);
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
