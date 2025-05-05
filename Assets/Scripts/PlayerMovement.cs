using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    private Vector3[] railPositions = new Vector3[]
    {
        new Vector3(-3.9f, 1f, -41f), // Left
        new Vector3(0f, 1f, -41f),  // Center
        new Vector3(3.9f, 1f, -41f)   // Right
    };
    private int currentRail;

    [SerializeField] private int transitionSpeed;

    private PlayerControls playerControls;

    void Awake()
    {
        playerControls = new();
    }

    void Start()
    {
        currentRail = 0;
    }

    void Update()
    {
        Vector3 targetPos = railPositions[currentRail];
        transform.position = Vector3.MoveTowards(transform.position, targetPos, transitionSpeed * Time.deltaTime);
    }

    void OnEnable()
    {
        playerControls.Enable();
        playerControls.Player.MoveLeft.performed += OnMoveLeft;
        playerControls.Player.MoveRight.performed += OnMoveRight;
    }

    void OnDisable()
    {
        playerControls.Player.MoveLeft.performed -= OnMoveLeft;
        playerControls.Player.MoveRight.performed -= OnMoveRight;
        playerControls.Disable();
    }

    public void OnMoveLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("OnLeft Performed");
            currentRail--;
            currentRail = Mathf.Clamp(currentRail, 0, railPositions.Length - 1);
            UpdateTargetPosition(railPositions[currentRail]);
        }
    }

    public void OnMoveRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("OnRight Performed");
            currentRail++;
            currentRail = Mathf.Clamp(currentRail, 0, railPositions.Length - 1);
            UpdateTargetPosition(railPositions[currentRail]);
        }
    }

    private void UpdateTargetPosition(Vector3 targetPosition)
    {
        Vector3 target = targetPosition;
        transform.position = Vector3.MoveTowards(transform.position, target, transitionSpeed * Time.deltaTime);
    }
}