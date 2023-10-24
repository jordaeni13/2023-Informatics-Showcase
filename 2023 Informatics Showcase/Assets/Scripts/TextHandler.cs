using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    private static float time = 0;
    private static float lastTime = 0;
    private static int count = 0;
    private static bool showing = false;
    private static float showTime = 0;
    private static string helperOnInv = "";
    private static string helper = "";
    private static List<SingleParagraph> queue = new List<SingleParagraph>(); //Actual Time

    public static float Time { get => time; set => time = value; }
    public static float LastTime { get => lastTime; set => lastTime = value; }
    public static int Count { get => count; set => count = value; }
    public static bool Showing { get => showing; set => showing = value; }
    public static float ShowTime { get => showTime; set => showTime = value; }
    public static string HelperOnInv { get => helperOnInv; set => helperOnInv = value; }
    public static string Helper { get => helper; set => helper = value; }
    public static List<SingleParagraph> Queue { get => queue; set => queue = value; }

    void Start()
    {
        Showing = false;
        ShowTime = 0;
        HelperOnInv = "";
        Helper = "";
        LastTime = 0;
        Time = 0;
        Count = 0;
        
        Queue.Clear();
    }

    void Update()
    {
        //Debug.Log(queue.Count);
        Time += UnityEngine.Time.deltaTime;
        updateText();
        timeCounter();
    }
    public static void addActionText(string txt, float show_time, bool preserve)
    {
        //Debug.Log("Msg :" + txt);
        if (LastTime < Time) LastTime = Time;
        SingleParagraph temp = new SingleParagraph(txt, LastTime + show_time, preserve);
        LastTime = LastTime + show_time;
        Queue.Add(temp);
    }

    public static void addActionTexts(string[] txts, float[] show_times)
    {
        for (int i = 0; i < txts.Length; i++)
        {
            addActionText(txts[i], i < show_times.Length ? show_times[i] : 3, true);
        }
    }
    public static void updateText()
    {
        if (Showing)
        {
            //Debug.Log("showing");
            if (Time >= ShowTime)
            {
                HelperOnInv = Helper;
                Helper = "";
                Showing = false;
            }
        }
        else
        {
            //Debug.Log("!showing");
            if (Queue.Count != 0)
            {
                Showing = true;
                HelperOnInv = "";
                if (Queue[0].Preserve == true)
                {
                    Helper = Queue[0].Text;
                }
                ShowTime = Queue[0].ValidTime;
                Debug.Log("Showing " + Helper + " for " + (ShowTime - Time) + " secs");
                Queue.RemoveAt(0);
            }
        }
    }
    public static void timeCounter()
    {
        if (Time >= Count)
        {
            //Debug.Log(time);
            Count++;
        }
    }
    public class SingleParagraph
    {
        public SingleParagraph(string text, float validTime, bool preserve)
        {
            this.Text = text;
            this.ValidTime = validTime;
            this.Talker = "Helper";
            this.Preserve = preserve;
        }
        public SingleParagraph(string text, float validTime, string talker, bool preserve)
        {
            this.Text = text;
            this.ValidTime = validTime;
            this.Talker = talker;
            this.Preserve = preserve;
        }
        public SingleParagraph(string text, float validTime, bool preserve, Action func)
        {
            this.Text = text;
            this.ValidTime = validTime;
            this.Talker = "Helper";
            this.Preserve = preserve;
            this.Func = func;
        }
        public SingleParagraph(string text, float validTime, string talker, bool preserve, Action func)
        {
            this.Text = text;
            this.ValidTime = validTime;
            this.Talker = talker;
            this.Preserve = preserve;
            this.Func = func;
        }
        private string text;
        private float validTime;
        private string talker;
        private bool preserve;
        private Action func = null;

        public string Text { get => text; set => text = value; }
        public float ValidTime { get => validTime; set => validTime = value; }
        public string Talker { get => talker; set => talker = value; }
        public bool Preserve { get => preserve; set => preserve = value; }
        public Action Func { get => func; set => func = value; }
    }

    public class TextUtil
    {
        public TextUtil()
        {
            this.AtStart = new List<SingleParagraph>();
            this.AtStart.Clear();
            this.Dialogue = new List<SingleParagraph>();
            this.Dialogue.Clear();
            this.Instructions = new List<SingleParagraph>();
            this.Instructions.Clear();
        }
        private List<SingleParagraph> atStart;//Relative Time
        private List<SingleParagraph> dialogue;
        private List<SingleParagraph> instructions;

        public List<SingleParagraph> AtStart { get => atStart; set => atStart = value; }
        public List<SingleParagraph> Dialogue { get => dialogue; set => dialogue = value; }
        public List<SingleParagraph> Instructions { get => instructions; set => instructions = value; }

        public void addAtStart(string text, float time, bool preserve)
        {
            this.AtStart.Add(new SingleParagraph(text, time, preserve));
        }
        public void addDialogue(string text, float time, string talker, bool preserve) {
            Dialogue.Add(new SingleParagraph(text, time, talker, preserve)); 
        }
        public void addInstructions(string text, float time, bool preserve)
        {
            Dialogue.Add(new SingleParagraph(text, time, preserve));
        }
        public void playStart()
        {
            for (int i = 0; i < AtStart.Count; i++)
            {
                addActionText(AtStart[i].Text,
                              AtStart[i].ValidTime,
                              AtStart[i].Preserve);
                AtStart[i].Func?.Invoke();
            }
        }

    }
}
