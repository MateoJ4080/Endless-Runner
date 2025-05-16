using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclesPrefabs;

    // Add in the inspector after creating a new config. 
    // - How to create a new config: Follow this path in the assets menu: "Right click > Create > Spawning > Spawnconfig".
    public List<SpawnConfig> configs;

    void Start()
    {
        // Start coroutine for each pool working with a config
        foreach (var config in configs) StartCoroutine(SpawnRoutine(config));
    }

    IEnumerator SpawnRoutine(SpawnConfig config)
    {
        GameObject currentGO = PoolManager.Instance.PeekAt(config.poolID);
        while (true)
        {
            yield return new WaitUntil(() => currentGO.transform.position.z <= config.despawnAtZ);
            SpawnSection(currentGO, config);

            // Take object from pool to respawn on next iteration
            currentGO = PoolManager.Instance.PeekAt(config.poolID);
        }
    }

    void SpawnSection(GameObject currentGO, SpawnConfig config)
    {
        // Returns object at the end of the Queue and SetActive(false)
        PoolManager.Instance.Return(config.poolID, currentGO);
        // Instantiates object at the end of scenario, using respawnAt reference from "config"
        PoolManager.Instance.Get(config);
    }
}