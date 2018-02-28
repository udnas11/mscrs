Shader "Sprites/Water2"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_BumpTex("Bump Texture", 2D) = "gray" {}
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
			#pragma multi_compile PIXELSNAP_ON
			#include "UnityCG.cginc"

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
				float4 grabPos : TEXCOORD1;
				
			};

			float4 _ReflectPoint;
			sampler2D _MainTex;
			sampler2D _BumpTex;
			sampler2D _ScreenTexture;

			v2f vert(appdata_t IN)
			{
				v2f result;
				result.texcoord = IN.texcoord;

				result.vertex = UnityObjectToClipPos(IN.vertex);
				result.grabPos = ComputeGrabScreenPos(result.vertex);
				result.grabPos.y = 2 * _ReflectPoint.y - result.grabPos.y;
				return result;
			}

			fixed4 frag(v2f IN) : SV_Target
			{				
				float4 uv = IN.grabPos;
				half4 col = tex2Dproj(_ScreenTexture, uv);

				float2 origUv = IN.texcoord;
				col = tex2D(_BumpTex, origUv);
				return col;
				/*
				half4 colGrayscale = (col.r + col.g + col.g) / 3.0f;
				colGrayscale.a = col.a;
				return pow(lerp(col, colGrayscale, 0.55f), 0.8f);
				*/
			}
			ENDCG
		}
	}
}