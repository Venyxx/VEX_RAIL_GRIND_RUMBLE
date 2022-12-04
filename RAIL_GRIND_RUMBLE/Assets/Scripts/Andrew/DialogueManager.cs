using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{ 
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI talkingToName;
    public GameObject dialogueBox;
    private Queue<string> paragraphDisplayed;

    // Start is called before the first frame update
    void Start()
    {
        paragraphDisplayed = new Queue<string>();
        textComponent.text = string.Empty;
        talkingToName.text = string.Empty;
        dialogueBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue(DialogueTemplate dialogue)
    {
        dialogueBox.SetActive(true);
        talkingToName.text = dialogue.name;

        paragraphDisplayed.Clear();

        foreach (string paragraph in dialogue.paragraphs)
        {
            paragraphDisplayed.Enqueue(paragraph);
        }

        DisplayNextParagraph();
    }

    public void DialogueInput(InputAction.CallbackContext context)
    {
        DisplayNextParagraph();

    }

    public void DisplayNextParagraph ()
    {
        if (paragraphDisplayed.Count == 0)
        {
            EndDialogue();
            return;
        }

        string paragraph = paragraphDisplayed.Dequeue();
        textComponent.text = paragraph;
    }

    void EndDialogue()
    {
        //dialogueBox.SetActive(false);
    }


    /* FROM THE ORIGINAL DIALOGUEBOX SCRIPT THAT PETE HELPED ME CHANGE
     * 
     public void DialogueInput(InputAction.CallbackContext context)
    {
        if(!context.started) return;

        if (textComponent.text == lines[conversationIndex])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = lines[conversationIndex];
        }
    
    }
     */
}
