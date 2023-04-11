using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    public enum DrawMode {ColorMap, NoiseMap, Mesh}
    public DrawMode drawMode;

    public int Width;
    public int Height;
    public float Scale;

    public int Octaves;
    [Range(0,1)]
    public float Persistance;
    public float Lacunarity;

    public float HeightScale;
    public AnimationCurve HeightCurve;

    public int Seed;
    public Vector2 Offset;

    public bool AutoGenerate;

    public Terrain[] terrain;

    public void GenerateMap() {
        float[,] map = Noise.GenerateNoiseMap(Height, Width, Seed, Scale, Octaves, Persistance, Lacunarity, Offset);
        Color[] colorMap = new Color[Height * Width];
        for(int x = 0; x < Width; x++) {
            for (int y = 0; y < Height; y++) {
                for (int i = 0; i < terrain.Length; i++) {
                    if (map[x,y] <= terrain[i].height) {
                        colorMap[y * Width + x] = terrain[i].color;
                        break;
                    }
                }
            }
        }

        MapDisplay mapDisplay = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.ColorMap)
            mapDisplay.DrawTexture(TextureGeneration.GenerateFromColor(colorMap, Height, Width));
        else if (drawMode == DrawMode.NoiseMap)
            mapDisplay.DrawTexture(TextureGeneration.GenerateFromNoise(map));
        else if (drawMode == DrawMode.Mesh)
            mapDisplay.DrawMesh(MeshGenerator.GenerateMesh(map, HeightScale, HeightCurve), TextureGeneration.GenerateFromColor(colorMap, Height, Width));
    }

    public void OnValidate() {
        if (Height < 1)
            Height = 1;
        if (Width < 1)
            Width = 1;
        if (Lacunarity < 1)
            Lacunarity = 1;
        if (Octaves < 0)
            Octaves = 0;
    }
}

[System.Serializable]
public struct Terrain {
    public string name;
    public float height;
    public Color color;
}
