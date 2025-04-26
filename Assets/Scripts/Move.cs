using UnityEngine;
using System;

public class Mover : MonoBehaviour
{
    public static event Action<string> OnDespawned;

    public float speed = 10f;
    public string poolID;

    void Update()
    {
        transform.position += Vector3.back * speed * Time.deltaTime;
    }
}