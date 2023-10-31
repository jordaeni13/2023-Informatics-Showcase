using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using static TextHandler.TextUtil;

public class PCInteraction : MonoBehaviour
{
    public enum JobsToBeDone
    {
        goToMulmi,
        collidePC,
        openPC,
        collideSSD,
        insertSSD,
        turnOnPcPostSSD,
        afterNoBoot,
        collideUSB,
        insertUSB
    }

    public static bool Enabled = false;
    public static bool Success = false;
    public static bool isColliding = false;
    public static List<GameObject> collidings = new List<GameObject>();
    public static GameObject pcOpened;
    public static GameObject pcClosed;
    public static GameObject insertedSSD;
    public static GameObject insertedUSB;
    public bool isMessageDone = false;
    public SteamVR_Action_Vibration hapticAction;
    public static readonly Vector3 init = new Vector3(0, 0, 0);
    public static readonly Quaternion initq = new Quaternion(0, 0, 0, 0);
    public static object[] tempPos = { init, initq };
    public static TextHandler.TextUtil TextUtil = new();
    public static JobHandler JobUtil;


    // Start is called before the first frame update
    void Start()
    {
        collidings.Clear();
        Debug.Log("Hello PC");
        Enabled = false;
        isMessageDone = false;
        Success = false;
        pcOpened = GameObject.Find("PC_Opened");
        pcClosed = GameObject.Find("PC_Closed");
        insertedSSD = GameObject.Find("980");
        insertedUSB = GameObject.Find("USB");
        pcOpened.SetActive(true);
        pcClosed.SetActive(true);
        insertedSSD.SetActive(false);
        insertedUSB.SetActive(false);
        TextUtil = new TextHandler.TextUtil();
        JobUtil = new(System.Enum.GetValues(typeof(JobsToBeDone)).Length, "PCInteraction");
        initTexts();
    }

