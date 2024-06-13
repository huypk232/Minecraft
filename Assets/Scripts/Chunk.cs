using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour 
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MeshFilter meshFilter;
    int vertexIndex = 0;
    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();
    List<Vector2> uvs = new List<Vector2>();

    bool [,,] voxelMap = new bool[VoxelData.ChunkWidth, VoxelData.ChunkHeight, VoxelData.ChunkWidth];

    void Start()
    {
        PopulateVoxelMap();
        CreateMeshData();
        CreateMesh();
    }
    
    void PopulateVoxelMap()
    {
        for (int y = 0; y < VoxelData.ChunkHeight; y ++)
        {
            for (int x = 0; x < VoxelData.ChunkWidth; x++)
            {
                for (int z = 0; z < VoxelData.ChunkWidth; z++)
                {
                    voxelMap[x, y, z]  = true;
                }
            }
        }
    }

    void CreateMeshData()
    {
        for (int y = 0; y < VoxelData.ChunkHeight; y ++)
        {
            for (int x = 0; x < VoxelData.ChunkWidth; x++)
            {
                for (int z = 0; z < VoxelData.ChunkWidth; z++)
                {
                    AddVoxelDataChunk(new Vector3(x, y, z));
                }
            }
        }

    }

    bool CheckVoxel(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x);
        int y = Mathf.FloorToInt(position.y);
        int z = Mathf.FloorToInt(position.z);
        if (x < 0 || x > VoxelData.ChunkWidth - 1 || y < 0 || y > VoxelData.ChunkHeight - 1 ||z < 0 || z > VoxelData.ChunkWidth - 1)
            return false;
        return voxelMap[x, y, z];
    }
    private void AddVoxelDataChunk(Vector3 position)
    {
        for (int p = 0; p < 6; p++)
        {
            if (!CheckVoxel(position + VoxelData.faceChecks[p]))
            {
                vertices.Add(position + VoxelData.vertices[VoxelData.triangles[p, 0]]);
                vertices.Add(position + VoxelData.vertices[VoxelData.triangles[p, 1]]);
                vertices.Add(position + VoxelData.vertices[VoxelData.triangles[p, 2]]);
                vertices.Add(position + VoxelData.vertices[VoxelData.triangles[p, 3]]);
                uvs.Add(VoxelData.uvs[0]);
                uvs.Add(VoxelData.uvs[1]);
                uvs.Add(VoxelData.uvs[2]);
                uvs.Add(VoxelData.uvs[3]);
                triangles.Add(vertexIndex);
                triangles.Add(vertexIndex + 1);
                triangles.Add(vertexIndex + 2);
                triangles.Add(vertexIndex + 2);
                triangles.Add(vertexIndex + 1);
                triangles.Add(vertexIndex + 3);
                vertexIndex++;
            }
            
        }
    }

    private void CreateMesh() 
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();

        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
    }
    
}
