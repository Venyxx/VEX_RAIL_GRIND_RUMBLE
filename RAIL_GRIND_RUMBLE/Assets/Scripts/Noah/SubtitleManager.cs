using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class SubtitleManager : MonoBehaviour
{

    [SerializeField] private GameObject subtitleGO;

    [SerializeField] private TMP_Text englishSubtitles;

    public EnglishSubtitleText[] englishSubtitleText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SubtitleCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SubtitleCoroutine()
    {
        subtitleGO.SetActive(true);
        foreach (var voiceLine in englishSubtitleText)
        {
            englishSubtitles.text = voiceLine.text;
            yield return new WaitForSecondsRealtime(voiceLine.time);
        }
        
        subtitleGO.SetActive(false);
    }
}

[System.Serializable]
public struct EnglishSubtitleText
{
    public float time;
    public string text;
}
