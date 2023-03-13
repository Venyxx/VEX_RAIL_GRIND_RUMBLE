
using UnityEngine;

public class MainQuest1Giver : QuestGiver
{
    [SerializeField] private MainQuest1 mainQuest1;
    void Start()
    {
        base.Start();
        questToGive = mainQuest1;
    }
}