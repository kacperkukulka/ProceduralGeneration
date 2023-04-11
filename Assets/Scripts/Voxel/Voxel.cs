using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voxel : MonoBehaviour {
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    void Start() {
        meshFilter.sharedMesh = DrawQuad();
    }

    public Mesh DrawQuad() {
        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[4];
        mesh.triangles = new int[6];
        mesh.uv = new Vector2[4];
        mesh.vertices[0] = new Vector3(0, 0, 1);
        mesh.vertices[1] = new Vector3(1, 0, 1);
        mesh.vertices[2] = new Vector3(0, 0, 0);
        mesh.vertices[3] = new Vector3(0, 0, 1);

        mesh.triangles[0] = 0;
        mesh.triangles[1] = 3;
        mesh.triangles[2] = 1;
        mesh.triangles[3] = 2;
        mesh.triangles[4] = 3;
        mesh.triangles[5] = 0;

        mesh.uv[0] = new Vector2(1, 0);
        mesh.uv[1] = new Vector2(1, 1);
        mesh.uv[2] = new Vector2(0, 0);
        mesh.uv[3] = new Vector2(0, 1);

        mesh.RecalculateNormals();
        return mesh;
    }
}
