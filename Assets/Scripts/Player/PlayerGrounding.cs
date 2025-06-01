using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerGrounding : MonoBehaviour
{
    [SerializeField] private float groundCheckDistance = 1.0f;
    [SerializeField] private float sphereRadius = 0.3f;
    [SerializeField] private float stickForce = 30f;
    [SerializeField] private LayerMask rampLayer;

    private Rigidbody _rb;
    private bool _onRamp;
    private RaycastHit _lastHit;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        Vector3 direction = Vector3.down;

        _onRamp = false;

        if (Physics.SphereCast(origin, sphereRadius, direction, out _lastHit, groundCheckDistance, rampLayer))
        {
            _onRamp = true;
            _rb.AddForce(Vector3.down * stickForce, ForceMode.Impulse);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 origin = transform.position + Vector3.up * 0.1f;
        Vector3 direction = Vector3.down;
        float step = 0.1f;

        for (float i = 0; i < groundCheckDistance; i += step)
        {
            Vector3 point = origin + direction * i;
            Gizmos.DrawWireSphere(point, sphereRadius);
        }

        Gizmos.DrawWireSphere(origin + direction * groundCheckDistance, sphereRadius);
    }
}