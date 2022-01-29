Shader "Hidden/ColorSwap"
{
    Properties
    {
		_MainTex ("Texture", 2D) = "white" {}
        _ColorA ("Color A", Color) = (0.1, 0.1, 0.1, 1.0)
        _ColorB ("Color B", Color) = (0.1, 0.7, 0.9, 1.0)
        _CrossFade ("Crossfade", Range(0, 1)) = 0
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

			float4 _ColorA;
			float4 _ColorB;

			float _CrossFade;

            fixed4 frag (v2f i) : SV_Target
            {
				float4 from = lerp(_ColorA, _ColorB, _CrossFade);
				float4 to = lerp(_ColorB, _ColorA, _CrossFade);

                fixed4 col = lerp(from, to, tex2D(_MainTex, i.uv).r);
                // just invert the colors
                // col.rgb = 1 - col.rgb;
                return col;
            }
            ENDCG
        }
    }
}
