using UnityEngine;


[System.Serializable]
public class DialogueTemplate
{
    public DialogueTrigger dialogueTrigger { get; set; }
    public DialogueParagraph paragraphs;

    public DialogueTemplate()
    {
    }

    public DialogueTemplate(string[] speakers, string[] spoken)
    {
        paragraphs = new DialogueParagraph();
        paragraphs.speakers = speakers;
        paragraphs.spokenDialogue = spoken;
    }

}

[System.Serializable]
public struct DialogueParagraph
{
    public string[] speakers;
    
    [TextArea(3,10)]
    public string[] spokenDialogue;
}
