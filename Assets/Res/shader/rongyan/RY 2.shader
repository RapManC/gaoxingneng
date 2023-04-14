// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33931,y:32208,varname:node_3138,prsc:2|emission-5268-OUT,custl-2262-OUT,clip-5944-OUT,voffset-9888-OUT;n:type:ShaderForge.SFN_TexCoord,id:5459,x:31643,y:31729,varname:node_5459,prsc:2,uv:0,uaff:True;n:type:ShaderForge.SFN_Sqrt,id:9669,x:31885,y:31754,varname:node_9669,prsc:2|IN-5459-W;n:type:ShaderForge.SFN_Power,id:8701,x:32196,y:31806,varname:node_8701,prsc:2|VAL-9669-OUT,EXP-1377-OUT;n:type:ShaderForge.SFN_Vector1,id:1377,x:32051,y:31868,varname:node_1377,prsc:2,v1:0;n:type:ShaderForge.SFN_Time,id:66,x:31378,y:31995,varname:node_66,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3619,x:31643,y:31965,varname:node_3619,prsc:2|A-9274-OUT,B-66-T;n:type:ShaderForge.SFN_Add,id:5011,x:31850,y:31965,varname:node_5011,prsc:2|A-5459-UVOUT,B-3619-OUT;n:type:ShaderForge.SFN_Tex2d,id:4332,x:32051,y:31984,ptovrint:False,ptlb:node_4332,ptin:_node_4332,varname:_node_4332,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:f8ff8b40639756941b347da7a0d1df50,ntxv:0,isnm:False|UVIN-5011-OUT,MIP-5768-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5768,x:31810,y:32128,ptovrint:False,ptlb:node_5768,ptin:_node_5768,varname:_node_5768,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Vector2,id:9274,x:31458,y:31880,varname:node_9274,prsc:2,v1:0,v2:0.5;n:type:ShaderForge.SFN_Vector2,id:677,x:31434,y:32238,varname:node_677,prsc:2,v1:0,v2:0.6;n:type:ShaderForge.SFN_Multiply,id:2578,x:31686,y:32190,varname:node_2578,prsc:2|A-9483-OUT,B-677-OUT,C-66-TDB;n:type:ShaderForge.SFN_Vector2,id:9483,x:31434,y:32143,varname:node_9483,prsc:2,v1:0,v2:0.6;n:type:ShaderForge.SFN_Add,id:1661,x:31853,y:32217,varname:node_1661,prsc:2|A-5459-UVOUT,B-2578-OUT;n:type:ShaderForge.SFN_Tex2d,id:8216,x:32069,y:32200,ptovrint:False,ptlb:node_8216,ptin:_node_8216,varname:_node_8216,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:f8ff8b40639756941b347da7a0d1df50,ntxv:0,isnm:False|UVIN-1661-OUT,MIP-5768-OUT;n:type:ShaderForge.SFN_Multiply,id:5361,x:32427,y:31986,varname:node_5361,prsc:2|A-3249-RGB,B-4332-RGB;n:type:ShaderForge.SFN_Color,id:3249,x:32203,y:32061,ptovrint:False,ptlb:node_3249,ptin:_node_3249,varname:_node_3249,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5566038,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:4400,x:32427,y:32148,varname:node_4400,prsc:2|A-3249-RGB,B-8216-RGB;n:type:ShaderForge.SFN_Multiply,id:5174,x:32646,y:32100,varname:node_5174,prsc:2|A-5361-OUT,B-4400-OUT;n:type:ShaderForge.SFN_Add,id:3024,x:32679,y:31910,varname:node_3024,prsc:2|A-833-OUT,B-5361-OUT,C-5174-OUT;n:type:ShaderForge.SFN_Multiply,id:2262,x:33026,y:31884,varname:node_2262,prsc:2|A-2867-RGB,B-3913-OUT;n:type:ShaderForge.SFN_Color,id:2867,x:32711,y:31685,ptovrint:False,ptlb:node_2867,ptin:_node_2867,varname:_node_2867,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5377358,c2:0.5377358,c3:0.5377358,c4:1;n:type:ShaderForge.SFN_Posterize,id:3913,x:32865,y:31910,varname:node_3913,prsc:2|IN-3024-OUT,STPS-2417-OUT;n:type:ShaderForge.SFN_Vector1,id:2417,x:32814,y:32076,varname:node_2417,prsc:2,v1:7;n:type:ShaderForge.SFN_Color,id:778,x:32246,y:31635,ptovrint:False,ptlb:node_778,ptin:_node_778,varname:_node_778,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:833,x:32427,y:31806,varname:node_833,prsc:2|A-778-A,B-8701-OUT;n:type:ShaderForge.SFN_Slider,id:9419,x:31639,y:32493,ptovrint:False,ptlb:node_9419,ptin:_node_9419,varname:_node_9419,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.3716197,max:1;n:type:ShaderForge.SFN_Multiply,id:3641,x:32041,y:32441,varname:node_3641,prsc:2|A-66-TSL,B-9419-OUT;n:type:ShaderForge.SFN_Power,id:4838,x:32318,y:32373,varname:node_4838,prsc:2|VAL-9669-OUT,EXP-3641-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:4283,x:32300,y:32549,ptovrint:False,ptlb:node_4283,ptin:_node_4283,varname:_node_4283,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:f8ff8b40639756941b347da7a0d1df50,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:9226,x:32614,y:32430,varname:node_9226,prsc:2,tex:f8ff8b40639756941b347da7a0d1df50,ntxv:0,isnm:False|MIP-4838-OUT,TEX-4283-TEX;n:type:ShaderForge.SFN_NormalVector,id:3926,x:32614,y:32586,prsc:2,pt:False;n:type:ShaderForge.SFN_Slider,id:4006,x:32457,y:32776,ptovrint:False,ptlb:node_4006,ptin:_node_4006,varname:_node_4006,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1905064,max:0.5;n:type:ShaderForge.SFN_Multiply,id:9888,x:32916,y:32591,varname:node_9888,prsc:2|A-9226-RGB,B-3926-OUT,C-4006-OUT;n:type:ShaderForge.SFN_Slider,id:4808,x:32547,y:32305,ptovrint:False,ptlb:node_4808,ptin:_node_4808,varname:_node_4808,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:5268,x:33000,y:32386,varname:node_5268,prsc:2|A-4808-OUT,B-9226-RGB;n:type:ShaderForge.SFN_Tex2d,id:3934,x:33206,y:32662,ptovrint:False,ptlb:node_3934,ptin:_node_3934,varname:_node_3934,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:47bec2dc37f4d2e4bbc4821920e12766,ntxv:3,isnm:False;n:type:ShaderForge.SFN_Slider,id:5065,x:32890,y:32828,ptovrint:False,ptlb:node_5065,ptin:_node_5065,varname:_node_5065,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0.08509894,max:1;n:type:ShaderForge.SFN_Blend,id:5944,x:33474,y:32625,varname:node_5944,prsc:2,blmd:10,clmp:False|SRC-3934-R,DST-5055-OUT;n:type:ShaderForge.SFN_Time,id:615,x:32890,y:32943,varname:node_615,prsc:2;n:type:ShaderForge.SFN_Add,id:5055,x:33398,y:32865,varname:node_5055,prsc:2|A-5065-OUT,B-4879-OUT;n:type:ShaderForge.SFN_Multiply,id:4879,x:33140,y:32975,varname:node_4879,prsc:2|A-615-TSL,B-2818-OUT;n:type:ShaderForge.SFN_Vector1,id:2818,x:32905,y:33094,varname:node_2818,prsc:2,v1:2;proporder:4332-5768-8216-3249-2867-778-9419-4283-4808-4006-3934-5065;pass:END;sub:END;*/

