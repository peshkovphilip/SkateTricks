using System;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class UIController : IStarter
{
    private ButtonsUIModel _buttonsUIModel;
    public event Action<PanelType> PanelView;
    
    public UIController(ButtonsUIModel buttonsUIModel)
    {
        _buttonsUIModel = buttonsUIModel;
    }
    
    public void Starter()
    {
        Debug.Log("start UIController");
        foreach (ButtonUIView button in _buttonsUIModel.Buttons)
        {
            button.OnTap += TryAction;
        }
    }

    public void TryAction(TapUI typeTap)
    {
        switch (typeTap)
        {
            case TapUI.StartGame:
                StartGame();
                break;
            case TapUI.RetryGame:
                RetryGame();
                break;
            case TapUI.NextLevel:
                NextLevel();
                break;    
            case TapUI.SwitchToEn:
                SwitchLanguage(ELanguage.English);
                break; 
            case TapUI.SwitchToRu:
                SwitchLanguage(ELanguage.Russian);
                break; 
        }
    }

    private void StartGame()
    {
        PanelView?.Invoke(PanelType.LevelGame);
        GameAnalytics.SendMessage("start_game");
    }
    
    private void RetryGame()
    {
        GameAnalytics.SendMessage("level_retry");
        Utils.Advertise.ShowInterstitial();
        Game.RestartGame = true;
        SceneManager.LoadScene("MainScene");
        Debug.Log("retry");
    }
    
    private void NextLevel()
    {
        SceneManager.LoadScene("MainScene");
        GameAnalytics.SendMessage("next_level");
    }

    private void SwitchLanguage(ELanguage language)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[(int)language];
    }
}
