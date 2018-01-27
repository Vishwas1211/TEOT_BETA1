// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.27 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.27;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:34013,y:32763,varname:node_4013,prsc:2|emission-513-OUT,alpha-7748-OUT;n:type:ShaderForge.SFN_Tex2d,id:8334,x:32558,y:32932,ptovrint:False,ptlb:node_8334,ptin:_node_8334,varname:node_8334,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:714b292a136627f4bb8119356cbb626d,ntxv:0,isnm:False|UVIN-8970-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:679,x:32558,y:33149,ptovrint:False,ptlb:node_679,ptin:_node_679,varname:node_679,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1a2a58a64f053764ebf96a0e6db6cc3d,ntxv:0,isnm:False|UVIN-3442-UVOUT;n:type:ShaderForge.SFN_Panner,id:8970,x:32373,y:32875,varname:node_8970,prsc:2,spu:0.3,spv:0.15|UVIN-140-UVOUT;n:type:ShaderForge.SFN_Panner,id:3442,x:32342,y:33136,varname:node_3442,prsc:2,spu:0.1,spv:-0.25|UVIN-5214-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:140,x:32162,y:32802,varname:node_140,prsc:2,uv:0;n:type:ShaderForge.SFN_TexCoord,id:5214,x:32136,y:33079,varname:node_5214,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:1879,x:32817,y:33090,varname:node_1879,prsc:2|A-8334-R,B-679-R;n:type:ShaderForge.SFN_Fresnel,id:5308,x:32914,y:32559,varname:node_5308,prsc:2;n:type:ShaderForge.SFN_Slider,id:4369,x:32771,y:32723,ptovrint:False,ptlb:node_4369,ptin:_node_4369,varname:node_4369,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:3,max:6;n:type:ShaderForge.SFN_Power,id:6273,x:33157,y:32595,varname:node_6273,prsc:2|VAL-5308-OUT,EXP-4369-OUT;n:type:ShaderForge.SFN_Multiply,id:7165,x:33490,y:32779,varname:node_7165,prsc:2|A-6273-OUT,B-8463-RGB;n:type:ShaderForge.SFN_VertexColor,id:4190,x:33338,y:33071,varname:node_4190,prsc:2;n:type:ShaderForge.SFN_Multiply,id:513,x:33680,y:32869,varname:node_513,prsc:2|A-7165-OUT,B-4190-RGB;n:type:ShaderForge.SFN_Panner,id:768,x:32928,y:32848,varname:node_768,prsc:2,spu:0,spv:0|UVIN-4899-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:4899,x:32745,y:32848,varname:node_4899,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:3988,x:33098,y:32929,varname:node_3988,prsc:2|A-768-UVOUT,B-7842-OUT;n:type:ShaderForge.SFN_Vector1,id:5839,x:32876,y:33271,varname:node_5839,prsc:2,v1:2;n:type:ShaderForge.SFN_Power,id:4804,x:33083,y:33177,varname:node_4804,prsc:2|VAL-1879-OUT,EXP-5839-OUT;n:type:ShaderForge.SFN_Multiply,id:7842,x:33325,y:33255,varname:node_7842,prsc:2|A-4804-OUT,B-3576-OUT;n:type:ShaderForge.SFN_Vector1,id:3576,x:33127,y:33394,varname:node_3576,prsc:2,v1:1.5;n:type:ShaderForge.SFN_Tex2d,id:8463,x:33305,y:32882,ptovrint:False,ptlb:node_8463,ptin:_node_8463,varname:node_8463,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:714b292a136627f4bb8119356cbb626d,ntxv:0,isnm:False|UVIN-3988-OUT;n:type:ShaderForge.SFN_Multiply,id:7748,x:33749,y:33107,varname:node_7748,prsc:2|A-8463-A,B-4190-A;proporder:8334-679-4369-8463;pass:END;sub:END;*/

Shader "Shader Forge/feinieer_niuqu" {
    Properties {
        _node_8334 ("node_8334", 2D) = "white" {}
        _node_679 ("node_679", 2D) = "white" {}
        _node_4369 ("node_4369", Range(0, 6)) = 3
        _node_8463 ("node_8463", 2D) = "white" {}
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
            uniform sampler2D _node_8334; uniform float4 _node_8334_ST;
            uniform sampler2D _node_679; uniform float4 _node_679_ST;
            uniform float _node_4369;
            uniform sampler2D _node_8463; uniform float4 _node_8463_ST;
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
                float4 node_3001 = _Time + _TimeEditor;
                float2 node_8970 = (i.uv0+node_3001.g*float2(0.3,0.15));
                float4 _node_8334_var = tex2D(_node_8334,TRANSFORM_TEX(node_8970, _node_8334));
                float2 node_3442 = (i.uv0+node_3001.g*float2(0.1,-0.25));
                float4 _node_679_var = tex2D(_node_679,TRANSFORM_TEX(node_3442, _node_679));
                float2 node_3988 = ((i.uv0+node_3001.g*float2(0,0))+(pow((_node_8334_var.r*_node_679_var.r),2.0)*1.5));
                float4 _node_8463_var = tex2D(_node_8463,TRANSFORM_TEX(node_3988, _node_8463));
                float3 emissive = ((pow((1.0-max(0,dot(normalDirection, viewDirection))),_node_4369)*_node_8463_var.rgb)*i.vertexColor.rgb);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(_node_8463_var.a*i.vertexColor.a));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
