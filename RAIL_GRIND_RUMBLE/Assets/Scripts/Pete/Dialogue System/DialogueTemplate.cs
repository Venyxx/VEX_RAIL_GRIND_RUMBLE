using UnityEngine;


[System.Serializable]
public class DialogueTemplate
{
    public DialogueTrigger dialogueTrigger { get; set; }
    public DialogueParagraph paragraphs;

}

[System.Serializable]
public struct DialogueParagraph
{
    public string[] speakers;
    
    [TextArea(3,10)]
    public string[] spokenDialogue;
}
