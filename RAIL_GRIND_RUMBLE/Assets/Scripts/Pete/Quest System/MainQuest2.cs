
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
}
