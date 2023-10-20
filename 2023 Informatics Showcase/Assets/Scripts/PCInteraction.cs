using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PCInteraction : MonoBehaviour
{
    private AudioSource tada;
    public bool isOpened;
    public static bool isColliding;
    public static GameObject collidings;
    public static GameObject pcOpened;
    public static GameObject pcClosed;
    public bool isSSD;
    // Start is called before the first frame update
    void Start()
    {
        tada = GetComponent<AudioSource>();
        collidings = null;
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
        print("isColliding : " + isColliding);
        if(isColliding)
        {
            isSSD = collidings.CompareTag("SSD") ? true : false;
            if (SteamVR_Input.GetStateDown("GrabPinch", SteamVR_Input_Sources.LeftHand))
            {
                if(isSSD)
                {
                    if(GrabObj.objectInHand.transform.parent.CompareTag("InventorySlot"))
                    {
                        GrabObj.objectInHand.SetActive(false);
                        playSound(tada, false);
                    }
                    Debug.Log("SSD!");
                }
                else
                {
                    Debug.Log("NOT SSD : " + collidings.tag);
                }
            }
        }

    }
    void ChangeState(bool state)
    {
        pcOpened.SetActive(state);
        pcClosed.SetActive(!state);
    }


    
    void playSound(AudioSource audioPlayer, bool loop)
    {
        audioPlayer.Stop();
        audioPlayer.loop = loop;
        audioPlayer.time = 0;
        audioPlayer.Play();
    }
}
