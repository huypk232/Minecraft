using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RednerMesh(Chunk.GetChunkMeshData(ChunkData));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
