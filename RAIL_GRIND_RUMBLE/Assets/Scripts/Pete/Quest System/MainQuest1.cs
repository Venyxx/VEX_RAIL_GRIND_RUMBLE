using Unity.VisualScripting;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


[Serializable]
public class MainQuest1 : CountQuest
{
    private bool destinationReached;
    private GameObject mainQuestParent;
    private TextMeshProUGUI questInfoText { get; set; }
    private bool endDialoguePlayed = false;
    private TotalWaypointController totalREF;
    
    
    public override void IncrementCount()
    {
        if (destinationReached)
        {
            base.IncrementCount("Goons Defeated");
            if (isComplete)
            {
                //change wp
                totalREF.currentIndex++;
                string dialogue = "Great work as always! Now grab the parts and come on back.";
                string speaker = "Diego";
                DialogueParagraph paragraph = new DialogueParagraph(speaker, dialogue);
                DialogueTemplate template = new DialogueTemplate(paragraph);
                ProgressionManager.Get().DialogueManager.StartAutoDialogue(template);
            }
        }
    }


    public void LoadMainQuest1(TotalWaypointController totalREF, GameObject mainQuestParent, TextMeshProUGUI questInfoText)
    {
        if (SceneManager.GetActiveScene().name == "Outskirts" && !RewardsGiven && !isComplete && totalREF != null && mainQuestParent != null && questInfoText != null)
        {
            Debug.Log("MainQuest1 OnSceneLoaded called successfully");
            this.totalREF = totalREF;
            this.mainQuestParent = mainQuestParent;
            this.questInfoText = questInfoText;
            if (isActive)
            {
                foreach (Transform child in mainQuestParent.transform)
                {
                    questInfoText.text = "Chase that van!";
                    child.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            Debug.Log("MainQuest1 OnSceneLoaded not called successfully");
        }
    }
    

    public void VanDestinationReached()
    {
        destinationReached = true;
        questInfoText = ProgressionManager.Get().QuestInfoText;
        questInfoText.text = $"Goons Defeated: {currentCount} / {completionCount}";
    }
}
