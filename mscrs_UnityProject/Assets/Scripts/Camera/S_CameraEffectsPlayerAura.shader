Shader "Hidden/S_CameraEffectPlayerAura"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float4 _Origin;
			float _Range;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				float dist = distance(_Origin, i.uv);
				float strength = smoothstep(0, _Range, dist);
				col = pow(col, 1 + strength * 0.75f);
				float avg = (col.r + col.g + col.b) / 3.0f;
				fixed4 avgCol = fixed4(avg, avg, avg, 1);
				//col = lerp(col, avg, strength*0.75f);
				return col;
			}
			ENDCG
		}
	}
}
