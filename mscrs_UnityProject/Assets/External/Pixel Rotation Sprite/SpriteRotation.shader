// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sprites/Rotation"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		[PerRendererData] _Angle("Rotation Angle", Range(0, 360)) = 0
		[PerRendererData] _CenterX("Sprite Width", Float) = 0
		[PerRendererData] _CenterY("Sprite Hight", Float) = 0
		[PerRendererData] _SpriteRect("Sprite Rect", Vector) = (0,0,0,0)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
	}

		SubShader
	{
		Tags
	{
		"Queue" = "Transparent"
		"IgnoreProjector" = "True"
		"RenderType" = "Transparent"
		"PreviewType" = "Plane"
		"CanUseSpriteAtlas" = "True"
	}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#pragma multi_compile _ PIXELSNAP_ON
#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
#include "UnityCG.cginc"

	struct appdata_t
	{
		float4 vertex   : POSITION;
		float4 color    : COLOR;
		float2 texcoord : TEXCOORD0;
	};

	struct v2f
	{
		float4 vertex   : SV_POSITION;
		fixed4 color : COLOR;
		float2 texcoord  : TEXCOORD0;
	};

	fixed4 _Color;

	v2f vert(appdata_t IN)
	{
		v2f OUT;
		OUT.vertex = UnityObjectToClipPos(IN.vertex);
		OUT.texcoord = IN.texcoord;
		OUT.color = IN.color * _Color;
#ifdef PIXELSNAP_ON
		OUT.vertex = UnityPixelSnap(OUT.vertex);
#endif

		return OUT;
	}

	sampler2D _MainTex;
	float4 _MainTex_TexelSize;
	float4 _MainTex_ST;
	half _Angle;
	float _CenterX;
	float _CenterY;
	float4 _SpriteRect;

	float2 getPixelSpaceUV(float2 uv) 
	{
		return floor(float2(uv.x / _MainTex_TexelSize.x, uv.y / _MainTex_TexelSize.y));
	}

	float2 getUVSpaceUV(float2 uv) 
	{
		float2 h = _MainTex_TexelSize.xy / 2;
		return float2(uv.x / _MainTex_TexelSize.z + h.x, uv.y / _MainTex_TexelSize.w + h.y);
	}

	float getDir(float x, float y) 
	{
		return atan2(y, x);
	}

	float getMag(float x, float y) 
	{
		return sqrt(x * x + y * y);
	}

	float calculateX(float dir, float mag)
	{
		return cos(dir) * mag;
	}

	float calculateY(float dir, float mag)
	{
		return sin(dir) * mag;
	}

	fixed4 SampleSpriteTexture(float2 uv)
	{
		float2 pxlUV = getPixelSpaceUV(uv);
		float2 center = float2(_CenterX - 0.5, _CenterY - 0.5);
		//float2 center = float2(_CenterX + 0.1, _CenterY + 0.1);

		float dir = getDir(pxlUV.x - center.x, pxlUV.y - center.y);
		float mag = getMag(pxlUV.x - center.x, pxlUV.y - center.y);

		dir = dir - radians(_Angle);

		float2 newPxlUV = round(float2(center.x + calculateX(dir, mag), center.y + calculateY(dir, mag)));

		newPxlUV = clamp(newPxlUV, _SpriteRect.xy, _SpriteRect.xy + _SpriteRect.zw);

		fixed4 color = tex2D(_MainTex, getUVSpaceUV(newPxlUV));
		return color;
	}

	fixed4 frag(v2f IN) : SV_Target
	{
		fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
		c.rgb *= c.a;
		return c;
	}
		ENDCG
	}
	}
}
