// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.27 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.27;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:33920,y:33132,varname:node_4795,prsc:2|emission-2393-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:33204,y:32992,varname:node_2393,prsc:2|A-887-OUT,B-2053-RGB,C-797-RGB,D-9211-OUT,E-1662-RGB;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32541,y:33091,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32377,y:33278,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Panner,id:995,x:31547,y:32710,varname:node_995,prsc:2,spu:0.5,spv:0.2|UVIN-7735-OUT;n:type:ShaderForge.SFN_Panner,id:7862,x:31521,y:33022,varname:node_7862,prsc:2,spu:0.1,spv:-0.3|UVIN-7214-OUT;n:type:ShaderForge.SFN_TexCoord,id:42,x:31173,y:32643,varname:node_42,prsc:2,uv:0;n:type:ShaderForge.SFN_TexCoord,id:4998,x:31142,y:33012,varname:node_4998,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:5133,x:32060,y:32808,varname:node_5133,prsc:2|A-2391-RGB,B-9314-RGB;n:type:ShaderForge.SFN_Tex2d,id:2391,x:31737,y:32709,ptovrint:False,ptlb:node_2391,ptin:_node_2391,varname:node_2391,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d5787e41e5346f144aa1960947a6fb09,ntxv:0,isnm:False|UVIN-995-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:9314,x:31763,y:33022,ptovrint:False,ptlb:node_9314,ptin:_node_9314,varname:node_9314,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ec1ae089a2de5b04090d5f314793461d,ntxv:0,isnm:False|UVIN-7862-UVOUT;n:type:ShaderForge.SFN_Vector2,id:7196,x:31173,y:32839,varname:node_7196,prsc:2,v1:2,v2:2;n:type:ShaderForge.SFN_Multiply,id:7735,x:31359,y:32710,varname:node_7735,prsc:2|A-42-UVOUT,B-7196-OUT;n:type:ShaderForge.SFN_Vector2,id:367,x:31159,y:33218,varname:node_367,prsc:2,v1:2,v2:2;n:type:ShaderForge.SFN_Multiply,id:7214,x:31361,y:33022,varname:node_7214,prsc:2|A-4998-UVOUT,B-367-OUT;n:type:ShaderForge.SFN_Panner,id:8121,x:32397,y:31940,varname:node_8121,prsc:2,spu:0,spv:0|UVIN-7495-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:7495,x:32163,y:31886,varname:node_7495,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:1443,x:32112,y:32442,varname:node_1443,prsc:2|A-8121-UVOUT,B-2003-OUT;n:type:ShaderForge.SFN_Panner,id:8004,x:31593,y:32036,varname:node_8004,prsc:2,spu:0.5,spv:0.2|UVIN-8838-OUT;n:type:ShaderForge.SFN_Panner,id:4112,x:31567,y:32348,varname:node_4112,prsc:2,spu:0.1,spv:-0.3|UVIN-2232-OUT;n:type:ShaderForge.SFN_TexCoord,id:7107,x:31219,y:31969,varname:node_7107,prsc:2,uv:0;n:type:ShaderForge.SFN_TexCoord,id:2316,x:31188,y:32338,varname:node_2316,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:2003,x:31998,y:32035,varname:node_2003,prsc:2|A-8792-RGB,B-630-RGB;n:type:ShaderForge.SFN_Tex2d,id:8792,x:31783,y:32035,ptovrint:False,ptlb:node_2391_copy,ptin:_node_2391_copy,varname:_node_2391_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d5787e41e5346f144aa1960947a6fb09,ntxv:0,isnm:False|UVIN-8004-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:630,x:31768,y:32348,ptovrint:False,ptlb:node_9314_copy,ptin:_node_9314_copy,varname:_node_9314_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ec1ae089a2de5b04090d5f314793461d,ntxv:0,isnm:False|UVIN-4112-UVOUT;n:type:ShaderForge.SFN_Vector2,id:336,x:31219,y:32165,varname:node_336,prsc:2,v1:2,v2:2;n:type:ShaderForge.SFN_Multiply,id:8838,x:31405,y:32036,varname:node_8838,prsc:2|A-7107-UVOUT,B-336-OUT;n:type:ShaderForge.SFN_Vector2,id:9259,x:31205,y:32544,varname:node_9259,prsc:2,v1:2,v2:2;n:type:ShaderForge.SFN_Multiply,id:2232,x:31368,y:32400,varname:node_2232,prsc:2|A-2316-UVOUT,B-9259-OUT;n:type:ShaderForge.SFN_Tex2d,id:5661,x:32364,y:32550,ptovrint:False,ptlb:node_6104_copy,ptin:_node_6104_copy,varname:_node_6104_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ec1ae089a2de5b04090d5f314793461d,ntxv:0,isnm:False|UVIN-1443-OUT;n:type:ShaderForge.SFN_Multiply,id:2873,x:32682,y:32740,varname:node_2873,prsc:2|A-5661-RGB,B-5133-OUT;n:type:ShaderForge.SFN_Tex2d,id:1169,x:32698,y:32550,ptovrint:False,ptlb:node_1169,ptin:_node_1169,varname:node_1169,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:c3b49e2451b4001408f2416c2a2e1d8b,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:8149,x:33070,y:32651,varname:node_8149,prsc:2|A-1169-RGB,B-2873-OUT;n:type:ShaderForge.SFN_Vector1,id:7080,x:32988,y:32866,varname:node_7080,prsc:2,v1:10;n:type:ShaderForge.SFN_Multiply,id:887,x:33228,y:32701,varname:node_887,prsc:2|A-8149-OUT,B-7080-OUT;n:type:ShaderForge.SFN_Color,id:1662,x:33072,y:33284,ptovrint:False,ptlb:node_1662,ptin:_node_1662,varname:node_1662,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Slider,id:9211,x:32631,y:33446,ptovrint:False,ptlb:node_9211,ptin:_node_9211,varname:node_9211,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:40;proporder:797-2391-9314-5661-8792-630-1169-1662-9211;pass:END;sub:END;*/

