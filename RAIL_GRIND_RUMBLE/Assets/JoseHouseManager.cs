using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoseHouseManager : NPCManager
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void HandleProgress()
    {
        /*ProgressionManager pm = ProgressionManager.Get();
        if (!pm.prologueComplete) return;

        pm.QuestInfoText.text = "Head to the skate park!";*/
        
        
        ProgressionManager.Get().PrologueTalkedToJose();
        
    }
}
