using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceInInventory : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("InventorySlot") && GrabObj.objectInHand)
        {
            //Check if the inventory slot already has a child
            if (other.transform.childCount > 1) {
                //If it does, don't place the item

                return;
            }

            GrabObj.objectInHand.transform.SetParent(other.transform, false);
            GrabObj.objectInHand.transform.localPosition = Vector3.zero;
            GrabObj.objectInHand.transform.localRotation = Quaternion.identity;
            GrabObj.objectInHand.GetComponent<Rigidbody>().isKinematic = true;
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