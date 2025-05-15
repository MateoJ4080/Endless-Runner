using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour

{
    // Assign in the inspector
    [SerializeField] string poolID;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Queue<GameObject> pool = new();
    public Queue<GameObject> Pool => pool;
    [SerializeField] private int _poolInitialSize;

    // This variable is only to use with the objects that are already in the scene when the game starts, to be able to
    // easily take them as reference and put them in their corresponding Queue. We need these objects instantiated from before so
    // we can show an ingame-scene in the background of the first screen.

    [SerializeField] private GameObject[] predefinedObjects;

    void Awake()
    {
        AddExistingObjectsToPool();

        for (int i = 0; i < _poolInitialSize - predefinedObjects.Length; i++) AddNewObjectToPool();

        // Register pool in pools dictionary from PoolManager
        PoolManager.Instance.RegisterPool(poolID, this);
    }

    // Use next object in queue in the scene
    public GameObject GetFromPool(SpawnConfig config)
    {
        if (pool.Count == 0)
            AddNewObjectToPool();

        GameObject obj = pool.Dequeue();
        GameObject roadBeforeThisOne = pool.ToArray()[1];

        obj.transform.position = roadBeforeThisOne.transform.position + new Vector3(0, 0, 100);
        obj.SetActive(true);
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.GetComponent<IPoolable>()?.OnDespawn();

        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    public void AddNewObjectToPool()
    {
        GameObject obj = Instantiate(prefab);

        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    // Used for objects already in the scene before starting the instantiating in loop.    
    public void AddExistingObjectsToPool()
    {
        foreach (var obj in predefinedObjects)
        {
            Debug.Log($"Adding {obj.name} to the end of the Queue");
            obj.SetActive(true);
            pool.Enqueue(obj);
        }
    }
}
