using Unity.VisualScripting;
using System;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


[Serializable]
public class MainQuest1 : CountQuest
{
    private bool destinationReached;
    private GameObject mainQuestParent;
    private TextMeshProUGUI questInfoText;
    private TotalWaypointController totalREF;
    private bool cutscenePlayed = false;
    
    
    public override void IncrementCount()
    {
        if (destinationReached)
        {
            base.IncrementCount("Goons Defeated");
            if (isComplete)
            {
                //change wp
                totalREF.currentIndex++;
                string dialogue = "Great work as always! Now that you've got the parts, come on back.";
                string speaker = "Diego";
                string spanishDialogue = "Gran trabajo como siempre! Ahora que tienes las piezas, vuelve.";
                DialogueParagraph paragraph = new DialogueParagraph(speaker, dialogue);
                paragraph.spanishDialogue = spanishDialogue;
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
                questInfoText.text = "Chase that van!";
                foreach (Transform child in mainQuestParent.transform)
                {
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
        if (!cutscenePlayed)
        {
            ProgressionManager.Get().PlayCutscene(2);
            cutscenePlayed = true;
        }

        destinationReached = true;
        questInfoText = ProgressionManager.Get().QuestInfoText;
        questInfoText.text = $"Goons Defeated: {currentCount} / {completionCount}";
        
    }
}
