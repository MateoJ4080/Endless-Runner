using System.Collections.Generic;

using UnityEngine;
public class PoolManager : MonoBehaviour
{
    // Singleton
    public static PoolManager Instance { get; private set; }

    // Pools get added to the dictionary for each object with an ObjectPool script on them.
    [SerializeField] private Dictionary<string, ObjectPool> pools = new Dictionary<string, ObjectPool>();

    void Awake()
    {
        // Singleton - Avoid multple instances
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    // Register new pool to work with
    public void RegisterPool(string id, ObjectPool pool)
    {
        if (!pools.ContainsKey(id)) pools.Add(id, pool);
        else Debug.LogError("Pool ID already in use for pool: <color:yellow>" + pool.name);
    }

    // Return next object in Queue from pool. Gets the right pool using an ID from SpawnConfig.cs.
    public GameObject Spawn(SpawnConfig config)
    {
        // Try to take pool from dictionary by value
        if (pools.TryGetValue(config.poolID, out var pool))
            return pool.GetFromPool();

        Debug.LogWarning($"No pool registered with ID: {config.poolID}");
        return null;
    }

    // Return object to pool. Get the right pool from the dictionary using an ID as reference.
    public void Despawn(string id, GameObject obj)
    {
        if (pools.TryGetValue(id, out var pool))
            pool.ReturnToPool(obj);
        else
            Debug.LogWarning($"No pool registered with ID: {id}");
    }

    public GameObject PeekAt(string poolID)
    {
        pools.TryGetValue(poolID, out var objectPool);
        return objectPool.Pool.Peek();
    }

    // Debugging method to print the order of objects in a specific pool.
    public void DebugPoolOrder(string poolID)
    {
        if (pools.TryGetValue(poolID, out var objectPool))
        {
            int i = 0;
            foreach (var obj in objectPool.Pool)
            {
                Debug.Log($"Pool[{i}]: {obj.name} at {obj.transform.position}");
                i++;
            }
        }
        else
        {
            Debug.LogWarning($"No pool registered with ID: {poolID}");
        }
    }
}