Shader "Sprites/Water"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		//_Tint("Tint", Color) = (1,1,1,1)
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent+1000"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Fog{ Mode Off }
		Blend One OneMinusSrcAlpha

		GrabPass
		{
			"_ScreenTexture"
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile DUMMY PIXELSNAP_ON
			#include "UnityCG.cginc"

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 grabPos : TEXCOORD0;
				float4 pos : SV_POSITION;
			};

			float4 _ReflectPoint;

			v2f vert(appdata_t IN)
			{
				v2f result;
				result.pos = UnityObjectToClipPos(IN.vertex);

				result.grabPos = ComputeGrabScreenPos(result.pos);
				result.grabPos.y = 2 * _ReflectPoint.y - result.grabPos.y;
				return result;
			}

			sampler2D _MainTex;
			sampler2D _ScreenTexture;			
			//half4 _Tint;

			fixed4 frag(v2f IN) : SV_Target
			{
				float4 uv = IN.grabPos;
				uv.x += sin(_Time*60) * 0.0015f * (sin(uv.x * 20));
				uv.x += sin(_Time * 23) * 0.001f * (sin(_Time * 30) + sin(uv.y * 120));
				uv.x += sin(_Time*10) * 0.015f * (sin(uv.x * 13));

				half4 col = tex2Dproj(_ScreenTexture, uv);
				float4 uv2 = uv;
				uv2.x += 0.004f;
				half4 col2 = tex2Dproj(_ScreenTexture, uv2);
				float4 uv3 = uv;
				uv3.x += -0.004f;
				half4 col3 = tex2Dproj(_ScreenTexture, uv3);

				col = (col*6 + col2 + col3) / 8.0f;

				half4 colGrayscale = (col.r + col.g + col.g) / 3.0f;
				colGrayscale.a = col.a;
				//col = col * _Tint;
				return pow(lerp(col, colGrayscale, 0.55f), 0.8f);
			}
			ENDCG
		}
	}
}