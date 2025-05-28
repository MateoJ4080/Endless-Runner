using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] SpawnConfig _chunkConfig;
    [SerializeField] private SpawnConfig[] _obstacleConfigs; // Used to spawn obstacles
    public static bool MovementEnabled;

    void OnEnable()
    {
        SpawnObstacles();
    }

    void Update()
    {
        // Object movement - If the game is paused, we don't want to move the chunk
        if (MovementEnabled) transform.position += _chunkConfig.speed * Time.deltaTime * Vector3.back;

        if (transform.position.z < _chunkConfig.despawnAt)
        {
            RespawnChunk();
        }
    }

    void SpawnObstacles()
    {
        foreach (var point in _spawnPoints)
        {
            if (Random.value < 0.5f) // 50% chance
            {
                SpawnConfig randomObstacle = _obstacleConfigs[Random.Range(0, _obstacleConfigs.Length)];
                var obstacle = PoolManager.Instance.Spawn(randomObstacle);
                obstacle.transform.position = point.position;
                obstacle.SetActive(true);
            }
        }
    }

    void RespawnChunk()
    {
        PoolManager.Instance.Despawn("chunk", gameObject);
        GameObject obj = PoolManager.Instance.Spawn(_chunkConfig);

        obj.transform.position = new Vector3(0, 0, 200);
        Debug.Log($"<color=yellow>Spawn at: {obj.transform.position} - Reference: {PoolManager.Instance.PeekAt("chunk").transform.position}</color>");
    }
}
