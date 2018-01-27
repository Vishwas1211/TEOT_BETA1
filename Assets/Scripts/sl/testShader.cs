using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class testShader : MonoBehaviour
{
    public Shader curShader;
    public float grayScaleAmount = 1.0f;
    private Material curMaterial;
    float x = 1.0f;
    float timer = 0;
    void Start()
    {
        if (SystemInfo.supportsImageEffects == false)
        {
            enabled = false;
            return;
        }

        if (curShader != null && curShader.isSupported == false)
        {
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //grayScaleAmount = Mathf.Clamp(grayScaleAmount, 0.0f, 1.0f);

    }
    void OnDisable()
    {
        if (curMaterial != null)
        {
            DestroyImmediate(curMaterial);
        }
    }

    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        if (curShader != null)
        {
            material.SetFloat("_LuminosityAmount", grayScaleAmount);

            Graphics.Blit(sourceTexture, destTexture, material);
        }
        else
        {
            Graphics.Blit(sourceTexture, destTexture);
        }
    }

    public Material material
    {
        get
        {
            if (curMaterial == null)
            {
                curMaterial = new Material(curShader);
                curMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return curMaterial;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, other.transform.position - transform.position);
        if (Physics.Raycast(ray, out hit, 0.2f))
        {
            if (!hit.transform.gameObject.layer.Equals(8))
            {
                x = Vector3.Distance(transform.position, hit.point) * 10;
                timer += Time.deltaTime * 2.0f;
                x = Mathf.Lerp(0, 1, timer);
                x = Mathf.Clamp(x, 0, 1);
                grayScaleAmount = x;
                //if (x <= 0.0f)
                //{
                //}
            }
            //PlayerStatus.isCanMove = false;
            PlayerStatus.isCanMove = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        timer = 0;
        grayScaleAmount = 0;
        //PlayerStatus.isCanMove = true;
        PlayerManager.Instance.playerStatus.SetPlayerState(PlayerState.move);
    }
}
