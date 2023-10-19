using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceInInventory : MonoBehaviour
{
    public GameObject objectInHand; // Assign in GrabObject script

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("InventorySlot") && objectInHand)
        {
            objectInHand.transform.SetParent(other.transform, false);
            objectInHand = null;
        }
    }
}
