using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TextHandler.TextUtil;
public class FindObjects : MonoBehaviour
{
    public static bool Enabled = false;
    public static bool Success = false;
    public static GameObject minchulSlot;
    public static List<GameObject> mainSlots = new List<GameObject>();
    public static TextHandler.TextUtil TextUill = new();
    public static JobHandler JobUtil;
    // Start is called before the first frame update
    public enum JobsToBeDone
    {
        confirmSSD,
        confirmUSB,
        getSSD,
        getUSB
    }
    void Awake()
    {
        Enabled = false;
        Success = false;
        minchulSlot = GameObject.Find("Slot Minchul");
        JobUtil = new(System.Enum.GetValues(typeof(JobsToBeDone)).Length, "FindObjects");
    }
    private void Start()
    {
        mainSlots.Clear();
        for (int i = 1; i < 5; i++)
        {
            mainSlots.Add(GameObject.Find("Slot " + i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Enabled)
        {
            if(!JobUtil.isDone(JobsToBeDone.confirmSSD, JobsToBeDone.confirmUSB)) for(int i = 0; i < 4; i++)
                {
                GameObject temp = FindGameObjectInChildWithTag(mainSlots[i], "Grabbable");
                if(temp)
                {
                    switch (temp.name)
                    {
                        case "980Pro":
                            if (!JobUtil.isDone(JobsToBeDone.getSSD))
                            {
                                JobUtil.setDone(JobsToBeDone.getSSD);
                            }
                            break;
                        case "memory":
                            if (!JobUtil.isDone(JobsToBeDone.getUSB))
                            {
                                JobUtil.setDone(JobsToBeDone.getUSB);
                            }
                            break;
                    }
                }
            }
            GameObject grabs = FindGameObjectInChildWithTag(minchulSlot, "Grabbable");
            GameObject trashes = FindGameObjectInChildWithTag(minchulSlot, "trash");
            if (grabs)
            {
                switch (grabs.name)
                {
                    case "980Pro":
                        if (!JobUtil.isDone(JobsToBeDone.confirmSSD))
                        {
                            JobUtil.setDone(JobsToBeDone.confirmSSD);
                        }
                        break;
                    case "memory":
                        if (!JobUtil.isDone(JobsToBeDone.confirmUSB))
                        {
                            JobUtil.setDone(JobsToBeDone.confirmUSB);
                        }
                        break;
                }
            }
            if (trashes)
            {
            }
            if (JobUtil.allDone()) {
                Enabled = false;
                Success = true;
            }
        }
    }
    void getOut(GameObject trash)
    {
        //trash.GetComponent<Rigidbody>;
        Vector3 camPos = Camera.current.transform.position;
        Vector3 handPos = GameObject.Find("RightHand").transform.position;
        if (trash.transform.parent.CompareTag("InventorySlot") && trash.transform.parent.name == "Slot Minchul")
        {
            trash.transform.SetParent(null, true);
        }
        trash.GetComponent<Rigidbody>().velocity = handPos - camPos;
        trash.GetComponent<Rigidbody>().useGravity = true;
    }


    public GameObject FindGameObjectInChildWithTag(GameObject parent, string tag)
    {
        Transform t = parent.transform;

        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).gameObject.tag == tag)
            {
                return t.GetChild(i).gameObject;
            }

        }

        return null;
    }
}