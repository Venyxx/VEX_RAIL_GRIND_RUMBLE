using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class EndCutSubtitles : MonoBehaviour
{

    [SerializeField] private GameObject subtitleGO;
    [SerializeField] private GameObject subtitleBackground;

    [SerializeField] private TMP_Text englishSubtitles;
    [SerializeField] private TMP_Text spanishSubtitles;

    public EndEnglishSubtitleText[] endEnglishSubtitleText;
    public EndSpanishSubtitleText[] endSpanishSubtitleText;

    public SpanishMode spanishModeScript;

    // Start is called before the first frame update
    void Start()
    {

        spanishModeScript = GetComponent<SpanishMode>();

        if (SpanishMode.spanishMode == false)
        {
            StartCoroutine(EnglishSubtitleCoroutine());
        }

        if (SpanishMode.spanishMode == true)
        {
            StartCoroutine(SpanishSubtitleCoroutine());
        }


    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator EnglishSubtitleCoroutine()
    {
        subtitleGO.SetActive(true);
        foreach (var voiceLine in endEnglishSubtitleText)
        {

            if (voiceLine.text == "")
            {
                subtitleBackground.SetActive(false);
                englishSubtitles.text = voiceLine.text;
                yield return new WaitForSecondsRealtime(voiceLine.time);
            }
            else
            {
                subtitleBackground.SetActive(true);
                englishSubtitles.text = voiceLine.text;
                yield return new WaitForSecondsRealtime(voiceLine.time);
            }

        }

        subtitleGO.SetActive(false);
    }

    IEnumerator SpanishSubtitleCoroutine()
    {
        subtitleGO.SetActive(true);
        foreach (var voiceLine in endSpanishSubtitleText)
        {

            if (voiceLine.text == "")
            {
                subtitleBackground.SetActive(false);
                spanishSubtitles.text = voiceLine.text;
                yield return new WaitForSecondsRealtime(voiceLine.time);
            }
            else
            {
                subtitleBackground.SetActive(true);
                spanishSubtitles.text = voiceLine.text;
                yield return new WaitForSecondsRealtime(voiceLine.time);
            }

        }

        subtitleGO.SetActive(false);
    }
}

[System.Serializable]
public struct EndEnglishSubtitleText
{
    public float time;
    public string text;
}

[System.Serializable]
public struct EndSpanishSubtitleText
{
    public float time;
    public string text;
}