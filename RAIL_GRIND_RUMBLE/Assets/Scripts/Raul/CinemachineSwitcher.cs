using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSwitcher : MonoBehaviour
{
    [SerializeField]
    private CinemachineFreeLook BasicCam;
    [SerializeField]
    private CinemachineVirtualCamera NPCTalkingCam;
    private DialogueManager dialogueManager;
    private AutomaticDialogueTrigger autoDialogue;
    private bool startDetecting = false;

    // Start is called before the first frame update
    
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        //autoDialogue = FindObjectOfType<AutomaticDialogueTrigger>();
        BasicCam = GameObject.Find("camerasPrefab").transform.Find("BasicCam").GetComponent<CinemachineFreeLook>();
        SwitchPriorityNeutral();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueManager.isTalking == true)
            {
                SwitchPriorityTalking();
            }

        if (dialogueManager.isTalking == false)
            {
                SwitchPriorityNeutral();
            }
    }

    private void SwitchPriorityTalking()
    {
            BasicCam.Priority = 0;
            NPCTalkingCam.Priority = 1;
            //Debug.Log("TalkingFire");
        
    } 

    private void SwitchPriorityNeutral()
    {
            BasicCam.Priority = 1;
            NPCTalkingCam.Priority = 0;   
            //Debug.Log("NeutralFire");
    } 




}
