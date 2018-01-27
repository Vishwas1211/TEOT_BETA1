using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class testChangToolsShader : MonoBehaviour
{
    public Material[] material;
    float num = 1;
    // Use this for initialization
    void Start()
    {
        material = GetComponentInChildren<Renderer>().materials;
    }

    public void Shader()
    {
        for (int i = 0; i < material.Length; i++)
        {
            material[i].DOFloat(0, "_DissolveThreshold", 1);
        }
    }
}
