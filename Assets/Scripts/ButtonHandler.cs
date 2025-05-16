using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject player;
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private UIFader uiFader;

    void Start()
    {
        animator = player.GetComponent<Animator>();
        startButton.onClick.AddListener(cameraMovement.StartCameraTransition);
        startButton.onClick.AddListener(uiFader.FadeOutAndDisable);
        cameraMovement.OnCameraTransitionComplete += OnGameStarted;
    }

    private void OnGameStarted()
    {
        Debug.Log("OnGameStarted");
        animator.SetBool("isRunning", true);
        cameraMovement.OnCameraTransitionComplete -= OnGameStarted;
    }
}
