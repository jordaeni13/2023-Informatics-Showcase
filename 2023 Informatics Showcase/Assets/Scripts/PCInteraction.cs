using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

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
    public static int doneJobs = 0;
    public bool isSSD = false;
    public bool isMessageDone = false;
    public SteamVR_Action_Vibration hapticAction;
    public static Vector3 tempPos = new Vector3(0, 0, 0);

    public enum JobsToBeDone
    {
        openPC = 1,
        insertSSD = 2,
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
        pcOpened.SetActive(true);
        pcClosed.SetActive(true);
        insertedSSD.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (pcInteractionEnabled)
        {
            if (GrabObj.objectInHand == pcClosed && doneJobs == 0)
            {
                tempPos = ReleaseObjectWithDisable();
                pcClosed.SetActive(false);
                setJobDone(JobsToBeDone.openPC);
            }
            if(doneJobs == 0x1)
            {
                if (hasTag("SSD"))
                {
                    if (!isSSD)
                    {
                        Debug.Log("SSD collided!");
                    }
                    isSSD = true;

                    if(GrabObj.objectInHand.name == "980Pro")
                    {
                        if (SteamVR_Actions.htc_viu.viu_press_33.GetStateDown(SteamVR_Input_Sources.RightHand))
                        {
                            setJobDone(JobsToBeDone.insertSSD);
                            Debug.Log("SSD Inserted");
                            //Sound : 장착
                            TextHandler.addActionText("SSD 장착완료", 5f);
                            ReleaseObjectWithDisable();
                            pcClosed.GetComponent<Transform>().position = tempPos;
                        }
                        else
                        {
                            if (!isMessageDone)
                            {
                                TextHandler.addActionText("이제 우측 트리거를 누르세요!", 3);
                                isMessageDone = true;
                            }
                        }
                    }
                }
            }
            if(doneJobs == 0x3)
            {
                pcInteratcionSuccess = true;
                pcInteractionEnabled = false;
            }

        }
    }

    void ChangePCState(bool state)
    {
        pcOpened.SetActive(state);
        pcClosed.SetActive(!state);
    }

    /*
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.gameObject.name);
        if (!collision.collider.GetComponent<Rigidbody>() || collision.collider.gameObject.CompareTag("Grabbable"))
        {
            return;
        }
        addCollision(collision.collider);
    }
    void OnCollisionStay(Collision collision)
    {

    }
    void OnCollisionExit(Collision collision)
    {
        if(!collision.collider.GetComponent<Rigidbody>() || collision.gameObject.CompareTag("Grabbable"))
        {
            return;
        }
        deleteCollsion(collision.collider);
    }
    */

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
        Debug.Log(collision.gameObject.name);
        collidings.Add(collision.gameObject);
        hapticAction.Execute(0, 150, 75, 0.1f, SteamVR_Input_Sources.RightHand);
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
        Debug.Log("DoneJobs : " + doneJobs);
    }
    private Vector3 ReleaseObjectWithDisable()
    {
        Vector3 res = new Vector3();
        if (GetComponent<FixedJoint>())
        {
            GrabObj.objectInHand.GetComponent<Collider>().isTrigger = false;
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            GrabObj.objectInHand.SetActive(false);
            res = GrabObj.objectInHand.transform.position;
            GrabObj.objectInHand.GetComponent<Transform>().position = new Vector3(1000, 1000, 1000);
            GrabObj.objectInHand = null;
        }
        return res;
    }
    
}
