using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private bool firstExecution = true;
    public List<SpawnConfig> configs;

    void Start()
    {
        // Start coroutine for each pool working with a config
        foreach (var config in configs)
            StartCoroutine(SpawnRoutine(config));
    }

    IEnumerator SpawnRoutine(SpawnConfig config)
    {
        GameObject obj = null;

        while (true)
        {
            if (firstExecution)
            {
                Debug.Log("Set firstExecution to off");
                firstExecution = false;
            }
            else
            {
                Debug.Log("SpawnRoutine");
                obj = PoolManager.Instance.Get(config.poolID);
                obj.transform.position = transform.position + config.respawnAt;

                obj.transform.rotation = Quaternion.identity;

                if (config.spawnParent)
                    obj.transform.SetParent(config.spawnParent, false);
            }
            Debug.Log("Coroutine executing...");
            Debug.Log(obj.transform.position.z <= config.despawnAt);

            yield return new WaitUntil(() => obj != null && obj.transform.position.z <= config.despawnAt);
        }
    }
}