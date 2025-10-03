//Include guard
#ifndef CUSTOM_LIGHTING_A_INCLUDED
#define CUSTOM_LIGHTING_A_INCLUDED

//#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

//Datos necesarios:
// 1. Dirección de la luz - float3
// 2. Color de la luz - half3
// 3. Intensidad - 
// 4. Sombra - float 0 - 1 donde 0 es sombra, 1 no es sombra

void GetMainLight_float(float3 positionWS, out float3 lightDirWS, out half3 lightColor, out float shadowAttenuation)
{
	#if SHADERGRAPH_PREVIEW
	lightDirWS = normalize(float3(1,1,-1));
	lightColor = 1;
	shadowAttenuation = 1;

	#else
	float4 shadowCoord = TransformWorldToShadowCoord(positionWS);
	Light mainLight = GetMainLight(shadowCoord);
	lightDirWS = mainLight.direction;
	lightColor = mainLight.color;
	shadowAttenuation = mainLight.shadowAttenuation;
	#endif

}










#endif