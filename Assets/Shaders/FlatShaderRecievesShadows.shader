// Flat shader which works for a single directional light source
// Shadows are received and cast
// Code for geometry shader adapted from Catlike Coding 
// Original Source: https://catlikecoding.com/unity/tutorials/advanced-rendering/flat-and-wireframe-shading/

Shader "Unlit/FlatShaderReceivesShadows"
{
	Properties
	{
		// The base colour of the material
		_BaseColor ("Main Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" "LightMode"="ForwardBase" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma geometry geom
			#pragma multi_compile_fwdbase
			
			
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"


			uniform fixed4 _BaseColor;

			struct vertIn
			{
				float4 vertex : POSITION;
			};

			struct v2g
			{
				float4 pos : SV_POSITION;
				float4 worldVertex : TEXCOORD0;
			};

			struct g2f
			{
				float4 pos : SV_POSITION;
				fixed4 dif : TEXCOORD1;
				fixed4 amb : TEXCOORD2;
				SHADOW_COORDS(3)

			};
			
			v2g vert (vertIn v)
			{
				v2g o;

				// Transform the vertex position into camera coordinates
				o.pos = UnityObjectToClipPos(v.vertex);

				// Transform the vertex position into world coordinates
				o.worldVertex = mul(unity_ObjectToWorld, v.vertex);

				

				return o;
			}

			[maxvertexcount(3)] 
			void geom (triangle v2g input[3], inout TriangleStream<g2f> stream) {
				// Get the positions of the vertices
				float3 p0 = input[0].worldVertex.xyz;
				float3 p1 = input[1].worldVertex.xyz;
				float3 p2 = input[2].worldVertex.xyz;

				// Calculate the normal of the triangle created by the vertices 
				float3 triangleNormal = normalize(cross(p1 - p0, p2 - p0));

				// Calculate ambient lighting
				fixed4 ambient = _BaseColor * UNITY_LIGHTMODEL_AMBIENT;

				// Calculate diffuse lighting
				float3 LightDir = normalize(_WorldSpaceLightPos0.xyz);
				float LdotN = max(0.0, dot(LightDir, triangleNormal));
				fixed4 diffuse = _BaseColor * LdotN * _LightColor0;

				// Set the colour and position of each vertex
				g2f o;
				for(int i = 0; i < 3; i++){
					o.pos = input[i].pos;
					o.amb = ambient;
					o.dif = diffuse;
					TRANSFER_SHADOW(o)
					stream.Append(o);
					
				}
			}
			
			fixed4 frag(g2f v) : SV_Target
			{
				// compute shadow attenuation (1.0 = fully lit, 0.0 = fully shadowed)
				fixed shadow = SHADOW_ATTENUATION(v);

				// Apply ambient and diffuse lighting and shadows
				return v.dif * shadow + v.amb;
			}
			ENDCG
		}

		// Implement Shadow casting using built in shader
		UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}
}
