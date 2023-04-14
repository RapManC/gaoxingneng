// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33345,y:32580,varname:node_3138,prsc:2|custl-2691-OUT,clip-5191-OUT,voffset-5498-OUT;n:type:ShaderForge.SFN_TexCoord,id:319,x:31443,y:32614,varname:node_319,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Vector2,id:5136,x:31310,y:32941,varname:node_5136,prsc:2,v1:0.5,v2:1;n:type:ShaderForge.SFN_Distance,id:3406,x:31476,y:32941,varname:node_3406,prsc:2|A-319-UVOUT,B-5136-OUT;n:type:ShaderForge.SFN_OneMinus,id:3067,x:31650,y:32941,varname:node_3067,prsc:2|IN-3406-OUT;n:type:ShaderForge.SFN_Tex2d,id:695,x:31902,y:32607,varname:node_695,prsc:2,tex:f8ff8b40639756941b347da7a0d1df50,ntxv:0,isnm:False|UVIN-3040-UVOUT,TEX-2829-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:2829,x:31131,y:32929,ptovrint:False,ptlb:node_2829,ptin:_node_2829,varname:node_2829,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:f8ff8b40639756941b347da7a0d1df50,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:3065,x:31930,y:32822,varname:node_3065,prsc:2|A-695-R,B-378-OUT;n:type:ShaderForge.SFN_Panner,id:3040,x:31708,y:32526,varname:node_3040,prsc:2,spu:0,spv:0.5|UVIN-319-UVOUT,DIST-5739-OUT;n:type:ShaderForge.SFN_Step,id:5191,x:32652,y:32940,varname:node_5191,prsc:2|A-3370-OUT,B-3333-OUT;n:type:ShaderForge.SFN_Multiply,id:9894,x:32949,y:32977,varname:node_9894,prsc:2|A-5191-OUT,B-6987-RGB;n:type:ShaderForge.SFN_Color,id:6987,x:32540,y:33089,ptovrint:False,ptlb:node_6987,ptin:_node_6987,varname:node_6987,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.4622642,c2:0.08354172,c3:0,c4:1;n:type:ShaderForge.SFN_Add,id:2691,x:32715,y:32715,varname:node_2691,prsc:2|A-6838-OUT,B-9894-OUT;n:type:ShaderForge.SFN_Color,id:7826,x:32308,y:32418,ptovrint:False,ptlb:node_7826,ptin:_node_7826,varname:node_7826,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.5240722,c3:0,c4:1;n:type:ShaderForge.SFN_Tex2d,id:4265,x:32273,y:32654,varname:node_4265,prsc:2,tex:f8ff8b40639756941b347da7a0d1df50,ntxv:0,isnm:False|UVIN-3040-UVOUT,TEX-2829-TEX;n:type:ShaderForge.SFN_Multiply,id:6838,x:32599,y:32500,varname:node_6838,prsc:2|A-7826-RGB,B-4265-RGB;n:type:ShaderForge.SFN_Power,id:378,x:31696,y:33098,varname:node_378,prsc:2|VAL-3067-OUT,EXP-3425-OUT;n:type:ShaderForge.SFN_Multiply,id:3806,x:32777,y:33365,varname:node_3806,prsc:2|A-4420-XYZ,B-360-OUT;n:type:ShaderForge.SFN_Multiply,id:5498,x:32975,y:33164,varname:node_5498,prsc:2|A-680-OUT,B-5702-OUT;n:type:ShaderForge.SFN_TexCoord,id:2655,x:31795,y:33494,varname:node_2655,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Vector2,id:2302,x:31795,y:33641,varname:node_2302,prsc:2,v1:0,v2:0.5;n:type:ShaderForge.SFN_Time,id:6553,x:31795,y:33740,varname:node_6553,prsc:2;n:type:ShaderForge.SFN_Vector2,id:5636,x:31795,y:33870,varname:node_5636,prsc:2,v1:0,v2:0.5;n:type:ShaderForge.SFN_Multiply,id:9819,x:32012,y:33667,varname:node_9819,prsc:2|A-2302-OUT,B-6553-T;n:type:ShaderForge.SFN_Multiply,id:8283,x:32012,y:33785,varname:node_8283,prsc:2|A-6553-T,B-5636-OUT;n:type:ShaderForge.SFN_Add,id:416,x:32225,y:33656,varname:node_416,prsc:2|A-2655-UVOUT,B-9819-OUT;n:type:ShaderForge.SFN_Add,id:6286,x:32225,y:33785,varname:node_6286,prsc:2|A-2655-UVOUT,B-8283-OUT;n:type:ShaderForge.SFN_Tex2d,id:8577,x:32495,y:33626,varname:node_8577,prsc:2,tex:f8ff8b40639756941b347da7a0d1df50,ntxv:0,isnm:False|UVIN-416-OUT,MIP-5837-OUT,TEX-2829-TEX;n:type:ShaderForge.SFN_Tex2d,id:1101,x:32495,y:33818,varname:node_1101,prsc:2,tex:f8ff8b40639756941b347da7a0d1df50,ntxv:0,isnm:False|UVIN-6286-OUT,MIP-5837-OUT,TEX-2829-TEX;n:type:ShaderForge.SFN_Vector1,id:5837,x:32316,y:33756,varname:node_5837,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:8317,x:32710,y:33758,varname:node_8317,prsc:2|A-8577-RGB,B-1101-RGB;n:type:ShaderForge.SFN_Add,id:360,x:32929,y:33663,varname:node_360,prsc:2|A-8199-OUT,B-8317-OUT;n:type:ShaderForge.SFN_Sqrt,id:5037,x:32018,y:33362,varname:node_5037,prsc:2|IN-2655-V;n:type:ShaderForge.SFN_Power,id:8199,x:32481,y:33386,varname:node_8199,prsc:2|VAL-5037-OUT,EXP-3006-OUT;n:type:ShaderForge.SFN_Vector1,id:3006,x:32217,y:33469,varname:node_3006,prsc:2,v1:1;n:type:ShaderForge.SFN_Slider,id:3370,x:31830,y:33066,ptovrint:False,ptlb:Opacity_Clip,ptin:_Opacity_Clip,varname:node_3370,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:3425,x:31342,y:33147,ptovrint:False,ptlb:Opacity_Clip_ Range,ptin:_Opacity_Clip_Range,varname:node_3425,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.720754,max:5;n:type:ShaderForge.SFN_Vector1,id:680,x:32754,y:33121,varname:node_680,prsc:2,v1:0.005;n:type:ShaderForge.SFN_Slider,id:5984,x:31320,y:32343,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_5984,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.9607677,max:2;n:type:ShaderForge.SFN_Time,id:172,x:31398,y:32435,varname:node_172,prsc:2;n:type:ShaderForge.SFN_Multiply,id:5739,x:31694,y:32333,varname:node_5739,prsc:2|A-5984-OUT,B-172-T;n:type:ShaderForge.SFN_Multiply,id:5702,x:33103,y:33362,varname:node_5702,prsc:2|A-3806-OUT,B-1147-OUT;n:type:ShaderForge.SFN_ObjectPosition,id:4420,x:32540,y:33228,varname:node_4420,prsc:2;n:type:ShaderForge.SFN_Vector1,id:7471,x:31944,y:32959,varname:node_7471,prsc:2,v1:1;n:type:ShaderForge.SFN_ArcSin,id:719,x:32091,y:32806,varname:node_719,prsc:2|IN-3065-OUT;n:type:ShaderForge.SFN_Multiply,id:3785,x:32252,y:32860,varname:node_3785,prsc:2|A-719-OUT,B-7471-OUT;n:type:ShaderForge.SFN_Add,id:3333,x:32444,y:32897,varname:node_3333,prsc:2|A-3785-OUT,B-8199-OUT;n:type:ShaderForge.SFN_Vector1,id:1147,x:32906,y:33458,varname:node_1147,prsc:2,v1:0.5;proporder:2829-6987-7826-3370-3425-5984;pass:END;sub:END;*/

