using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoStarter : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage rawImage;
    public RenderTexture renderTexture;

void Start()
{
    
    rawImage.texture = renderTexture;
    videoPlayer.prepareCompleted += OnPrepared;
    videoPlayer.Prepare();
}

void OnPrepared(VideoPlayer vp)
{
    vp.Play();
}
}
