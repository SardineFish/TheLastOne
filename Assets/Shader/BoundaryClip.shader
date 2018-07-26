Shader "Custom/BoundaryClip" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_OutBoundColor ("Color Out of Boundary", Color) = (0, 0, 0, 1)
		_BoundSize ("Boundary Size", Vector) = (0, 0, 0, 0)
		_CenterPos ("Center Position", Vector) = (0, 0, 0, 1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows finalcolor:clipColor

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed4 _OutBoundColor;
		float3 _BoundSize;
		float3 _CenterPos;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		bool outOfRange(float2 range, float x){
			return x < range.x || range.y < x;
		}
		void clipColor(Input IN, SurfaceOutputStandard o, inout fixed4 color){
			if(
				outOfRange(float2(-_BoundSize.x/2 + _CenterPos.x, _BoundSize.x/2 + _CenterPos.x), IN.worldPos.x) ||
				outOfRange(float2(-_BoundSize.y/2 + _CenterPos.y, _BoundSize.y/2 + _CenterPos.y), IN.worldPos.y) ||
				outOfRange(float2(-_BoundSize.z/2 + _CenterPos.z, _BoundSize.z/2 + _CenterPos.z), IN.worldPos.z)
			)
				color = _OutBoundColor;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
