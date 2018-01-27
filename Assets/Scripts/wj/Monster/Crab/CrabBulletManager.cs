//CrabBulletManager.cs
//TEOT_ONLINE
//
//Create by WangJie on 10/26/2017 5:44 PM
//Description: 
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBulletManager : MonoBehaviour 
{
    private const string EMISSION_POINT_PATH = "CharacterG/SpineJ_1/SpineJ_2/SpineJ_3/SpineJ_4/SpineJ_5/" +
        "SpineJ_6/SpineJ_7/SpineJ_8/NeckJ_1/NeckJ_2/HeadJ_1/FirePos";
    private const string MACHINE_POINT_RIGHT_PATH = "CharacterG/SpineJ_1/SpineJ_2/SpineJ_3/SpineJ_4/SpineJ_5/" +
        "SpineJ_6/SpineJ_7/SpineJ_8/LShoulderPlateJ_1/LShoulderJ_1/LForeArmJ_1/LForeArmJ_2/LWristJ_1/FireShellPos";
    private const string MACHINE_POINT_LEFT_PATH = "CharacterG/SpineJ_1/SpineJ_2/SpineJ_3/SpineJ_4/SpineJ_5/" +
        "SpineJ_6/SpineJ_7/SpineJ_8/RShoulderPlateJ_1/RShoulderJ_1/RForeArmJ_1/RForeArmJ_2/RWristJ_1/FireShellPos";
    private const string SHIELD_PATH = "CrabShield";

    private const string EMISSION_PATH = "Prefabs/Character/Enemy/Crab/Bullet/Emission";
    private const string MACHINE_PATH = "Prefabs/Character/Enemy/Crab/Bullet/Machine";
    private const string STRUGGLE_PATH = "Prefabs/Character/Enemy/Crab/Bullet/Struggle";

    private GameObject[] _emissionPointArray;
    private GameObject _emissionPointParent;
    private GameObject _machinePointL;
    private GameObject _machinePointR;

    //private GameObject _emissionBullet;
    //private GameObject _machineBullet;
    private GameObject _shield;

    private int _machineCount = 0;

    public enum SKILL_TYPE
    {
        CYCLON,
        HUMMAN,
        GRAVEL,
        POWER,
        MACHINE,
        EMISSION,
        ARTILLERY_COMBO,
        STRUGGLE,
    }

    public enum CRAB_EFFECT
    {
        SHIELD,
    }
	
	public void Init ()
	{
        _shield = transform.Find(SHIELD_PATH).gameObject;
        _machinePointL = transform.Find(MACHINE_POINT_LEFT_PATH).gameObject;
        _machinePointR = transform.Find(MACHINE_POINT_RIGHT_PATH).gameObject;
        _emissionPointParent = transform.Find(EMISSION_POINT_PATH).gameObject;
        int count = _emissionPointParent.transform.childCount;
        _emissionPointArray = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            _emissionPointArray[i] = _emissionPointParent.transform.GetChild(i).gameObject;
        }
    }

    public void PlaySkill(SKILL_TYPE curSkill, GameObject target, float delayTime = 0f)
    {
        StartCoroutine(DelayPlaySkill(curSkill, target, delayTime));
    }

    IEnumerator DelayPlaySkill(SKILL_TYPE curSkill, GameObject target, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        switch (curSkill)
        {
            case SKILL_TYPE.CYCLON:
                break;
            case SKILL_TYPE.HUMMAN:
                break;
            case SKILL_TYPE.GRAVEL:
                break;
            case SKILL_TYPE.POWER:
                break;
            case SKILL_TYPE.MACHINE:
                StartCoroutine(MachineBullet(target));
                break;
            case SKILL_TYPE.EMISSION:
                EmissionBullet(target);
                break;
            case SKILL_TYPE.ARTILLERY_COMBO:
                ArtilleryCombo(target);
                break;
            case SKILL_TYPE.STRUGGLE:
                StartCoroutine(StruggleBullet(target));
                break;
        }
    }

    private void ArtilleryCombo(GameObject target) //»ðÅÚÁ¬»÷
    {
        StartCoroutine(ArtilleryComboDelay(target));
    }

    IEnumerator ArtilleryComboDelay(GameObject target)
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < _emissionPointParent.transform.childCount - 3; i++)
        {
            GameObject go = UtilFunction.ResourceLoadOnPosition(EMISSION_PATH, _emissionPointArray[i].transform.position, _emissionPointArray[i].transform.rotation);
            go.GetComponent<EmissionBullet>().SetTarget(target);
        }
        yield return new WaitForSeconds(0.4f);
        for (int i = 3; i < _emissionPointParent.transform.childCount; i++)
        {
            GameObject go = UtilFunction.ResourceLoadOnPosition(EMISSION_PATH, _emissionPointArray[i].transform.position, _emissionPointArray[i].transform.rotation);
            go.GetComponent<EmissionBullet>().SetTarget(target);
        }
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < _emissionPointParent.transform.childCount - 3; i++)
        {
            GameObject go = UtilFunction.ResourceLoadOnPosition(EMISSION_PATH, _emissionPointArray[i].transform.position, _emissionPointArray[i].transform.rotation);
            go.GetComponent<EmissionBullet>().SetTarget(target);
        }
        yield return new WaitForSeconds(0.4f);
        for (int i = 3; i < _emissionPointParent.transform.childCount; i++)
        {
            GameObject go = UtilFunction.ResourceLoadOnPosition(EMISSION_PATH, _emissionPointArray[i].transform.position, _emissionPointArray[i].transform.rotation);
            go.GetComponent<EmissionBullet>().SetTarget(target);
        }
    }

    private void EmissionBullet(GameObject target)
    {
        for (int i = 0; i < _emissionPointParent.transform.childCount; i++)
        {
           GameObject go = UtilFunction.ResourceLoadOnPosition(EMISSION_PATH, _emissionPointArray[i].transform.position, _emissionPointArray[i].transform.rotation);
            go.GetComponent<EmissionBullet>().SetTarget(target);
        }
    }

    IEnumerator MachineBullet(GameObject target)
    {
        while (_machineCount <= 10)
        {
            yield return new WaitForSeconds(0.2f);
            GameObject go = UtilFunction.ResourceLoadOnPosition(MACHINE_PATH, _machinePointL.transform.position, _machinePointL.transform.rotation);
            go.GetComponent<MachineBullet>().SetTarget(target);
            GameObject go1 = UtilFunction.ResourceLoadOnPosition(MACHINE_PATH, _machinePointR.transform.position, _machinePointR.transform.rotation);
            go1.GetComponent<MachineBullet>().SetTarget(target);
            _machineCount++;
        }
        _machineCount = 0;
    }

    IEnumerator StruggleBullet(GameObject target)
    {
        while (_machineCount <= 10)
        {
            yield return new WaitForSeconds(0.2f);
            GameObject go = UtilFunction.ResourceLoadOnPosition(STRUGGLE_PATH, _machinePointL.transform.position, _machinePointL.transform.rotation);
            go.GetComponent<MachineBullet>().SetTarget(target);
            GameObject go1 = UtilFunction.ResourceLoadOnPosition(STRUGGLE_PATH, _machinePointR.transform.position, _machinePointR.transform.rotation);
            go1.GetComponent<MachineBullet>().SetTarget(target);
            _machineCount++;
        }
        _machineCount = 0;
    }

    public void EffectsControl(CRAB_EFFECT crabEffect, bool isShow, float delayTime = 0)
    {
        StartCoroutine(DelayEffectControl(crabEffect, isShow, delayTime));
    }

    IEnumerator DelayEffectControl(CRAB_EFFECT crabEffect, bool isShow, float time)
    {
        yield return new WaitForSeconds(time);
        switch (crabEffect)
        {
            case CRAB_EFFECT.SHIELD:
                _shield.SetActive(isShow);
                break;
        }
    }

    void Update () 
	{
		
	}
}
