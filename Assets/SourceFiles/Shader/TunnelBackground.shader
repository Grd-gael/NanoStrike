Shader "Custom/TunnelBackground"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _RotationSpeed ("Rotation Speed", Float) = 0.03
        _PulseSpeed ("Pulse Speed", Float) = 0.3
        _PulseAmount ("Pulse Amount", Float) = 0.001
        _DistortStrength ("Distort Strength", Float) = 0.01
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata { float4 vertex : POSITION; float2 uv : TEXCOORD0; };
            struct v2f { float2 uv : TEXCOORD0; float4 vertex : SV_POSITION; };

            sampler2D _MainTex;
            float _RotationSpeed;
            float _PulseSpeed;
            float _PulseAmount;
            float _DistortStrength;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv - 0.5;
                float angle = _Time.y * _RotationSpeed;
                float c = cos(angle), s = sin(angle);
                uv = float2(c * uv.x - s * uv.y, s * uv.x + c * uv.y);

                float dist = length(uv);
                float pulse = 1.0 + sin(_Time.y * _PulseSpeed) * _PulseAmount;
                uv *= pulse;

                float swirl = sin(dist * 8.0 - _Time.y * 0.5) * _DistortStrength;
                uv += normalize(uv) * swirl;

                uv += 0.5;
                return tex2D(_MainTex, uv);
            }
            ENDCG
        }
    }
}