    // Update is called once per frame
    void Update()
    {
        if (Enabled)
        {
            if (hasName("�ֹ̽�") && JobUtil.isPost(JobsToBeDone.goToMulmi))
            {
                TextUtil.PlaySingle(ParaType.Instruction, (int)Instruction.goToPC, true);
                JobUtil.setDone(JobsToBeDone.goToMulmi);
            }

            if(hasName("PC_Opened") && JobUtil.isPost(JobsToBeDone.collidePC)){
                TextUtil.PlaySingle(ParaType.Instruction, (int)Instruction.assembleParts, true);
                TextUtil.PlaySingle(ParaType.Instruction, (int)Instruction.openPC, false);
                JobUtil.setDone(JobsToBeDone.collidePC);
            }

            if (GrabObj.objectInHand == pcClosed && JobUtil.isDone(JobsToBeDone.openPC))
            {
                TextUtil.PlaySingle(ParaType.Instruction, (int)Instruction.touchSSD, true);
                tempPos = killGrab();
                pcClosed.SetActive(false);
                JobUtil.setDone(JobsToBeDone.openPC);
            }

            if(JobUtil.isPost(JobsToBeDone.collideSSD))
            {
                if(hasTag("SSD") && GrabObj.objectInHand != null)
                    
                    if(GrabObj.objectInHand.name == "980Pro")
                        {
                            TextUtil.PlaySingle(
                                    TextHandler.TextUtil.ParaType.Instruction,
                                    (int)Instruction.triggerSSD,
                                    true
                                    );
                            JobUtil.setDone(JobsToBeDone.collideSSD);
                        }
            }

            if(JobUtil.isPost(JobsToBeDone.insertSSD))
            {
                if (hasTag("SSD") && GrabObj.objectInHand != null)
                    if (GrabObj.objectInHand.name == "980Pro" && Buttons.RTrigger())
                    {
                        JobUtil.setDone(JobsToBeDone.insertSSD);
                        Debug.Log("SSD Inserted");
                        //Sound : ����
                        TextUtil.PlaySingle(
                            TextHandler.TextUtil.ParaType.Instruction,
                            (int)Instruction.doneSSD,
                            true
                            );
                        insertedSSD.SetActive(true);
                        killGrab();
                        fixObject(pcClosed,tempPos);
                    }
            }
            if(JobUtil.isPost(JobsToBeDone.turnOnPcPostSSD))
            {
                if (hasTag("Power") && Buttons.RTrigger())
                {
                    JobUtil.setDone(JobsToBeDone.turnOnPcPostSSD);
                    Debug.Log("Power Button Clicked");

                }
                if (Buttons.RTrigger())
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
            if (JobUtil.isPost(JobsToBeDone.afterNoBoot))
            {
                if (true)
                {
                    JobUtil.setDone(JobsToBeDone.afterNoBoot);
                }
            }
            if (JobUtil.isPost(JobsToBeDone.collideUSB))
            {
                if (hasName("USB_Slot_Collider") && GrabObj.objectInHand != null) 
                    if(GrabObj.objectInHand.name == "memory")
                        {
                            TextUtil.PlaySingle(
                                TextHandler.TextUtil.ParaType.Instruction,
                                (int)Instruction.triggerUSB,
                                true
                                );
                            JobUtil.setDone(JobsToBeDone.collideUSB);
                        }
            }
            if(JobUtil.isPost(JobsToBeDone.insertUSB))
            {
                if(hasName("USB_Slot_Collider") && GrabObj.objectInHand != null)
                    if(GrabObj.objectInHand.name == "memory")
                        {
                            if (Buttons.RTrigger())
                            {
                                JobUtil.setDone(JobsToBeDone.insertUSB);
                                Debug.Log("USB Inserted");
                                //Sound : ����
                                TextUtil.PlaySingle(
                                    TextHandler.TextUtil.ParaType.Instruction,
                                    (int)Instruction.doneUSB,
                                    true
                                    );
                                insertedUSB.SetActive(true);
                                killGrab();
                            }
                        }
            }
            if(JobUtil.allDone())
            {
                Success = true;
                Enabled = false;
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

    void fixObject(GameObject @object, object[] temppos)
    {
        @object.GetComponent<Transform>().position = (Vector3)temppos[0];
        @object.GetComponent<Transform>().rotation = (Quaternion)temppos[1];
        @object.GetComponent<Rigidbody>().isKinematic = true;
        @object.SetActive(true);
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
    private object[] killGrab()
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
            Instruction.goToPC,
            "���� ��ǻ�ͷ� �����ϼ���",
            1.0f,
            "Helper",
            true
            );
        TextUtil.Assign(ParaType.Instruction,
            Instruction.assembleParts,
            "��ǻ�͸� ���� ��ǰ�� �����ϼ���",
            3.0f,
            "Helper",
            true
            );
        TextUtil.Assign(ParaType.Instruction,
            Instruction.openPC,
            "�����ִ� ��ǻ�͸� ������ �ڵ����� �����ϴ�",
            1.0f,
            "Helper",
            false
            );
        TextUtil.Assign(ParaType.Instruction,
            Instruction.touchSSD,
            "�κ��丮���� SSD�� ���� ���Կ� ���ٴ뺸����",
            3.0f,
            "Helper",
            true
            );
        TextUtil.Assign(ParaType.Instruction,
            Instruction.triggerSSD,
            "���� ���� Ʈ���Ÿ� ��� â���ϼ���!",
            1.0f,
            "Helper",
            false
            );
        TextUtil.Assign(ParaType.Instruction,
            Instruction.doneSSD,
            "SSD�� �����Ǿ����ϴ�",
            1.0f,
            "Helper",
            false
            );
        TextUtil.Assign(ParaType.Instruction,
            Instruction.touchUSB,
            "�κ��丮���� USB�� ���� ���� ���Կ� ���ٴ뺸����",
            3.0f,
            "Helper",
            true
            );
        TextUtil.Assign(ParaType.Instruction,
            Instruction.triggerUSB,
            "���� ���� Ʈ���Ÿ� ��� â���ϼ���!",
            1.0f,
            "Helper",
            false
            );
        TextUtil.Assign(ParaType.Instruction,
            Instruction.doneUSB,
            "USB�� �����Ǿ����ϴ�",
            1.0f,
            "Helper",
            false
            );
        TextUtil.Assign(ParaType.Instruction,
            Instruction.touchButton,
            "������ư�� ���� �Ѻ��ô�!",
            0.1f,
            "Helper",
            false
            );
    }
}
