using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Video;

public class SubtitleManager : MonoBehaviour
{

    [SerializeField] private GameObject subtitleGO;
    [SerializeField] private GameObject subtitleBackground;

    [SerializeField] private TMP_Text englishSubtitles;
    [SerializeField] private TMP_Text spanishSubtitles;
    
    public EnglishSubtitleText[] englishSubtitleText;
    public SpanishSubtitleText[] SpanishSubtitleText;

    public SpanishMode spanishModeScript;
    public GameObject cutsceneplayerREF;
    CutscenePlayer cutsceneplayer;
    [SerializeField] private VideoClip[] cutsceneClips;

    private bool subtitlePlaying;
    
    
    // Start is called before the first frame update
    void Start()
    {

        spanishModeScript = GetComponent<SpanishMode>();
        cutsceneplayer = cutsceneplayerREF.GetComponent<CutscenePlayer>();
        cutsceneClips = cutsceneplayer.cutsceneClips;
        
        subtitleGO.SetActive(false);
        subtitlePlaying = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (cutsceneplayer.cutscenePlaying == true && subtitlePlaying == false )
        {
            StartSubtitiles();
        }

        if (cutsceneplayer.cutscenePlaying == false)
        {
            subtitlePlaying = false;
        }
            
    }

    public void StartSubtitiles()
    {
        subtitlePlaying = true;
        
        if (SpanishMode.spanishMode == false)
        {
            StartCoroutine(EnglishSubtitleCoroutine());
        }

        if (SpanishMode.spanishMode == true)
        {
            StartCoroutine(SpanishSubtitleCoroutine());
        }
    }
    IEnumerator EnglishSubtitleCoroutine()
    {
        subtitleGO.SetActive(true);


        
        foreach (var voiceLine in englishSubtitleText)
        {
            for (int i = 0; i < cutsceneClips.Length; i++)
            {


                if (voiceLine.cutsceneclip == cutsceneClips[i])
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
                
            }
        }
        
        subtitleGO.SetActive(false);
    }
    
    IEnumerator SpanishSubtitleCoroutine()
    {
        subtitleGO.SetActive(true);
        foreach (var voiceLine in SpanishSubtitleText)
        {
            if (voiceLine.cutsceneclip == "")
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
        }
        
        subtitleGO.SetActive(false);
    }
}

[System.Serializable]
public struct EnglishSubtitleText
{
    public VideoClip cutsceneclip;
    public float time;
    public string text;
}

[System.Serializable]
public struct SpanishSubtitleText
{
    public string cutsceneclip;
    public float time;
    public string text;
}