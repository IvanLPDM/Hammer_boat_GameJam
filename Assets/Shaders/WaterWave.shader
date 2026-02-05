Shader "Custom/WaterWave"
{
    Properties
    {
        _Amplitude ("Amplitude", Float) = 1
        _Length ("Wave Length", Float) = 2
        _Speed ("Speed", Float) = 1
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float _Amplitude;
            float _Length;
            float _Speed;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;

                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                // amplitude * Mathf.Sin((_x + _z)/ length + offset);
                float wave = sin(((worldPos.x + worldPos.z)/ _Length) + _Time.y * _Speed) * _Amplitude;

                worldPos.y += wave;

                o.vertex = mul(UNITY_MATRIX_VP, float4(worldPos, 1.0));

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return fixed4(0.0, 0.5, 0.8, 1.0); // azul agua
            }
            ENDCG
        }
    }
}
