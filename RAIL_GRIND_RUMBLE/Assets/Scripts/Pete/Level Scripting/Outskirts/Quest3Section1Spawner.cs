
using UnityEngine;

public class Quest3Section1Spawner : ContinuousSpawner
{
    [SerializeField] private DialogueTemplate encounterEndDialogue;

    protected override void HandleProgress()
    {
        //POINT CAMERA AT DIEGO FOR HIM TO GET KIDNAPPED
        FindObjectOfType<DialogueManager>().StartAutoDialogue(encounterEndDialogue);
        ProgressionManager progressionManager = ProgressionManager.Get();
        progressionManager.SetQuestInfoText("Save Diego!");
        progressionManager.mainQuest3.GetTotalRef().currentIndex++;
        progressionManager.mainQuest3.ActivateKidnapperVan();
    }


}