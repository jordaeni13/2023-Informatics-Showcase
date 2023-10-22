using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PCInteraction : MonoBehaviour
{
    public bool isOpened;
    public static bool isColliding;
    public static List<GameObject> collidings;
    public static GameObject pcOpened;
    public static GameObject pcClosed;
    public bool isSSD;
    // Start is called before the first frame update
    void Start()
    {
        collidings = new List<GameObject>();
        isSSD = false;
        Debug.Log("Hello PC");
        isOpened = false;
        pcOpened = GameObject.Find("PC_Opened").gameObject;
        pcClosed = GameObject.Find("PC_Closed").gameObject;
        ChangePCState(isOpened);
    }

    // Update is called once per frame
    void Update()
    {
        if(hasTag("SSD"))
        {
            isSSD = true;
            Debug.Log("SSD collided!");
            if(GameObject.Find("980Pro").transform.parent.CompareTag("InventorySlot"))
            {

            }
        }

    }

    void ChangePCState(bool state)
    {
        pcOpened.SetActive(state);
        pcClosed.SetActive(!state);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(!collision.collider.GetComponent<Rigidbody>() || collision.collider.gameObject.CompareTag("Grabbable"))
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
    void addCollision(Collider collision)
    {
        collidings.Add(collision.gameObject);
    }

    void deleteCollsion(Collider collision)
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

}
