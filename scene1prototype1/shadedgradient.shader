Shader "Outlined/shadedgradient"
{
    Properties
    {
        _BlendColor("Top Color", Color) = (1,1,1,1)
        _Color("Bottom Color", Color) = (0,0,0,1)
        
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline color", Color) = (0,0,0,1)
        _OutlineWidth ("Outlines width", Range (0.0, 2.0)) = .04
        _Diffuse ("Diffuse Light", Range(0,6)) = 3
    }

    CGINCLUDE
    #include "UnityCG.cginc"

    struct appdata
    {
        float4 vertex : POSITION;
    };

    struct v2f
    {
        float4 pos : POSITION;
        
    };

    uniform float _OutlineWidth;
    uniform float4 _OutlineColor;
    uniform sampler2D _MainTex;
    uniform float4 _Color;
    uniform float4 _BlendColor;
    uniform float _Diffuse;
    uniform float vertStretch;

    ENDCG

    SubShader
    {
   
      Tags { "RenderType" = "Transparent"  }
      CGPROGRAM
      #pragma surface surf Lambert 
      struct Input {
          float2 uv_MainTex;
          float4 screenPos;
      };
      //fixed4 _Color;
      void mycolor (Input IN, SurfaceOutput o, inout fixed4 color)
      {
            float blend  = -1  + 3* IN.screenPos.y/IN.screenPos.w;
          
          color = ( _Color * (1- blend)+ _BlendColor * (blend) )/2  + tex2D (_MainTex, IN.uv_MainTex)/2 ;
          
          
      }
      //sampler2D _MainTex;
      void surf (Input IN, inout SurfaceOutput o) {
      float blend  =  IN.screenPos.y /IN.screenPos.w;
      fixed4 color = sin(100*IN.uv_MainTex.x )
      fixed4 color = ( _Color * (1- blend)+ _BlendColor * (blend) )/2  + tex2D (_MainTex, IN.uv_MainTex)/2 ;
      o.Albedo = color * tex2D (_MainTex, IN.uv_MainTex).rgb * _Diffuse;
      }
      
      ENDCG
    
    
    }
}