using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using static TextHandler.TextUtil;

public class PCInteraction : MonoBehaviour
{
    public static bool pcInteractionEnabled = false;
    public static bool pcInteratcionSuccess = false;
    public bool isOpened = false;
    public static bool isColliding = false;
    public static List<GameObject> collidings = new List<GameObject>();
    public static GameObject pcOpened;
    public static GameObject pcClosed;
    public static GameObject insertedSSD;
    public static GameObject insertedUSB;
    public static int doneJobs = 0;
    public bool isSSD = false;
    public bool isMessageDone = false;
    public SteamVR_Action_Vibration hapticAction;
    public static readonly Vector3 init = new Vector3(0, 0, 0);
    public static readonly Quaternion initq = new Quaternion(0, 0, 0, 0);
    public static object[] tempPos = { init, initq };
    public static TextHandler.TextUtil TextUtil = new();

    public enum JobsToBeDone
    {
        goToMulmi = 1,
        collidePC = 2,
        openPC = 4,
        collideSSD = 8,
        insertSSD = 16,
        turnOnPcPostSSD = 32,
        afterNoBoot = 64,
        collideUSB = 128,
        insertUSB = 256,
        allDone = 512,
    }
    // Start is called before the first frame update
    void Start()
    {
        collidings.Clear();
        isSSD = false;
        doneJobs = 0;
        Debug.Log("Hello PC");
        isOpened = false;
        pcInteractionEnabled = false;
        isMessageDone = false;
        pcInteratcionSuccess = false;
        pcOpened = GameObject.Find("PC_Opened");
        pcClosed = GameObject.Find("PC_Closed");
        insertedSSD = GameObject.Find("980");
        insertedUSB = GameObject.Find("USB");
        pcOpened.SetActive(true);
        pcClosed.SetActive(true);
        insertedSSD.SetActive(false);
        insertedUSB.SetActive(false);
        TextUtil = new TextHandler.TextUtil();
        initTexts();
    }

