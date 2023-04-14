// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32930,y:32529,varname:node_3138,prsc:2|custl-9785-OUT,alpha-5961-A;n:type:ShaderForge.SFN_TexCoord,id:8593,x:31689,y:32599,varname:node_8593,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:5961,x:32232,y:32690,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_5961,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b0eb63616d5d1924d8969de245fd8336,ntxv:2,isnm:False|UVIN-9808-OUT;n:type:ShaderForge.SFN_Time,id:8410,x:31614,y:32944,varname:node_8410,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3505,x:31849,y:32928,varname:node_3505,prsc:2|A-7754-OUT,B-8410-TSL;n:type:ShaderForge.SFN_ValueProperty,id:7754,x:31614,y:32876,ptovrint:False,ptlb:U_Speed,ptin:_U_Speed,varname:node_7754,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:5;n:type:ShaderForge.SFN_Add,id:9808,x:32041,y:32707,varname:node_9808,prsc:2|A-8593-UVOUT,B-4185-OUT;n:type:ShaderForge.SFN_Multiply,id:3464,x:31859,y:33089,varname:node_3464,prsc:2|A-8410-TSL,B-7056-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7056,x:31614,y:33149,ptovrint:False,ptlb:V_Speed,ptin:_V_Speed,varname:node_7056,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Append,id:4185,x:32079,y:33045,varname:node_4185,prsc:2|A-3505-OUT,B-3464-OUT;n:type:ShaderForge.SFN_Color,id:3594,x:32218,y:32448,ptovrint:False,ptlb:node_3594,ptin:_node_3594,varname:node_3594,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:9785,x:32507,y:32525,varname:node_9785,prsc:2|A-3594-RGB,B-5961-RGB;proporder:5961-7754-7056-3594;pass:END;sub:END;*/

Shader "Shader Forge/UVliudong" {
    Properties {
        _Texture ("Texture", 2D) = "black" {}
        _U_Speed ("U_Speed", Float ) = 5
        _V_Speed ("V_Speed", Float ) = 0
        [HDR]_node_3594 ("node_3594", Color) = (1,1,1,1)
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
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma target 3.0
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _U_Speed)
                UNITY_DEFINE_INSTANCED_PROP( float, _V_Speed)
                UNITY_DEFINE_INSTANCED_PROP( float4, _node_3594)
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
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
////// Lighting:
                float4 _node_3594_var = UNITY_ACCESS_INSTANCED_PROP( Props, _node_3594 );
                float _U_Speed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _U_Speed );
                float4 node_8410 = _Time;
                float _V_Speed_var = UNITY_ACCESS_INSTANCED_PROP( Props, _V_Speed );
                float2 node_9808 = (i.uv0+float2((_U_Speed_var*node_8410.r),(node_8410.r*_V_Speed_var)));
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_9808, _Texture));
                float3 finalColor = (_node_3594_var.rgb*_Texture_var.rgb);
                return fixed4(finalColor,_Texture_var.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
