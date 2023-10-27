using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorHandler : MonoBehaviour
{
    // Start is called before the first frame update
    UnityEngine.Video.VideoClip Clip = null;
    List<UnityEngine.Video.VideoClip> Clips = new List<UnityEngine.Video.VideoClip>();
    void Start()
    {
        GetComponent<UnityEngine.Video.VideoPlayer>().clip = null;
        Clip = null;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
    