Shader "Shader Forge/RY" {
    Properties {
        _node_4332 ("node_4332", 2D) = "white" {}
        _node_5768 ("node_5768", Float ) = 0
        _node_8216 ("node_8216", 2D) = "white" {}
        [HDR]_node_3249 ("node_3249", Color) = (0.5566038,0,0,1)
        [HDR]_node_2867 ("node_2867", Color) = (0.5377358,0.5377358,0.5377358,1)
        _node_778 ("node_778", Color) = (1,1,1,1)
        _node_9419 ("node_9419", Range(0, 1)) = 0.3716197
        _node_4283 ("node_4283", 2D) = "white" {}
        _node_4808 ("node_4808", Range(0, 1)) = 0
        _node_4006 ("node_4006", Range(0, 0.5)) = 0.1905064
        _node_3934 ("node_3934", 2D) = "bump" {}
        _node_5065 ("node_5065", Range(-1, 1)) = 0.08509894
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
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma target 3.0
            uniform sampler2D _node_4332; uniform float4 _node_4332_ST;
            uniform sampler2D _node_8216; uniform float4 _node_8216_ST;
            uniform sampler2D _node_4283; uniform float4 _node_4283_ST;
            uniform sampler2D _node_3934; uniform float4 _node_3934_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _node_5768)
                UNITY_DEFINE_INSTANCED_PROP( float4, _node_3249)
                UNITY_DEFINE_INSTANCED_PROP( float4, _node_2867)
                UNITY_DEFINE_INSTANCED_PROP( float4, _node_778)
                UNITY_DEFINE_INSTANCED_PROP( float, _node_9419)
                UNITY_DEFINE_INSTANCED_PROP( float, _node_4006)
                UNITY_DEFINE_INSTANCED_PROP( float, _node_4808)
                UNITY_DEFINE_INSTANCED_PROP( float, _node_5065)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 uv0 : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float node_9669 = sqrt(o.uv0.a);
                float4 node_66 = _Time;
                float _node_9419_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_9419 );
                float4 node_9226 = tex2Dlod(_node_4283,float4(TRANSFORM_TEX(o.uv0, _node_4283),0.0,pow(node_9669,(node_66.r*_node_9419_var))));
                float _node_4006_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_4006 );
                v.vertex.xyz += (node_9226.rgb*v.normal*_node_4006_var);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float4 _node_3934_var = tex2D(_node_3934,TRANSFORM_TEX(i.uv0, _node_3934));
                float _node_5065_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_5065 );
                float4 node_615 = _Time;
                clip(( (_node_5065_var+(node_615.r*2.0)) > 0.5 ? (1.0-(1.0-2.0*((_node_5065_var+(node_615.r*2.0))-0.5))*(1.0-_node_3934_var.r)) : (2.0*(_node_5065_var+(node_615.r*2.0))*_node_3934_var.r) ) - 0.5);
