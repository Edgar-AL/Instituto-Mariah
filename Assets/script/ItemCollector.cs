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
        Destroy(item);
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
