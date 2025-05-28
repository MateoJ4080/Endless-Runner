using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using System;

public class CameraMovement : MonoBehaviour
{
    private Vector3 camMenuPosition = new(2.3f, 1.4f, -40.3f);
    private Vector3 camMenuRotation = new(13.2f, -90, 0);
    private Vector3 camInGamePosition = new(0, 2.3f, -44f);
    private Vector3 camInGameRotation = new(17.5f, 0, 0);

    public event Action OnCameraTransitionComplete;

    private readonly float _duration = 1f;

    void OnEnable()
    {
        transform.SetPositionAndRotation(camMenuPosition, Quaternion.Euler(camMenuRotation));
    }

    private IEnumerator MoveCameraThenMoveObjects()
    {
        float elapsed = 0f;
        Quaternion startRot = Quaternion.Euler(camMenuRotation);
        Quaternion endRot = Quaternion.Euler(camInGameRotation);

        while (elapsed < _duration)
        {
            float t = elapsed / _duration;
            t = t * t * (3f - 2f * t);

            transform.position = Vector3.Lerp(camMenuPosition, camInGamePosition, elapsed / _duration);
            transform.rotation = Quaternion.Lerp(startRot, endRot, t);

            elapsed += Time.deltaTime;
            yield return null;
        }
        // Ensures camera ends in the exact desired position
        transform.position = camInGamePosition;

        FindFirstObjectByType<SpawnManager>().EnableMovement();
        OnCameraTransitionComplete?.Invoke();
    }

    public void StartCameraTransition()
    {
        StartCoroutine(MoveCameraThenMoveObjects());
    }
}