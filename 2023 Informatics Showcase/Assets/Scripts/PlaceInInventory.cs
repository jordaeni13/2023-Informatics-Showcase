using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceInInventory : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("InventorySlot") && GrabObj.objectInHand)
        {
            GrabObj.objectInHand.transform.SetParent(other.transform, false);
            GrabObj.objectInHand = null;
        }
    }
}