Shader "Shader Forge/NewShader" {
    Properties {
        _node_2829 ("node_2829", 2D) = "white" {}
        [HDR]_node_6987 ("node_6987", Color) = (0.4622642,0.08354172,0,1)
        [HDR]_node_7826 ("node_7826", Color) = (1,0.5240722,0,1)
        _Opacity_Clip ("Opacity_Clip", Range(0, 1)) = 1
        _Opacity_Clip_Range ("Opacity_Clip_ Range", Range(0, 5)) = 1.720754
        _Speed ("Speed", Range(0, 2)) = 0.9607677
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
            uniform sampler2D _node_2829; uniform float4 _node_2829_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _node_6987)
                UNITY_DEFINE_INSTANCED_PROP( float4, _node_7826)
                UNITY_DEFINE_INSTANCED_PROP( float, _Opacity_Clip)
                UNITY_DEFINE_INSTANCED_PROP( float, _Opacity_Clip_Range)
                UNITY_DEFINE_INSTANCED_PROP( float, _Speed)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                float node_5037 = sqrt(o.uv0.g);
                float node_8199 = pow(node_5037,1.0);
                float4 node_6553 = _Time;
                float2 node_416 = (o.uv0+(float2(0,0.5)*node_6553.g));
                float node_5837 = 1.0;
                float4 node_8577 = tex2Dlod(_node_2829,float4(TRANSFORM_TEX(node_416, _node_2829),0.0,node_5837));
                float2 node_6286 = (o.uv0+(node_6553.g*float2(0,0.5)));
                float4 node_1101 = tex2Dlod(_node_2829,float4(TRANSFORM_TEX(node_6286, _node_2829),0.0,node_5837));
                v.vertex.xyz += (0.005*((objPos.rgb*(node_8199+(node_8577.rgb*node_1101.rgb)))*0.5));
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                float _Opacity_Clip_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Opacity_Clip );
                float _Speed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Speed );
                float4 node_172 = _Time;
                float2 node_3040 = (i.uv0+(_Speed_var*node_172.g)*float2(0,0.5));
                float4 node_695 = tex2D(_node_2829,TRANSFORM_TEX(node_3040, _node_2829));
                float _Opacity_Clip_Range_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Opacity_Clip_Range );
                float node_3065 = (node_695.r*pow((1.0 - distance(i.uv0,float2(0.5,1))),_Opacity_Clip_Range_var));
                float node_7471 = 1.0;
                float node_3785 = (asin(node_3065)*node_7471);
                float node_5037 = sqrt(i.uv0.g);
                float node_8199 = pow(node_5037,1.0);
                float node_5191 = step(_Opacity_Clip_var,(node_3785+node_8199));
                clip(node_5191 - 0.5);
