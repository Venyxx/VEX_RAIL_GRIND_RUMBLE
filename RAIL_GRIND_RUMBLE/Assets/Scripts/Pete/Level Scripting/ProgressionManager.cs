using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{

    private static ProgressionManager instance;

    public int coinCount;
    public bool diegoQuestFinished;
    
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
}
