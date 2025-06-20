using UnityEngine;

public class CollectibleSound : MonoBehaviour
{
    public AudioSource pickupAudio;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickupAudio.Play();
            // Desactivá el objeto después de reproducir el sonido
            Destroy(gameObject, pickupAudio.clip.length);
        }
    }
}
