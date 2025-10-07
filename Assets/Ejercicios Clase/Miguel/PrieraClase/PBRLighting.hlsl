#ifndef PBR_LIGHTING_INCLUDED
#define PBR_LIGHTING_INCLUDED

void OrenNayar_float(float roughness, float3 albedo, float3 normalWS, float3 viewDirWs, float3 lightDirection, out float3 diffuse)
{
    float NdotL = dot(normalWS, lightDirection);
    float NdotV = dot(normalWS, viewDirWs);
    
    float angleLN = acos(NdotL);
    float angleVN = acos(NdotV);
    
    float alpha = max(angleLN, angleVN);
    float beta = min(angleLN, angleVN);
    float gamma = cos(angleVN - angleLN);
    
    float rougnessSq = roughness * roughness;
    
    float a = 1.0f - 0.5 * (rougnessSq / (rougnessSq + 0.57f));
    float b = 0.45 * (rougnessSq / (rougnessSq + 0.09f));
    float c = sin(alpha) * tan(beta);
    
    float orenNayar = saturate(NdotL) * (a + b * max(0.0f, gamma) * c);
    
    diffuse = albedo * orenNayar;
    
}

#endif