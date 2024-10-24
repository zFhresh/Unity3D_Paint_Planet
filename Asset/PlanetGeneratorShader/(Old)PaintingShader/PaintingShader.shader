Shader "Unlit/PaintingShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Coordinates ("Coordinates", Vector) = (0,0,0,0)
        _Color("Draw Color", Color) = (1,0,0,0)
        _Strength("Strength", Range(0,1)) = 1
        _Size("Size", Range(1,500)) = 0
        _Eraser("Eraser", Range(0,1)) = 0
        _EaserColor("Eraser Color", Color) = (0,0,0,0)
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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Coordinates,_Color;
            half _Size,_Strength;
            float _Eraser; // Bool yerine float olarak tanımlandı

            float4 _EaserColor;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float draw = saturate(pow(saturate(1- distance(i.uv, _Coordinates.xy)), 500/_Size));
                //float draw = 1- distance(i.uv, _Coordinates.xy);
                fixed4 drawcol;
                drawcol = _Color;

                //return saturate(col + drawcol * _Eraser);
                col = draw * drawcol;
                return col;

                // fixed4 col = tex2D(_MainTex, i.uv);
                // float draw = saturate(pow(saturate(1- distance(i.uv, _Coordinates.xy)), 500/_Size));
                // //float draw = 1- distance(i.uv, _Coordinates.xy);
                // fixed4 drawcol;
                // if (_Eraser > 0){ // Eraser değişkeni float olduğu için sıfırdan büyükse silgi aktif olur

                //     drawcol = (draw * _Strength);
                //     return saturate(col - drawcol);
                // }
                // else {
                //     drawcol = _Color * (draw * _Strength);
                //     return saturate(col + drawcol);
                // }

                

            }
            ENDCG
        }
    }
}
