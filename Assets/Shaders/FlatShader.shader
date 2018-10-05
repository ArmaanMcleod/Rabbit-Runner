// Flat shader which works for a single directional light source
// Code for geometry shader adapted from Catlike Coding 
// Original Source: https://catlikecoding.com/unity/tutorials/advanced-rendering/flat-and-wireframe-shading/



Shader "Unlit/FlatShader"
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
			
			uniform fixed4 _BaseColor;
			
			#include "UnityCG.cginc"
			#include "Lighting.cginc"

			struct vertIn
			{
				float4 vertex : POSITION;
			};

			struct v2g
			{
				float4 vertex : SV_POSITION;
				float4 worldVertex : TEXCOORD0;
			};

			struct g2f
			{
				float4 vertex : SV_POSITION;
				float3 normal : TEXCOORD0;
			};

			
			
			v2g vert (vertIn v)
			{
				v2g o;

				// Transform the vertex position into camera coordinates
				o.vertex = UnityObjectToClipPos(v.vertex);

				// Transform the vertex position into world coordinates
				o.worldVertex = mul(unity_ObjectToWorld, v.vertex);
				
				return o;
			}

			[maxvertexcount(3)] 
			void geom (triangle v2g i[3], inout TriangleStream<g2f> stream) {
				// Get the positions of the vertices
				float3 p0 = i[0].worldVertex.xyz;
				float3 p1 = i[1].worldVertex.xyz;
				float3 p2 = i[2].worldVertex.xyz;

				// Calculate the normal of the triangle created by the vertices 
				float3 triangleNormal = normalize(cross(p1 - p0, p2 - p0));

				g2f o;
				o.normal = triangleNormal;

				o.vertex = i[0].vertex;
				stream.Append(o);

				o.vertex = i[1].vertex;
				stream.Append(o);

				o.vertex = i[2].vertex;
				stream.Append(o);
			}
			
			fixed4 frag(g2f v) : SV_Target
			{
				// Calculate ambient lighting
				fixed4 ambient = _BaseColor * UNITY_LIGHTMODEL_AMBIENT;


				// Calculate diffuse lighting
				float3 LightDir = normalize(_WorldSpaceLightPos0.xyz);
				float LdotN = max(0.0, dot(LightDir, v.normal));
				fixed4 diffuse = _BaseColor * LdotN * _LightColor0;
				
				// Apply ambient and diffuse lighting
				fixed4 col = diffuse + ambient;
				return col;
			}
			ENDCG
		}
	}
}
