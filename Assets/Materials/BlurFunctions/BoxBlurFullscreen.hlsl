#ifndef BOX_BLUR_FULLSCREEN_INCLUDED
#define BOX_BLUR_FULLSCREEN_INCLUDED

float4 _BlitTexture_TexelSize;

void BoxBlurFullscreen_half(
    float2 UV,
    float Blur,
    out float3 Out_RGB,
    out float Out_Alpha)
{
    float4 colx = float4(0, 0, 0, 0);
    float4 coly = float4(0, 0, 0, 0);

    float kernelSum = 0.0;

    int upper = ((int)Blur - 1) / 2;
    int lower = -upper;

    // Horizontal blur
    for (int x = lower; x <= upper; ++x)
    {
        float2 offset = float2(
            _BlitTexture_TexelSize.x * x,
            0
        );

        colx += SAMPLE_TEXTURE2D_X(
            _BlitTexture,
            sampler_LinearClamp,
            UV + offset
        );

        kernelSum += 1.0;
    }

    colx /= kernelSum;

    // Vertical blur
    kernelSum = 0.0;

    for (int y = lower; y <= upper; ++y)
    {
        float2 offset = float2(
            0,
            _BlitTexture_TexelSize.y * y
        );

        coly += SAMPLE_TEXTURE2D_X(
            _BlitTexture,
            sampler_LinearClamp,
            UV + offset
        );

        kernelSum += 1.0;
    }

    coly /= kernelSum;

    // Average horizontal and vertical blur
    float3 blended = (colx.rgb + coly.rgb) * 0.5;
    float alpha = (colx.a + coly.a) * 0.5;

    Out_RGB = blended;
    Out_Alpha = alpha;
}

#endif