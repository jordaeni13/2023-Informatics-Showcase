using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    public static float time = 0;
    public static float lastTime = 0;
    public static int count = 0;
    public static bool showing = false;
    public static float showTime = 0;
    public static string helperOnInv = "";
    public static string helper = "";
    public static List<SingleParagraph> queue = new List<SingleParagraph>();
    void Start()
    {
        showing = false;
        showTime = 0;
        helperOnInv = "";
        helper = "";
        lastTime = 0;
        time = 0;
        count = 0;
        
        queue.Clear();
    }

    void Update()
    {
        //Debug.Log(queue.Count);
        time += Time.deltaTime;
        updateText();
        timeCounter();
    }
    public static void addActionText(string txt, float show_time)
    {
        Debug.Log("Msg :" + txt);
        if (lastTime < time) lastTime = time;
        SingleParagraph temp = new SingleParagraph(txt, lastTime + show_time);
        lastTime = lastTime + show_time;
        queue.Add(temp);
    }

    public static void addActionTexts(string[] txts, float[] show_times)
    {
        for (int i = 0; i < txts.Length; i++)
        {
            addActionText(txts[i], show_times[i]);
        }
    }
    public static void updateText()
    {
        if (showing)
        {
            //Debug.Log("showing");
            if (time >= showTime)
            {
                helperOnInv = helper;
                helper = "";
                showing = false;
            }
        }
        else
        {
            //Debug.Log("!showing");
            if (queue.Count != 0)
            {
                showing = true;
                helperOnInv = "";
                helper = queue[0].text;
                showTime = queue[0].validTime;
                Debug.Log("showing" + helper + "for" + showTime + "sec");
                queue.RemoveAt(0);
            }
        }
    }
    public static void timeCounter()
    {
        if (time >= count)
        {
            //Debug.Log(time);
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
