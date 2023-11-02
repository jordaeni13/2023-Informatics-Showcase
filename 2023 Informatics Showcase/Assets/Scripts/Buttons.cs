using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Buttons : MonoBehaviour
{
    public static bool _RTrigger = false;
    public static bool _LTrigger = false;
    private void Start()
    {
        
    }
    private void Update()
    {
        if (RTrigger_Dn()) _RTrigger = true;
        if (LTrigger_Dn()) _LTrigger = true;

        if (RTrigger_Up()) _RTrigger = false;
        if (LTrigger_Up()) _LTrigger = false;
    }
    public static bool RTrigger() {
        return SteamVR_Actions.htc_viu.viu_press_33.GetStateDown(SteamVR_Input_Sources.RightHand);
    }
    public static bool RTrigger_Dn()
    {
        return SteamVR_Actions.htc_viu.viu_press_33.GetStateDown(SteamVR_Input_Sources.RightHand);
    }
    public static bool RTrigger_Up()
    {
        return SteamVR_Actions.htc_viu.viu_press_33.GetStateUp(SteamVR_Input_Sources.RightHand);
    }


    public static bool LTrigger_Up()
    {
        return SteamVR_Actions.htc_viu.viu_press_33.GetStateUp(SteamVR_Input_Sources.LeftHand);
    }
    public static bool LTrigger_Dn()
    {
        return SteamVR_Actions.htc_viu.viu_press_33.GetStateDown(SteamVR_Input_Sources.LeftHand);
    }
}
