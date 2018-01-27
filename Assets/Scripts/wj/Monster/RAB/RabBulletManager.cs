//RabBulletManager.cs
//TEOT_ONLINE
//
//Create by WangJie on 12/29/2017 10:44 AM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabBulletManager : MonoBehaviour 
{
    private const string SHOOT_POINT_PATH = "Rab/RAB_SpineJ_1/ShootPoint";
    private const string SHOOT_PREFAB_PATH = "Prefabs/Character/Enemy/FS/Effects/FsCannonBullet";

    private GameObject _shootPoint;

    public enum INST_EFFECT_TYPE    //需要生成的特效
    {
        SHOOT,
    }

    public enum COM_EFFECT_TYPE
    {

    }

    public void Init()
    {
        _shootPoint = transform.Find(SHOOT_POINT_PATH).gameObject;
    }

    public void PlayEffect(INST_EFFECT_TYPE type, Vector3 targetPoint, float timeDelay = 0)
    {
        StartCoroutine(DelayInstEffect(type, targetPoint, timeDelay));
    }

    IEnumerator DelayInstEffect(INST_EFFECT_TYPE type, Vector3 targetPoint, float timeDelay = 0)
    {
        yield return new WaitForSeconds(timeDelay);

        switch (type)
        {
            case INST_EFFECT_TYPE.SHOOT:
                GameObject cannon = UtilFunction.ResourceLoadOnPosition(SHOOT_PREFAB_PATH, _shootPoint.transform.position, _shootPoint.transform.rotation);
                cannon.transform.LookAt(targetPoint);
                yield return new WaitForSeconds(0.2f);
                GameObject cannon1 = UtilFunction.ResourceLoadOnPosition(SHOOT_PREFAB_PATH, _shootPoint.transform.position, _shootPoint.transform.rotation);
                cannon1.transform.LookAt(targetPoint);
                yield return new WaitForSeconds(0.2f);
                GameObject cannon2 = UtilFunction.ResourceLoadOnPosition(SHOOT_PREFAB_PATH, _shootPoint.transform.position, _shootPoint.transform.rotation);
                cannon2.transform.LookAt(targetPoint);
                yield return new WaitForSeconds(0.2f);
                GameObject cannon3 = UtilFunction.ResourceLoadOnPosition(SHOOT_PREFAB_PATH, _shootPoint.transform.position, _shootPoint.transform.rotation);
                cannon3.transform.LookAt(targetPoint);
                break;
        }
    }

    public void PlayEffect(COM_EFFECT_TYPE type, float timeDelay)
    {

    }
}
