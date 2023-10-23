using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindObjects : MonoBehaviour
{
    public static bool enableFind = false;
    public static bool successFind = false;
    // Start is called before the first frame update
    void Start()
    {
        enableFind = false;
        successFind = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(enableFind)
        {
            GameObject trash = FindGameObjectInChildWithTag(this.gameObject, "trash");
            if(trash)
            {

            }
        }
    }
    void ơ(GameObject trash)
    {
        //trash.GetComponent<Rigidbody>;
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