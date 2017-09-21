Shader "Custom/Sketch"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#define Res0 textureSize(iChannel0,0)
			#define Res1 textureSize(iChannel1,0)
			#define iResolution Res0
			#define Res  iResolution.xy
			#define randSamp iChannel1
			#define colorSamp iChannel0

			#include "UnityCG.cginc"

			struct v2f
			{
				float4 pos : SV_POSITION;
				float4 screenuv : TEXCOORD1;
			};

			float4 getCol(float2 pos)
			{
				// take aspect ratio into account
				float2 uv=pos/Res.xy;
				float4 c1=texture(iChannel0,uv);
				float4 e=smoothstep(float4(-0.05),float4(-0.0),float4(uv,float2(1)-uv));
				c1=lerp(float4(1,1,1,0),c1,e.x*e.y*e.z*e.w);
				float d=clamp(dot(c1.xyz,float3(-.5,1.,-.5)),0.0,1.0);
				float4 c2=float4(.7);
				return min(lerp(c1,c2,1.8*d),.7);
			}

			float4 getColHT(float2 pos)
			{
 				return smoothstep(0.795,1.05,getCol(pos)*.8+.2+1.0);
			}

			float getVal(float2 pos)
			{
				float4 c=getCol(pos);
 				return pow(dot(c.xyz,float3(.333)),1.)*1.;
			}

			float2 getGrad(float2 pos, float eps)
			{
   				float2 d=float2(eps,0.);
				return float2(
					getVal(pos+d.xy)-getVal(pos-d.xy),
					getVal(pos+d.yx)-getVal(pos-d.yx)) / eps /2.;
			}

			float lum( float3 c) {
				return dot(c, float3(0.3, 0.59, 0.11));
			}

			float3 clipcolor( float3 c) { 
				float l = lum(c);
				float n = min(min(c.r, c.g), c.b);
				float x = max(max(c.r, c.g), c.b);
				if (n < 0.0) {
					c.r = l + ((c.r - l) * l) / (l - n);
					c.g = l + ((c.g - l) * l) / (l - n);
					c.b = l + ((c.b - l) * l) / (l - n);
				}
				if (x > 1.25) {
					c.r = l + ((c.r - l) * (1.0 - l)) / (x - l);
					c.g = l + ((c.g - l) * (1.0 - l)) / (x - l);
					c.b = l + ((c.b - l) * (1.0 - l)) / (x - l);
				}
				return c;
			}

			 float3 setlum( float3 c,  float l) {
							 float d = l - lum(c);
							 c = c + float3(d);
							 return clipcolor(0.85*c);
			 }

			#define AngleNum 3
			#define SampNum 9
			#define PI2 6.28318530717959

			void mainImage( out float4 fragColor, in float2 fragCoord )
			{
				float2 pos = fragCoord;
				float3 col = float3(0);
				float3 col2 = float3(0);
				float sum=0.;
    
				for(int i=0;i<AngleNum;i++)
				{
					float ang=PI2/float(AngleNum)*(float(i)+0.8);
					float2 v=float2(cos(ang),sin(ang));
					for(int j=0;j<SampNum;j++)
					{
						float2 dpos  = v.yx*float2(1,-1)*float(j)*iResolution.y/920.;
						float2 dpos2 = 5.0*( v.xy*float(j*j)/float(SampNum)*.5*iResolution.y/920.);
						float2 g;
						float fact;
						float fact2;
						float s=3.5;
						float2 pos2=pos+s*dpos+dpos2;
                
            			g=getGrad(pos2,0.08);
            			fact=dot(g,v)-.5*abs(dot(g,v.yx*float2(1,-1)));
            			fact2=dot(normalize(g+float2(.0001)),v.yx*float2(1,-1));
                
						fact=clamp(fact,0.,.05);
						fact2=abs(fact2);
                
						fact*=1.-float(j)/float(SampNum);
            			col += fact;
            			col2 += fact2;
            			sum+=fact2;
					}
				}
				col/=float(SampNum*AngleNum)*0.65/sqrt(iResolution.y);
				col2/=sum;
				col.x*=1.6;
				col.x=1.-col.x;
				col.x*=col.x*col.x;

				float2 s=sin(pos.xy*.1/sqrt(iResolution.y/720.));
				float3 karo=float3(1);
				karo-=.75755*float3(.25,.1,.1)*dot(exp(-s*s*80.),float2(1.));
				float r=length(pos-iResolution.xy*.5)/iResolution.x;
				float vign=1.-r*r*r;
				fragColor = float4(float3(col.x*col2*karo*vign ),1);
				float4 origCol = texture(iChannel0, gl_FragCoord.xy/iResolution.xy);
				float4 overlayColor = float4(0.3755,0.05,0.,0.0)*origCol;
           
				fragColor = float4( setlum(1.25*overlayColor.rgb, lum(fragColor.rgb)) * 1.0, 0.);
				fragColor.rgb -= 0.75- clamp (origCol.r + origCol.g + origCol.b , 0.0 , 0.75);
				//fragColor += 0.077;
				//fragColor = min(fragColor , 5.5*texture (iChannel0 , fragCoord/iResolution.xy));
			}
			ENDCG
		}
	}
}