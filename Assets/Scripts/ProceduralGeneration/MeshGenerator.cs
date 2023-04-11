using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator {

    public static MeshData GenerateMesh(float[,] map, float heightScale, AnimationCurve heightCurve) {
        int width = map.GetLength(0);
        int height = map.GetLength(1);

        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;

        MeshData meshData = new MeshData(width, height);
        int verticeIndex = 0;

        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                meshData.vertices[verticeIndex] = new Vector3(topLeftX + x, heightCurve.Evaluate(map[x, y]) * heightScale, topLeftZ - y);
                meshData.uvs[verticeIndex] = new Vector2(x/(float)width, y/(float)height);
                if (x < width - 1 &&  y < height - 1) {
                    meshData.AddTriangle(verticeIndex, verticeIndex + width + 1, verticeIndex + width);
                    meshData.AddTriangle(verticeIndex + width + 1, verticeIndex, verticeIndex + 1);
                }
                verticeIndex++;
            }
        }

        return meshData;
    }
}

public class MeshData {
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;

    int triangleIndex;

    public MeshData(int width, int height) {
        vertices = new Vector3[width * height];
        triangles = new int[(width-1)*(height-1)*6];
        uvs = new Vector2[width * height];
    }

    public void AddTriangle(int a, int b, int c) {
        triangles[triangleIndex] = a;
        triangles[triangleIndex+1] = b;
        triangles[triangleIndex+2] = c;
        triangleIndex += 3;
    }

    public Mesh GenerateMesh() {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
}
