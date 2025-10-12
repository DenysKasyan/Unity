using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class InstantVideoStart : MonoBehaviour
{
    [Header("Video Components")]
    public VideoPlayer videoPlayer;
    public RawImage rawImage;

    [Header("Placeholder")]
    public Texture firstFrameImage; // PNG із першим кадром

    void Start()
    {
        // Показуємо PNG як заставку
        rawImage.texture = firstFrameImage;

        // Готуємо відео у фоновому режимі
        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnVideoPrepared;
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        // Як тільки відео готове — замінюємо PNG на RenderTexture
        rawImage.texture = vp.targetTexture;

        // Запускаємо відео
        vp.Play();
    }
}
