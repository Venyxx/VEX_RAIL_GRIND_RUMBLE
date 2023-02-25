using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{

    private static ProgressionManager instance;

    public int coinCount;
    public bool mainQuest1Active;
    
    public static ProgressionManager Get()
    {
        if (instance == null)
        {
            var gameObject = new GameObject("Progression Manager");
            instance = gameObject.AddComponent<ProgressionManager>();
        }

        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (mainQuest1Active)
        {
            FindObjectOfType<QuestTracker>().QuestInfoText.text = "Leave and chase the van!";
        }
    }
}
