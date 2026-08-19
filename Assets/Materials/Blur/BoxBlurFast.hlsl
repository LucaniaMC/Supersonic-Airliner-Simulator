//https://discussions.unity.com/t/urp-sprite-gaussian-blur-customer-subshadergraph/892367/26

void BoxBlurFast_half(
UnityTexture2D Texture,
float2 UV,
float Blur,
UnitySamplerState Sampler,
out float3 Out_RGB,
out float Out_Alpha)
{
    float4 colx = float4(0.0, 0.0, 0.0, 0.0);
    float4 coly = float4(0.0, 0.0, 0.0, 0.0);
    float kernelSum = 0.0;

    int upper = ((int)Blur - 1) / 2;
    int lower = -upper;

    for (int x = lower; x <= upper; ++x)
    {
        float2 offset = float2(_MainTex_TexelSize.x * x, 0);
        colx += Texture.Sample(Sampler, UV + offset);
        kernelSum += 1.0;
    }

    colx /= kernelSum;

    kernelSum = 0.0;
    for (int y = lower; y <= upper; ++y)
    {
        float2 offset = float2(0, _MainTex_TexelSize.y * y);
        coly += Texture.Sample(Sampler, UV + offset);
        kernelSum += 1.0;
    }

    coly /= kernelSum;

    // Stylized multiplicative blend
    float3 blended = (colx.rgb + coly.rgb) *0.5f;
    float alpha = (colx.a + coly.a) * 0.5f;

    Out_RGB = blended;
    Out_Alpha = alpha;
}