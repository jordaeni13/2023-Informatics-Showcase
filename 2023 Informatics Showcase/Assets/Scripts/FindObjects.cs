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
    public static TextHandler.TextUtil TextUtil = new();
    public static JobHandler JobUtil;
    public static bool Inst = false;

    public enum ParaType
    {
        Dialogue_USB,
        Dialogue_SSD,
        Dialogue_NotDone,
        Dialogue_Trash,
        Instruction
    }
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
        TextUtil = new(System.Enum.GetValues(typeof(ParaType)).Length);
        JobUtil = new(System.Enum.GetValues(typeof(JobsToBeDone)).Length, "FindObjects");
    }
    private void Start()
    {
        mainSlots.Clear();
        for (int i = 1; i < 5; i++)
        {
            mainSlots.Add(GameObject.Find("Slot " + i));
        }
        initTexts();
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
                                waypointHandler.setTarget(GameObject.Find("Minchul").transform);
                            }
                            break;
                        case "memory":
                            if (!JobUtil.isDone(JobsToBeDone.getUSB))
                            {
                                JobUtil.setDone(JobsToBeDone.getUSB);
                                waypointHandler.setTarget(GameObject.Find("Minchul").transform);
                            }
                            break;
                    }
                }
            }
            else
            {
                waypointHandler.setActive(true);
                waypointHandler.setTarget(minchulSlot.transform);
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
                            TextUtil.PlaySequence(ParaType.Dialogue_SSD, true);
                            JobUtil.setDone(JobsToBeDone.confirmSSD);
                        }
                        if (!JobUtil.isDone(JobsToBeDone.confirmUSB))
                        {
                            if (Inst == false)
                            {
                                TextUtil.PlaySingle(ParaType.Instruction, Instruction.noUSBConfirm, false);
                                if (!JobUtil.isDone(JobsToBeDone.getUSB))
                                {
                                    TextUtil.PlaySingle(ParaType.Instruction, Instruction.noUSB, false);
                                    waypointHandler.setTarget(GameObject.Find("2_2").transform);
                                }
                                Inst = true;
                            }
                        }
                        break;
                    case "memory":
                        if (!JobUtil.isDone(JobsToBeDone.confirmUSB))
                        {
                            TextUtil.PlaySequence(ParaType.Dialogue_USB, true);
                            JobUtil.setDone(JobsToBeDone.confirmUSB);
                        }
                        if (!JobUtil.isDone(JobsToBeDone.confirmSSD))
                        {
                            if (Inst == false)
                            {
                                TextUtil.PlaySingle(ParaType.Instruction, Instruction.noSSDConfirm, false);
                                if (!JobUtil.isDone(JobsToBeDone.getSSD))
                                {
                                    TextUtil.PlaySingle(ParaType.Instruction, Instruction.noSSD, false);
                                    waypointHandler.setTarget(GameObject.Find("1_1").transform);
                                }
                                Inst = true;
                            }
                        }
                        break;
                }
            }
            else
            {
                Inst = false;
            }
            if (trashes)
            {
                trashes.transform.SetParent(null, false);
                trashes.GetComponent<Transform>().SetPositionAndRotation(new Vector3(1000, 1000, 1000), new Quaternion(0, 0, 0, 0));
                trashes.GetComponent<Rigidbody>().isKinematic = true;
                trashes.SetActive(false);
                TextUtil.PlaySequence(ParaType.Dialogue_Trash, true);
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

    void initTexts()
    {
        initDialogue_Minchul_Trash();
        initInstruction();
        initDialogue_Minchul_SSD();
        initDialogue_Minchul_USB();
        initDialogue_Minchul_Trash();
        
    }
    void initDialogue_Minchul_Trash()
    {
        TextUtil.AddParagraph(
            ParaType.Dialogue_Trash,
            "ÀÌ°Å ¸ÀÀÖ±ä ÇÑµ¥..",
            3f,
            "Dialogue:±è¹ÎÃ¶ ¼±»ý´Ô",
            false,
            InteractTeacher.talkplay,
            7
            );
        TextUtil.AddDelay(ParaType.Dialogue_Trash, 0.5f, "Dialogue:±è¹ÎÃ¶ ¼±»ý´Ô");
        TextUtil.AddParagraph(
            ParaType.Dialogue_Trash,
            "ÇÊ¿äÇÑ°Ç ¾Æ´Ñ°Í °°¾Æ",
            2f,
            "Dialogue:±è¹ÎÃ¶ ¼±»ý´Ô",
            false
            );
        TextUtil.AddParagraph(
            ParaType.Dialogue_Trash,
            "³Ü",
            1f,
            "Dialogue:³ª",
            false
            );
    }
    void initDialogue_Minchul_SSD()
    {
        TextUtil.AddParagraph(
            ParaType.Dialogue_SSD,
            "SSD Àß °¡Á®¿Ô±¸³ª",
            3f,
            "Dialogue:±è¹ÎÃ¶ ¼±»ý´Ô",
            false,
            InteractTeacher.talkplay,
            5
            );

    }
    void initDialogue_Minchul_USB()
    {
        TextUtil.AddParagraph(
            ParaType.Dialogue_USB,
            "USB Àß °¡Á®¿Ô±¸³ª",
            3f,
            "Dialogue:±è¹ÎÃ¶ ¼±»ý´Ô",
            false,
            InteractTeacher.talkplay,
            6
            );
    }
    enum Instruction
    {
        noSSDConfirm,
        noUSBConfirm,
        noSSD,
        noUSB
    }
    void initInstruction()
    {
        TextUtil.Assign(
            ParaType.Instruction,
            Instruction.noSSDConfirm,
            "±×·±µ¥ SSD´Â?",
            0.5f,
            "Dialogue:±è¹ÎÃ¶ ¼±»ý´Ô",
            false
            );
        TextUtil.Assign(
            ParaType.Instruction,
            Instruction.noUSBConfirm,
            "±×·±µ¥ USB´Â?",
            0.5f,
            "Dialogue:±è¹ÎÃ¶ ¼±»ý´Ô",
            false
            );
        TextUtil.Assign(
            ParaType.Instruction,
            Instruction.noUSB,
            "2ÇÐ³â 2¹ÝÀ¸·Î °¡¼­ USB¸¦ Ã£¾Æº¾½Ã´Ù",
            0.5f,
            "Helper",
            false
            );
        TextUtil.Assign(
            ParaType.Instruction,
            Instruction.noSSD,
            "1ÇÐ³â 1¹ÝÀ¸·Î °¡¼­ SSD¸¦ Ã£¾Æº¾½Ã´Ù",
            0.5f,
            "Helper",
            false
            );
    }
}