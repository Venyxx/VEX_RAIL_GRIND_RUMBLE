using UnityEngine;


public class MainQuest2Giver : QuestGiver
{
    [SerializeField] private MainQuest2 mainQuest2;
    void Start()
    {
        base.Start();
        
        if (ProgressionManager.Get().mainQuest2.isActive || ProgressionManager.Get().mainQuest2.isComplete)
        {
            this.mainQuest2 = ProgressionManager.Get().mainQuest2;
        }
        questToGive = this.mainQuest2;
    }
}