using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallProcess : MonoBehaviour
{
    public static bool Enabled = false, Success = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Enabled)
        {
            MonitorHandler.BootProcess();
        }
    }
}
