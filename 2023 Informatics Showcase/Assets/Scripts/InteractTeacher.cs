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
            "�ƴ� �۽� �� ��ö�̰� SSD�� �������帵ũ�� ���� ����̾�",
            5,
            "Dialogue:���ö ������",
            false
            );
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "�ƴ� �۽� �� ��ö�̰� SSD�� �������帵ũ�� ���� ����̾�.\n�ʵ� �˴ٽ��� ��ǻ�� ��ǰ �� �Ѱ��� ���峪�� �ƿ� �۵��� �����ݾ�",
            5,
            "Dialogue:���ö ������",
            false
            );
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "�׷��� �ʰ� ��ǰ�� ��ü�ϰ�, OS�� �ٽ� ��������� ���ھ�",
            5,
            "Dialogue:���ö ������",
            false
            );
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "��",
            0.5f,
            "Dialogue:��",
            false
            );
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "USB�� �Ʊ� ���������� ����µ�",
            5,
            "Dialogue:���ö ������",
            false
            );
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "SSD�� 1�г� 1�ݿ� �ִ��� ����..",
            0.7f,
            "Dialogue:��",
            false
            );
        TextUtil.AddParagraph(
            ParaType.Dialogue,
            "USB",
            0.7f,
            "Dialogue:��",
            false
            );
    }
}