////// Lighting:
                float4 _node_7826_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_7826 );
                float4 node_4265 = tex2D(_node_2829,TRANSFORM_TEX(node_3040, _node_2829));
                float4 _node_6987_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_6987 );
                float3 finalColor = ((_node_7826_var.rgb*node_4265.rgb)+(node_5191*_node_6987_var.rgb));
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
            uniform sampler2D _node_2829; uniform float4 _node_2829_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _Opacity_Clip)
                UNITY_DEFINE_INSTANCED_PROP( float, _Opacity_Clip_Range)
                UNITY_DEFINE_INSTANCED_PROP( float, _Speed)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                float node_5037 = sqrt(o.uv0.g);
                float node_8199 = pow(node_5037,1.0);
                float4 node_6553 = _Time;
                float2 node_416 = (o.uv0+(float2(0,0.5)*node_6553.g));
                float node_5837 = 1.0;
                float4 node_8577 = tex2Dlod(_node_2829,float4(TRANSFORM_TEX(node_416, _node_2829),0.0,node_5837));
                float2 node_6286 = (o.uv0+(node_6553.g*float2(0,0.5)));
                float4 node_1101 = tex2Dlod(_node_2829,float4(TRANSFORM_TEX(node_6286, _node_2829),0.0,node_5837));
                v.vertex.xyz += (0.005*((objPos.rgb*(node_8199+(node_8577.rgb*node_1101.rgb)))*0.5));
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                float _Opacity_Clip_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Opacity_Clip );
                float _Speed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Speed );
                float4 node_172 = _Time;
                float2 node_3040 = (i.uv0+(_Speed_var*node_172.g)*float2(0,0.5));
                float4 node_695 = tex2D(_node_2829,TRANSFORM_TEX(node_3040, _node_2829));
                float _Opacity_Clip_Range_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Opacity_Clip_Range );
                float node_3065 = (node_695.r*pow((1.0 - distance(i.uv0,float2(0.5,1))),_Opacity_Clip_Range_var));
                float node_7471 = 1.0;
                float node_3785 = (asin(node_3065)*node_7471);
                float node_5037 = sqrt(i.uv0.g);
                float node_8199 = pow(node_5037,1.0);
                float node_5191 = step(_Opacity_Clip_var,(node_3785+node_8199));
                clip(node_5191 - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
