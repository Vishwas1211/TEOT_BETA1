Shader "Custom/NewSurfaceShader 1"
{
    Properties
    {
        _Color("主颜色", Color) = (1, 1, 1, 0)
        _SpecColor("高光颜色", Color) = (1, 1, 1, 1)
        _Emission("光泽颜色", Color) = (0, 0, 0, 0)
        _Shininess("光泽度", Range(0.01, 1)) = 0.7
        _MainTex("基础纹理 (RGB)-透明度(A)", 2D) = "white" {}
    }
    SubShader
    {
        pass
        {
            Material
            {
                Diffuse[_Color]
                Ambient[_Color]
                Shininess[_Shininess]
                Specular[_SpecColor]
                Emission[_Emission]
            }
            Lighting On
            SetTexture[_MainTex]
            {
                Combine Primary * Texture
            }
        }
        pass
		{
            Color(0, 0, 0, 1)
            Cull Front
        }
    }
}
