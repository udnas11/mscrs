Shader "Sprites/Water2"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_BumpTex("Bump Texture", 2D) = "gray" {}
		_BumpMask("Bump Mask", 2D) = "white" {}
		_ReflectScale("Scale", Range(0.25, 2.0)) = 1.0
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
				float2 texcoordST : TEXCOORD0;
				float2 texcoord : TEXCOORD1;
				float4 grabPos : TEXCOORD2;
				
			};

			float4 _ReflectPoint;
			float _ReflectScale;
			sampler2D _MainTex;
			sampler2D _BumpTex;
			sampler2D _BumpMask;
			float4 _BumpTex_ST;
			float _BumpMult;
			half4 _BumpSpeed;
			sampler2D _ScreenTexture;

			v2f vert(appdata_t IN)
			{
				v2f result;
				result.texcoord = IN.texcoord;
				result.texcoordST = TRANSFORM_TEX(IN.texcoord, _BumpTex);

				result.vertex = UnityObjectToClipPos(IN.vertex);
				result.grabPos = ComputeGrabScreenPos(result.vertex);
				//result.grabPos.y = 2.0f * _ReflectPoint.y - result.grabPos.y;
				result.grabPos.y = _ReflectPoint.y + (_ReflectPoint.y - result.grabPos.y) * _ReflectScale;
				return result;
			}

			fixed4 frag(v2f IN) : SV_Target
			{				
				float4 uv = IN.grabPos;

				float2 bumpUV = IN.texcoordST;
				float2 bumpUvDisplaced = bumpUV + float2(_Time.x * _BumpSpeed.x, _Time.x * _BumpSpeed.y);

				half4 bump = tex2D(_BumpTex, bumpUvDisplaced);
				half bumpMask = tex2D(_BumpMask, IN.texcoord).a;
				float4 uvAdjusted = uv;

				uvAdjusted.x += ((bump.r - 1.0f) * _BumpMult) * bumpMask;
				uvAdjusted.y += ((bump.g - 0.5f ) * _BumpMult) * bumpMask;

				half4 col = tex2Dproj(_ScreenTexture, uvAdjusted);
				
				half4 colGrayscale = (col.r + col.g + col.g) / 3.0f;
				colGrayscale.a = col.a;
				return pow(lerp(col, colGrayscale, 0.55f), 0.8f);
				
			}
			ENDCG
		}
	}
}