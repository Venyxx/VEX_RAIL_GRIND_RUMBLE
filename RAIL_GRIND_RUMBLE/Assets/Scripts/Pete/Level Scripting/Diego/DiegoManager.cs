using System.Collections;
using System.Collections.Generic;
using Pete.Level_Scripting;
using UnityEngine;

public class DiegoManager : NPCManager
{

    public string[] secondSetNames;
    [TextArea(3,10)]
    public string[] secondSetParagraphs;

    public override void HandleProgress()
    {
        /*Quest firstQuest = GetComponent<QuestGiver>().GetQuest();
        
        if (!ProgressionManager.Get().diegoQuestFinished && firstQuest.RewardsGiven)
        {
            ProgressionManager.Get().diegoQuestFinished = true;
        }
        
        if (ProgressionManager.Get().diegoQuestFinished)
        {
            dialogueTemplate.paragraphs.spokenDialogue = newParagraphs;
        }*/
    }
}
