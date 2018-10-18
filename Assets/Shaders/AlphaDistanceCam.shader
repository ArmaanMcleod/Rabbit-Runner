// Flat Shader which works for a single directional light source
// Casts shadows only
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
        LOD 100
        ZWrite On
 
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
                float4 worldVertex : TEXCOORD1;
                

			};

			struct g2f
			{
				float4 pos : SV_POSITION;
                float4 worldVertex : TEXCOORD2;
                fixed4 col : COLOR;
			};

            // Vertex Shader
            v2g vert(vertIn v) {
                v2g o;

                // Transform the vertex position into camera coordinates
                o.pos = UnityObjectToClipPos(v.vertex);

                 // Transform the vertex position into world coordinates
                o.worldVertex = mul(unity_ObjectToWorld, v.vertex);

                return o;
            }

            // Geometry Shader
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

                // Apply ambient and diffuse lighting and shadows
				fixed4 col = ambient  + diffuse;

				// Set the colour and position of each vertex
				g2f o;
				for(int i = 0; i < 3; i++){
					o.pos = input[i].pos;
					o.col = col;
                    o.worldVertex = input[i].worldVertex;
					stream.Append(o);
				}
			}

            // Fragment Shader
            fixed4 frag(g2f v) : SV_Target {
                fixed4 col = v.col;
                
                // Calculate alpha value based on distance from camera
                float dist = distance(v.worldVertex, _WorldSpaceCameraPos);
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