Shader "Custom/Checkerboard"
{
    Properties
    {
        _ColorA ("Color A", Color) = (1, 1, 1, 1) 
        _ColorB ("Color B", Color) = (0, 0, 0, 1) 
        _Scale ("Scale", Range(1, 50)) = 8       
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

            fixed4 _ColorA;
            fixed4 _ColorB;
            float _Scale;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv * _Scale; 
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                
                float2 cell = floor(i.uv);
                float checker = fmod(cell.x + cell.y, 2.0);
                fixed4 color = lerp(_ColorA, _ColorB, saturate(checker));
                return color;
            }
            ENDCG
        }
    }
}