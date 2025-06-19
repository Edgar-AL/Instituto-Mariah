using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TMP_Text interactionText;

    void Awake()
    {
        instance = this;
        interactionText.gameObject.SetActive(false);
    }

    public void ShowInteractionMessage(string msg)
    {
        interactionText.text = msg;
        interactionText.gameObject.SetActive(true);
    }

    public void HideInteractionMessage()
    {
        interactionText.gameObject.SetActive(false);
    }
}