Shader "Shader Forge/kongzhitai_xian_liudong" {
    Properties {
        _TintColor ("Color", Color) = (0.5,0.5,0.5,1)
        _node_2391 ("node_2391", 2D) = "white" {}
        _node_9314 ("node_9314", 2D) = "white" {}
        _node_6104_copy ("node_6104_copy", 2D) = "white" {}
        _node_2391_copy ("node_2391_copy", 2D) = "white" {}
        _node_9314_copy ("node_9314_copy", 2D) = "white" {}
        _node_1169 ("node_1169", 2D) = "white" {}
        _node_1662 ("node_1662", Color) = (1,0,0,1)
        _node_9211 ("node_9211", Range(0, 40)) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _TintColor;
            uniform sampler2D _node_2391; uniform float4 _node_2391_ST;
            uniform sampler2D _node_9314; uniform float4 _node_9314_ST;
            uniform sampler2D _node_2391_copy; uniform float4 _node_2391_copy_ST;
            uniform sampler2D _node_9314_copy; uniform float4 _node_9314_copy_ST;
            uniform sampler2D _node_6104_copy; uniform float4 _node_6104_copy_ST;
            uniform sampler2D _node_1169; uniform float4 _node_1169_ST;
            uniform float4 _node_1662;
            uniform float _node_9211;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 _node_1169_var = tex2D(_node_1169,TRANSFORM_TEX(i.uv0, _node_1169));
                float4 node_9226 = _Time + _TimeEditor;
                float2 node_8004 = ((i.uv0*float2(2,2))+node_9226.g*float2(0.5,0.2));
                float4 _node_2391_copy_var = tex2D(_node_2391_copy,TRANSFORM_TEX(node_8004, _node_2391_copy));
                float2 node_4112 = ((i.uv0*float2(2,2))+node_9226.g*float2(0.1,-0.3));
                float4 _node_9314_copy_var = tex2D(_node_9314_copy,TRANSFORM_TEX(node_4112, _node_9314_copy));
                float3 node_1443 = (float3((i.uv0+node_9226.g*float2(0,0)),0.0)+(_node_2391_copy_var.rgb*_node_9314_copy_var.rgb));
                float4 _node_6104_copy_var = tex2D(_node_6104_copy,TRANSFORM_TEX(node_1443, _node_6104_copy));
                float2 node_995 = ((i.uv0*float2(2,2))+node_9226.g*float2(0.5,0.2));
                float4 _node_2391_var = tex2D(_node_2391,TRANSFORM_TEX(node_995, _node_2391));
                float2 node_7862 = ((i.uv0*float2(2,2))+node_9226.g*float2(0.1,-0.3));
                float4 _node_9314_var = tex2D(_node_9314,TRANSFORM_TEX(node_7862, _node_9314));
                float3 emissive = (((_node_1169_var.rgb*(_node_6104_copy_var.rgb*(_node_2391_var.rgb*_node_9314_var.rgb)))*10.0)*i.vertexColor.rgb*_TintColor.rgb*_node_9211*_node_1662.rgb);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
