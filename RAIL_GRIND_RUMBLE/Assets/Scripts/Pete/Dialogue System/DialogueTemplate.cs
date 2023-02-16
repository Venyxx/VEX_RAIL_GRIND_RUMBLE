using UnityEngine;


[System.Serializable]
public class DialogueTemplate
{
    public string name;
    public DialogueTrigger dialogueTrigger;

    [TextArea(3,10)]
    public string[] paragraphs;
}
