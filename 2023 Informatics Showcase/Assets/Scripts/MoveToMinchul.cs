using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMinchul : MonoBehaviour
{
    public static GameObject minchul;
    public static Vector3 offset;
    public static Vector3 constPos;
    public static Quaternion constRot;
    // Start is called before the first frame update
    void Start()
    {
        minchul = GameObject.Find("Minchul");
        minchul.transform.localScale = new Vector3(0, 0, 0);
        offset = new Vector3(-0.099f, 0.542f, 0.19f);
        constPos = minchul.transform.position + offset;
        constRot = minchul.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = constPos;
        gameObject.transform.rotation = constRot;
    }
}
