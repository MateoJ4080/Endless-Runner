using UnityEngine;
using UnityEngine.UIElements;

public class DebugScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < -100)
        {
            PoolManager.Instance.Despawn("chunk", gameObject);
        }
    }
}
