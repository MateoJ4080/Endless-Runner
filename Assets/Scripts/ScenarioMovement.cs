using UnityEngine;
using System;
using Unity.VisualScripting;

public class ScenarioMovement : MonoBehaviour
{
    public static event Action<string> OnDespawned;

    private bool _shouldMove;
    private float _speed = 40f;

    void Update()
    {
        if (_shouldMove)
        {
            transform.position += Vector3.back * _speed * Time.deltaTime;

        }
    }

    public void EnableMovement()
    {
        _shouldMove = true;
    }
}