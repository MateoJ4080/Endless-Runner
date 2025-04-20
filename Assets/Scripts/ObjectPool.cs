using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour

{
    // Assign in the inspector
    [SerializeField] string poolID;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Queue<GameObject> _pool = new();
    [SerializeField] private int _poolInitialSize;

    // This variable is only to use with the objects that are already in the scene when the game starts, to be able to
    // easily take them as reference and put them in their corresponding Queue. We need these objects instantiated from before so
    // we can show an ingame-scene in the background of the first screen.

    [SerializeField] private GameObject[] predefinedObjects;

    void Awake()
    {
        AddExistingObjectsToPool();
        for (int i = 0; i < _poolInitialSize - predefinedObjects.Length; i++) AddNewObjectToPool();

        PoolManager.Instance.RegisterPool(poolID, this);
    }

    public GameObject GetFromPool()
    {
        if (_pool.Count == 0)
            AddNewObjectToPool();

        GameObject obj = _pool.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.GetComponent<IPoolable>()?.OnDespawn();

        obj.SetActive(false);
        _pool.Enqueue(obj);
    }

    public void AddNewObjectToPool()
    {
        GameObject obj = Instantiate(prefab);

        obj.SetActive(false);
        _pool.Enqueue(obj);
    }

    // Used for objects already in the scene before starting the instantiating in loop.    
    public void AddExistingObjectsToPool()
    {
        foreach (var obj in predefinedObjects)
        {
            _pool.Enqueue(obj);
        }
    }
}
