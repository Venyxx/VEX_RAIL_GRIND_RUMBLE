using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraffitiChecking : MonoBehaviour
{
    public bool isSprayed;
    // Start is called before the first frame update
    void Start()
    {
        isSprayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.tag == "DECAL")
        {
            if (col.gameObject.GetComponent<PosterActive>().isSprayed)
                {
                    Debug.Log("this poster was sprayed");
                    return;
                }
            
            
            ProgressionManager manager = ProgressionManager.Get();
                if (manager.currentQuest is CountQuest countQuest && countQuest.GetCountQuestType() is CountQuestType.Graffiti)
                {
                    countQuest.IncrementCount();
                }

            
        }
    }
}
