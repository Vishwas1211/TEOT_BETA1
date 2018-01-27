// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.27 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.27;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:34009,y:32700,varname:node_4013,prsc:2|emission-7278-OUT,alpha-4874-A;n:type:ShaderForge.SFN_Fresnel,id:2769,x:32396,y:32616,varname:node_2769,prsc:2;n:type:ShaderForge.SFN_Slider,id:925,x:32258,y:32736,ptovrint:False,ptlb:node_925,ptin:_node_925,varname:node_925,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:4.76734,max:10;n:type:ShaderForge.SFN_Power,id:9834,x:32763,y:32673,varname:node_9834,prsc:2|VAL-2769-OUT,EXP-925-OUT;n:type:ShaderForge.SFN_Tex2d,id:6696,x:32482,y:32918,ptovrint:False,ptlb:node_6696,ptin:_node_6696,varname:node_6696,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:13a779baa4e09e645a99e4a3520e280a,ntxv:0,isnm:False|UVIN-5404-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:7420,x:32482,y:33153,ptovrint:False,ptlb:node_7420,ptin:_node_7420,varname:node_7420,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e8101847673c8eb459928172d10510cb,ntxv:0,isnm:False|UVIN-8592-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:1999,x:32928,y:33355,ptovrint:False,ptlb:node_1999,ptin:_node_1999,varname:node_1999,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:13a779baa4e09e645a99e4a3520e280a,ntxv:0,isnm:False|UVIN-4877-UVOUT;n:type:ShaderForge.SFN_Panner,id:5404,x:32258,y:32949,varname:node_5404,prsc:2,spu:-0.2,spv:0.05|UVIN-6280-UVOUT;n:type:ShaderForge.SFN_Panner,id:8592,x:32263,y:33153,varname:node_8592,prsc:2,spu:0.08,spv:-0.05|UVIN-1447-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:6280,x:32049,y:32949,varname:node_6280,prsc:2,uv:0;n:type:ShaderForge.SFN_TexCoord,id:1447,x:32049,y:33153,varname:node_1447,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:3155,x:33294,y:32963,varname:node_3155,prsc:2|A-2577-OUT,B-1999-RGB,C-8219-RGB;n:type:ShaderForge.SFN_Multiply,id:9719,x:33432,y:32803,varname:node_9719,prsc:2|A-1177-OUT,B-3155-OUT;n:type:ShaderForge.SFN_Slider,id:3803,x:32559,y:32816,ptovrint:False,ptlb:node_3803,ptin:_node_3803,varname:node_3803,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:8.318727,max:10;n:type:ShaderForge.SFN_Color,id:8581,x:32763,y:32258,ptovrint:False,ptlb:node_8581,ptin:_node_8581,varname:node_8581,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Color,id:8219,x:33142,y:33237,ptovrint:False,ptlb:node_8219,ptin:_node_8219,varname:node_8219,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:1177,x:33129,y:32662,varname:node_1177,prsc:2|A-9834-OUT,B-3803-OUT;n:type:ShaderForge.SFN_Fresnel,id:3209,x:32391,y:32052,varname:node_3209,prsc:2;n:type:ShaderForge.SFN_Power,id:3129,x:32901,y:32151,varname:node_3129,prsc:2|VAL-3209-OUT,EXP-3417-OUT;n:type:ShaderForge.SFN_Add,id:6233,x:33543,y:32667,varname:node_6233,prsc:2|A-7278-OUT;n:type:ShaderForge.SFN_Slider,id:3417,x:32290,y:32279,ptovrint:False,ptlb:node_3417,ptin:_node_3417,varname:node_3417,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:10;n:type:ShaderForge.SFN_Multiply,id:9994,x:33280,y:32203,varname:node_9994,prsc:2|A-3129-OUT,B-8581-RGB;n:type:ShaderForge.SFN_Add,id:2577,x:32805,y:33003,varname:node_2577,prsc:2|A-6696-RGB,B-7420-RGB;n:type:ShaderForge.SFN_Panner,id:4877,x:32648,y:33382,varname:node_4877,prsc:2,spu:0.1,spv:0|UVIN-599-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:599,x:32406,y:33387,varname:node_599,prsc:2,uv:0;n:type:ShaderForge.SFN_Tex2d,id:4377,x:31581,y:32242,ptovrint:False,ptlb:node_6696_copy,ptin:_node_6696_copy,varname:_node_6696_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:13a779baa4e09e645a99e4a3520e280a,ntxv:0,isnm:False|UVIN-4949-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:1368,x:31560,y:32545,ptovrint:False,ptlb:node_7420_copy,ptin:_node_7420_copy,varname:_node_7420_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e8101847673c8eb459928172d10510cb,ntxv:0,isnm:False|UVIN-2148-UVOUT;n:type:ShaderForge.SFN_Panner,id:4949,x:31417,y:32292,varname:node_4949,prsc:2,spu:-0.01,spv:0.01|UVIN-101-UVOUT;n:type:ShaderForge.SFN_Panner,id:2148,x:31362,y:32545,varname:node_2148,prsc:2,spu:0,spv:-0.02|UVIN-8518-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:101,x:31140,y:32277,varname:node_101,prsc:2,uv:0;n:type:ShaderForge.SFN_TexCoord,id:8518,x:31106,y:32531,varname:node_8518,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:6140,x:32301,y:32391,varname:node_6140,prsc:2|A-7098-OUT,B-8383-OUT;n:type:ShaderForge.SFN_Multiply,id:7278,x:33484,y:32365,varname:node_7278,prsc:2|A-9994-OUT,B-5080-OUT,C-4874-RGB;n:type:ShaderForge.SFN_Tex2d,id:533,x:31365,y:32918,ptovrint:False,ptlb:node_6696_copy_copy,ptin:_node_6696_copy_copy,varname:_node_6696_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:13a779baa4e09e645a99e4a3520e280a,ntxv:0,isnm:False|UVIN-609-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:236,x:31365,y:33153,ptovrint:False,ptlb:node_7420_copy_copy,ptin:_node_7420_copy_copy,varname:_node_7420_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e8101847673c8eb459928172d10510cb,ntxv:0,isnm:False|UVIN-7632-UVOUT;n:type:ShaderForge.SFN_Panner,id:609,x:31146,y:32918,varname:node_609,prsc:2,spu:0.05,spv:0.1|UVIN-9088-UVOUT;n:type:ShaderForge.SFN_Panner,id:7632,x:31146,y:33153,varname:node_7632,prsc:2,spu:0.1,spv:0.05|UVIN-541-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:9088,x:30915,y:32918,varname:node_9088,prsc:2,uv:0;n:type:ShaderForge.SFN_TexCoord,id:541,x:30932,y:33153,varname:node_541,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:4139,x:31683,y:32975,varname:node_4139,prsc:2|A-533-RGB,B-236-RGB;n:type:ShaderForge.SFN_Tex2d,id:3002,x:31208,y:31764,ptovrint:False,ptlb:node_6696_copy_copy_copy,ptin:_node_6696_copy_copy_copy,varname:_node_6696_copy_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:13a779baa4e09e645a99e4a3520e280a,ntxv:0,isnm:False|UVIN-4477-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:8570,x:31208,y:31999,ptovrint:False,ptlb:node_7420_copy_copy_copy,ptin:_node_7420_copy_copy_copy,varname:_node_7420_copy_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e8101847673c8eb459928172d10510cb,ntxv:0,isnm:False|UVIN-4522-UVOUT;n:type:ShaderForge.SFN_Panner,id:4477,x:30989,y:31764,varname:node_4477,prsc:2,spu:0.1,spv:0.05|UVIN-7259-UVOUT;n:type:ShaderForge.SFN_Panner,id:4522,x:30989,y:31999,varname:node_4522,prsc:2,spu:0.05,spv:0.1|UVIN-6969-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:7259,x:30758,y:31764,varname:node_7259,prsc:2,uv:0;n:type:ShaderForge.SFN_TexCoord,id:6969,x:30775,y:31999,varname:node_6969,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:4896,x:31563,y:31909,varname:node_4896,prsc:2|A-3002-RGB,B-8570-RGB;n:type:ShaderForge.SFN_Multiply,id:7098,x:31973,y:32123,varname:node_7098,prsc:2|A-4896-OUT,B-4377-RGB;n:type:ShaderForge.SFN_Multiply,id:8383,x:31903,y:32680,varname:node_8383,prsc:2|A-1368-RGB,B-4139-OUT;n:type:ShaderForge.SFN_Slider,id:7718,x:32499,y:32643,ptovrint:False,ptlb:node_7718,ptin:_node_7718,varname:node_7718,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2,max:20;n:type:ShaderForge.SFN_Multiply,id:5080,x:32962,y:32464,varname:node_5080,prsc:2|A-4423-OUT,B-7718-OUT;n:type:ShaderForge.SFN_Power,id:4423,x:32513,y:32471,varname:node_4423,prsc:2|VAL-6140-OUT,EXP-7678-OUT;n:type:ShaderForge.SFN_Slider,id:7678,x:32119,y:32566,ptovrint:False,ptlb:node_7678,ptin:_node_7678,varname:node_7678,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.9698554,max:20;n:type:ShaderForge.SFN_VertexColor,id:4874,x:33589,y:33034,varname:node_4874,prsc:2;proporder:925-6696-7420-1999-3803-8581-8219-3417-4377-1368-3002-8570-533-236-7718-7678;pass:END;sub:END;*/

