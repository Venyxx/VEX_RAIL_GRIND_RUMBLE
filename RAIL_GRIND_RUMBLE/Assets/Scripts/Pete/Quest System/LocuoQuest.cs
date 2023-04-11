using PathCreation;


[System.Serializable]
public class LocuoQuest : Quest
{
    public LocuoQuestGiver questGiver;
    public void Activate()
    {
        questGiver.Activate();
    }
}
