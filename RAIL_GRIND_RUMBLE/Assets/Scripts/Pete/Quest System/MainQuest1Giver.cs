
using Unity.VisualScripting;
using UnityEngine;

public class MainQuest1Giver : QuestGiver
{
    [SerializeField] private MainQuest1 mainQuest1;
    void Start()
    {
        base.Start();
        
        if (ProgressionManager.Get().mainQuest1.isActive || ProgressionManager.Get().mainQuest1.isComplete)
        {
            this.mainQuest1 = ProgressionManager.Get().mainQuest1;
        }
        questToGive = this.mainQuest1;
    }
}