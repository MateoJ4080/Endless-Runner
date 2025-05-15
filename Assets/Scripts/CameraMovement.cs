using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using System;

public class CameraMovement : MonoBehaviour
{
    private Vector3 camMenuPosition = new(5.4f, 3.8f, -47.3f);
    private Vector3 camMenuRotation = new(7, -14, 0);
    private Vector3 camInGamePosition = new(0, 6.7f, -47.8f);
    private Vector3 camInGameRotation = new(15, 0, 0);

    private ScenarioMovement[] objectsToMove;

    public event Action OnCameraTransitionComplete;

    private float duration = 1f;

    private IEnumerator MoveCameraThenMoveObjects()
    {
        float elapsed = 0f;
        Quaternion startRot = Quaternion.Euler(camMenuRotation);
        Quaternion endRot = Quaternion.Euler(camInGameRotation);

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            t = t * t * (3f - 2f * t);

            transform.position = Vector3.Lerp(camMenuPosition, camInGamePosition, elapsed / duration);
            transform.rotation = Quaternion.Lerp(startRot, endRot, t);

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = camInGamePosition;

        OnCameraTransitionComplete?.Invoke();
        // Start moving objects after camera transition ends
        objectsToMove = FindObjectsByType<ScenarioMovement>(FindObjectsSortMode.None);
        foreach (var obj in objectsToMove)
        {
            obj.EnableMovement();
        }
    }

    public void StartCameraTransition()
    {
        StartCoroutine(MoveCameraThenMoveObjects());
    }
}