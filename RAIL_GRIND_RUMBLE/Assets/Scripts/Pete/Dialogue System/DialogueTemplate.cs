using UnityEngine;


[System.Serializable]
public class DialogueTemplate
{
    public DialogueTrigger dialogueTrigger { get; set; }
    public DialogueParagraph[] paragraphs;

    public DialogueTemplate()
    {
    }

    public DialogueTemplate(DialogueParagraph[] paragraphs)
    {
        this.paragraphs = paragraphs;
    }

    public DialogueTemplate(DialogueParagraph paragraph)
    {
        this.paragraphs = new DialogueParagraph[1];
        paragraphs[0] = paragraph;
    }

}

[System.Serializable]
public struct DialogueParagraph
{
    public DialogueParagraph(string speakerName, string englishDialogue)
    {
        this.speakerName = speakerName;
        this.englishDialogue = englishDialogue;
        englishVoiceLine = null;
        spanishDialogue = null;
        spanishVoiceLine = null;
    }

    public string speakerName;
    [TextArea(3,10)] public string englishDialogue;
    public AudioClip englishVoiceLine;
    [TextArea(3,10)] public string spanishDialogue;
    public AudioClip spanishVoiceLine;
    


}
