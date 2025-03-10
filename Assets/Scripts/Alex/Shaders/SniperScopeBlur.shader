Shader "UI/SniperScopeBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize("Blur Size", Range(0, 10)) = 3
        _ScopeCenter("Scope Center", Vector) = (0.5, 0.5, 0, 0)
        _ScopeRadius("Scope Radius", Range(0, 1)) = 0.2
    }
    SubShader
    {
        Tags { "RenderType"="OpaquTransparente" "Queue"="Overlay" }
        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _BlurSize;
            float2 _ScopeCenter;
            float _ScopeRadius;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float dist = distance(uv, _ScopeCenter);

                if (dist > _ScopeRadius)
                {
                    // Apply blur only out of teh circle
                    float4 color = tex2D(_MainTex, uv);
                    float4 blur = 0;
                    float offset = _BlurSize * 0.002;

                    for (int x = -2; x <= 2; x++)
                    {
                        for (int y = -2; y <= 2; y++)
                        {
                            blur += tex2D(_MainTex, uv + float2(x,y) * offset);
                        }
                    }

                    blur /= 25;
                    return blur;
                }

                // Central part no blur
                return tex2D(_MainTex, uv);
            }
            ENDCG
        }
    }
}
