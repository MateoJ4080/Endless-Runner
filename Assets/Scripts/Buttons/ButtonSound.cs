using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField] private AudioClip hoverClip;
    [SerializeField, Range(0f, 1f)] private float hoverVolume;
    [SerializeField] private AudioClip clickClip;
    [SerializeField, Range(0f, 1f)] private float clickVolume;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = FindFirstObjectByType<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _audioSource.PlayOneShot(hoverClip, hoverVolume);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _audioSource.PlayOneShot(clickClip, clickVolume);
    }
}