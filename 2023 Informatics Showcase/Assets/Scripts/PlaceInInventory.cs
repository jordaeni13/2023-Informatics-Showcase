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
            FixedJoint joint = GrabObj.objectInHand.GetComponent<FixedJoint>();
            if (joint)
            {
                // Make the object's collider solid again when it's released
                GrabObj.objectInHand.GetComponent<Collider>().isTrigger = false;
                joint.connectedBody = null;
                Destroy(joint);
                GrabObj.objectInHand = null;
            }
        }
    }
}