using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PCInteration : MonoBehaviour
{
    public bool isOpened;
    public bool isColliding;
    public GameObject collidingObject;
    public GameObject pcOpened;
    public GameObject pcClosed;
    public bool isSSD;
    // Start is called before the first frame update
    void Start()
    {
        collidingObject = null;
        isSSD = false;
        Debug.Log("Hello PC");
        isOpened = false;
        pcOpened = GameObject.Find("PC_Opened");
        pcClosed = GameObject.Find("PC_Closed");
        ChangeState(isOpened);
    }

    // Update is called once per frame
    void Update()
    {
        if(isColliding)
        {
            isSSD = collidingObject.CompareTag("SSD") ? true : false;
            if (SteamVR_Input.GetStateDown("GrabPinch", SteamVR_Input_Sources.LeftHand))
            {
                if(isSSD)
                {
                    Debug.Log("SSD!");
                }
                else
                {
                    Debug.Log("NOT SSD : " + collidingObject.tag);
                }
            }
        }

    }
    void ChangeState(bool state)
    {
        pcOpened.SetActive(state);
        pcClosed.SetActive(!state);
    }

    void OnCollisionEnter(Collision col)
    {
        collidingObject = col.gameObject;
        isColliding = true;
    }

    void OnCollisionExit(Collision col)
    {
        collidingObject = null;
        isColliding = false;
    }
}
