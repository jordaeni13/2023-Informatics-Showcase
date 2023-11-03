using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainHelper : MonoBehaviour
{
    // Start is called before the first frame update
    public static TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        text.text = TextHandler.Helper;
    }
}
