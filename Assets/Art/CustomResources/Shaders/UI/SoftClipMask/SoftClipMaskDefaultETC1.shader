Shader "Hidden/UI Default ETC1 (Soft Masked)"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        [PerRendererData] _AlphaTex("Sprite Alpha Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)

        [PerRendererData] _SoftMask("Mask", 2D) = "white" {}

        _StencilComp("Stencil Comparison", Float) = 8
        _Stencil("Stencil ID", Float) = 0
        _StencilOp("Stencil Operation", Float) = 0
        _StencilWriteMask("Stencil Write Mask", Float) = 255
        _StencilReadMask("Stencil Read Mask", Float) = 255

        _ColorMask("Color Mask", Float) = 15
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

        Stencil
        {
            Ref[_Stencil]
            Comp[_StencilComp]
            Pass[_StencilOp]
            ReadMask[_StencilReadMask]
            WriteMask[_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest[unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask[_ColorMask]

		CGINCLUDE
			#include "UnityCG.cginc"
			#include "SoftClipMask.cginc"

		struct appdata_t
		{
			float4 vertex : POSITION;
			float4 color : COLOR;
			float2 texcoord : TEXCOORD0;
			UNITY_VERTEX_INPUT_INSTANCE_ID
		};

		struct v2f
		{
			float4 vertex : SV_POSITION;
			fixed4 color : COLOR;
			float2 texcoord : TEXCOORD0;
			float4 worldPosition : TEXCOORD1;
			
			UNITY_VERTEX_OUTPUT_STEREO

			SOFTMASK_COORDS(2)
		};

		sampler2D _MainTex;
		sampler2D _AlphaTex;
		fixed4 _Color;
		fixed4 _TextureSampleAdd;

		v2f vert(appdata_t IN)
		{
			v2f OUT;

			UNITY_SETUP_INSTANCE_ID(IN);
			UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

			OUT.vertex = UnityObjectToClipPos(IN.vertex);
			OUT.texcoord = IN.texcoord;
			OUT.color = IN.color * _Color;

			OUT.worldPosition = IN.vertex;
			
			SOFTMASK_CALCULATE_COORDS(OUT, IN.vertex)

			return OUT;
		}

		fixed4 frag(v2f IN) : SV_Target
		{
			half4 color = UnityGetUIDiffuseColor(IN.texcoord, _MainTex, _AlphaTex, _TextureSampleAdd) * IN.color;
			color.a *= SOFTMASK_GET_MASK(IN);

			return color;
		}

		ENDCG

        Pass
        {
            Name "Default"
			CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			#pragma multi_compile __ SOFTCLIPMASK_ENABLE
			ENDCG
        }
    }
}

