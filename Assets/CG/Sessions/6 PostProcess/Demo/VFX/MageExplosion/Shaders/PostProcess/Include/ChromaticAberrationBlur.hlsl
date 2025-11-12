#ifndef CHROMATIC_ABERRATION_DEMO_INCLUDED
#define CHROMATIC_ABERRATION_DEMO_INCLUDED

#ifndef _BlitTexture
TEXTURE2D(_BlitTexture);
#endif

float2 ClampOffsetCoords(float2 coords, float2 screenSize)
{
    return clamp(coords, float2(0, 0), screenSize - 1);
}
#define RECIPROCAL_32 0.03125

void ChromaticAberrationRadial_float(float2 uv, float radialMaskIntensity, float3 perChannelOffsets, float2 screenSize, out half3 filtered)
{
    filtered = 0.0f;

    float2 radialDirection = uv * 2 - 1;
    float radialMask = lerp(1,length(radialDirection), radialMaskIntensity);

    for (int i = 0; i < 32; i++)
    {
        float2 pixelCoords = screenSize * uv;
        float2 offUvR = pixelCoords + radialDirection * i * perChannelOffsets.r * radialMask;
        float2 offUvG = pixelCoords + radialDirection * i * perChannelOffsets.g * radialMask;
        float2 offUvB = pixelCoords + radialDirection * i * perChannelOffsets.b * radialMask;

        half preFilterR = LOAD_TEXTURE2D_LOD(_BlitTexture, ClampOffsetCoords(offUvR, screenSize), 0).r;
        half preFilterG = LOAD_TEXTURE2D_LOD(_BlitTexture, ClampOffsetCoords(offUvG, screenSize), 0).g;
        half preFilterB = LOAD_TEXTURE2D_LOD(_BlitTexture, ClampOffsetCoords(offUvB, screenSize), 0).b;
        filtered += half3(preFilterR, preFilterG, preFilterB) * RECIPROCAL_32;
    }
}
#endif