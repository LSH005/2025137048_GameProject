using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewDialogue",menuName ="Dialogue/DialogueData")]
public class DialogueDataSO : ScriptableObject
{
    [Header("캐릭터 정보")]
    public string characterName = "캐릭터";    // 이름
    public Sprite characterImage;   //얼굴

    [Header("대화 내용")]
    [TextArea(3, 10)]   //인스펙터에서 여러 줄 입력 가능하도록 하기
    public List<string> dialogueLines = new List<string>(); // 대화 내용들
}
