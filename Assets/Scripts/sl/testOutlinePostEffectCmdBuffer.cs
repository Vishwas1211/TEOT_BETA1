using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;

public class testOutlinePostEffectCmdBuffer : MonoBehaviour
{
    RenderTexture renderTexture = null;
    CommandBuffer commandBuffer = null;
    Material outlineMaterial = null;

    public Shader outlineShader = null;
    public Color outlineColor = Color.green;
    [Range(0.0f,10.0f)]
    public float outlineStrength = 3.0f;
    public GameObject target = null;

    private void OnEnable()
    {
        if (outlineShader == null)
        {
            return;
        }
        if (outlineMaterial == null)
        {
            outlineMaterial = new Material(outlineShader);
        }
        Renderer[] renderers = target.GetComponentsInChildren<Renderer>();
        if (renderTexture == null)
        {
            //renderTexture = RenderTexture.GetTemporary(Screen.width>>dow)
        }
        commandBuffer = new CommandBuffer();
        commandBuffer.SetRenderTarget(renderTexture);
        commandBuffer.ClearRenderTarget(true, true, Color.black);
        foreach (Renderer r in renderers)
        {
            commandBuffer.DrawRenderer(r, outlineMaterial);
        }
    }

    void OnDisable()
    {
        if (renderTexture)
        {
            RenderTexture.ReleaseTemporary(renderTexture);
            renderTexture = null;
        }
        if (outlineMaterial)
        {
            DestroyImmediate(outlineMaterial);
            outlineMaterial = null;
        }
        if (commandBuffer != null)
        {
            commandBuffer.Release();
            commandBuffer = null;
        }

    }

    //void OnRenderImage(RenderTexture source, RenderTexture destination)
    //{
    //    if (_Material && renderTexture && outlineMaterial && commandBuffer != null)
    //    {
    //        //通过Command Buffer可以设置自定义材质的颜色  
    //        outlineMaterial.SetColor("_OutlineCol", outLineColor);
    //        //直接通过Graphic执行Command Buffer  
    //        Graphics.ExecuteCommandBuffer(commandBuffer);

    //        //对RT进行Blur处理  
    //        RenderTexture temp1 = RenderTexture.GetTemporary(source.width >> downSample, source.height >> downSample, 0);
    //        RenderTexture temp2 = RenderTexture.GetTemporary(source.width >> downSample, source.height >> downSample, 0);

    //        //高斯模糊，两次模糊，横向纵向，使用pass0进行高斯模糊  
    //        _Material.SetVector("_offsets", new Vector4(0, samplerScale, 0, 0));
    //        Graphics.Blit(renderTexture, temp1, _Material, 0);
    //        _Material.SetVector("_offsets", new Vector4(samplerScale, 0, 0, 0));
    //        Graphics.Blit(temp1, temp2, _Material, 0);

    //        //如果有叠加再进行迭代模糊处理  
    //        for (int i = 0; i < iteration; i++)
    //        {
    //            _Material.SetVector("_offsets", new Vector4(0, samplerScale, 0, 0));
    //            Graphics.Blit(temp2, temp1, _Material, 0);
    //            _Material.SetVector("_offsets", new Vector4(samplerScale, 0, 0, 0));
    //            Graphics.Blit(temp1, temp2, _Material, 0);
    //        }

    //        //用模糊图和原始图计算出轮廓图  
    //        _Material.SetTexture("_BlurTex", temp2);
    //        Graphics.Blit(renderTexture, temp1, _Material, 1);

    //        //轮廓图和场景图叠加  
    //        _Material.SetTexture("_BlurTex", temp1);
    //        _Material.SetFloat("_OutlineStrength", outLineStrength);
    //        Graphics.Blit(source, destination, _Material, 2);

    //        RenderTexture.ReleaseTemporary(temp1);
    //        RenderTexture.ReleaseTemporary(temp2);
    //    }
    //    else
    //    {
    //        Graphics.Blit(source, destination);
    //    }
    //}
}
