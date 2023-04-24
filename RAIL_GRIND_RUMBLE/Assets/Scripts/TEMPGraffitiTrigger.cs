using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMPGraffitiTrigger : MonoBehaviour
{
    // Start is called before the first frame update
   void OnTriggerEnter (Collider col)
   {
     if (col.gameObject.tag == "Player")
     {
        ProgressionManager manager = ProgressionManager.Get();
                    if (manager.currentQuest is CountQuest countQuest && countQuest.GetCountQuestType() is CountQuestType.Graffiti)
                    {
                        countQuest.IncrementCount();
                        countQuest.IncrementCount();
                        countQuest.IncrementCount();
                    }
     Destroy(gameObject);
     }
   }
}
