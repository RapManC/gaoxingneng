// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33476,y:32509,varname:node_3138,prsc:2|emission-3523-OUT,custl-9933-OUT,clip-6391-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:7877,x:31284,y:32572,varname:node_7877,prsc:2;n:type:ShaderForge.SFN_ObjectPosition,id:5162,x:31271,y:32764,varname:node_5162,prsc:2;n:type:ShaderForge.SFN_Subtract,id:5340,x:31522,y:32698,varname:node_5340,prsc:2|A-7877-XYZ,B-5162-XYZ;n:type:ShaderForge.SFN_ComponentMask,id:2377,x:31781,y:32606,varname:node_2377,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-5340-OUT;n:type:ShaderForge.SFN_ComponentMask,id:5940,x:31781,y:32782,varname:node_5940,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-5340-OUT;n:type:ShaderForge.SFN_Add,id:6415,x:31816,y:32446,varname:node_6415,prsc:2|A-6284-OUT,B-2377-OUT;n:type:ShaderForge.SFN_Sin,id:6647,x:32019,y:32446,varname:node_6647,prsc:2|IN-6415-OUT;n:type:ShaderForge.SFN_Multiply,id:6681,x:32326,y:32448,varname:node_6681,prsc:2|A-6647-OUT,B-6628-OUT;n:type:ShaderForge.SFN_Add,id:2264,x:32118,y:32697,varname:node_2264,prsc:2|A-6681-OUT,B-5940-OUT;n:type:ShaderForge.SFN_Step,id:8321,x:32364,y:32715,varname:node_8321,prsc:2|A-2264-OUT,B-9102-OUT;n:type:ShaderForge.SFN_Slider,id:9102,x:31920,y:32899,ptovrint:False,ptlb:node_9102,ptin:_node_9102,varname:node_9102,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0.1565212,max:1;n:type:ShaderForge.SFN_Multiply,id:9933,x:32708,y:32977,varname:node_9933,prsc:2|A-4546-RGB,B-4761-RGB;n:type:ShaderForge.SFN_Tex2d,id:4546,x:32341,y:32911,ptovrint:False,ptlb:node_4546,ptin:_node_4546,varname:node_4546,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:4761,x:32341,y:33098,ptovrint:False,ptlb:node_4761,ptin:_node_4761,varname:node_4761,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9622642,c2:0.4031796,c3:0,c4:0.5686275;n:type:ShaderForge.SFN_Multiply,id:3523,x:33065,y:32595,varname:node_3523,prsc:2|A-2877-RGB,B-4546-A;n:type:ShaderForge.SFN_Color,id:2877,x:32702,y:32551,ptovrint:False,ptlb:node_2877,ptin:_node_2877,varname:node_2877,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.2253238,c3:0,c4:1;n:type:ShaderForge.SFN_FaceSign,id:601,x:32315,y:31623,varname:node_601,prsc:2,fstp:1;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:7639,x:32612,y:31806,varname:node_7639,prsc:2|IN-601-VFACE,IMIN-3706-OUT,IMAX-3149-OUT,OMIN-7189-OUT,OMAX-3149-OUT;n:type:ShaderForge.SFN_Vector1,id:3706,x:32277,y:31793,varname:node_3706,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:3149,x:32381,y:31964,varname:node_3149,prsc:2,v1:1;n:type:ShaderForge.SFN_Slider,id:7189,x:32027,y:31924,ptovrint:False,ptlb:node_7189,ptin:_node_7189,varname:node_7189,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.3683848,max:1;n:type:ShaderForge.SFN_Slider,id:6284,x:31480,y:32433,ptovrint:False,ptlb:node_6284,ptin:_node_6284,varname:node_6284,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:3.043481,max:5;n:type:ShaderForge.SFN_Slider,id:6628,x:31982,y:32606,ptovrint:False,ptlb:node_6628,ptin:_node_6628,varname:node_6628,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:6391,x:32885,y:32795,varname:node_6391,prsc:2|A-8321-OUT,B-9294-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9294,x:32608,y:32831,ptovrint:False,ptlb:node_9294,ptin:_node_9294,varname:node_9294,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:3026,x:33039,y:32096,varname:node_3026,prsc:2|A-2233-OUT;n:type:ShaderForge.SFN_TexCoord,id:1222,x:32131,y:32096,varname:node_1222,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Power,id:7165,x:32548,y:32116,varname:node_7165,prsc:2|VAL-1222-UVOUT,EXP-3712-OUT;n:type:ShaderForge.SFN_Slider,id:3712,x:32110,y:32273,ptovrint:False,ptlb:node_3712,ptin:_node_3712,varname:node_3712,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:2233,x:32807,y:32001,varname:node_2233,prsc:2|A-7639-OUT,B-7165-OUT;proporder:9102-4546-4761-2877-7189-6284-6628-9294-3712;pass:END;sub:END;*/

Shader "Shader Forge/NewShader" {
    Properties {
        _node_9102 ("node_9102", Range(-1, 1)) = 0.1565212
        _node_4546 ("node_4546", 2D) = "white" {}
        [HDR]_node_4761 ("node_4761", Color) = (0.9622642,0.4031796,0,0.5686275)
        _node_2877 ("node_2877", Color) = (1,0.2253238,0,1)
        _node_7189 ("node_7189", Range(0, 1)) = 0.3683848
        _node_6284 ("node_6284", Range(0, 5)) = 3.043481
        _node_6628 ("node_6628", Range(0, 1)) = 0
        _node_9294 ("node_9294", Float ) = 1
        _node_3712 ("node_3712", Range(0, 1)) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma target 3.0
            uniform sampler2D _node_4546; uniform float4 _node_4546_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _node_9102)
                UNITY_DEFINE_INSTANCED_PROP( float4, _node_4761)
                UNITY_DEFINE_INSTANCED_PROP( float4, _node_2877)
                UNITY_DEFINE_INSTANCED_PROP( float, _node_6284)
                UNITY_DEFINE_INSTANCED_PROP( float, _node_6628)
                UNITY_DEFINE_INSTANCED_PROP( float, _node_9294)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                float _node_6284_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_6284 );
                float3 node_5340 = (i.posWorld.rgb-objPos.rgb);
                float _node_6628_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_6628 );
                float _node_9102_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_9102 );
                float _node_9294_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_9294 );
                clip((step(((sin((_node_6284_var+node_5340.r))*_node_6628_var)+node_5340.g),_node_9102_var)*_node_9294_var) - 0.5);
////// Lighting:
////// Emissive:
                float4 _node_2877_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_2877 );
                float4 _node_4546_var = tex2D(_node_4546,TRANSFORM_TEX(i.uv0, _node_4546));
                float3 emissive = (_node_2877_var.rgb*_node_4546_var.a);
                float4 _node_4761_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_4761 );
                float3 finalColor = emissive + (_node_4546_var.rgb*_node_4761_var.rgb);
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma target 3.0
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _node_9102)
                UNITY_DEFINE_INSTANCED_PROP( float, _node_6284)
                UNITY_DEFINE_INSTANCED_PROP( float, _node_6628)
                UNITY_DEFINE_INSTANCED_PROP( float, _node_9294)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 posWorld : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                float _node_6284_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_6284 );
                float3 node_5340 = (i.posWorld.rgb-objPos.rgb);
                float _node_6628_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_6628 );
                float _node_9102_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_9102 );
                float _node_9294_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_9294 );
                clip((step(((sin((_node_6284_var+node_5340.r))*_node_6628_var)+node_5340.g),_node_9102_var)*_node_9294_var) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
