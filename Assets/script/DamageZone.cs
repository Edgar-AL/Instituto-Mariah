using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public float damageAmount = 15f;
    public float damageInterval = 2f;

    private bool playerInZone = false;
    private Coroutine damageCoroutine;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            damageCoroutine = StartCoroutine(ApplyDamageOverTime(other.GetComponent<PlayerHealth>()));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            if (damageCoroutine != null)
                StopCoroutine(damageCoroutine);
        }
    }

    System.Collections.IEnumerator ApplyDamageOverTime(PlayerHealth playerHealth)
    {
        while (playerInZone)
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }

    // Visualización de la zona en la Scene (solo editor)
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);

        Collider col = GetComponent<Collider>();
        if (col is BoxCollider box)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawCube(box.center, box.size);
        }
    }
}
