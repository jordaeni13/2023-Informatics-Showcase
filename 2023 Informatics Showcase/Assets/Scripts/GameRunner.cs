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
    private int seq = 0;
    void Start()
    {
        time = 0;
        Debug.Log("Game Started");
        TextHandler.showActionText("Game Started", 5.0f);
        
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
            for (int i = 0; i < Help.starter.Length; i++)
            {
                TextHandler.showActionText(Help.starter[i], Help.starterTime[i]);
            }
            status = 1;
        }
        if(status == 1)
        {
            if (SteamVR_Actions.htc_viu.viu_press_02.GetStateDown(SteamVR_Input_Sources.LeftHand) && TextHandler.queue.Count == 0) status = 0;
        }
    }
    void BroadCast()
    {
        if(status == 0)
        {
            //����� ��� �߰�
            status = 999;
        }
    }
    void InteractWithTeacher()
    {
        if(status == 0)
        {
            TextHandler.showActionText(Help.interactTeacher,Help.interactTeacherTime);
            status = 1;
        }
        if(status == 1)
        {
            if (SteamVR_Actions.htc_viu.viu_press_02.GetStateDown(SteamVR_Input_Sources.LeftHand)) status = 0; //���� �ٽ�

            //waitForInteract
        }
    }
    void FindParts()
    {
        if(status == 0)
        {
            for(int i = 0; i < Help.findParts.Length; i++)
            {
                TextHandler.showActionText(Help.findParts[i], Help.findPartsTime[i]);
            }
            status = 1;
        }
        if(status == 1)
        {
            if (SteamVR_Actions.htc_viu.viu_press_02.GetStateDown(SteamVR_Input_Sources.LeftHand)) status = 0; //���� �ٽ�
        }
    }
    void Assemble()
    {
        if(status == 0)
        {
            for(int i = 0; i < Help.assemble.Length; i++)
            {
                TextHandler.showActionText(Help.assemble[i], Help.assembleTime[i]);
            }
            status = 1;
        }
        if (status == 1)
        {
            if (SteamVR_Actions.htc_viu.viu_press_02.GetStateDown(SteamVR_Input_Sources.LeftHand)) status = 0; //���� �ٽ�
        }
    }

    void Install()
    {
        if (status == 0)
        {
            for (int i = 0; i < Help.install.Length; i++)
            {
                TextHandler.showActionText(Help.install[i], Help.installTime[i]);
            }
            status = 1;
        }
        if (status == 1)
        {
       
        }
    }
    void GoHome()
    {

    }
}

namespace Texts
{
    public class Help
    {
        public static string[] starter = {"���� Ʈ���� Ű�� �̿��� �κ��丮�� �������", "" , "���� ���� Ű�� �̿��� ��ü�� ��ƺ�����", "" , "������ ���� ��Ÿ����", "",  "�ٽ� ���� �ʹٸ� ���� ���� ��ư�� �����ּ���" };
        public static float[] starterTime = {3, 3, 3, 3, 3, 1, 3};
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