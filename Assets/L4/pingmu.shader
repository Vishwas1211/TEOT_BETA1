// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.27 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.27;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33420,y:32688,varname:node_4013,prsc:2|diff-6434-OUT,emission-708-OUT,alpha-7110-OUT;n:type:ShaderForge.SFN_Tex2d,id:871,x:32346,y:33141,ptovrint:False,ptlb:node_871,ptin:_node_871,varname:node_871,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Panner,id:9449,x:32274,y:33005,varname:node_9449,prsc:2,spu:0.05,spv:0.05|UVIN-4723-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:4723,x:32025,y:33005,varname:node_4723,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:589,x:32726,y:33022,varname:node_589,prsc:2|A-5852-RGB,B-7411-OUT;n:type:ShaderForge.SFN_Tex2d,id:5852,x:32478,y:33005,ptovrint:False,ptlb:node_5852,ptin:_node_5852,varname:node_5852,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-9449-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:4814,x:32649,y:32168,ptovrint:False,ptlb:node_4814,ptin:_node_4814,varname:node_4814,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:af1d6ae95f1f0794a95eb2117a42e224,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:720,x:32642,y:32764,ptovrint:False,ptlb:node_4814_copy,ptin:_node_4814_copy,varname:_node_4814_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:af1d6ae95f1f0794a95eb2117a42e224,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:708,x:32990,y:32807,varname:node_708,prsc:2|A-720-A,B-6548-OUT;n:type:ShaderForge.SFN_Multiply,id:7929,x:32900,y:32283,varname:node_7929,prsc:2|A-4814-A,B-6542-RGB;n:type:ShaderForge.SFN_Vector1,id:6249,x:32738,y:33267,varname:node_6249,prsc:2,v1:5;n:type:ShaderForge.SFN_Multiply,id:2500,x:32959,y:33156,varname:node_2500,prsc:2|A-589-OUT,B-6249-OUT;n:type:ShaderForge.SFN_Tex2d,id:8072,x:32376,y:33540,ptovrint:False,ptlb:node_871_copy,ptin:_node_871_copy,varname:_node_871_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Panner,id:9756,x:32170,y:33369,varname:node_9756,prsc:2,spu:0,spv:0.01|UVIN-8950-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:8950,x:31934,y:33397,varname:node_8950,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:4272,x:32706,y:33367,varname:node_4272,prsc:2|A-5164-RGB,B-8072-RGB;n:type:ShaderForge.SFN_Tex2d,id:5164,x:32376,y:33351,ptovrint:False,ptlb:node_5852_copy,ptin:_node_5852_copy,varname:_node_5852_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-9756-UVOUT;n:type:ShaderForge.SFN_Vector1,id:7186,x:32591,y:33651,varname:node_7186,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:5581,x:33014,y:33401,varname:node_5581,prsc:2|A-4272-OUT,B-7186-OUT;n:type:ShaderForge.SFN_Add,id:6548,x:33260,y:33245,varname:node_6548,prsc:2|A-2500-OUT,B-5581-OUT;n:type:ShaderForge.SFN_VertexColor,id:6542,x:32760,y:32553,varname:node_6542,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7110,x:33046,y:32607,varname:node_7110,prsc:2|A-4814-A,B-6542-A;n:type:ShaderForge.SFN_Fresnel,id:146,x:32132,y:32337,varname:node_146,prsc:2;n:type:ShaderForge.SFN_Slider,id:1273,x:32047,y:32539,ptovrint:False,ptlb:node_1273,ptin:_node_1273,varname:node_1273,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.430952,max:10;n:type:ShaderForge.SFN_Multiply,id:3846,x:32370,y:32353,varname:node_3846,prsc:2|A-146-OUT,B-1273-OUT;n:type:ShaderForge.SFN_Vector3,id:4683,x:32391,y:32609,varname:node_4683,prsc:2,v1:0,v2:0.1233265,v3:0.9411765;n:type:ShaderForge.SFN_Multiply,id:6434,x:32636,y:32442,varname:node_6434,prsc:2|A-3846-OUT,B-4683-OUT;n:type:ShaderForge.SFN_Vector3,id:4982,x:32478,y:33299,varname:node_4982,prsc:2,v1:0,v2:0.3230227,v3:0.9558824;n:type:ShaderForge.SFN_Multiply,id:7411,x:32636,y:33146,varname:node_7411,prsc:2|A-871-RGB,B-4982-OUT;proporder:871-5852-4814-720-8072-5164-1273;pass:END;sub:END;*/

