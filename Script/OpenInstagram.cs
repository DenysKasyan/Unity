using UnityEngine;
using UnityEngine.UI;

public class OpenInstagram : MonoBehaviour
{
    public string instagramURL = "https://www.instagram.com/kda.games/";
    public string tiktokurl = "https://t.me/KDAGAMEScompnay";


    public void OnClick()
    {
        Application.OpenURL(instagramURL);
    }

    public void Opentiktok()
    {
        Application.OpenURL(tiktokurl);
    }
}