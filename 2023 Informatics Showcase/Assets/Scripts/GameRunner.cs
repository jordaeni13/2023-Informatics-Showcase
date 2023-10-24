using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Texts;
using Valve.VR;

public class GameRunner : MonoBehaviour
{
    private float time;
    private int status = 0;
    private int seq = -1;
    void Start()
    {
        time = 0;
        status = 999;
        seq = -1;
        Debug.Log("Game Started");
        TextHandler.addActionText("Game Started", 5.0f);
        
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
            TextHandler.addActionTexts(Help.starter, Help.starterTime);
            status = 1;
        }
        if(status == 1)
        {
            if (SteamVR_Actions.htc_viu.viu_press_33.GetStateDown(SteamVR_Input_Sources.RightHand) && TextHandler.queue.Count == 0)
                {
                    TextHandler.addActionTexts(Help.started, Help.startedTime);
                    status = 999;
                }
            getReHelp();
        }
    }
    void BroadCast()
    {
        if(status == 0)
        {
            //����� ��� �߰�
            GameObject.Find("speaker").GetComponent<AudioSource>().Play();
            status = 1;
        }
        if(status == 1)
        {
            if(TextHandler.queue.Count == 0) status = 999;
        }
    }
    void InteractWithTeacher()
    {
        if(status == 0)
        {
            TextHandler.addActionText(Help.interactTeacher,Help.interactTeacherTime);
            status = 1;
        }
        if(status == 1)
        {
            getReHelp();
            if (TextHandler.queue.Count == 0) status = 999; // ������
        }
    }
    void FindParts()
    {
        if(status == 0)
        {
            TextHandler.addActionTexts(Help.findParts, Help.findPartsTime);
            status = 1;
            FindObjects.enableFind = true;
        }
        if(status == 1)
        {
            if(FindObjects.successFind)
            {
                TextHandler.addActionText("�� ã�ҳ׿�!", 3);
                status = 999;
            }
            getReHelp();
        }
    }
    void Assemble()
    {
        if(status == 0)
        {
            TextHandler.addActionTexts(Help.assemble, Help.assembleTime);
            status = 1;
            PCInteraction.pcInteractionEnabled = true;
        }
        if (status == 1)
        {
            getReHelp();
        }
    }

    void Install()
    {
        if (status == 0)
        {
            TextHandler.addActionTexts(Help.install, Help.installTime);
            status = 1;
        }
        if (status == 1)
        {
       
        }
    }
    void GoHome()
    {

    }

    void getReHelp()
    {
        if (SteamVR_Actions.htc_viu.viu_press_02.GetStateDown(SteamVR_Input_Sources.LeftHand) && TextHandler.queue.Count == 0) status = 0;
    }
}

namespace Texts
{
    public class Help
    {
        public static string[] starter = {"���� Ʈ���Ÿ� �̿��� �κ��丮�� �������", "" , "���� ���� ��ư�� �̿��� ��ü�� ��ƺ�����", "" , "������ ���� ��Ÿ����", "",  "�ٽ� ���� �ʹٸ� ���� ���� ��ư�� �����ּ���" , "�غ� �Ǿ��ٸ� ���� Ʈ���Ÿ� �������ô�"};
        public static float[] starterTime = {3, 3, 3, 3, 3, 1, 3};
        public static string[] started = { "�����ϴ�", "", "���� �����غ����?" };
        public static float[] startedTime = { 3, 1, 3 };
        public static string interactTeacher = "1�� ������ �տ� ���ö �������� ã�� ��ȭ�ϼ���";
        public static float interactTeacherTime = 5;
        public static string[] findParts = { "���� ��ǰ�� ã�ƺ�����" , "", "SSD�� ã�ƺ��ô�" };
        public static float[] findPartsTime = { 3, 0.5f, 2 };
        public static string[] assemble = { "��ǻ�͸� ���� ��ǰ�� �����ϼ���" , "", "�κ��丮���� SSD�� ���� ���Կ� ���ٴ��\n �ڵ����� �����˴ϴ�."};
        public static float[] assembleTime = { 3, 0.5f, 2 };
        public static string[] install = { "�������� ���� ģ���ϰ� �����Բ���\n�̸� ���� USB�� �ȾƵμ̳׿�!", "", "OS�� ��ġ�Ǳ⸦ ��ٸ��ô�."};
        public static float[] installTime = { 3, 1, 4 };
    }
}