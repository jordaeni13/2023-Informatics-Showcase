using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GrabObj : MonoBehaviour
{
    private GameObject collidingObject;  //null
    public static GameObject objectInHand; //null

    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>() || !(col.gameObject.CompareTag("Grabbable") || col.gameObject.CompareTag("trash")))
        {
            return;
        }
        collidingObject = col.gameObject;
        //Debug.Log(collidingObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter");
        SetCollidingObject(other);
    }

    public void OnTriggerStay(Collider other)
    {
        ///Debug.Log("Trigger Staying");
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit");
        if (!collidingObject)
        {
            return;
        }
        collidingObject = null;
    }

    private void GrabObject()
    {
        objectInHand = collidingObject;
        collidingObject = null;

        // If the object is a child of an inventory slot, detach it from the slot
        if (objectInHand.transform.parent != null && objectInHand.transform.parent.CompareTag("InventorySlot"))
        {
            objectInHand.transform.SetParent(null, true);
        }

        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();

        // Make the object's collider a trigger while it's being held
        objectInHand.GetComponent<Collider>().isTrigger = true;

        GrabObj.objectInHand.GetComponent<Rigidbody>().isKinematic = false;
        Debug.Log("Grabbed!");
    }

    private void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            // Make the object's collider solid again when it's released
            objectInHand.GetComponent<Collider>().isTrigger = false;
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            objectInHand = null;
        }
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }
    void Update()
    {

        if (SteamVR_Actions.htc_viu.viu_press_02.GetStateDown(SteamVR_Input_Sources.RightHand) && collidingObject)
        {
            Debug.Log("Tried to Grab!");
            GrabObject();
        }
        else if (SteamVR_Actions.htc_viu.viu_press_02.GetStateUp(SteamVR_Input_Sources.RightHand) && objectInHand)
        {
            Debug.Log("Tried to throw!");
            ReleaseObject();
        }
    }
}