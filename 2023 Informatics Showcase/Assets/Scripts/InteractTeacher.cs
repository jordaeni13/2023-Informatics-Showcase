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
    JobHandler JobUtil;
    public static bool Enabled, Success;
    private void Awake()
    {
        TextUtil = new(System.Enum.GetValues(typeof(ParaType)).Length);
        JobUtil = new(System.Enum.GetValues(typeof(ParaType)).Length, "InteractTeacher");
        Enabled = Success = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        initTexts();
    }

    // Update is called once per frame
    void Update()
    {
        if (Enabled)
        {
            if (JobUtil.isPost(Jobs.InteractMinchul) && PCInteraction.hasName("Collider_Minchul"))
            {
                TextUtil.PlaySequence(ParaType.Dialogue, true);
                    JobUtil.setDone(Jobs.InteractMinchul);
            }
            if (JobUtil.allDone() && TextUtil.Available())
            {
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
            "1층 교무실 앞에 김민철 선생님을 찾아 대화하세요",
            2,
            "Helper",
            true
            );
    }
    void initDialogue_1()
    {
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "아니 글쎄 그 한철이가 SSD에 에너지드링크를 쏟은 모양이야",
            5,
            "Dialogue:김민철 선생님",
            false
            );
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "아니 글쎄 그 한철이가 SSD에 에너지드링크를 쏟은 모양이야.\n너도 알다시피 컴퓨터 부품 중 한개만 고장나도 아예 작동을 멈추잖아",
            5,
            "Dialogue:김민철 선생님",
            false
            );
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "그래서 너가 부품을 교체하고, OS도 다시 깔아줬으면 좋겠어",
            5,
            "Dialogue:김민철 선생님",
            false
            );
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "넵",
            0.5f,
            "Dialogue:나",
            false
            );
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "USB는 아까 서준이한테 줬었는데",
            5,
            "Dialogue:김민철 선생님",
            false
            );
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "SSD는 1학년 1반에 있던것 같고..",
            0.7f,
            "Dialogue:나",
            false
            );
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "USB",
            0.7f,
            "Dialogue:나",
            false
            );
    }
}
