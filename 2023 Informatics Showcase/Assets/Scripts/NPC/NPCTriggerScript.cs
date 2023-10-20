using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTriggerScript : MonoBehaviour
{
    public string ChatText = "";
    private GameObject Main;
    void Start()
    {
        Main = GameObject.Find("NPCMain");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            Main.GetComponent<NPCMainScript>().NPCChatEnter(ChatText);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "player")
        {
            Main.GetComponent<NPCMainScript>().NPCChatExit();
        }
    }
}