Shader "Shader Forge/pangxiefangyuzhao_xin" {
    Properties {
        _node_925 ("node_925", Range(0, 10)) = 4.76734
        _node_6696 ("node_6696", 2D) = "white" {}
        _node_7420 ("node_7420", 2D) = "white" {}
        _node_1999 ("node_1999", 2D) = "white" {}
        _node_3803 ("node_3803", Range(0, 10)) = 8.318727
        _node_8581 ("node_8581", Color) = (0.5,0.5,0.5,1)
        _node_8219 ("node_8219", Color) = (0.5,0.5,0.5,1)
        _node_3417 ("node_3417", Range(0, 10)) = 0
        _node_6696_copy ("node_6696_copy", 2D) = "white" {}
        _node_7420_copy ("node_7420_copy", 2D) = "white" {}
        _node_6696_copy_copy_copy ("node_6696_copy_copy_copy", 2D) = "white" {}
        _node_7420_copy_copy_copy ("node_7420_copy_copy_copy", 2D) = "white" {}
        _node_6696_copy_copy ("node_6696_copy_copy", 2D) = "white" {}
        _node_7420_copy_copy ("node_7420_copy_copy", 2D) = "white" {}
        _node_7718 ("node_7718", Range(0, 20)) = 2
        _node_7678 ("node_7678", Range(0, 20)) = 0.9698554
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
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
            Blend SrcAlpha One
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
            uniform float4 _node_8581;
            uniform float _node_3417;
            uniform sampler2D _node_6696_copy; uniform float4 _node_6696_copy_ST;
            uniform sampler2D _node_7420_copy; uniform float4 _node_7420_copy_ST;
            uniform sampler2D _node_6696_copy_copy; uniform float4 _node_6696_copy_copy_ST;
            uniform sampler2D _node_7420_copy_copy; uniform float4 _node_7420_copy_copy_ST;
            uniform sampler2D _node_6696_copy_copy_copy; uniform float4 _node_6696_copy_copy_copy_ST;
            uniform sampler2D _node_7420_copy_copy_copy; uniform float4 _node_7420_copy_copy_copy_ST;
            uniform float _node_7718;
            uniform float _node_7678;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 node_6594 = _Time + _TimeEditor;
                float2 node_4477 = (i.uv0+node_6594.g*float2(0.1,0.05));
                float4 _node_6696_copy_copy_copy_var = tex2D(_node_6696_copy_copy_copy,TRANSFORM_TEX(node_4477, _node_6696_copy_copy_copy));
                float2 node_4522 = (i.uv0+node_6594.g*float2(0.05,0.1));
                float4 _node_7420_copy_copy_copy_var = tex2D(_node_7420_copy_copy_copy,TRANSFORM_TEX(node_4522, _node_7420_copy_copy_copy));
                float2 node_4949 = (i.uv0+node_6594.g*float2(-0.01,0.01));
                float4 _node_6696_copy_var = tex2D(_node_6696_copy,TRANSFORM_TEX(node_4949, _node_6696_copy));
                float2 node_2148 = (i.uv0+node_6594.g*float2(0,-0.02));
                float4 _node_7420_copy_var = tex2D(_node_7420_copy,TRANSFORM_TEX(node_2148, _node_7420_copy));
                float2 node_609 = (i.uv0+node_6594.g*float2(0.05,0.1));
                float4 _node_6696_copy_copy_var = tex2D(_node_6696_copy_copy,TRANSFORM_TEX(node_609, _node_6696_copy_copy));
                float2 node_7632 = (i.uv0+node_6594.g*float2(0.1,0.05));
                float4 _node_7420_copy_copy_var = tex2D(_node_7420_copy_copy,TRANSFORM_TEX(node_7632, _node_7420_copy_copy));
                float3 node_7278 = ((pow((1.0-max(0,dot(normalDirection, viewDirection))),_node_3417)*_node_8581.rgb)*(pow((((_node_6696_copy_copy_copy_var.rgb*_node_7420_copy_copy_copy_var.rgb)*_node_6696_copy_var.rgb)+(_node_7420_copy_var.rgb*(_node_6696_copy_copy_var.rgb*_node_7420_copy_copy_var.rgb))),_node_7678)*_node_7718)*i.vertexColor.rgb);
                float3 emissive = node_7278;
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,i.vertexColor.a);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
