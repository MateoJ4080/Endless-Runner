using UnityEngine;

// In the inspector, create a config for each prefab being continuosly spawned.
[CreateAssetMenu(fileName = "SpawnConfig", menuName = "Spawning/SpawnConfig")]
public class SpawnConfig : ScriptableObject
{
    public GameObject prefab;
    public string poolID;
    public float speed;
    public float despawnAt;
    public Transform spawnParent;
}
