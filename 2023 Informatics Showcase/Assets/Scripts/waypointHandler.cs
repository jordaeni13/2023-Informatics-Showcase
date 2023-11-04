using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class waypointHandler : MonoBehaviour
{
    public static GameObject Img;
    public static Transform Target;
    public static GameObject DistObject;
    public static TextMeshProUGUI Dist;
    public static bool updated = false;
    // Start is called before the first frame update
    void Start()
    {
        Img = GameObject.Find("waypoint");
        DistObject = GameObject.Find("distanceText");
        Dist = DistObject.GetComponent<TextMeshProUGUI>();
        Dist.text = "";
        setTarget(GameObject.Find("Minchul").transform);
        Img.transform.position = Camera.main.WorldToScreenPoint(Target.position);
        updated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (updated)
        {
            Dist.text = Vector3.Distance(Target.position, transform.position).ToString() + "m";
            Img.transform.position = Camera.main.WorldToScreenPoint(Target.position);
        }
    }

    public static void setTarget(Transform target)
    {
        Target = target;
        setActive(true);
    }
    public static void setActive(bool _)
    {
        Img.SetActive(_);
        DistObject.SetActive(_);
    }
}