Shader "Shader Forge/pingmu" {
    Properties {
        _node_871 ("node_871", 2D) = "white" {}
        _node_5852 ("node_5852", 2D) = "white" {}
        _node_4814 ("node_4814", 2D) = "white" {}
        _node_4814_copy ("node_4814_copy", 2D) = "white" {}
        _node_871_copy ("node_871_copy", 2D) = "white" {}
        _node_5852_copy ("node_5852_copy", 2D) = "white" {}
        _node_1273 ("node_1273", Range(0, 10)) = 0.430952
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
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _node_871; uniform float4 _node_871_ST;
            uniform sampler2D _node_5852; uniform float4 _node_5852_ST;
            uniform sampler2D _node_4814; uniform float4 _node_4814_ST;
            uniform sampler2D _node_4814_copy; uniform float4 _node_4814_copy_ST;
            uniform sampler2D _node_871_copy; uniform float4 _node_871_copy_ST;
            uniform sampler2D _node_5852_copy; uniform float4 _node_5852_copy_ST;
            uniform float _node_1273;
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
                float3 lightColor = _LightColor0.rgb;
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
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuseColor = (((1.0-max(0,dot(normalDirection, viewDirection)))*_node_1273)*float3(0,0.1233265,0.9411765));
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 _node_4814_copy_var = tex2D(_node_4814_copy,TRANSFORM_TEX(i.uv0, _node_4814_copy));
                float4 node_8291 = _Time + _TimeEditor;
                float2 node_9449 = (i.uv0+node_8291.g*float2(0.05,0.05));
                float4 _node_5852_var = tex2D(_node_5852,TRANSFORM_TEX(node_9449, _node_5852));
                float4 _node_871_var = tex2D(_node_871,TRANSFORM_TEX(i.uv0, _node_871));
                float2 node_9756 = (i.uv0+node_8291.g*float2(0,0.01));
                float4 _node_5852_copy_var = tex2D(_node_5852_copy,TRANSFORM_TEX(node_9756, _node_5852_copy));
                float4 _node_871_copy_var = tex2D(_node_871_copy,TRANSFORM_TEX(i.uv0, _node_871_copy));
                float3 emissive = (_node_4814_copy_var.a*(((_node_5852_var.rgb*(_node_871_var.rgb*float3(0,0.3230227,0.9558824)))*5.0)+((_node_5852_copy_var.rgb*_node_871_copy_var.rgb)*2.0)));
/// Final Color:
                float3 finalColor = diffuse + emissive;
                float4 _node_4814_var = tex2D(_node_4814,TRANSFORM_TEX(i.uv0, _node_4814));
                fixed4 finalRGBA = fixed4(finalColor,(_node_4814_var.a*i.vertexColor.a));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _node_871; uniform float4 _node_871_ST;
            uniform sampler2D _node_5852; uniform float4 _node_5852_ST;
            uniform sampler2D _node_4814; uniform float4 _node_4814_ST;
            uniform sampler2D _node_4814_copy; uniform float4 _node_4814_copy_ST;
            uniform sampler2D _node_871_copy; uniform float4 _node_871_copy_ST;
            uniform sampler2D _node_5852_copy; uniform float4 _node_5852_copy_ST;
            uniform float _node_1273;
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
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 diffuseColor = (((1.0-max(0,dot(normalDirection, viewDirection)))*_node_1273)*float3(0,0.1233265,0.9411765));
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                float4 _node_4814_var = tex2D(_node_4814,TRANSFORM_TEX(i.uv0, _node_4814));
                fixed4 finalRGBA = fixed4(finalColor * (_node_4814_var.a*i.vertexColor.a),0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
