using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameObject currentGO;
    public List<SpawnConfig> configs;

    void Start()
    {
        Debug.Log(PoolManager.Instance.PeekAt("road").name);

        // Start coroutine for each pool working with a config
        foreach (var config in configs) StartCoroutine(SpawnRoutine(config));
    }

    void Update()
    {
        Debug.Log($"Peeking at: {currentGO.name}");
    }

    IEnumerator SpawnRoutine(SpawnConfig config)
    {
        currentGO = PoolManager.Instance.PeekAt(config.poolID);
        Debug.Log("<color=green>Outside code");
        while (true)
        {
            Debug.Log("<color=yellow>Executing...");
            // Wait until object is at his despawn position
            yield return new WaitUntil(() => currentGO.transform.position.z <= config.despawnAtZ);
            // Returns object at the end of the Queue and SetActive(false)
            PoolManager.Instance.Return(config.poolID, currentGO);
            // Instantiates object at the end of scenario, using respawnAt reference from "config"
            PoolManager.Instance.Get(config);
            // Takes next object from pool
            currentGO = PoolManager.Instance.PeekAt(config.poolID);
        }
    }
}