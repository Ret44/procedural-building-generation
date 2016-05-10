Shader "Custom/Wall" {
	Properties{
		_Color("Main Color", Color) = (1,1,1,0.5)
		_Color1("Additional Color 1", Color) = (1,1,1,0.5)
		_Color2("Additional Color 2", Color) = (1,1,1,0.5)
		_Color3("Additional Color 3", Color) = (1,1,1,0.5)
		_MainTex("Texture", 2D) = "white" { }
	}
		SubShader{
		Pass{

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

	fixed4 _Color;
	fixed4 _Color1;
	fixed4 _Color2;
	fixed4 _Color3;

	sampler2D _MainTex;

	struct v2f {
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
	};

	float4 _MainTex_ST;

	v2f vert(appdata_base v)
	{
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		fixed4 texcol = tex2D(_MainTex, i.uv);
	if (i.uv.y > 0.75)
	{
		if (i.uv.x < 0.25) texcol *= _Color;
		if (i.uv.x >= 0.25 && i.uv.x < 0.5) texcol *= _Color1;
		if (i.uv.x >= 0.5 && i.uv.x < 0.75) texcol *= _Color2;
		if (i.uv.x >= 0.75) texcol *= _Color3;
	}
			return texcol;
		}
		ENDCG

	}
	}
}