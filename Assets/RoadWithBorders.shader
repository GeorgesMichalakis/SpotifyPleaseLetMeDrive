Shader "Custom/RoadWithBorders"
{
    Properties
    {
        _MainTex ("Main Road Texture", 2D) = "white" {}
        _BorderTex ("Border Texture", 2D) = "white" {}
        _BorderWidth ("Border Width", Float) = 0.1
        _CenterZ ("Road Center Z", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            HLSLPROGRAM
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
                float3 localPos : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _BorderTex;
            float _BorderWidth;
            float _CenterZ;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.localPos = v.vertex.xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float distFromCenter = abs(i.localPos.z - _CenterZ);

                // Border mask: 0 in center, 1 at edges
                float mask = smoothstep(_BorderWidth - 0.01, _BorderWidth + 0.01, distFromCenter);

                fixed4 mainCol = tex2D(_MainTex, i.uv);
                fixed4 borderCol = tex2D(_BorderTex, i.uv);

                return lerp(mainCol, borderCol, mask);
            }
            ENDHLSL
        }
    }
}
