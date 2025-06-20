using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    public int itemsCollected = 0;
    public TMP_Text counterText;
    public TMP_Text messageText;

    private CollectibleItem nearbyItem;

    void Start()
    {
        UpdateCounter();
        messageText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (nearbyItem != null && Input.GetKeyDown(KeyCode.E))
        {
            CollectItem(nearbyItem.gameObject);
            UIManager.instance.HideInteractionMessage();
            nearbyItem = null;
        }
    }

    void CollectItem(GameObject item)
{
    AudioSource audio = item.GetComponent<AudioSource>();

    if (audio != null && audio.clip != null)
    {
        audio.Play();
        // Desactivamos el mesh renderer y el collider para que desaparezca visualmente, pero siga sonando
        foreach (var renderer in item.GetComponentsInChildren<MeshRenderer>())
            renderer.enabled = false;

        foreach (var collider in item.GetComponents<Collider>())
            collider.enabled = false;

        Destroy(item, audio.clip.length);
    }
    else
    {
        Destroy(item); // si no tiene sonido, se destruye normal
    }

    itemsCollected++;
    UpdateCounter();
    StartCoroutine(ShowMessage("¡Objeto recogido!"));
}

    void UpdateCounter()
    {
        counterText.text = "Objetos: " + itemsCollected;
    }

    System.Collections.IEnumerator ShowMessage(string message)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        messageText.gameObject.SetActive(false);
    }

    public void SetNearbyItem(CollectibleItem item)
    {
        nearbyItem = item;
    }

    public void ClearNearbyItem(CollectibleItem item)
    {
        if (nearbyItem == item)
            nearbyItem = null;
    }
}
