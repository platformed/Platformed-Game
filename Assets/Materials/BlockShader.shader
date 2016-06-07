Shader "Platformed/Block" {
	Properties {
		_MainTex("Base (RGB)", 2D) = "white" {}
		_AO("Ambient Occlusion Amount", Range(0, 1)) = 1
	}

	SubShader {
		Tags { "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		float _AO;

		struct Input {
			float2 uv_MainTex;
			float4 color : COLOR;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb * lerp(IN.color, fixed4(1, 1, 1, 1), _AO);
			o.Alpha = c.a;
		}
		ENDCG
	}

	Fallback "Legacy Shaders/VertexLit"
}
