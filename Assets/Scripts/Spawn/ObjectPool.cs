using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour

{
    private readonly Queue<GameObject> pool = new();
    public Queue<GameObject> Pool => pool;

    [SerializeField] string poolID;
    [SerializeField] private GameObject prefab;
    [SerializeField] private int _poolInitialSize;

    // This variable is only to use with the objects that are already in the scene when the game starts, to be able to easily take them as reference and put them in their 
    // corresponding Queue. We need these objects instantiated from before so we can show an ingame-scene in the background of the first screen.
    [SerializeField] private GameObject[] preSpawnedObjects;

    void Awake()
    {
        AddExistingObjectsToPool();

        for (int i = 0; i < _poolInitialSize - preSpawnedObjects.Length; i++) AddNewObjectToPool();

        // Register pool in pools dictionary from PoolManager
        PoolManager.Instance.RegisterPool(poolID, this);
    }

    // Use next object in queue
    public GameObject GetFromPool()
    {
        if (pool.Count == 0)
            AddNewObjectToPool();

        GameObject obj = pool.Dequeue(); // Dequeue the first object in the pool
        GameObject objBeforeThisOne = pool.ToArray()[1];

        // Set new obj position based on previous obj in the pool' position to avoid a gap between them.

        // Debug.Log($"<color=yellow>Taking as reference {objBeforeThisOne.transform.position}");
        //Debug.Log($"<color=green>Spawning at {obj.transform.position}");
        obj.SetActive(true);
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj); // Add object to the end of the pool
    }

    public void AddNewObjectToPool()
    {
        GameObject obj = Instantiate(prefab);

        obj.SetActive(false);
        pool.Enqueue(obj); // Add object to the end of the pool
    }

    // Used for objects already in the scene before starting the instantiating in loop.    
    public void AddExistingObjectsToPool()
    {
        foreach (var obj in preSpawnedObjects)
        {
            obj.SetActive(true);
            pool.Enqueue(obj);
        }
    }
}
