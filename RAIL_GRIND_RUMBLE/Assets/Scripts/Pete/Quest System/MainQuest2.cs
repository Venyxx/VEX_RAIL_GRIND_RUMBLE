
using TMPro;

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

    public void LoadMainQuest2InnerRing(TotalWaypointController totalWaypointController, TextMeshProUGUI questInfoText)
    {
        if (isActive)
        {
            questInfoText.text = $"Posters Sprayed: {currentCount} / {completionCount}";
            //totalREF = totalWaypointController;
        }
    }

    public override void IncrementCount()
    {
        base.IncrementCount("Posters Sprayed");
    }
}
