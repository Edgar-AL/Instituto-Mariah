
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class ObjectPickable : MonoBehaviour
{
    public GameObject whatCanIPickUp;
    public GameObject playerRightHand;

    void Star()
    {

    }

    void Update()
    {

    }

    public void PickUpObject()
    {
        whatCanIPickUp.transform.SetParent(playerRightHand.transform);
        whatCanIPickUp.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickableOject"))
        {
            whatCanIPickUp = other.gameObject;
        }

    }
}