    // Update is called once per frame
    void Update()
    {
        if (pcInteractionEnabled)
        {
            if (hasName("�ֹ̽�") && jobDoneBefore(JobsToBeDone.goToMulmi))
            {
                TextUtil.PlaySingle(ParaType.Instruction, (int)Instruction.goToPC, true);
                setJobDone(JobsToBeDone.goToMulmi);
            }

            if(hasName("PC_Opened") && jobDoneBefore(JobsToBeDone.collidePC)){
                TextUtil.PlaySingle(ParaType.Instruction, (int)Instruction.assembleParts, true);
                TextUtil.PlaySingle(ParaType.Instruction, (int)Instruction.openPC, false);
                setJobDone(JobsToBeDone.collidePC);
            }

            if (GrabObj.objectInHand == pcClosed && jobDoneBefore(JobsToBeDone.openPC))
            {
                TextUtil.PlaySingle(ParaType.Instruction, (int)Instruction.touchSSD, true);
                tempPos = ReleaseObjectWithDisable();
                pcClosed.SetActive(false);
                setJobDone(JobsToBeDone.openPC);
            }

            if(jobDoneBefore(JobsToBeDone.collideSSD))
            {
                if(hasTag("SSD") && GrabObj.objectInHand != null)
                    
                    if(GrabObj.objectInHand.name == "980Pro")
                        {
                            TextUtil.PlaySingle(
                                    TextHandler.TextUtil.ParaType.Instruction,
                                    (int)Instruction.triggerSSD,
                                    true
                                    );
                            setJobDone(JobsToBeDone.collideSSD);
                        }
            }

            if(jobDoneBefore(JobsToBeDone.insertSSD))
            {
                if (hasTag("SSD") && GrabObj.objectInHand != null)
                    if (GrabObj.objectInHand.name == "980Pro" && SteamVR_Actions.htc_viu.viu_press_33.GetStateDown(SteamVR_Input_Sources.RightHand))
                    {
                        setJobDone(JobsToBeDone.insertSSD);
                        Debug.Log("SSD Inserted");
                        //Sound : ����
                        TextUtil.PlaySingle(
                            TextHandler.TextUtil.ParaType.Instruction,
                            (int)Instruction.doneSSD,
                            true
                            );
                        insertedSSD.SetActive(true);
                        ReleaseObjectWithDisable();
                        pcClosed.GetComponent<Transform>().position = (Vector3)tempPos[0];
                        pcClosed.GetComponent<Transform>().rotation = (Quaternion)tempPos[1];
                        pcClosed.GetComponent<Rigidbody>().isKinematic = true;
                        pcClosed.SetActive(true);
                    }
            }
            if(jobDoneBefore(JobsToBeDone.turnOnPcPostSSD))
            {
                if (hasTag("Power") && SteamVR_Actions.htc_viu.viu_press_33.GetStateDown(SteamVR_Input_Sources.RightHand))
                {
                    setJobDone(JobsToBeDone.turnOnPcPostSSD);
                    Debug.Log("Power Button Clicked");

                }
                if (SteamVR_Actions.htc_viu.viu_press_33.GetStateDown(SteamVR_Input_Sources.RightHand))
                {
                    if (TextUtil.Available())
                    {
                        TextUtil.PlaySingle(
                            ParaType.Instruction,
                            (int)Instruction.touchButton,
                            true
                            );
                    }
                }
            }
            if (jobDoneBefore(JobsToBeDone.afterNoBoot))
            {
                if (true)
                {
                    setJobDone(JobsToBeDone.afterNoBoot);
                }
            }
            if (jobDoneBefore(JobsToBeDone.collideUSB))
            {
                if (hasName("USB_Slot_Collider") && GrabObj.objectInHand != null) 
                    if(GrabObj.objectInHand.name == "memory")
                        {
                            TextUtil.PlaySingle(
                                TextHandler.TextUtil.ParaType.Instruction,
                                (int)Instruction.triggerUSB,
                                true
                                );
                            setJobDone(JobsToBeDone.collideUSB);
                        }
            }
            if(jobDoneBefore(JobsToBeDone.insertUSB))
            {
                if(hasName("USB_Slot_Collider") && GrabObj.objectInHand != null)
                    if(GrabObj.objectInHand.name == "memory")
                        {
                            if (SteamVR_Actions.htc_viu.viu_press_33.GetStateDown(SteamVR_Input_Sources.RightHand))
                            {
                                setJobDone(JobsToBeDone.insertUSB);
                                Debug.Log("USB Inserted");
                                //Sound : ����
                                TextUtil.PlaySingle(
                                    TextHandler.TextUtil.ParaType.Instruction,
                                    (int)Instruction.doneUSB,
                                    true
                                    );
                                insertedUSB.SetActive(true);
                                ReleaseObjectWithDisable();
                            }
                        }
            }
            if(jobDoneBefore(JobsToBeDone.allDone))
            {
                pcInteratcionSuccess = true;
                pcInteractionEnabled = false;
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponent<Rigidbody>() || other.gameObject.CompareTag("Grabbable")) return;
        addCollision(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.GetComponent<Rigidbody>() || other.gameObject.CompareTag("Grabbable")) return;
        deleteCollision(other);
    }
    void addCollision(Collider collision)
    {
        collidings.Add(collision.gameObject);
        //hapticAction.Execute(0, 150, 75, 0.1f, SteamVR_Input_Sources.RightHand);
    }

    void deleteCollision(Collider collision)
    {
        collidings.Remove(collision.gameObject);
    }
    bool hasTag(string tag)
    {
        foreach(GameObject collision in collidings)
        {
            if (collision.CompareTag(tag)) return true;
        }
        return false;
    }

    bool hasName(string name)
    {
        foreach(GameObject collision in collidings)
        {
            if (collision.name == name) return true;
        }
        return false;
    }

    bool jobCheck(JobsToBeDone job)
    {
        return (doneJobs & (int)job) == (int)job;
    }

    bool jobDoneBefore(JobsToBeDone job)
    {
        return ((int)job) - 1 == doneJobs;
    }
    bool jobCheck(JobsToBeDone job1, JobsToBeDone job2)
    {
        return ((doneJobs & (int)job1) == (int)job1) && ((doneJobs & (int)job2) == (int)job2);
    }

    void setJobDone(JobsToBeDone job)
    {
        doneJobs |= (int)job;
        Debug.Log("DoneJobs : " + doneJobs);
    }
    private object[] ReleaseObjectWithDisable()
    {
        Vector3 res = new Vector3();
        Quaternion res2 = new Quaternion();
        if (GetComponent<FixedJoint>())
        {
            GrabObj.objectInHand.GetComponent<Collider>().isTrigger = false;
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            GrabObj.objectInHand.SetActive(false);
            res = GrabObj.objectInHand.transform.position;
            res2 = GrabObj.objectInHand.transform.rotation;
            GrabObj.objectInHand.GetComponent<Transform>().position = new Vector3(1000, 1000, 1000);
            GrabObj.objectInHand = null;
        }
        object[] returner = { res, res2 };
        return returner;
    }
    enum Instruction
    {
        goToPC,
        assembleParts,
        openPC,
        touchSSD,
        triggerSSD,
        doneSSD,
        touchUSB,
        triggerUSB,
        doneUSB,
        touchButton
    }
    void initTexts()
    {
        //atStart
        TextUtil.AddParagraph(
            ParaType.atStart,
            "�����Ƿ� �̵��غ��ô�",
            3.0f,
            "Helper",
            true
            );
        TextUtil.AddDelay(ParaType.atStart, 1.0f, "Helper");
        TextUtil.AddParagraph(
            ParaType.atStart,
            "�������� 1�� ���� �ݴ��� ���� �ֽ��ϴ�.",
            3.0f,
            "Helper",
            true
            );
        //Instruction
        TextUtil.Assign(ParaType.Instruction,
            (int)Instruction.goToPC,
            "���� ��ǻ�ͷ� �����ϼ���",
            1.0f,
            "Helper",
            true
            );
        TextUtil.Assign(ParaType.Instruction,
            (int)Instruction.assembleParts,
            "��ǻ�͸� ���� ��ǰ�� �����ϼ���",
            3.0f,
            "Helper",
            true
            );
        TextUtil.Assign(ParaType.Instruction,
            (int)Instruction.openPC,
            "�����ִ� ��ǻ�͸� ������ �ڵ����� �����ϴ�",
            1.0f,
            "Helper",
            false
            );
        TextUtil.Assign(ParaType.Instruction,
            (int)Instruction.touchSSD,
            "�κ��丮���� SSD�� ���� ���Կ� ���ٴ뺸����",
            3.0f,
            "Helper",
            true
            );
        TextUtil.Assign(ParaType.Instruction,
            (int)Instruction.triggerSSD,
            "���� ���� Ʈ���Ÿ� ��� â���ϼ���!",
            1.0f,
            "Helper",
            false
            );
        TextUtil.Assign(ParaType.Instruction,
            (int)Instruction.doneSSD,
            "SSD�� �����Ǿ����ϴ�",
            1.0f,
            "Helper",
            false
            );
        TextUtil.Assign(ParaType.Instruction,
            (int)Instruction.touchUSB,
            "�κ��丮���� USB�� ���� ���� ���Կ� ���ٴ뺸����",
            3.0f,
            "Helper",
            true
            );
        TextUtil.Assign(ParaType.Instruction,
            (int)Instruction.triggerUSB,
            "���� ���� Ʈ���Ÿ� ��� â���ϼ���!",
            1.0f,
            "Helper",
            false
            );
        TextUtil.Assign(ParaType.Instruction,
            (int)Instruction.doneUSB,
            "USB�� �����Ǿ����ϴ�",
            1.0f,
            "Helper",
            false
            );
        TextUtil.Assign(ParaType.Instruction,
            (int)Instruction.touchButton,
            "������ư�� ���� �Ѻ��ô�!",
            0.1f,
            "Helper",
            false
            );
    }
}
