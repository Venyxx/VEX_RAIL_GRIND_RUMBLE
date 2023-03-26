using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoController : MonoBehaviour
{
    public UnityEngine.Video.VideoPlayer videoPlayer;
    public RenderTexture renderTexture;
    public Texture2D firstFrameTexture;

    void Awake()
    {
        renderTexture.DiscardContents();
        Graphics.Blit(firstFrameTexture, renderTexture);
    }
    void Start()
    {
       
    }

    void Update()
    {
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        renderTexture.DiscardContents();
        Graphics.Blit(firstFrameTexture, renderTexture);
        this.gameObject.SetActive(false);
    }
}
