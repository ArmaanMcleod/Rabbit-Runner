// Flat Shader which works for a single directional light source
// Casts and receives shadows
// Transparency changes as object moves closer to camera 
// The rate of transparency change is set by the properties of the shader 
// ---------------------------------------------------------------------------------------------------------
// Code for geometry shader adapted from Catlike Coding 
// Original Source: https://catlikecoding.com/unity/tutorials/advanced-rendering/flat-and-wireframe-shading/
// Code for transparency effect adapted from Shaders Laboratory Demo 48
// Original Source Code: http://www.shaderslab.com/demo-48---alpha-depending-distance-camera.html

Shader "Unlit/AlphaDistanceCam"
{
    Properties
    {
        // Colour of the material
        _BaseColor ("Main Color", Color) = (1,1,1,1)

        // The distance from the camera the object becomes fully transparent
		_DistTransparent("Disappears at", float) = 4

        // The distances from the camera the object starts to become transparent
        _DistStartTransparent("Starts disappearing at", float) = 9
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent" "LightMode"="ForwardBase"}
        Blend SrcAlpha OneMinusSrcAlpha
 
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

            float _DistTransparent;
			fixed4 _BaseColor;
            float _DistStartTransparent;

            struct vertIn
			{
				float4 vertex : POSITION;
			};

			struct v2g
			{
				float4 pos : SV_POSITION;
                float4 worldPos : TEXCOORD1;
			};

			struct g2f
			{
				float4 pos : SV_POSITION;
				float3 normal : TEXCOORD0;
                float4 worldPos : TEXCOORD2;
				SHADOW_COORDS(1)
			};

            // Vertex Shader
            v2g vert(vertIn v) {
                v2g o;

                // Transform the vertex position into camera coordinates
                o.pos = UnityObjectToClipPos(v.vertex);

                 // Transform the vertex position into world coordinates
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);

                return o;
            }

            // Geometry Shader
            [maxvertexcount(3)] 
			void geom (triangle v2g input[3], inout TriangleStream<g2f> stream) {
				// Get the positions of the vertices
				float3 p0 = input[0].worldPos.xyz;
				float3 p1 = input[1].worldPos.xyz;
				float3 p2 = input[2].worldPos.xyz;

				// Calculate the normal of the triangle created by the vertices 
				float3 triangleNormal = normalize(cross(p1 - p0, p2 - p0));

				// Set the normal of all three vertices to the triangle normal
				g2f o;
				o.normal = triangleNormal;

				for(int i = 0; i < 3; i++){
					o.pos = input[i].pos;
                    o.worldPos = input[i].worldPos;
					TRANSFER_SHADOW(o)
					stream.Append(o);
				}
			}

            // Fragment Shader
            fixed4 frag(g2f v) : SV_Target {
                // Calculate ambient lighting
				fixed4 ambient = _BaseColor * UNITY_LIGHTMODEL_AMBIENT;

				// Calculate diffuse lighting
				float3 LightDir = normalize(_WorldSpaceLightPos0.xyz);
				float LdotN = max(0.0, dot(LightDir, v.normal));
				fixed4 diffuse = _BaseColor * LdotN * _LightColor0;

				// compute shadow attenuation (1.0 = fully lit, 0.0 = fully shadowed)
                fixed shadow = SHADOW_ATTENUATION(v);
				
				// Apply ambient and diffuse lighting
				fixed4 col = diffuse * shadow + ambient;
                
                // Calculate alpha value based on distance from camera
                float dist = distance(v.worldPos, _WorldSpaceCameraPos);
                float alpha = (dist - _DistTransparent) / (_DistStartTransparent - _DistTransparent);
                
                // Clamps the alpha value between 0 and 1
                col.a = saturate(alpha);
                return col;
            }
 
            ENDCG
        }

        // Implement Shadow casting using built in shader
		UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}