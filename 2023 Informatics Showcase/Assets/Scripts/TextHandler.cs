using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    private static float time = 0;
    private static float lastTime = 0;
    private static int count = 0;
    private static bool showing;
    public static float showTime = 0;
    public static string helperOnInv;
    public static string helper;
    public static List<SingleParagraph> queue;
    void Start()
    {
        showing = false;
        showTime = 0;
        helperOnInv = "";
        helper = "";
        queue = new List<SingleParagraph>();
    }

    void Update()
    {
        time += Time.deltaTime;
        updateText();
        timeCounter();
    }
    public static void showActionText(string txt, float show_time)
    {
        if (lastTime < time) lastTime = time;
        SingleParagraph temp = new SingleParagraph(txt, lastTime + show_time);
        lastTime = lastTime + show_time;
        queue.Add(temp);
    }
    public void updateText()
    {
        if (showing)
        {
            if (time >= showTime)
            {
                helperOnInv = helper;
                helper = "";
                showing = false;
            }
        }
        else
        {
            if(queue.Count > 0)
            {
                showing = true;
                helperOnInv = "";
                helper = queue[0].text;
                showTime = queue[0].validTime;
                queue.RemoveAt(0);
            }
        }
    }
    public void timeCounter()
    {
        if (time >= count)
        {
            Debug.Log(time);
            count++;
        }
    }
    public class SingleParagraph
    {
        public SingleParagraph(string text, float validTime)
        {
            this.text = text;
            this.validTime = validTime;
        }
        public string text;
        public float validTime;
    }
}
