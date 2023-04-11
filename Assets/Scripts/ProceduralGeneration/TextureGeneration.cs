using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGeneration {
    public static Texture2D GenerateFromColor(Color[] colorMap, int height, int width) {
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }

    public static Texture2D GenerateFromNoise(float[,] map) {
        int width = map.GetLength(0);
        int height = map.GetLength(1);

        Texture2D texture = new Texture2D(width, height);
        Color[] colorMap = new Color[width * height];

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, map[x, y]);
            }
        }
        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }
}
