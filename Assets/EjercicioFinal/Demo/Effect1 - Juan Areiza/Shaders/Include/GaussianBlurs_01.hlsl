#ifndef GAUSSIAN_G1_INCLUDED
#define GAUSSIAN_G1_INCLUDED

//#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/"

#ifndef _BlitTexture
TEXTURE2D(_BlitTexture);
#endif


void ThreeTapBoxBlur_float(float2 UV, float2 ScreenSize, float Offset, out float3 Filtered)
{
	Filtered = 0;
	for (int x = -1; x < 2; x++)
	{
		for (int y = 1; y > -2; y--)
		{
			float2 offUv = UV * ScreenSize + float2(x,y) * Offset;
			float3 pixelValue = LOAD_TEXTURE2D_LOD(_BlitTexture, clamp(offUv, 0, ScreenSize - 1), 0);
			Filtered += pixelValue / 9.0f;
		}
	}
}

#endif