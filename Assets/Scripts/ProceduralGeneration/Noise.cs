using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise{
    public static float[,] GenerateNoiseMap(int height, int width, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset) {
        float[,] noiseMap = new float[width, height];
        System.Random prng = new System.Random(seed);   //Perlin Random Noise Generator
        Vector2[] offsetOctaves = new Vector2[octaves];
        for (int i = 0; i < octaves; i++) {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            offsetOctaves[i] = new Vector2(offsetX, offsetY);
        }

        if (scale <= 0) {
            scale = 0.001f;
        }

        float minHeight = float.MaxValue;
        float maxHeight = float.MinValue;

        float halfWidth = width / 2;
        float halfHeight = height / 2;

        for (int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;
                for (int i = 0; i < octaves; i++) {
                    float sampleX = (x - halfWidth) / scale * frequency + offsetOctaves[i].x;
                    float sampleY = (y - halfHeight) / scale * frequency + offsetOctaves[i].y;

                    float perlin = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlin * amplitude;
                    amplitude *= persistance;
                    frequency *= lacunarity;
                }
                if (noiseHeight > maxHeight)
                    maxHeight = noiseHeight;
                else if (noiseHeight < minHeight)
                    minHeight = noiseHeight;
                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                noiseMap[x, y] = Mathf.InverseLerp(minHeight, maxHeight, noiseMap[x, y]);
            }
        }
        return noiseMap;
    }
}
