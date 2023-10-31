using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueHelper : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        text.text = TextHandler.Dialogue;
    }
}
