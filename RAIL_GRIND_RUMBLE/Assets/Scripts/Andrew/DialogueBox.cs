using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBox : MonoBehaviour
{
     
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI talkingToName;
    public string dialogBoxName;
    public string[] lines;
    public float textSpeed;
    private int conversationIndex;
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        talkingToName.text = string.Empty;
        StartDialogue();
        StartCoroutine(TypeTalkingToName());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
         {
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
    }

    void StartDialogue()
    {
        conversationIndex = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[conversationIndex].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    IEnumerator TypeTalkingToName()
    {
        foreach (char c in dialogBoxName.ToCharArray())
        {
            talkingToName.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }


    void NextLine()
    {
        if (conversationIndex < lines.Length - 1)
        {
            conversationIndex++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
