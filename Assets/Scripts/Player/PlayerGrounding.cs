using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerGrounding : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private float rayDistance = 2f;
    [SerializeField] private float heightOffset = 0.05f;
    [SerializeField] private LayerMask rampLayer;
    [SerializeField] private Color rayColor;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        SnapToGround();
    }

    void SnapToGround()
    {
        Vector3 origin = transform.position + Vector3.up * (controller.center.y + controller.height / 2f);
        Vector3 direction = Vector3.down;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, controller.height * 1.5f, rampLayer))
        {
            Debug.Log("Raycast hit");
            if (hit.collider.CompareTag("Ramp"))
            {
                Debug.Log("Hit collider");
                Vector3 targetPos = hit.point;
                transform.position = targetPos;
                return;
            }
        }
        else
        {
            // Apply gravity only in Y
            Debug.Log("Applying gravity");
            float gravityY = Physics.gravity.y * Time.deltaTime;
            controller.Move(new Vector3(0, gravityY, 0));
        }
    }

    void OnDrawGizmos()
    {
        if (!TryGetComponent<CharacterController>(out var controller)) return;

        Vector3 origin = transform.position + Vector3.up * (controller.center.y + controller.height / 2f);
        Vector3 direction = Vector3.down;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(origin, origin + direction * (controller.height * 1.5f));
    }
}
