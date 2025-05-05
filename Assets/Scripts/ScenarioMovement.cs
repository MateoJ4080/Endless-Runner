using UnityEngine;
using System;
using Unity.VisualScripting;

public class ScenarioMovement : MonoBehaviour
{
    public static event Action<string> OnDespawned;

    private float _speed = 40f;

    void Update()
    {
        transform.position += Vector3.back * _speed * Time.deltaTime;
    }
}