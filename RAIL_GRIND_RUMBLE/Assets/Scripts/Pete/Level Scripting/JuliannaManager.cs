
using UnityEngine;

public class JuliannaManager: NPCManager
{
    public DialogueTemplate secondSetDialogue;

    void Start()
    {
        ProgressionManager pm = ProgressionManager.Get();
        if (pm.prologueComplete)
        {
            GetComponent<DialogueTrigger>().dialogue = secondSetDialogue;
            secondSetDialogue.dialogueTrigger = GetComponent<DialogueTrigger>();
        }
    }

    public override void HandleProgress()
    {
        ProgressionManager pm = ProgressionManager.Get();
        if (!pm.prologueComplete) return;

        pm.QuestInfoText.text = "Head to the skate park!";
    }
}
