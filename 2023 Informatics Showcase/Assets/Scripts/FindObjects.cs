using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindObjects : MonoBehaviour
{
    public static bool enableFind = false;
    public static bool[] doneJobs = new bool[4];
    public static bool successFind = false;
    public static GameObject minchulSlot = GameObject.Find("Slot Minchul");
    // Start is called before the first frame update
    public enum JobsToBeDone
    {
        getSSD,
        getUSB,
        confirmSSD,
        confirmUSB,
    }
    void Awake()
    {
        enableFind = false;
        successFind = false;
        minchulSlot = GameObject.Find("Slot Minchul");
        for(int i = 0; i < doneJobs.Length; i++)
        {
            doneJobs[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(enableFind)
        {
            GameObject trash = FindGameObjectInChildWithTag(minchulSlot, "trash");
            if(trash)
            {
                getOut(trash);
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

    public static GameObject FindGameObjectInChildWithTag(GameObject parent, string tag)
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