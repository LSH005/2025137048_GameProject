using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    public DialogueDataSO myDialogue;

    private DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager=FindObjectOfType<DialogueManager>();
        if (dialogueManager == null)
        {
            Debug.LogError("[CS1002] 다이얼로그 매니저 없음");
        }
    }

    private void OnMouseDown()
    {
        if (dialogueManager == null) return;
        if (dialogueManager.IsDialogueActive()) return;
        if (myDialogue == null) return ;
        dialogueManager.StartDialogue(myDialogue);
    }
}
