// Adpated from Shaders Laboratory Demo 48
// Original Source Code: http://www.shaderslab.com/demo-48---alpha-depending-distance-camera.html

Shader "Custom/AlphaDependingDistance"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)

        // The distance from the camera the object becomes fully transparent
		_DistTransparent("Disappears at", float) = 2

        // The distances from the camera the object starts to become transparent
        _DistStartTransparent("Starts disappearing at", float) = 8
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float _DistTransparent;
			fixed4 _Color;
            float _DistStartTransparent;
 
            struct v2f {
                float4 pos : SV_POSITION;
                float4 worldPos : TEXCOORD1;
            };

            // Vertex Shader
            v2f vert(appdata_base v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            // Fragment Shader
            fixed4 frag(v2f i) : SV_Target {
                fixed4 col = _Color;
                
                // Calculate alpha value based on distance from camera
                float dist = distance(i.worldPos, _WorldSpaceCameraPos);
                float alpha = (dist - _DistTransparent) / (_DistStartTransparent - _DistTransparent);
                
                // Clamps the alpha value between 0 and 1
                col.a = saturate(alpha);
                return col;
            }
 
            ENDCG
        }
    }
}