////// Lighting:
////// Emissive:
                float _node_4808_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_4808 );
                float node_9669 = sqrt(i.uv0.a);
                float4 node_66 = _Time;
                float _node_9419_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_9419 );
                float4 node_9226 = tex2Dlod(_node_4283,float4(TRANSFORM_TEX(i.uv0, _node_4283),0.0,pow(node_9669,(node_66.r*_node_9419_var))));
                float3 emissive = (_node_4808_var*node_9226.rgb);
                float4 _node_2867_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_2867 );
                float4 _node_778_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_778 );
                float4 _node_3249_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_3249 );
                float2 node_5011 = (i.uv0+(float2(0,0.5)*node_66.g));
                float _node_5768_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_5768 );
                float4 _node_4332_var = tex2Dlod(_node_4332,float4(TRANSFORM_TEX(node_5011, _node_4332),0.0,_node_5768_var));
                float3 node_5361 = (_node_3249_var.rgb*_node_4332_var.rgb);
                float2 node_1661 = (i.uv0+(float2(0,0.6)*float2(0,0.6)*node_66.b));
                float4 _node_8216_var = tex2Dlod(_node_8216,float4(TRANSFORM_TEX(node_1661, _node_8216),0.0,_node_5768_var));
                float node_2417 = 7.0;
                float3 finalColor = emissive + (_node_2867_var.rgb*floor(((_node_778_var.a*pow(node_9669,0.0))+node_5361+(node_5361*(_node_3249_var.rgb*_node_8216_var.rgb))) * node_2417) / (node_2417 - 1));
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
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma target 3.0
            uniform sampler2D _node_4283; uniform float4 _node_4283_ST;
            uniform sampler2D _node_3934; uniform float4 _node_3934_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _node_9419)
                UNITY_DEFINE_INSTANCED_PROP( float, _node_4006)
                UNITY_DEFINE_INSTANCED_PROP( float, _node_5065)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 uv0 : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float node_9669 = sqrt(o.uv0.a);
                float4 node_66 = _Time;
                float _node_9419_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_9419 );
                float4 node_9226 = tex2Dlod(_node_4283,float4(TRANSFORM_TEX(o.uv0, _node_4283),0.0,pow(node_9669,(node_66.r*_node_9419_var))));
                float _node_4006_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_4006 );
                v.vertex.xyz += (node_9226.rgb*v.normal*_node_4006_var);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float4 _node_3934_var = tex2D(_node_3934,TRANSFORM_TEX(i.uv0, _node_3934));
                float _node_5065_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_5065 );
                float4 node_615 = _Time;
                clip(( (_node_5065_var+(node_615.r*2.0)) > 0.5 ? (1.0-(1.0-2.0*((_node_5065_var+(node_615.r*2.0))-0.5))*(1.0-_node_3934_var.r)) : (2.0*(_node_5065_var+(node_615.r*2.0))*_node_3934_var.r) ) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
