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
    private static string dialogue = "Dialogue";
    private static string sender = "Sender";
    private static GameObject dialogueLine;
    private static Action func;
    private static List<SingleParagraph> queue = new List<SingleParagraph>(); //Actual Time

    public static float Time { get => time; set => time = value; }
    public static float LastTime { get => lastTime; set => lastTime = value; }
    public static int Count { get => count; set => count = value; }
    public static bool Showing { get => showing; set => showing = value; }
    public static float ShowTime { get => showTime; set => showTime = value; }
    public static string HelperOnInv { get => helperOnInv; set => helperOnInv = value; }
    public static string Helper { get => helper; set => helper = value; }
    public static string Dialogue { get => dialogue; set => dialogue = value; }
    public static string Sender { get => sender; set => sender = value; }
    public static List<SingleParagraph> Queue { get => queue; set => queue = value; }

    void Start()
    {
        Showing = false;
        ShowTime = 0;
        HelperOnInv = "";
        Helper = "";
        Dialogue = "";
        Sender = "";
        LastTime = 0;
        Time = 0;
        Count = 0;
        dialogueLine = GameObject.Find("DialogueLine");
        dialogueLine.SetActive(false);
        Queue.Clear();
    }

    void Update()
    {
        //Debug.Log(queue.Count);
        Time += UnityEngine.Time.deltaTime;
        UpdateText();
        TimeCounter();
    }
    public static void AddActionText(string txt, float show_time, bool preserve, Action func)
    {
        //Debug.Log("Msg :" + txt);
        if (LastTime < Time) LastTime = Time;
        SingleParagraph temp = new SingleParagraph(txt, LastTime + show_time, "Helper" , preserve, func);
        LastTime = LastTime + show_time;
        Queue.Add(temp);
    }
    public static void AddActionText(string txt, float show_time, string talker, bool preserve, Action func)
    {
        //Debug.Log("Msg :" + txt);
        if (LastTime < Time) LastTime = Time;
        SingleParagraph temp = new SingleParagraph(txt, LastTime + show_time, talker, preserve, func);
        LastTime = LastTime + show_time;
        Queue.Add(temp);
    }

    public static void AddActionTexts(string[] txts, float[] show_times)
    {
        for (int i = 0; i < txts.Length; i++)
        {
            AddActionText(txts[i], i < show_times.Length ? show_times[i] : 3, true, null);
        }
    }

    public static void Delay(float show_time)
    {
        AddActionText("", show_time, false, null);
    }
    public static void UpdateText()
    {
        if (Showing)
        {
            //Debug.Log("showing");
            if (Time >= ShowTime)
            {
                dialogueLine.SetActive(false);
                Dialogue = "";
                Sender = "";
                Helper = "";
                Showing = false;
            }
        }
        if (!Showing && Queue.Count != 0) //바로 표시되게
        {
            Showing = true;
            if (!Queue[0].Talker.Contains("Dialogue:"))
            {
                HelperOnInv = "";
                if (Queue[0].Preserve == true)
                {
                    HelperOnInv = Queue[0].Text;
                }
                Helper = Queue[0].Text;
                ShowTime = Queue[0].ValidTime;
                func?.Invoke();
                Debug.Log("Showing " + Helper + " for " + (ShowTime - Time) + " secs");
                Queue.RemoveAt(0);
            }
            else
            {
                Sender = Queue[0].Talker.Replace("Dialogue:", "");
                dialogueLine.SetActive(true);
                Dialogue = Queue[0].Text;
                ShowTime = Queue[0].ValidTime;
                func?.Invoke();
                Debug.Log("Showing " + Sender + "'s Message " + Dialogue + " for " + (ShowTime - Time) + " secs");
                Queue.RemoveAt(0);
            }
        }
    }
    public static void TimeCounter()
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
            Paragraphs = new List<List<SingleParagraph>>();
            Paragraphs.Clear();
            for(int i = 0; i < 3; i++)
            {
                Paragraphs.Add(new List<SingleParagraph>());
                Paragraphs[i].Clear();
            }
        }

        public TextUtil(int length)
        {
            Paragraphs = new List<List<SingleParagraph>>();
            Paragraphs.Clear();
            for (int i = 0; i < length; i++)
            {
                Paragraphs.Add(new List<SingleParagraph>());
                Paragraphs[i].Clear();
            }
        }
        public enum ParaType
        {
            atStart,
            Dialogue,
            Instruction,
        }
        public List<List<SingleParagraph>> Paragraphs;//Relative Time

        public void AddParagraph(object para, string text, float time, string talker, bool preserve)
        {
            Paragraphs[(int)para].Add(new SingleParagraph(text, time, talker, preserve));
        }
        public void AddParagraph(object para, string text, float time, string talker, bool preserve, Action action)
        {
            Paragraphs[(int)para].Add(new SingleParagraph(text, time, talker, preserve,action));
        }
        public void AddDelay(object para, float time, string talker)
        {
            Paragraphs[(int)para].Add(new SingleParagraph("", time, talker, false));
        }
        public void Assign(object para, object index, string text, float time, string talker, bool preserve)
        {
            while((int)index >= Paragraphs[(int)para].Count)
            {
                Paragraphs[(int)para].Add(null);
            }
            Paragraphs[(int)para][(int)index] = new SingleParagraph(text, time, talker, preserve);
        }
        public void Assign(object para, object index, string text, float time, string talker, bool preserve, Action action)
        {
            while ((int)index >= Paragraphs[(int)para].Count)
            {
                Paragraphs[(int)para].Add(null);
            }
            Paragraphs[(int)para][(int)index] = new SingleParagraph(text, time, talker, preserve, action);
        }
        public void PlaySequence(object paraType, bool force)
        {
            if (force) {
                Clear();
            }
            for (int i = 0; i < Paragraphs[(int)paraType].Count; i++)
            {
                AddActionText(Paragraphs[(int)paraType][i].Text,
                              Paragraphs[(int)paraType][i].ValidTime,
                              Paragraphs[(int)paraType][i].Talker,
                              Paragraphs[(int)paraType][i].Preserve,
                              Paragraphs[(int)paraType][i].Func);
            }
        }
        public void PlaySingle(object paraType, object index, bool force)
        {
            if(force)
            {
                Clear();
            }
            AddActionText(Paragraphs[(int)paraType][(int)index].Text,
                          Paragraphs[(int)paraType][(int)index].ValidTime,
                          Paragraphs[(int)paraType][(int)index].Talker,
                          Paragraphs[(int)paraType][(int)index].Preserve,
                          Paragraphs[(int)paraType][(int)index].Func);
        }
        public void Clear()
        {
            Queue.Clear();
            LastTime = Time;
            Helper = HelperOnInv = "";
            Showing = false;
        }
        public void Delay(float show_time)
        {
            TextHandler.Delay(show_time);
        }
        public bool Available()
        {
            return Queue.Count == 0;
        }
    }
}
