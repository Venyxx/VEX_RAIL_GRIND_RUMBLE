using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CutscenePlayer : MonoBehaviour
{
    public VideoClip[] cutsceneClips;
    public RenderTexture[] cutsceneRTs;
    [SerializeField] Texture2D firstFrameTexture;
    VideoPlayer videoPlayer;
    RawImage renderTexture;
    VideoController videoScript;
    public bool cutscenePlaying;
    
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = this.transform.GetChild(0).GetComponent<VideoPlayer>();
        renderTexture = this.transform.GetChild(1).GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.C))
        {
            PlayCutscene(0);
        }*/

        videoPlayer.loopPointReached += EndReached;
    }

    public void PlayCutscene(int clip)
    {
        cutscenePlaying = true;
        Time.timeScale = 0f;

        //Pause other game audio here
        /*AudioListener audio = GameObject.Find("Main Camera").GetComponent<AudioListener>();
        audio.pause = true;*/

        cutsceneRTs[clip].DiscardContents();
        Graphics.Blit(firstFrameTexture, cutsceneRTs[clip]);

        videoPlayer.clip = cutsceneClips[clip];
        videoPlayer.targetTexture = cutsceneRTs[clip];
        renderTexture.texture = cutsceneRTs[clip];

        videoPlayer.gameObject.SetActive(true);
        renderTexture.gameObject.SetActive(true);
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        videoPlayer.gameObject.SetActive(false);
        renderTexture.gameObject.SetActive(false);

        //Unpause other game audio here

        //

        Time.timeScale = 1f;
        cutscenePlaying = false;
    }
}
