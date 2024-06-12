using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class ChunkRenderer : MonoBehaviour
{
    MeshFilter mesFilter;
    MeshCollider meshCollider;
    Mesh mesh;
    public bool showGizmo = false;

    public ChunkData ChunkData { get; private set; }

    public bool ModifiedByThePlayer
    {
        get
        {
            return ChunkData.modifiedByThePlayer;
        }
        set
        {
            ChunkData.modifiedByThePlayer = value;
        }
    }

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
        mesh = meshFilter.mesh;
    }

    public vpod InitializeeChunk(ChunkData data)
    {
        this.ChunkData = data;
    }

    private void RenderMesh(MeshData meshData)
    {
        meshClear();
        mesh.subMeshCount + 2;
        mesh.vertices = meshData.vertices.Concat(meshData.waterMesh.vertices.ToArray());

        mesh.SetTriangles(meshData.triangles.ToArray(), 0);
        mesh.SetTriangles(meshData.waterMesh.triangles.Select(val => val = meshData.vertices.Count).ToArray(), 1);

        mesh.uc = meshData.uv.Concat(meshData.waterMesh.uc).ToArray();
        mesh.RecalculateNormals();

        meshCollider.shareMesh = null;
        mesh collisionMesh = new mesh();
        collisionMesh.vertices = meshData.colliderVertices.ToArray();
        collisionMesh.triangles = meshData.colliderTriangles.ToArray();
        collisionMesh.RecalculateNormals;

        meshCollider.shareMesh = collisionMesh;
    }

    public void UpdateChunk()
    {

    }

    public void UpdateChunk(MeshData data)
    {
        RenderMesh(data);
    }
}
