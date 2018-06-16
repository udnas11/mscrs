Shader "Sprites/Water2"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
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
				float4 vertexOrig : TEXCOORD4;
				float4 vertex : SV_POSITION;
				float2 texcoordST : TEXCOORD0;
				float2 texcoord : TEXCOORD1;
				float4 grabPos : TEXCOORD2;
				float2 uvDistress : TEXCOORD3;
			};

			float4 _ReflectPoint;
			float _ReflectScale;
			sampler2D _MainTex;
			sampler2D _BumpTex;
			sampler2D _BumpMask;
			float4 _BumpTex_ST;
			float2 _BumpMult;
			half4 _BumpSpeed;
			sampler2D _ScreenTexture;
			float _Exposure;
			float _Saturation;

			sampler2D _BumpTexDistress;
			float4 _BumpTexDistress_ST;
			float2 _BumpMultDistress;
			float4 _DistressInfo[4]; //posx, posy, mult, range

			v2f vert(appdata_t IN)
			{
				v2f result;
				result.texcoord = IN.texcoord;
				result.texcoordST = TRANSFORM_TEX(IN.texcoord, _BumpTex);
				result.uvDistress = TRANSFORM_TEX(IN.texcoord, _BumpTexDistress);

				result.vertexOrig = mul(unity_ObjectToWorld, IN.vertex);
				result.vertex = UnityObjectToClipPos(IN.vertex);
				result.grabPos = ComputeGrabScreenPos(result.vertex);
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

				uvAdjusted.x += ((bump.b - 0.5f) * _BumpMult.x) * bumpMask;
				uvAdjusted.y += ((bump.g - 0.5f) * _BumpMult.y) * bumpMask;

				float2 posOrig = float2(IN.vertexOrig.x, IN.vertexOrig.y);
				for (int i = 0; i < 4; i++)
				{
					if (_DistressInfo[i].z > 0)
					{
						float2 posThis = float2(_DistressInfo[i].x, _DistressInfo[i].y);
						half dist = 1 - (clamp(distance(posOrig, posThis), 0, _DistressInfo[i].w) / _DistressInfo[i].w);
						dist = pow(dist, 3);
						half4 bumpDistress = tex2D(_BumpTexDistress, IN.uvDistress + float2(0, _Time.y * 1.2f));
						uvAdjusted.x += ((bumpDistress.b - 0.5f) * _BumpMultDistress.x) * _DistressInfo[i].z * dist;
						uvAdjusted.y += ((bumpDistress.g - 0.5f) * _BumpMultDistress.y) * _DistressInfo[i].z * dist;
					}
				}

				half4 col = tex2Dproj(_ScreenTexture, uvAdjusted);
				
				half4 colGrayscale = (col.r + col.g + col.b) / 3.0f;
				colGrayscale.a = col.a;
				return pow(lerp(colGrayscale, col, _Saturation), _Exposure);				
			}
			ENDCG
		}
	}
}