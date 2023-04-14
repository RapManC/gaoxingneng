// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "New Amplify Shader"
{
	Properties
	{
		_lambert1_Emissive("lambert1_Emissive", 2D) = "white" {}
		_lambert1_Normal_OpenGL("lambert1_Normal_OpenGL", 2D) = "bump" {}
		_YJ("YJ", 2D) = "white" {}
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_LOL_N("LOL_N", 2D) = "white" {}
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_QP("QP", 2D) = "white" {}
		_TextureSample2("Texture Sample 2", 2D) = "white" {}
		_Float3("Float 3", Float) = 0
		_Float4("Float 4", Range( 0 , 1)) = 0.0001
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityStandardUtils.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
			float3 worldNormal;
			INTERNAL_DATA
		};

		uniform sampler2D _QP;
		uniform float _Float3;
		uniform sampler2D _TextureSample2;
		uniform float _Float4;
		uniform sampler2D _LOL_N;
		uniform float4 _LOL_N_ST;
		uniform sampler2D _lambert1_Normal_OpenGL;
		uniform sampler2D _lambert1_Emissive;
		uniform sampler2D _YJ;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform sampler2D _TextureSample1;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float2 uv_TexCoord107 = v.texcoord.xy * float2( 8,8 );
			float2 panner108 = ( 1.0 * _Time.y * float2( 0,0.02 ) + uv_TexCoord107);
			float3 ase_vertexNormal = v.normal.xyz;
			float4 temp_cast_1 = (_Float3).xxxx;
			float2 uv_TexCoord101 = v.texcoord.xy * float2( 2,2 );
			float2 panner102 = ( 1.0 * _Time.y * float2( 0,0.01 ) + uv_TexCoord101);
			float clampResult100 = clamp( tex2Dlod( _TextureSample2, float4( panner102, 0, 0.0) ).r , 0.0 , 0.95 );
			float4 lerpResult103 = lerp( ( tex2Dlod( _QP, float4( panner108, 0, 0.0) ) * float4( ase_vertexNormal , 0.0 ) ) , temp_cast_1 , clampResult100);
			v.vertex.xyz += ( lerpResult103 * _Float4 ).rgb;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_LOL_N = i.uv_texcoord * _LOL_N_ST.xy + _LOL_N_ST.zw;
			float2 uv_TexCoord96 = i.uv_texcoord * float2( 2,2 );
			float2 panner97 = ( 1.0 * _Time.y * float2( 0,0.01 ) + uv_TexCoord96);
			float2 panner95 = ( 1.0 * _Time.y * float2( 0,0.02 ) + i.uv_texcoord);
			float4 tex2DNode4 = tex2D( _lambert1_Emissive, panner95 );
			float clampResult59 = clamp( ( 5.0 * tex2DNode4.r ) , 0.0 , 1.0 );
			float3 lerpResult51 = lerp( UnpackScaleNormal( tex2D( _lambert1_Normal_OpenGL, panner97 ), 3.0 ) , float3(0,0,1) , clampResult59);
			o.Normal = ( tex2D( _LOL_N, uv_LOL_N ) + float4( lerpResult51 , 0.0 ) ).rgb;
			float2 uv_TexCoord46 = i.uv_texcoord * float2( 5,5 );
			float2 panner47 = ( 1.0 * _Time.y * float2( 0,0.1 ) + uv_TexCoord46);
			float clampResult57 = clamp( ( 5.0 * tex2DNode4.r ) , 0.0 , 1.0 );
			float4 lerpResult48 = lerp( float4( float3(3,0,0) , 0.0 ) , ( float4(5,0.5,0.1,0) * tex2D( _YJ, panner47 ) ) , clampResult57);
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float fresnelNdotV73 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode73 = ( -0.1 + 1.0 * pow( 1.0 - fresnelNdotV73, 3.46 ) );
			float4 lerpResult72 = lerp( lerpResult48 , float4( float3(2,0.05,0) , 0.0 ) , fresnelNode73);
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float2 uv_TexCoord89 = i.uv_texcoord * float2( 2,2 );
			float2 panner88 = ( 1.0 * _Time.y * float2( 0,0.01 ) + uv_TexCoord89);
			float clampResult87 = clamp( tex2D( _TextureSample1, panner88 ).r , 0.0 , 0.95 );
			float4 lerpResult69 = lerp( lerpResult72 , ( float4( float3(0.8,0,0) , 0.0 ) * tex2D( _TextureSample0, uv_TextureSample0 ) ) , clampResult87);
			o.Albedo = lerpResult69.rgb;
			o.Emission = lerpResult69.rgb;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows exclude_path:deferred vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float4 tSpace0 : TEXCOORD2;
				float4 tSpace1 : TEXCOORD3;
				float4 tSpace2 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17000
0;14;1920;1005;1885.354;-347.1288;1.30883;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;94;-2846.027,-857.0148;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;95;-2549.607,-867.7328;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0.02;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;46;-2584.3,-297.6165;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;5,5;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;56;-1723.756,-612.727;Float;False;Constant;_Float2;Float 2;4;0;Create;True;0;0;False;0;5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;47;-2286.88,-309.3346;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0.1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;4;-2343.571,-890.1853;Float;True;Property;_lambert1_Emissive;lambert1_Emissive;0;0;Create;True;0;0;False;0;554eafa42fe0d1c44884d903761f9fd4;554eafa42fe0d1c44884d903761f9fd4;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;107;-1949.309,602.818;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;8,8;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;101;-1829.363,1238.046;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;2,2;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;50;-1985.841,-502.1105;Float;False;Constant;_Vector0;Vector 0;4;0;Create;True;0;0;False;0;5,0.5,0.1,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;45;-2019.678,-315.7737;Float;True;Property;_YJ;YJ;2;0;Create;True;0;0;False;0;18cc35cec72e0ff4ca455f09f8fe336f;18cc35cec72e0ff4ca455f09f8fe336f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;54;-2089.044,-1344.175;Float;False;Constant;_Float1;Float 1;4;0;Create;True;0;0;False;0;5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;96;-2812.83,-1609.4;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;2,2;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;108;-1661.789,645.6999;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0.02;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;55;-1583.992,-576.6417;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;89;-1259.875,-92.00127;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;2,2;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;102;-1612.844,1324.928;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0.01;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;53;-1705.917,-1215.38;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;57;-1341.575,-581.1965;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;90;-1408.027,-879.355;Float;False;Constant;_Vector4;Vector 4;6;0;Create;True;0;0;False;0;3,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;49;-1582.841,-318.7105;Float;True;2;2;0;FLOAT4;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.PannerNode;97;-2476.31,-1532.518;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0.01;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NormalVertexDataNode;112;-1361.145,857.7643;Float;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;99;-1438.913,1160.033;Float;True;Property;_TextureSample2;Texture Sample 2;7;0;Create;True;0;0;False;0;eb3638842632a1342b0a2f7fee0ff125;eb3638842632a1342b0a2f7fee0ff125;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;98;-1426.058,595.282;Float;True;Property;_QP;QP;6;0;Create;True;0;0;False;0;d568f6c8ef3e12b4ea6d891c8df1e782;d568f6c8ef3e12b4ea6d891c8df1e782;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;88;-1043.356,-5.119274;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0.01;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;68;-1422.205,309.3317;Float;True;Property;_TextureSample0;Texture Sample 0;3;0;Create;True;0;0;False;0;98f87ead152690043bc16a35fe34b8ab;98f87ead152690043bc16a35fe34b8ab;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;106;-1071.341,993.2138;Float;False;Property;_Float3;Float 3;8;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;48;-1103.841,-539.1105;Float;True;3;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.Vector3Node;83;-1392.626,59.15427;Float;False;Constant;_Vector3;Vector 3;6;0;Create;True;0;0;False;0;0.8,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;52;-1796.047,-1477.327;Float;False;Constant;_Vector1;Vector 1;4;0;Create;True;0;0;False;0;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.FresnelNode;73;-1102.125,-313.0225;Float;False;Standard;WorldNormal;ViewDir;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;-0.1;False;2;FLOAT;1;False;3;FLOAT;3.46;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;41;-2039.072,-1756.784;Float;True;Property;_lambert1_Normal_OpenGL;lambert1_Normal_OpenGL;1;0;Create;True;0;0;False;0;7af4d64fe8f5c304dbfc9d5b2b8da6e5;7af4d64fe8f5c304dbfc9d5b2b8da6e5;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;3;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;74;-892.3134,-711.3547;Float;False;Constant;_Vector2;Vector 2;6;0;Create;True;0;0;False;0;2,0.05,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ClampOpNode;100;-1128.233,1148.33;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.95;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;104;-991.0507,735.6902;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;86;-869.4257,-170.014;Float;True;Property;_TextureSample1;Texture Sample 1;5;0;Create;True;0;0;False;0;eb3638842632a1342b0a2f7fee0ff125;eb3638842632a1342b0a2f7fee0ff125;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;59;-1412.565,-1304.429;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;87;-558.746,-181.7165;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.95;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;103;-769.6068,973.0257;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;82;-902.3113,309.4694;Float;True;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;51;-1158.815,-1408.936;Float;True;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;72;-582.3134,-555.3547;Float;True;3;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SamplerNode;75;-875.895,-1661.279;Float;True;Property;_LOL_N;LOL_N;4;0;Create;True;0;0;False;0;10f675f7950167a4b9b05da6a2701e0b;10f675f7950167a4b9b05da6a2701e0b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;114;-852.2455,1276.965;Float;True;Property;_Float4;Float 4;9;0;Create;True;0;0;False;0;0.0001;0.0001;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-1691.739,-902.0953;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;69;-140.1196,-228.0209;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;44;-1909.443,-625.5276;Float;False;Constant;_Float0;Float 0;3;0;Create;True;0;0;False;0;5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;77;-544.1277,-1376.397;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;113;-498.2454,1124.964;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-242.9277,550.86;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;New Amplify Shader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;95;0;94;0
WireConnection;47;0;46;0
WireConnection;4;1;95;0
WireConnection;45;1;47;0
WireConnection;108;0;107;0
WireConnection;55;0;56;0
WireConnection;55;1;4;1
WireConnection;102;0;101;0
WireConnection;53;0;54;0
WireConnection;53;1;4;1
WireConnection;57;0;55;0
WireConnection;49;0;50;0
WireConnection;49;1;45;0
WireConnection;97;0;96;0
WireConnection;99;1;102;0
WireConnection;98;1;108;0
WireConnection;88;0;89;0
WireConnection;48;0;90;0
WireConnection;48;1;49;0
WireConnection;48;2;57;0
WireConnection;41;1;97;0
WireConnection;100;0;99;1
WireConnection;104;0;98;0
WireConnection;104;1;112;0
WireConnection;86;1;88;0
WireConnection;59;0;53;0
WireConnection;87;0;86;1
WireConnection;103;0;104;0
WireConnection;103;1;106;0
WireConnection;103;2;100;0
WireConnection;82;0;83;0
WireConnection;82;1;68;0
WireConnection;51;0;41;0
WireConnection;51;1;52;0
WireConnection;51;2;59;0
WireConnection;72;0;48;0
WireConnection;72;1;74;0
WireConnection;72;2;73;0
WireConnection;42;0;4;0
WireConnection;42;1;44;0
WireConnection;69;0;72;0
WireConnection;69;1;82;0
WireConnection;69;2;87;0
WireConnection;77;0;75;0
WireConnection;77;1;51;0
WireConnection;113;0;103;0
WireConnection;113;1;114;0
WireConnection;0;0;69;0
WireConnection;0;1;77;0
WireConnection;0;2;69;0
WireConnection;0;11;113;0
ASEEND*/
//CHKSM=0B4ADE7661A543C8DB23BB9C3E96F679C2B908A4