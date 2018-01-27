// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.27 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.27;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:1,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:33291,y:32686,varname:node_4795,prsc:2|emission-4037-RGB,alpha-8123-OUT,refract-6033-OUT;n:type:ShaderForge.SFN_Multiply,id:6033,x:32701,y:33100,varname:node_6033,prsc:2|A-8228-OUT,B-5968-OUT;n:type:ShaderForge.SFN_Tex2d,id:4469,x:32232,y:33014,ptovrint:False,ptlb:node_4469,ptin:_node_4469,varname:node_4469,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7c62d0493949c19448ca1acf3fbc3906,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Append,id:8228,x:32457,y:33014,varname:node_8228,prsc:2|A-4469-R,B-4469-G;n:type:ShaderForge.SFN_Slider,id:5968,x:32317,y:33206,ptovrint:False,ptlb:node_5968,ptin:_node_5968,varname:node_5968,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.238929,max:2;n:type:ShaderForge.SFN_Vector1,id:3982,x:32597,y:32864,varname:node_3982,prsc:2,v1:0;n:type:ShaderForge.SFN_VertexColor,id:4037,x:32568,y:32731,varname:node_4037,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8123,x:32957,y:32949,varname:node_8123,prsc:2|A-4037-A,B-4469-A,C-3982-OUT;proporder:4469-5968;pass:END;sub:END;*/

Shader "Shader Forge/kongqi_niuqu-xin" {
    Properties {
        _node_4469 ("node_4469", 2D) = "white" {}
        _node_5968 ("node_5968", Range(0, 2)) = 1.238929
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha Zero
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
            uniform sampler2D _GrabTexture;
            uniform sampler2D _node_4469; uniform float4 _node_4469_ST;
            uniform float _node_5968;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float4 _node_4469_var = tex2D(_node_4469,TRANSFORM_TEX(i.uv0, _node_4469));
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (float2(_node_4469_var.r,_node_4469_var.g)*_node_5968);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
////// Emissive:
                float3 emissive = i.vertexColor.rgb;
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(lerp(sceneColor.rgb, finalColor,(i.vertexColor.a*_node_4469_var.a*0.0)),1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
