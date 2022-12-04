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
    private bool isBoxActive = false;
    [SerializeField]
    private float textSpeed = 0.1f;

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
        isBoxActive = true;
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
        if (!context.started) return;
      
        if (context.started)
        {
            DisplayNextParagraph();
        }
        
 
    }

    public void DisplayNextParagraph ()
    {
        if (paragraphDisplayed.Count == 0)
        {
            EndDialogue();
            return;
        }

        string paragraph = paragraphDisplayed.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeParagraph(paragraph));
    }

    IEnumerator TypeParagraph(string paragraph)
    {
        textComponent.text = "";
        foreach (char c in paragraph.ToCharArray())
        {
            
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false);
        isBoxActive = false;
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
