using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector3[] _railPositions = new Vector3[]
    {
        new Vector3(-2, 0f, -41.6f), // Left
        new Vector3(0f, 0f, -41.6f),    // Center
        new Vector3(2f, 0f, -41.6f)   // Right
    };
    private int _currentRail = 1;

    [SerializeField] private int _transitionSpeed;

    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private Animator _animator;
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
        _cameraMovement.OnCameraTransitionComplete += ActivateControls;
    }

    void OnDisable()
    {
        _playerControls.Player.MoveLeft.performed -= OnMoveLeft;
        _playerControls.Player.MoveRight.performed -= OnMoveRight;
        _playerControls.Player.Slide.performed -= OnSlide;
        _playerControls.Disable();
    }

    void OnMoveLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Debug.Log("OnLeft Performed");
            _currentRail--;
            _currentRail = Mathf.Clamp(_currentRail, 0, _railPositions.Length - 1);
        }
    }

    void OnMoveRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Debug.Log("OnRight Performed");
            _currentRail++;
            _currentRail = Mathf.Clamp(_currentRail, 0, _railPositions.Length - 1);
        }
    }

    void OnSlide(InputAction.CallbackContext context)
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Slide")) return;

        _animator.ResetTrigger("Slide");
        _animator.SetTrigger("Slide");
    }

    void ActivateControls()
    {
        _playerControls.Enable();
        _playerControls.Player.MoveLeft.performed += OnMoveLeft;
        _playerControls.Player.MoveRight.performed += OnMoveRight;
        _playerControls.Player.Slide.performed += OnSlide;
    }
}
