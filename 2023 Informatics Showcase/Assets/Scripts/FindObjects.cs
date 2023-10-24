using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FindObjects : MonoBehaviour
{
    public static bool enableFind = false;
    public static int doneJobs = 0;
    public static bool successFind = false;
    public static GameObject minchulSlot;
    public static List<GameObject> mainSlots = new List<GameObject>();
    // Start is called before the first frame update
    public enum JobsToBeDone
    {
        confirmSSD = 0b0001,
        confirmUSB = 0b0010,
        getSSD = 0b0100,
        getUSB = 0b1000,
    }
    void Awake()
    {
        enableFind = false;
        successFind = false;
        
        doneJobs = 0;
        minchulSlot = GameObject.Find("Slot Minchul");
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
        if(enableFind)
        {
            if((doneJobs & 0x3) != 0x3) for(int i = 0; i < 4; i++) // jobCheck(JobsToBeDone.getSSD,setJobDone(JobsToBeDone.getUSB));
                {
                GameObject temp = FindGameObjectInChildWithTag(mainSlots[i], "Grabbable");
                if(temp)
                {
                    switch (temp.name)
                    {
                        case "980Pro":
                            if (!jobCheck(JobsToBeDone.getSSD))
                            {
                                setJobDone(JobsToBeDone.getSSD);
                            }
                            break;
                        case "memory":
                            if (!jobCheck(JobsToBeDone.getUSB))
                            {
                                setJobDone(JobsToBeDone.getUSB);
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
                        if (!jobCheck(JobsToBeDone.confirmSSD))
                        {
                            setJobDone(JobsToBeDone.confirmSSD);
                        }
                        break;
                    case "memory":
                        if (!jobCheck(JobsToBeDone.confirmUSB))
                        {
                            setJobDone(JobsToBeDone.confirmUSB);
                        }
                        break;
                }
            }
            if (trashes)
            {
            }
            if (doneJobs == 0b1111) {
                enableFind = false;
                successFind = true;
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

    bool jobCheck(JobsToBeDone job)
    {
        return (doneJobs & (int)job) == (int)job;
    }
    bool jobCheck(JobsToBeDone job1, JobsToBeDone job2)
    {
        return ((doneJobs & (int)job1) == (int)job1) && ((doneJobs & (int)job2) == (int)job2);
    }

    void setJobDone(JobsToBeDone job)
    {
        doneJobs |= (int)job;
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