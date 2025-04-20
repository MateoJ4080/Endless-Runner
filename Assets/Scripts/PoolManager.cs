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
        // Avoid registering pool with the same id
        if (!pools.ContainsKey(id)) pools.Add(id, pool);
        else Debug.LogError("Pool ID already in use for pool: <color:yellow>" + pool.name);
    }

    // Returns next object in Queue from pool. Gets the right pool using an ID from SpawnConfig.cs.
    public GameObject Get(string id)
    {
        // Tries to find pool in dictionary to return object
        if (pools.TryGetValue(id, out var pool))
            return pool.GetFromPool();

        Debug.LogWarning($"No pool registered with ID: {id}");
        return null;
    }

    // Return object from pool. Get the right pool using an ID
    public void Return(string id, GameObject obj)
    {
        if (pools.TryGetValue(id, out var pool))
            pool.ReturnToPool(obj);
        else
            Debug.LogWarning($"No pool registered with ID: {id}");
    }
}