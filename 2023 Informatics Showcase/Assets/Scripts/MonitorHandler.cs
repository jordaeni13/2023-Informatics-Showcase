using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MonitorHandler : MonoBehaviour
{
    public static RawImage img;
    public static bool Success;
    public static bool Done;
    public static bool Showing;
    public static float ShowTime;
    public static float LastTime;
    public static List<SingleScreen> Lister = new();

    public static GameObject noboot = GameObject.Find("Display");
    public static GameObject booted = GameObject.Find("Booted");

    public static readonly string parentPath = "C:\\";

    public static float Time = 0;
    // Start is called before the first frame update
    void Start()
    {
        Time = 0;
        GameObject noboot = GameObject.Find("Display");
        GameObject booted = GameObject.Find("Booted");
        img = GetComponent<RawImage>();
        Done = true;
        Lister = new();
        Blank();
        Lister.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        Time += UnityEngine.Time.deltaTime;
        if (!Done)
        {
            update();

        }
    }

    public static void update()
    {
        if (Showing)
        {
            //Debug.Log("showing");
            if (Time >= ShowTime)
            {
                Showing = false;
                Blank();
            }
        }
        if (!Showing && Lister.Count != 0) //바로 표시되게
        {
            Showing = true;
            SetDisplay(Lister[0].Text);
            ShowTime = Lister[0].Time;
            if(Lister[0].End == true)
            {
                Done = true;
                Success = true;
                UnityEngine.Debug.Log("Success");
            }
            Lister.RemoveAt(0);
        }
    }
    public static void BootProcess()
    {
        Done = false;
        Success = false;
        Splash();

        if (!(PCInteraction.JobUtil.isDone(PCInteraction.JobsToBeDone.insertUSB) && PCInteraction.JobUtil.isDone(PCInteraction.JobsToBeDone.insertSSD)))
        {
            //NoBoot();
            noboot.SetActive(true);
            booted.SetActive(false);

        }
        else
        {
            //Boot();
            booted.SetActive(false);
            noboot.SetActive(true);
        }
        LastTime = Time + 5;
        Success = true;
        //Lister[^1].End = true;
        Done = true;
    }
    public static void Splash()
    {

    }
    public static void NoBoot()
    {
        string path = parentPath + "Assets\\MonitorImages\\no_medium.jpg";
        
        Lister.Add(new SingleScreen(null, 5));
        Lister[^1].Text = LoadPNG(path);
    }
    public static void Boot()
    {
        string path = "";
        for(int i = 1; i < (int)(2161/30); i++)
        {
            path = parentPath + string.Format("Assets\\MonitorImages\\bootImgs\\out{0}.png", i*30);
            
            Lister.Add(
                new SingleScreen(null, 0.5f)
            );
            Lister[^1].Text = LoadPNG(path);
        }
    }

    public static void addBlank()
    {
        string path = parentPath + "Assets\\MonitorImages\\Blank.png";
        
        Lister.Add(new SingleScreen(null, 5));
        Lister[^1].Text = LoadPNG(path);
    }

    public static void Blank()
    {
        /*
        string path = parentPath + "Assets\\MonitorImages\\Blank.png";
        SetDisplay(LoadPNG(path));
        Time = 0;
        Lister.Clear();
        */

        booted.SetActive(false);
        noboot.SetActive(false);
    }

    public static void SetDisplay(Texture text)
    {
        img.texture = text;
    }

    public static Texture2D LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            UnityEngine.Debug.Log(filePath);
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        if(tex == null)
        {
            UnityEngine.Debug.Log("null");
        }
        return tex;
    }

    public class SingleScreen {
        public SingleScreen(Texture text, float time)
        {
            Text = text;
            Time = time;
            End = false;
        }
        public Texture Text;
        public float Time;
        public bool End = false;
    }
}
    