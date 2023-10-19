using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class InventoryDisplay : MonoBehaviour
{
    public GameObject inventoryUI; // Assign in inspector

    void Start()
    {
        inventoryUI.transform.SetParent(this.transform);
        inventoryUI.transform.localPosition = new Vector3(0, 0.15f, 0); // Adjust these values as needed
        inventoryUI.transform.localRotation = Quaternion.Euler(90, 0, 0); // Adjust these values as needed
        inventoryUI.SetActive(false);
    }

    
    void Update()
    {
        if (SteamVR_Actions.htc_viu.viu_press_33.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            inventoryUI.SetActive(true);
        }
        else if (SteamVR_Actions.htc_viu.viu_press_33.GetStateUp(SteamVR_Input_Sources.LeftHand))
        {
            inventoryUI.SetActive(false);
        }
    }
}