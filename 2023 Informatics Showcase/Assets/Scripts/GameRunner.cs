using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Texts;
using Valve.VR;
using static TextHandler.TextUtil;

public class GameRunner : MonoBehaviour
{
    private float time;
    private int status = 0;
    private int seq = -1;
    private TextHandler.TextUtil textUtil = new();
    private bool statum = false;
    void Start()
    {
        time = 0;
        status = 999;
        seq = -1;
        statum = false;
        Debug.Log("Game Started");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (status == 999)
        {
            seq++;
            status = 0;
            Debug.Log("seq = " + seq);
        }
        switch(seq)
        {
            case 0:
                Starter();
                break;
            case 1:
                BroadCast();
                break;
            case 2:
                InteractWithTeacher();
                break;
            case 3:
                FindParts();
                break;
            case 4:
                Assemble();
                break;
            case 5:
                Install();
                break;
            case 6:
                GoHome();
                break;
        }
        
    }
    void Starter()
    {
        if(status == 0)
        {
            TextHandler.AddActionTexts(Help.initstory, Help.initstoryTime);
            TextHandler.AddActionTexts(Help.starter, Help.starterTime);
            status = 1;
        }
        if(status == 1)
        {
            if (Buttons.RTrigger() && TextHandler.Queue.Count == 0)
                {
                    TextHandler.AddActionTexts(Help.started, Help.startedTime);
                    status = 999;
                }
            getReHelp();
        }
    }
    void BroadCast()
    {
        if(status == 0)
        {
            Broadcast.Enabled = true;
            Broadcast.toPlay = true;
            status = 1;
        }
        if(status == 1)
        {
            if(Broadcast.Success)
            {
                status = 999;
            }
            if(TextHandler.Queue.Count == 0) status = 999;
        }
    }
    void InteractWithTeacher()
    {
        if(status == 0)
        {
            TextHandler.Delay(0.7f);
            InteractTeacher.TextUtil.PlaySequence(InteractTeacher.ParaType.atStart,true);
            InteractTeacher.Enabled = true;
            status = 1;
        }
        if(status == 1)
        {
            getReHelp();
            if (InteractTeacher.Success)
            {
                status = 999;
            }

        }
    }
    void FindParts()
    {
        if(status == 0)
        {
            TextHandler.AddActionTexts(Help.findParts, Help.findPartsTime);
            status = 1;
            FindObjects.Enabled = true;
        }
        if(status == 1)
        {
            if(FindObjects.Success)
            {
                TextHandler.AddActionText("�� ã�ҳ׿�!", 3, false, null, 0);
                status = 999;
            }
            getReHelp();
        }
    }
    void Assemble()
    {
        if(status == 0)
        {
            PCInteraction.TextUtil.PlaySequence(PCInteraction.ParaType.atStart, true);
            waypointHandler.setTarget(GameObject.Find("multimedia").transform);
            status = 1;
            PCInteraction.Enabled = true;
        }
        if (status == 1)
        {
            if(PCInteraction.Success)
            {
                TextHandler.AddActionText("������ư�� ���� �ٽ� �Ѻ��ô�.", 3, false, null, 0);
                status = 999;
            }
            getReHelp();
        }
    }

    void Install()
    {
        if (status == 0)
        {
            InstallProcess.Enabled = true;
            status = 1;
        }
        if (status == 1)
        {
            if (Buttons._RTrigger && PCInteraction.hasName("PowerButton"))
            {
                TextHandler.AddActionText("�Ϻ��մϴ�!", 3, false, null, 0);
                TextHandler.AddActionText("��� ������ �Ϸ��Ͽ����ϴ�.", 5, false, null, 0);
                TextHandler.AddActionText("�������� ���� ���� ���ô�", 5, false, null, 0);
            }
            status = 999;
        }
    }
    void GoHome()
    {
        
        if (status == 0)
        {
            status = 1;
        }
        if (status == 1)
        {
            if (PCInteraction.hasName("Collider_Minchul") && statum == false)
            {
                InteractTeacher.talkplay(8);
                statum = true;
                status = 2;
            }

        }
        if( status == 2)
        {
            if (!InteractTeacher.MinchulVoice.GetComponent<AudioSource>().isPlaying)
            {
                TextHandler.AddActionText("The End", 180, false, null, 0);
            }
        }
    }

    void getReHelp()
    {
        if (SteamVR_Actions.htc_viu.viu_press_02.GetStateDown(SteamVR_Input_Sources.LeftHand) && TextHandler.Queue.Count == 0) status = 0;
    }
}

namespace Texts
{
    public class Help
    {
        public static string[] initstory = { "\"�б��� �л� ��Ȱ ����Ʈ ������ ������ ���� ������ ������� � 1��,\"", "\"������ �йڿ� �������� �ٽ� �б��� ������ �ƴ�.\"", "\"�� ���ϰ� ���ϰ��� ���� ������ �߻��ؼ� ū ��Ʈ������ �޾�����\"", "\"���ø�ŭ�� �ƹ��� ���� ���� �߻����� �ʾҴ�\"", "\"���� �ƹ� �� ���� �Ͱ��� �ϴ� ���� �°ǰ�?\"" };
        public static float[] initstoryTime = { 5, 3, 2, 3, 3 };
        public static string[] starter = { "���� Ʈ���Ÿ� �̿��� �κ��丮�� �������", "", "���� ���� ��ư�� �̿��� ��ü�� ��ƺ�����", "", "������ ���� ��Ÿ����", "", "�ٽ� ���� �ʹٸ� ���� ���� ��ư�� �����ּ���", "�غ� �Ǿ��ٸ� ���� Ʈ���Ÿ� �������ô�" };
        public static float[] starterTime = { 3, 3, 3, 3, 3, 2, 3 };
        public static string[] started = { "�����ϴ�", "", "���� �����غ����?" };
        public static float[] startedTime = { 3, 1, 3 };
        public static string interactTeacher = "1�� ������ �տ� ���ö �������� ã�� ��ȭ�ϼ���";
        public static float interactTeacherTime = 5;
        public static string[] findParts = { "���� ��ǰ�� ã�ƺ�����" , "", "SSD�� USB�� ã�ƺ��ô�" };
        public static float[] findPartsTime = { 3, 0.5f, 2 };
        public static string[] assemble = { "��ǻ�͸� ���� ��ǰ�� �����ϼ���" , "", "�κ��丮���� SSD�� ���� ���Կ� ���ٴ뺾�ô�."};
        public static float[] assembleTime = { 3, 1, 2 };
        public static string[] install = { "USB�� ���� �г��� ���Կ� �Ⱦƺ����?", "", "�κ��丮���� USB�� ���� �� ���Կ� ���ٴ뺾�ô�."};
        public static float[] installTime = { 3, 1, 2 };
    }
    public class Dialogue
    {

    }
}