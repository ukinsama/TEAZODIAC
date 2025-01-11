Shader "Custom/TeaWaterSurfaceShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        
        // --- Tea Water Properties ---
        _TeaColor ("Tea Color", Color) = (0,0,0,0) // お茶の色 (例: 茶色)
        _WaterDistortion ("Water Distortion", Range(0, 1)) = 0.6 // 水面の歪みの強さ
        _WaterSpeed ("Water Speed", Float) = 1// 水面の動きの速さ
        _NoiseTex ("Noise", 2D) = "white" {} // 水面用のノイズテクスチャ
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" } // レンダリングタイプを透明に変更
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:blend // 透明度を有効にする
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _NoiseTex; // ノイズテクスチャ用のサンプラー

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_NoiseTex; // ノイズテクスチャ用のUV座標
            float3 worldPos;    // ワールド座標
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // --- Tea Water Properties ---
        fixed4 _TeaColor;
        float _WaterDistortion;
        float _WaterSpeed;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // --- Water Effect ---
            // Time based animation
            float time = _Time.y * _WaterSpeed;

            // Panning the noise texture for distortion
            float2 distortionOffset1 = float2(time, time * 0.5);
            float2 distortionOffset2 = float2(time * 0.7, -time * 0.3);
            float4 noise1 = tex2D(_NoiseTex, IN.uv_NoiseTex + distortionOffset1);
            float4 noise2 = tex2D(_NoiseTex, IN.uv_NoiseTex + distortionOffset2);
            float2 distortion = (noise1.rg + noise2.rg) * _WaterDistortion - _WaterDistortion;

            // --- Albedo and Color ---
            // Distorted UVs for texture sampling
            float2 distortedUV = IN.uv_MainTex + distortion;
            
            // Albedo with distortion
            fixed4 c = tex2D (_MainTex, distortedUV) * _Color;

            // --- Tea Color Blending ---
            // Blend the albedo with tea color, making it more dominant
            fixed4 finalColor = lerp(c, _TeaColor, 0.7); // お茶の色を強めにブレンド

            // --- Output ---
            o.Albedo = finalColor.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = finalColor.a; // 透明度を適用
        }
        ENDCG
    }
    FallBack "Diffuse"
}