using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewDialogue",menuName ="Dialogue/DialogueData")]
public class DialogueDataSO : ScriptableObject
{
    [Header("ĳ���� ����")]
    public string characterName = "ĳ����";    // �̸�
    public Sprite characterImage;   //��

    [Header("��ȭ ����")]
    [TextArea(3, 10)]   //�ν����Ϳ��� ���� �� �Է� �����ϵ��� �ϱ�
    public List<string> dialogueLines = new List<string>(); // ��ȭ �����
}
