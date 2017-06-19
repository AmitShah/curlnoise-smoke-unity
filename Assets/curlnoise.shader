Shader "Custom/curlnoise"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_NoiseTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		//Cull Off ZWrite Off ZTest Always
		//Blend one one

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"


			sampler2D _MainTex;
			sampler2D _NoiseTex;
			uniform float4 _MainTex_TexelSize; 
			uniform float4 _NoiseTex_TexelSize; 

			float4 ssamp( float2 uv, float oct )
			{
			    return tex2D( _NoiseTex, uv/oct );
			}

			float2 e(){ return float2(1./20., .0); }

			//learn how to sample texel
			//https://forum.unity3d.com/threads/obtaining-screen-space-texel-size.365304/
			float4 dx( float2 uv, float oct ) { return (ssamp(uv+e().xy,oct) - ssamp(uv-e().xy,oct)) / (2.*e().x); }
			float4 dy( float2 uv, float oct ) { return (ssamp(uv+e().yx,oct) - ssamp(uv-e().yx,oct)) / (2.*e().x); }



			
	
			float4 frag (v2f_img i) : COLOR
			{
				float4 col = float4(1.,1.,1.,1.);
				//float4 col = tex2D(_MainTex, i.uv);


				//move 0.15 over frames
				float2 off = float2(.0,.75) * unity_DeltaTime.x;//_Time.y;//* unity_DeltaTime.y;
				//col.xy = tex2D(_MainTex, i.uv - off);// - clamp(0.,1.,off));
				float oct = 1.25;
				float2 curl1 = 0.0015*float2( dy(i.uv,oct).x, -dx(i.uv,oct).x )*oct;
				off+= curl1;
				off *= .4;
				//col.xy = tex2D(_MainTex,i.uv - off);
				//col.xy =off;//+curl1;
				//if(length(i.uv - float2(0.5,0.5 + _SinTime.y * 0.5))  < 0.2){
					
					//col = dx(i.uv, oct);
					//col.xy = ssamp(i.uv, oct);//curl1;//float2(0.5,0.5);// .999*tex2D( _MainTex, i.uv-off);

					//col.z = 1.;
					//col.xy = off;
				//}

				col = 0.98 * tex2D( _MainTex, i.uv-off);

//				if(length(i.uv - float2(0.5,0.5)) < 0.1){
//					col = 1 - col;
//				}else{
//					//col.xy = curl1;
//				}
				//col.xy = tex2D(_MainTex,i.uv);
				//float4 prev = tex2D( _MainTex, i.uv);
				return col;
			}
			ENDCG
		}

	}
}
