using UnityEngine;

public class CanvasandPanel : MonoBehaviour
{
    //Panel
    public GameObject PanelUpgrade;
    public GameObject PanelMagazin;
    public GameObject PanelBot;
    public GameObject PanelExitf;
    public GameObject PanelSeting;
    public GameObject PanelRozrobnika;
    public GameObject PanelproGry;
    public GameObject PanelSkin;
    public GameObject BackgroundPanel;
    public GameObject homePanel;
    //Button
    public GameObject ButtonSkinOsn;
    public GameObject ButtonSkin;
    public GameObject ButtonBack;
    public GameObject ButtonBot;
    public GameObject ButtonUpgardeS;
    public GameObject ButtonMagazin;

    public GameObject panelMusic;

    public MusicManager musicManager;




    private void Start()
    {   // Panel
        PanelUpgrade.SetActive(false);
        PanelMagazin.SetActive(false);
        PanelBot.SetActive(false);
        PanelExitf.SetActive(false);
        PanelSeting.SetActive(true);
        PanelSeting.SetActive(false);
        PanelproGry.SetActive(false);
        PanelRozrobnika.SetActive(false);
        PanelSkin.SetActive(true);
        PanelSkin.SetActive(false);
        BackgroundPanel.SetActive(true);
        BackgroundPanel.SetActive(false);
        //Button
        ButtonSkin.SetActive(false);
        ButtonBack.SetActive(false);
        ButtonBot.SetActive(false);
        ButtonUpgardeS.SetActive(false);
        ButtonMagazin.SetActive(false);

        panelMusic.SetActive(false);

    }

    public void OnPanelMusic()
    {
        panelMusic.SetActive(true);
        PanelMagazin.SetActive(false);
    }

    public void OffPanelMusic()
    {
        panelMusic.SetActive(false);
        PanelMagazin.SetActive(true);

        if (musicManager != null)
        {
            musicManager.RestoreSelectedMusicAfterPanel();
        }
    }


    public void SKINPanel()
    {
        PanelSkin.SetActive(true);
        PanelMagazin.SetActive(false);

    }

    public void SKINSETTINGSPanel()
    {
        ButtonSkin.SetActive(true);
        ButtonSkinOsn.SetActive(false);
        PanelSeting.SetActive(true);
        PanelSkin.SetActive(false);
    }

    public void SKINPanelExit()
    {
        PanelSkin.SetActive(false);
        PanelMagazin.SetActive(true);
    }

    public void SKINPanelExitS()
    {
        PanelSkin.SetActive(true);
        ButtonSkinOsn.SetActive(true);
        ButtonSkin.SetActive(false);
        PanelSeting.SetActive(false);
    }
    public void UpgradePanel()
    {
        PanelUpgrade.SetActive(true);
        homePanel.SetActive(false);
    }

    public void UpgradePanelSet()
    {
        PanelUpgrade.SetActive(false);
        ButtonSkinOsn.SetActive(false);
        PanelSeting.SetActive(true);
        ButtonUpgardeS.SetActive(true);

    }

    public void UpgradePanelExitSet()
    {
        PanelUpgrade.SetActive(true);
        ButtonSkinOsn.SetActive(true);
        PanelSeting.SetActive(false);
        ButtonUpgardeS.SetActive(false);
    }

    public void UpgradePanelExit()
    {
        PanelUpgrade.SetActive(false);
        homePanel.SetActive(true);
    }
    public void MagazinPanel()
    {
        PanelMagazin.SetActive(true);
        homePanel.SetActive(false);
    }

    public void MagazinPanelSet()
    {
        PanelMagazin.SetActive(false);
        ButtonSkinOsn.SetActive(false);
        PanelSeting.SetActive(true);
        ButtonMagazin.SetActive(true);
    }

    public void MagazinPanelExitSet()
    {
        PanelMagazin.SetActive(true);
        ButtonSkinOsn.SetActive(true);
        PanelSeting.SetActive(false);
        ButtonMagazin.SetActive(false);
    }

    public void MagazinPanelExit()
    {
        PanelMagazin.SetActive(false);
        homePanel.SetActive(true);
    }
    public void BotPanel()
    {
        PanelBot.SetActive(true);
        PanelMagazin.SetActive(false);
    }

    public void BOTPanelW()
    {
        PanelBot.SetActive(false);
        PanelSeting.SetActive(true);
        ButtonBot.SetActive(true);
        ButtonSkinOsn.SetActive(false);
    }
    public void BotPanelExit()
    {
        PanelBot.SetActive(false);
        PanelMagazin.SetActive(true);
    }

    public void BotPanelExitS()
    {
        PanelBot.SetActive(true);
        ButtonSkinOsn.SetActive(true);
        ButtonBot.SetActive(false);
        PanelSeting.SetActive(false);
    }

    public void Exitf()
    {
        PanelExitf.SetActive(true);
    }

    public void Exitf1()
    {
        PanelExitf.SetActive(false);
    }

    public void settingsPanelSKIN()
    {
        PanelSeting.SetActive(true);
        PanelSkin.SetActive(false);
    }

    public void settingsPanel()
    {
        PanelSeting.SetActive(true);
        homePanel.SetActive(false);
    }

    public void settingsPanelBack()
    {
        PanelSeting.SetActive(true);
        homePanel.SetActive(false);
    }

    public void settingsPanelBackW()
    {
        PanelSeting.SetActive(false);
        PanelSeting.SetActive(true);
    }

    public void settingsPanelExit()
    {
        PanelSeting.SetActive(false);
        homePanel.SetActive(true);
    }

    public void settingsPanelWW()
    {
        BackgroundPanel.SetActive(false);
        ButtonSkinOsn.SetActive(false);
        ButtonBack.SetActive(true);
        PanelSeting.SetActive(true);
    }
    public void settingsPanelExitS()
    {
        BackgroundPanel.SetActive(true);
        ButtonSkinOsn.SetActive(true);
        ButtonBack.SetActive(false);
        PanelSeting.SetActive(false);
    }

    public void panelrozrob()
    {
        PanelRozrobnika.SetActive(true);
    }

    public void panelrozrobExit()
    {
        PanelRozrobnika.SetActive(false);
    }

    public void panelprogry()
    {
        PanelproGry.SetActive(true);
    }

    public void panelprogryExit()
    {
        PanelproGry.SetActive(false);
    }



    public void BackON() { BackgroundPanel.SetActive(true); PanelMagazin.SetActive(false);}
    public void BackOFF() { BackgroundPanel.SetActive(false); PanelMagazin.SetActive(true);}



}
