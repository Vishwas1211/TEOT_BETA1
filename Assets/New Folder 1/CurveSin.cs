using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class CurveSin : MonoBehaviour
{
    public Slider slider;
    public CurveSinManager.TYPE sinType;
    public GameObject crabSield;
    Material mat;
    public float f;
    public static bool isT;
    public int _i;

    private void Start()
    {
    }
    private void OnEnable()
    {
        crabSield = GameObject.Find("Crab");
        mat = gameObject.GetComponent<MeshRenderer>().material;
        if (sinType == CurveSinManager.TYPE.BLACK)
        {
            f = Random.Range(0.1f, 1.0f);
            mat.SetTextureScale("_node_1169", new Vector2(f * 10, 1.0f));
        }
    }

    private void Update()
    {
        if (sinType == CurveSinManager.TYPE.BLACK)
        {
            if (qwe())
            {
                isT = true;
                Debug.Log(1);
            }
            else
            {
                isT = false;
                Debug.Log(2);
            }
            return;
        }

        UpdateScale(123);
    }

    public void UpdateScale(float scaleX)
    {
        //		Material mat = gameObject.GetComponent<MeshRenderer> ().material;
        //		mat.SetTextureScale("_MainTex", new Vector2(scaleX, 1.0f));
        mat.SetTextureScale("_node_1169", new Vector2(slider.value * 10, 1.0f));
    }

    public bool qwe()
    {
        if (Mathf.Abs(f - slider.value) <= 0.03 && _i == (int)crabSield.GetComponent<testCrab>().shield)
        {
            return true;
        }
        return false;
    }

    public void setI(int i) {
        _i = i;
    }
}


