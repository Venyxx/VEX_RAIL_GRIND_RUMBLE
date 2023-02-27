using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{

    private static ProgressionManager instance;
    public int coinCount;
    public MainQuest1 mainQuest1;
    public QuestTracker questTracker;

    public static ProgressionManager Get()
    {
        if (instance == null)
        {
            var gameObject = new GameObject("Progression Manager");
            instance = gameObject.AddComponent<ProgressionManager>();
            instance.CreateQuestTracker();
        }

        return instance;
    }

    private void CreateQuestTracker()
    {
        questTracker = gameObject.AddComponent<QuestTracker>();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            CreateQuestTracker();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        
    }
}
