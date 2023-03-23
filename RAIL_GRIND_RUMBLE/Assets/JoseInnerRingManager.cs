using System.Collections;
using System.Collections.Generic;
using Pete.Level_Scripting;
//using UnityEditor.Tilemaps;
using UnityEngine;

public class JoseInnerRingManager : NPCManager
{
    private bool used = false;
    public DialogueTemplate joseQuestFinishedDialogue;
    
    public override void HandleProgress()
    {
        if (!used)
        {
            ProgressionManager.Get().mainQuest2.IncrementWayPoint();
            used = true;
        }
    }
}
