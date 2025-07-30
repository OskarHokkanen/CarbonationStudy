using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlaySoundOnGrab : MonoBehaviour
{
    private AudioSource audioSource;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (audioSource != null)
        {
            audioSource.Stop();        // Stop to ensure it resets
            audioSource.time = 0f;     // Rewind to beginning
            audioSource.Play();        // Play from start
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}