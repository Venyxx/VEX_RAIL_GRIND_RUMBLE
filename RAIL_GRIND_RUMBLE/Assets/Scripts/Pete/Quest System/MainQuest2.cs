
using System;
using TMPro;
using UnityEngine;

[System.Serializable]
public class MainQuest2 : CountQuest
{
    private TotalWaypointController totalREF;
    
    
    public void LoadMainQuest2Outskirts(TotalWaypointController totalWaypointController, TextMeshProUGUI questInfoText)
    {
        if (isActive)
        {
            totalREF = totalWaypointController;
            questInfoText.text = "Take the bus to the Inner Ring!"; 
        }
    }

    public void LoadMainQuest2InnerRing(TotalWaypointController totalWaypointController, TextMeshProUGUI questInfoText, GameObject mainQuest2Parent)
    {
        if (isActive)
        {
            totalREF = totalWaypointController;
            foreach (Transform obj in mainQuest2Parent.transform)
            {
                obj.gameObject.SetActive(true);
            }
            questInfoText.text = $"Posters Sprayed: {currentCount} / {completionCount}";
            //totalREF = totalWaypointController;
        }
    }

    public override void IncrementCount()
    {
        base.IncrementCount("Posters Sprayed"); 
        if (isComplete)
        {
            IncrementWayPoint();
            GameObject jose = GameObject.Find("JoseDialogueTrigger");
            DialogueTemplate finishedDialogue = jose.GetComponent<JoseInnerRingManager>().joseQuestFinishedDialogue;
            ProgressionManager.Get().DialogueManager.StartAutoFreezeDialogue(finishedDialogue);
            DialogueParagraph paragraph = new DialogueParagraph("Jose", "...see you at home.");
            jose.GetComponent<DialogueTrigger>().dialogue = new DialogueTemplate(paragraph);

        }
    }

    public void IncrementWayPoint()
    {
        totalREF.currentIndex++;
    }
}
