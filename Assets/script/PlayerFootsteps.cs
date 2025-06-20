using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource footstepAudio; // referencia al audio
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded && controller.velocity.magnitude > 0.2f)
        {
            if (!footstepAudio.isPlaying)
                footstepAudio.Play();
        }
        else
        {
            if (footstepAudio.isPlaying)
                footstepAudio.Stop();
        }
    }
}
