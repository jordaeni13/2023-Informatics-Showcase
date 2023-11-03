using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broadcast : MonoBehaviour
{
    public static bool Enabled;
    public static bool Success;
    public static GameObject speaker;
    public static AudioClip audioClip;
    public static bool toPlay;
    public static TextHandler.TextUtil textUtil = new();
    // Start is called before the first frame update
    void Start()
    {
        Enabled = false;
        toPlay = false;
        Success = false;
        speaker = GameObject.Find("speaker");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Enabled)
        {
            if (!speaker.GetComponent<AudioSource>().isPlaying)
            {
                if (toPlay)
                {
                    speaker.GetComponent<AudioSource>().Play();
                    toPlay = false;
                }
                else
                {
                    waypointHandler.setActive(true);
                    waypointHandler.setTarget(GameObject.Find("Minchul").transform);
                    Success = true;
                }
            }
        }
    }
}
