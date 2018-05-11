Shader "Custom/Water" {  
    Properties {  
        _Color ("Color", Color) = (1,1,1,1)  
        _MainTex ("Albedo (RGB)", 2D) = "white" {}  
    }  
    SubShader {  
        Tags{"IgnoreProjector"="True" "LightMode" = "ForwardBase" "Queue"="Transparent" "RenderType"="Transparent"}    

        Pass{  
            Blend SrcAlpha OneMinusSrcAlpha    
            ZWrite Off    
            CGPROGRAM  
 
            #pragma vertex vert  
            //#pragma geometry geom  
            #pragma fragment frag  
 
            #include "Lighting.cginc"  

            sampler2D _MainTex;  
            float4 _MainTex_ST;  
            fixed4 _Color;  
            fixed4 _BackColor;
  
            struct a2v{  
                float4 vertex : POSITION;  
                float3 normal : NORMAL;  
                float4 texcoord : TEXCOORD0;  
            };  
  
            struct v2f {  
                float4 pos : SV_POSITION;  
                float3 worldNormal : TEXCOORD0;  
                float3 worldPos : TEXCOORD1;  
                float4 color : TEXCOORD2; 
                float2 uv : TEXCOORD3;   
            };  
  
            v2f vert(a2v v) {  
                v2f o;  
                o.pos = UnityObjectToClipPos(v.vertex); 
                o.worldNormal = UnityObjectToWorldNormal(v.normal);  

                o.worldPos =  mul(unity_ObjectToWorld,v.vertex).xyz;  

                o.uv = TRANSFORM_TEX(v.texcoord,_MainTex);   
                return o;  
            }  
  
            fixed4 frag(v2f i) : SV_Target {  

                float3 viewDirection = normalize(_WorldSpaceCameraPos - i.worldPos);

                //获取法线方向  
                fixed3 worldNormal = normalize(i.worldNormal);  

                float r = dot(viewDirection,worldNormal);
                fixed4 texcolor = tex2D(_MainTex,i.uv);  
                //float d = dot(worldLightDir,worldNormal);
                fixed3 f = (_Color + texcolor) / 2 * r;
                return  fixed4(f,texcolor.a);//lerp( _Color,_BackColor,r) ;
            }  
            ENDCG  
        }  
    }  
    FallBack "Transparent/VertexLit"  
}  