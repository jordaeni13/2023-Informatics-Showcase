using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GrabObj : MonoBehaviour
{
    private GameObject collidingObject; 
    private GameObject objectInHand; 

    private void SetCollidingObject(Collider col)
{
    if (collidingObject || !col.GetComponent<Rigidbody>() || !col.gameObject.CompareTag("Grabbable"))
    {
        return;
    }
    Debug.Log(collidingObject)
}

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter");
        SetCollidingObject(other);
    }

    public void OnTriggerStay(Collider other)
    {
        Debug.Log("Trigger Staying");
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
    }

    private void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            objectInHand = null;
        }
    }

    void Update()
    {

        if (SteamVR_Actions.htc_viu.viu_press_33.GetStateDown(SteamVR_Input_Sources.RightHand) && collidingObject)
        {
            Debug.Log("Tried to Grab!");
            GrabObject();
        }
        else if (SteamVR_Actions.htc_viu.viu_press_33.GetStateDown(SteamVR_Input_Sources.RightHand) && objectInHand)
        {
            ReleaseObject();
        }
    }
}