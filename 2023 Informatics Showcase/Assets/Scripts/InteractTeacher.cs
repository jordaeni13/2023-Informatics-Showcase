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
            "1�� ������ �տ� ���ö �������� ã�� ��ȭ�ϼ���",
            2,
            "Helper",
            true
            );
    }
    void initDialogue_1()
    {
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "�ƴ� �۽� �� ��ö�̰� SSD�� �������帵ũ�� ������ ����",
            7,
            "Dialogue:���ö ������",
            false
            ,talkplay
            ,0
            );
        TextUtil.AddDelay(ParaType.Dialogue, 1, "");
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "�ƴ� �۽� �� ��ö�̰� SSD�� �������帵ũ�� ���� ����̾�.\n�ʵ� �˴ٽ��� ��ǻ�� ��ǰ �� �Ѱ��� ���峪�� �ƿ� �۵��� �����ݾ�",
            7,
            "Dialogue:���ö ������",
            false,
            talkplay,
            1
            );
        TextUtil.AddDelay(ParaType.Dialogue, 1, "");
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "�׷��� �ʰ� ��ǰ�� ��ü�ϰ�, OS�� �ٽ� ��������� ���ھ�",
            6,
            "Dialogue:���ö ������",
            false,
            talkplay,
            2
            );
        TextUtil.AddDelay(ParaType.Dialogue, 1, "");
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "��",
            1.5f,
            "Dialogue:��",
            false
            );
        TextUtil.AddDelay(ParaType.Dialogue, 1, "");
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "�� ��ġ USB�� �Ʊ� ���������� ����µ�",
            3.7f,
            "Dialogue:���ö ������",
            false,
            talkplay,
            3
            );
        TextUtil.AddDelay(ParaType.Dialogue, 1, "");
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "Ȥ�� �����̰� USB �����?",
            3.3f,
            "Dialogue:���ö ������",
            false

            );
        TextUtil.AddDelay(ParaType.Dialogue, 1.5f, "");

        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "�Ƹ� �����̰� ������� ���ƿ�.",
            3.5f,
            "Dialogue:���ö ������",
            false
            );
        TextUtil.AddDelay(ParaType.Dialogue, 1, "");
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "���̰� ���ؾ�..",
            2.5f,
            "Dialogue:���ö ������",
            false,
            talkplay,
            4
            );
        TextUtil.AddDelay(ParaType.Dialogue, 1, "");
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "SSD�� 1�г� 1�ݿ� �ִ��� ����..",
            4f,
            "Dialogue:��",
            false
            );
        TextUtil.AddDelay(ParaType.Dialogue, 1, "");
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "USB�� ������ �ڸ��� ������ ���ƿ�.",
            4f,
            "Dialogue:��",
            false
            );
        
        TextUtil.AddDelay(ParaType.Dialogue, 1, "");
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "�׷��� �װ͵� �����ͼ� Ȯ�ιް� ���Ķ�",
            5f,
            "Dialogue:���ö ������",
            false
            );
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "��",
            0.7f,
            "Dialogue:��",
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
