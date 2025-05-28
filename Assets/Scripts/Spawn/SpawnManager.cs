using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private string chunkPoolID = "chunk";
    [SerializeField] private float speed = 30;
    [SerializeField] private float chunkLength = 100;
    [SerializeField] private int activeChunks = 3;
    [SerializeField] private float despawnAt = -100;
    [SerializeField] private SpawnConfig _chunkConfig;

    // Set 
    private List<GameObject> chunksInScene = new();

    void Awake()
    {
        var foundChunks = GameObject.FindGameObjectsWithTag("Chunk");

        // Add chunks to list in order.
        System.Array.Sort(foundChunks, (a, b) => a.transform.position.z.CompareTo(b.transform.position.z));
        chunksInScene.AddRange(foundChunks);
    }

    void Update()
    {
        for (int i = 0; i < chunksInScene.Count; i++)
        {
            var chunk = chunksInScene[i];
            // Respawn chunk if it has moved past the despawn point, taking as reference another one to keep consistency.
            if (chunk.transform.position.z <= despawnAt)
            {
                PoolManager.Instance.Despawn(chunkPoolID, chunk);
                var newChunk = PoolManager.Instance.Spawn(_chunkConfig);
                newChunk.transform.position = PoolManager.Instance.PeekAt("chunk").transform.position + new Vector3(0, 0, 200);
            }
        }

        // Move all chunks in the scene.
        foreach (var chunk in chunksInScene)
        {
            chunk.transform.position += Vector3.back * speed * Time.deltaTime;
        }
    }
}