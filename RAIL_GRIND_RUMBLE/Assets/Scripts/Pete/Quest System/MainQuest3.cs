using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


[System.Serializable]
public class MainQuest3 : Quest
{
    private TotalWaypointController totalREF;
    private GameObject mainQuestParent;
    private TextMeshProUGUI questInfoText;
    private GameObject firstSectionSpawner;
    private GameObject secondSectionSpawner;
    private Transform firstSectionFenceBlocker;
    private GameObject kidnapperVan;



    public void LoadMainQuest3(TotalWaypointController totalREF, GameObject mainQuestParent, TextMeshProUGUI questInfoText)
    {
        if (SceneManager.GetActiveScene().name == "Outskirts" && !RewardsGiven && !isComplete && totalREF != null && mainQuestParent != null && questInfoText != null)
        {
            Debug.Log("MainQuest3 OnSceneLoaded called successfully");
            this.totalREF = totalREF;
            this.mainQuestParent = mainQuestParent;
            this.questInfoText = questInfoText;
            if (isActive)
            {
                questInfoText.text = "Go meet Diego at the Orphanage";
                mainQuestParent.transform.Find("OutskirtsDiego").gameObject.SetActive(true);
                firstSectionSpawner = mainQuestParent.transform.Find("VanSpawnerSection1").gameObject;
                secondSectionSpawner = mainQuestParent.transform.Find("VanSpawnerSetSection2").gameObject;
                firstSectionFenceBlocker = mainQuestParent.transform.Find("FenceBlocker");
                kidnapperVan = mainQuestParent.transform.Find("KidnapperVan").gameObject;
            }
        }
        else
        {
            Debug.Log("MainQuest3 OnSceneLoaded not called successfully");
        }
    }

    public void BeginCombat()
    {
        firstSectionSpawner.SetActive(true);
        firstSectionFenceBlocker.gameObject.SetActive(true);
        secondSectionSpawner.SetActive(true);
        GameObject.Find("OpenGatesQuest3").SetActive(false);
        kidnapperVan.SetActive(true);
        totalREF.currentIndex++;
    }

    public TotalWaypointController GetTotalRef()
    {
        return totalREF;
    }

    public void ActivateKidnapperVan()
    {
        kidnapperVan.GetComponent<ServosVanController>().enabled = true;
        secondSectionSpawner.GetComponent<ContinuousSpawner>().enabled = true;
        ProgressionManager.Get().CompleteQuest();
        //RewardsGiven = true;
    }

}
