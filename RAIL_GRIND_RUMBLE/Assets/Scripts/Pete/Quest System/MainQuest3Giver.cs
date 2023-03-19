
using Unity.VisualScripting;
using UnityEngine;

public class MainQuest3Giver : QuestGiver
{
    [SerializeField] private MainQuest3 mainQuest3;
    void Start()
    {
        base.Start();
        
        if (ProgressionManager.Get().mainQuest1.isActive || ProgressionManager.Get().mainQuest1.isComplete)
        {
            this.mainQuest3 = ProgressionManager.Get().mainQuest3;
        }
        questToGive = this.mainQuest3;
    }
}