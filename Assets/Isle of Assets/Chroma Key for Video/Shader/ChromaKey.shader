Shader "Custom/ChromaKey"
{
	Properties
	{
		_MainTex ("Main Texture", 2D) = "white" { }
		_Color ("Color", Color)  = (0.0, 1.0, 0.0, 1.0)
		_Sensitivity ("Threshold", Range(0, 1)) = 0.45
		_Smooth ("Smoothing", Range(0, 1)) = 0.05
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		LOD 100
		ZTest Always Cull Back ZWrite On Lighting Off Fog { Mode off }
		CGPROGRAM
		#pragma surface surf Lambert alpha
		struct Input
		{
			float2 uv_MainTex;
		};
		sampler2D _MainTex;
		float4 _Color;
		float _Sensitivity;
		float _Smooth;
		void surf (Input IN, inout SurfaceOutput o)
		{
			half4 c = tex2D(_MainTex, IN.uv_MainTex);
			float maskY = 0.2989 * _Color.r + 0.5866 * _Color.g + 0.1145 * _Color.b;
			float Y = 0.2989 * c.r + 0.5866 * c.g + 0.1145 * c.b;
			float blendValue = smoothstep(_Sensitivity, _Sensitivity + _Smooth, distance(float2(0.7132 * (c.r - Y), 0.5647 * (c.b - Y)), float2(0.7132 * (_Color.r - maskY), 0.5647 * (_Color.b - maskY))));
			o.Alpha = 1.0 * blendValue;
			o.Emission = c.rgb * blendValue;
		}
		ENDCG
	}
	FallBack "Diffuse"
}