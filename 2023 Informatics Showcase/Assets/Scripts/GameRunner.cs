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
            //오디오 재생 추가
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
            if (TextHandler.queue.Count == 0) status = 999; // 디버깅용
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
                TextHandler.addActionText("다 찾았네요!", 3);
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
        public static string[] starter = {"좌측 트리거를 이용해 인벤토리를 열어보세요", "" , "우측 측면 버튼을 이용해 물체를 잡아보세요", "" , "도움말은 여기 나타나며", "",  "다시 보고 싶다면 촤측 측면 버튼을 눌러주세요" , "준비가 되었다면 우측 트리거를 눌러봅시다"};
        public static float[] starterTime = {3, 3, 3, 3, 3, 1, 3};
        public static string[] started = { "좋습니다", "", "이제 시작해볼까요?" };
        public static float[] startedTime = { 3, 1, 3 };
        public static string interactTeacher = "1층 교무실 앞에 김민철 선생님을 찾아 대화하세요";
        public static float interactTeacherTime = 5;
        public static string[] findParts = { "이제 부품을 찾아보세요" , "", "SSD를 찾아봅시다" };
        public static float[] findPartsTime = { 3, 0.5f, 2 };
        public static string[] assemble = { "컴퓨터를 열고 부품을 조립하세요" , "", "인벤토리에서 SSD를 꺼내 슬롯에 갖다대면\n 자동으로 장착됩니다."};
        public static float[] assembleTime = { 3, 0.5f, 2 };
        public static string[] install = { "여러분을 위해 친절하게 선생님께서\n미리 복구 USB를 꽂아두셨네요!", "", "OS가 설치되기를 기다립시다."};
        public static float[] installTime = { 3, 1, 4 };
    }
}