float2 unity_gradientNoise_dir(float2 p, float seed)
{
    p = p % 289;
    float x = (34 * p.x + 1 + seed) * p.x % 289 + p.y;
    x = (34 * x + 1 + seed) * x % 289;
    x = frac(x / 41) * 2 - 1;
    return normalize(float2(x - floor(x + 0.5), abs(x) - 0.5));
}

float unity_gradientNoise(float2 p, float seed)
{
    float2 ip = floor(p);
    float2 fp = frac(p);
    float d00 = dot(unity_gradientNoise_dir(ip, seed), fp);
    float d01 = dot(unity_gradientNoise_dir(ip + float2(0, 1), seed), fp - float2(0, 1));
    float d10 = dot(unity_gradientNoise_dir(ip + float2(1, 0), seed), fp - float2(1, 0));
    float d11 = dot(unity_gradientNoise_dir(ip + float2(1, 1), seed), fp - float2(1, 1));
    fp = fp * fp * fp * (fp * (fp * 6 - 15) + 10);
    return lerp(lerp(d00, d01, fp.y), lerp(d10, d11, fp.y), fp.x);
}

void GradientNoiseWithSeed_float(float2 UV, float Scale, float Seed, out float Out)
{
    Out = unity_gradientNoise(UV * Scale, Seed) + 0.5;
}
