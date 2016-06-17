Shader "Platformed/ColorBlock" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Color("Block Color", Color) = (1, 1, 1, 1)
	}

		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
#pragma surface surf Lambert

		sampler2D _MainTex;
	float4 _Color;

	struct Input {
		float2 uv_MainTex;
		float4 color : COLOR;
	};

	void surf(Input IN, inout SurfaceOutput o) {
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
		o.Albedo = c.rgb * IN.color * _Color;
		o.Alpha = c.a;
	}
	ENDCG
	}

		Fallback "Legacy Shaders/VertexLit"
}
