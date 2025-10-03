#ifndef CUSTOM_LIGHTING_G2_INCLUDED
#define CUSTOM_LIGHTING_G2_INCLUDED

//#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

//Datos necesarios:
//  1. Dirección -> float3
//  2. Color -> half3
//  3. Intensidad -> 
//  4. Sombra -> float 0 - 1 donde 0 es sombra, 1 es no sombra
void GetMainLight_float(float3 positionWS, out float3 lightDirWS, out half3 lightColor, out half shadowAttenuation)
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

void SimpleLightingAdditional_float(float3 positionWS, float3 normalWS, float3 viewDirWS, float smoothness, out float3 diffuse, out float3 specular)
{
    
    diffuse = 0;
    specular = 0;
   #ifndef SHADERGRAPH_PREVIEW 
    uint LightCount = GetAdditionalLightsCount();
    
    LIGHT_LOOP_BEGIN(LightCount)
    
    Light light = GetAdditionalLight(LightIndex, positionWS, 1);
    float lambert = saturate(dot(normalWS, light.direction));
    diffuse += lambert * light.color * light.distanceAttenuation * light.shadowAttenuation;
    
    
    float3 h = normalize(light.direction + viewDirWS);
    float blinnPhong = saturate(dot(normalWS, h));
    blinnPhong = pow(blinnPhong, exp2((smoothness + 0.01) * 10));
    specular += blinnPhong * light.color * light.distanceAttenuation * light.shadowAttenuation * lambert;
    
    LIGHT_LOOP_END
    
    
    #endif
}




#endif