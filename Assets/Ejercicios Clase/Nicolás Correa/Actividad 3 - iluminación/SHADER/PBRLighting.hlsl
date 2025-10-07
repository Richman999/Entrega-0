#idndef PBR_LIGHTING_G2_INCLUDE
#define PBR_LIGHTING_G2_INCLUDE

void OrenNayar_float(float roughness, float3 albedo,float3 normalWS, float3 viewDirWS,float3 lightDirection, out float3 diffuse)
{
	float NdotL = dot(normalWS, lightDirection);
	float NdotV = dot(normalWS, vieDirWS);

	float angleLN = acos(NdotL);
	float angleLN = acos(NdotV);

	float alpha = max(angleLN, angleVN);
	float beta = min(angleLN, angleVN);
	float gamma = cos(angleVN - angleLN);

	float roughnessSq = roughness * roughness;

	float a = 1.0f - 0.5 * (roughnessSq / (roughnessSq + 0.57f));
	float b = 0.45 * (roughnessSq / (roughnessSq + 0.09f));
	float c = sin(alpha) * tan(beta);

	float orenNayar = saturate(NdotL) * (a + b * max(0.0f, gamma) * c);
}

#endif