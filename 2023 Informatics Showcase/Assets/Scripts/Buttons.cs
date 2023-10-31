using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Buttons
{
    public static bool RTrigger() {
        return SteamVR_Actions.htc_viu.viu_press_33.GetStateDown(SteamVR_Input_Sources.RightHand);
    }
}
