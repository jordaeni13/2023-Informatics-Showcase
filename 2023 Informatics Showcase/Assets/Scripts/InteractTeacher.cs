using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTeacher : MonoBehaviour
{
    public enum ParaType{
        atStart,
        Dialogue
    }
    public enum Jobs
    {
        InteractMinchul
    }
    public static TextHandler.TextUtil TextUtil;
    public static GameObject MinchulVoice;
    JobHandler JobUtil;
    public static bool Enabled, Success;
    public static AudioClip[] audioClipArray;
    public AudioClip[] audioClipImsi;
    private void Awake()
    {
        TextUtil = new(System.Enum.GetValues(typeof(ParaType)).Length);
        JobUtil = new(System.Enum.GetValues(typeof(Jobs)).Length, "InteractTeacher");
        MinchulVoice = GameObject.Find("MinchulVoice");
        Enabled = Success = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        initTexts();
        for(int i = 0; i < 5; i++)
        {
            audioClipArray[i] = audioClipImsi[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Enabled)
        {
            if (JobUtil.isPost(Jobs.InteractMinchul) && PCInteraction.hasName("Collider_Minchul"))
            {
                waypointHandler.setActive(false);
                TextUtil.PlaySequence(ParaType.Dialogue, true);
                JobUtil.setDone(Jobs.InteractMinchul);
                
            }
            if (JobUtil.allDone() && TextUtil.Available())
            {
                Enabled = false;
                Success = true;
            }
        }
    }
    void initTexts()
    {
        initAtStart();
        initDialogue_1();
    }
    void initAtStart()
    {
        TextUtil.AddParagraph(
            ParaType.atStart,
            "1Ãþ ±³¹«½Ç ¾Õ¿¡ ±è¹ÎÃ¶ ¼±»ý´ÔÀ» Ã£¾Æ ´ëÈ­ÇÏ¼¼¿ä",
            2,
            "Helper",
            true
            );
    }
    void initDialogue_1()
    {
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "¾Æ´Ï ±Û½ê ±× ÇÑÃ¶ÀÌ°¡ SSD¿¡ ¿¡³ÊÁöµå¸µÅ©¸¦ ½ñÀº°Í °°¾Æ",
            7,
            "Dialogue:±è¹ÎÃ¶ ¼±»ý´Ô",
            false
            ,talkplay
            ,0
            );
        TextUtil.AddDelay(ParaType.Dialogue, 1, "");
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "¾Æ´Ï ±Û½ê ±× ÇÑÃ¶ÀÌ°¡ SSD¿¡ ¿¡³ÊÁöµå¸µÅ©¸¦ ½ñÀº ¸ð¾çÀÌ¾ß.\n³Êµµ ¾Ë´Ù½ÃÇÇ ÄÄÇ»ÅÍ ºÎÇ° Áß ÇÑ°³¸¸ °íÀå³ªµµ ¾Æ¿¹ ÀÛµ¿À» ¸ØÃßÀÝ¾Æ",
            7,
            "Dialogue:±è¹ÎÃ¶ ¼±»ý´Ô",
            false,
            talkplay,
            1
            );
        TextUtil.AddDelay(ParaType.Dialogue, 1, "");
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "±×·¡¼­ ³Ê°¡ ºÎÇ°À» ±³Ã¼ÇÏ°í, OSµµ ´Ù½Ã ±ò¾ÆÁáÀ¸¸é ÁÁ°Ú¾î",
            6,
            "Dialogue:±è¹ÎÃ¶ ¼±»ý´Ô",
            false,
            talkplay,
            2
            );
        TextUtil.AddDelay(ParaType.Dialogue, 1, "");
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "³Ü",
            1.5f,
            "Dialogue:³ª",
            false
            );
        TextUtil.AddDelay(ParaType.Dialogue, 1, "");
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "±× ¼³Ä¡ USB´Â ¾Æ±î ¼­ÁØÀÌÇÑÅ× Áá¾ú´Âµ¥",
            3.7f,
            "Dialogue:±è¹ÎÃ¶ ¼±»ý´Ô",
            false,
            talkplay,
            3
            );
        TextUtil.AddDelay(ParaType.Dialogue, 1, "");
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "È¤½Ã ¼­ÁØÀÌ°¡ USB ¾ÈÁá´Ï?",
            3.3f,
            "Dialogue:±è¹ÎÃ¶ ¼±»ý´Ô",
            false

            );
        TextUtil.AddDelay(ParaType.Dialogue, 1.5f, "");

        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "¾Æ¸¶ ¼­ÁØÀÌ°¡ ±î¸ÔÀº°Í °°¾Æ¿ä.",
            3.5f,
            "Dialogue:±è¹ÎÃ¶ ¼±»ý´Ô",
            false
            );
        TextUtil.AddDelay(ParaType.Dialogue, 1, "");
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "¾ÆÀÌ°í ¼­ÁØ¾Æ..",
            2.5f,
            "Dialogue:±è¹ÎÃ¶ ¼±»ý´Ô",
            false,
            talkplay,
            4
            );
        TextUtil.AddDelay(ParaType.Dialogue, 1, "");
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "SSD´Â 1ÇÐ³â 1¹Ý¿¡ ÀÖ´ø°Í °°°í..",
            4f,
            "Dialogue:³ª",
            false
            );
        TextUtil.AddDelay(ParaType.Dialogue, 1, "");
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "USB´Â ¼­ÁØÀÌ ÀÚ¸®¿¡ ÀÖÀ»°Í °°¾Æ¿ä.",
            4f,
            "Dialogue:³ª",
            false
            );
        
        TextUtil.AddDelay(ParaType.Dialogue, 1, "");
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "±×·¯¸é ±×°Íµé °¡Á®¿Í¼­ È®ÀÎ¹Þ°í °íÃÄ¶ó",
            5f,
            "Dialogue:±è¹ÎÃ¶ ¼±»ý´Ô",
            false
            );
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "³Ü",
            0.7f,
            "Dialogue:³ª",
            false
            );
        TextUtil.AddDelay(ParaType.Dialogue, 1, "");
    }

    public static Action<int> talkplay = delegate (int index)
    {
        talk(audioClipArray[index]);
    };
    public static void talk(AudioClip audioClip)
    {
        AudioSource minchulVoice = MinchulVoice.GetComponent<AudioSource>();
        minchulVoice.Stop();
        minchulVoice.clip = audioClip;
        minchulVoice.loop = false;
        minchulVoice.time = 0;
        minchulVoice.Play();
    }
}
