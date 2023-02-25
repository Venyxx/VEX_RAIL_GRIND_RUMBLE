using Unity.VisualScripting;
using System;
using UnityEngine;


public class MainQuest1 : Quest
{
    void Update()
    {
        if (isActive)
        {
            ProgressionManager.Get().mainQuest1Active = true;
        }
    }
}
