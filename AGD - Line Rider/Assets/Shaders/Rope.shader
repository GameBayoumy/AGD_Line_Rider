// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// See https://www.shadertoy.com/view/4dl3zn
// GLSL -> HLSL reference: https://msdn.microsoft.com/en-GB/library/windows/apps/dn166865.aspx

Shader "Custom/Rope" {

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

				float2	pos = i.position.xy / _ScreenParams.xx;
	
	
				// destiny
				pos *= 40.0;
	
		
				// center of trunk	
				pos += float2(10.0, 10.0);
	
				float	x;
	
				x = sqrt(pos.x * pos.x + pos.y * pos.y);	
				x = frac(x);
    
				// colors
				float3	cl = lerp(float3(0.88, 0.72, 0.5), float3(0.76, 0.54, 0.26), x);
		 
				return float4(cl.x, cl.y, cl.z, 1.0);

            }

            ENDCG
        }
    }
}