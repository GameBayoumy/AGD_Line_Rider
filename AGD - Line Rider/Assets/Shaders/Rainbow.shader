Shader "Custom/Rainbow" {

    SubShader {
    
        Pass {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            
            struct v2f{
                float4 position : SV_POSITION;
            };
            
            v2f vert(float4 v:POSITION) : SV_POSITION {
                v2f o;
                o.position = UnityObjectToClipPos (v);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {

				float2 uv = i.position.xy / _ScreenParams.xy;
	
				float frequency = 3.0;
    
				float index = uv.x + _Time.y + uv.y;
    
				float red = sin(frequency * index + 0.0) * 0.5 + 0.5;
				float green = sin(frequency * index + 2.0) * 0.5 + 0.5;
				float blue = sin(frequency * index + 4.0) * 0.5 + 0.5;
    
				return float4(red,green,blue,1.0);

            }

            ENDCG
        }
    }
}