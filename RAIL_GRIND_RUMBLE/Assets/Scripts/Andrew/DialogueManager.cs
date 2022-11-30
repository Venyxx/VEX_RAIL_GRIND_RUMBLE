using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> paragraphDisplayed;

    // Start is called before the first frame update
    void Start()
    {
        paragraphDisplayed = new Queue<string>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter()
    {
        Debug.Log("trigger");
    }

    public void StartDialogue(DialogueTemplate dialogue)
    {
        Debug.Log("Starting dialogue");
    }
}
