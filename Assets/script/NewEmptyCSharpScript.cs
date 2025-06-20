using UnityEngine;

public class CollectibleSound : MonoBehaviour
{
    public AudioSource pickupAudio;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickupAudio.Play();
            // Desactiv� el objeto despu�s de reproducir el sonido
            Destroy(gameObject, pickupAudio.clip.length);
        }
    }
}
