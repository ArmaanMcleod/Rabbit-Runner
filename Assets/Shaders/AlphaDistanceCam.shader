// Adpated from  Shaders Laboratory Demo 48
// Original Source: http://www.shaderslab.com/demo-48---alpha-depending-distance-camera.html

Shader "Custom/AlphaDependingDistance"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _Radius ("Radius", Range(0.001, 500)) = 4
		_DistTransparent("Disappears at", float) = 0.1
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
 
            struct v2f {
                float4 pos : SV_POSITION;
                float4 worldPos : TEXCOORD1;
            };

 
            v2f vert(appdata_base v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }
 
            float _Radius;
			float _DistTransparent;
			fixed4 _Color;
 
            fixed4 frag(v2f i) : SV_Target {
                fixed4 col = _Color;
                float dist = distance(i.worldPos, _WorldSpaceCameraPos);
				if(dist < _DistTransparent){
					col.a=0;
					return col;
				}
                col.a = saturate(dist / _Radius);
                return col;
            }
 
            ENDCG
        }
    }
}