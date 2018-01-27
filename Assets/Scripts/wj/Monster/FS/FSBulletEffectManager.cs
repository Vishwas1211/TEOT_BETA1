//FSBulletEffectManager.cs
//TEOT_ONLINE
//
//Create by WangJie on 12/20/2017 11:32 AM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSBulletEffectManager : MonoBehaviour 
{
    private const string CANNON_POINT = "FS/FS_MainJ/FS_TopJ0/FS_TopJ1/FS_TopJ2/FS_TopJ3/FS_GunJ0/FS_GunJ4/Point";

    private const string CANNON_PREFAB_PATH = "Prefabs/Character/Enemy/FS/Effects/FsCannonBullet";
    private const string BLAST_WAVE_PREFAB_PATH = "Prefabs/Character/Enemy/FS/Effects/FsBlastWave";

    private GameObject _cannonPoint;

    public enum INST_EFFECT_TYPE    //需要生成的特效
    {
        CANNON,
        BLAST_WAVE,
    }

    public enum COM_EFFECT_TYPE
    {

    }

    public void Init()
    {
        _cannonPoint = transform.Find(CANNON_POINT).gameObject;
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
            case INST_EFFECT_TYPE.CANNON:
                GameObject cannon = UtilFunction.ResourceLoadOnPosition(CANNON_PREFAB_PATH, _cannonPoint.transform.position, _cannonPoint.transform.rotation);
                cannon.transform.LookAt(targetPoint);
                break;
            case INST_EFFECT_TYPE.BLAST_WAVE:
                GameObject blastWave = UtilFunction.ResourceLoadOnPosition(BLAST_WAVE_PREFAB_PATH, _cannonPoint.transform.position, _cannonPoint.transform.rotation);
                blastWave.transform.LookAt(targetPoint);
                break;
        }
    }

    public void PlayEffect(COM_EFFECT_TYPE type, float timeDelay)
    {

    }
}
