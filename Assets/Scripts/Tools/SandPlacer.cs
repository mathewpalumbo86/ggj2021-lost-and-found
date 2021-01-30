using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandPlacer : MonoBehaviour
{
    [System.Serializable]
    public struct Int2
    {
        public int x;
        public int y;

        public Int2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Int2 operator +(Int2 a, Int2 b)
        {
            return new Int2(a.x + b.x, a.y + b.y);
        }
    }
    [SerializeField]
    GameObject SandChunkPrefab;

    [SerializeField]
    Int2 beachSize;

    [SerializeField]
    Vector3 startPoint;

    [SerializeField]
    Vector3 chunkSize;

    Dictionary<Int2, MeshDeformer> sandChunks = new Dictionary<Int2, MeshDeformer>();

    Int2[] sides =
    {
        new Int2(-1,-1),
        new Int2(0,-1),
        new Int2(1, -1),
        new Int2(-1,0),
        new Int2(0,0),
        new Int2(1,0),
        new Int2(-1,1),
        new Int2(0,1),
        new Int2(1,1),
    };

    private void Awake()
    {
        for(int x = 0; x < beachSize.x; x++)
        {
            for(int y = 0; y < beachSize.y; y++)
            {
                Int2 position = new Int2(x, y);
                var sandChunk = Instantiate(SandChunkPrefab).GetComponent<MeshDeformer>();
                sandChunks[position] = sandChunk;
                Vector3 offset = new Vector3(chunkSize.x * x, transform.position.y, chunkSize.z * y);
                sandChunk.transform.position = startPoint + offset;
                //sandChunk.position = position;
                //sandChunk.placer = this;
            }
        }
    }

    public MeshDeformer[] GetAdjacent(Int2 position)
    {
        MeshDeformer[] adjacents = new MeshDeformer[9];
        for(int i = 0; i < adjacents.Length; i++)
        {
            sandChunks.TryGetValue(position + sides[i], out adjacents[i]);
        }

        return adjacents;
    }
}
