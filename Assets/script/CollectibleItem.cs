using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public string message = "Presiona [E] para recoger";

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.instance.ShowInteractionMessage(message);
            other.GetComponent<ItemCollector>().SetNearbyItem(this);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.instance.HideInteractionMessage();
            other.GetComponent<ItemCollector>().ClearNearbyItem(this);
        }
    }
}
