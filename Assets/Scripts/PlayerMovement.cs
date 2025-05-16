using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector3[] _railPositions = new Vector3[]
    {
        new Vector3(-3.9f, 0f, -41f), // Left
        new Vector3(0f, 0f, -41f),    // Center
        new Vector3(3.9f, 0f, -41f)   // Right
    };
    private int _currentRail = 1;

    [SerializeField] private int _transitionSpeed;

    private PlayerControls _playerControls;

    void Awake()
    {
        _playerControls = new();
    }

    void Update()
    {
        Debug.Log(_currentRail);
        Vector3 targetPos = _railPositions[_currentRail];
        transform.position = Vector3.MoveTowards(transform.position, targetPos, _transitionSpeed * Time.deltaTime);

    }

    void OnEnable()
    {
        _playerControls.Enable();
        _playerControls.Player.MoveLeft.performed += OnMoveLeft;
        _playerControls.Player.MoveRight.performed += OnMoveRight;
    }

    void OnDisable()
    {
        _playerControls.Player.MoveLeft.performed -= OnMoveLeft;
        _playerControls.Player.MoveRight.performed -= OnMoveRight;
        _playerControls.Disable();
    }

    public void OnMoveLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Debug.Log("OnLeft Performed");
            _currentRail--;
            _currentRail = Mathf.Clamp(_currentRail, 0, _railPositions.Length - 1);
        }
    }

    public void OnMoveRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Debug.Log("OnRight Performed");
            _currentRail++;
            _currentRail = Mathf.Clamp(_currentRail, 0, _railPositions.Length - 1);
        }
    }
}
