using System.IO;
using UnityEngine;

public class ProgressController : IAwaker, IStarter
{
    private ButtonsUIModel _buttonsUIModel;
    private PlayerData _playerData;
    
    public ProgressController(PlayerData playerData, ButtonsUIModel buttonsUIModel)
    {
        _buttonsUIModel = buttonsUIModel;
        _playerData = playerData;
    }

    public void Awaker()
    {
        //NewGame();
        _playerData = Game.LoadGame();
    }
    
    public void Starter()
    {
        foreach (ButtonUIView button in _buttonsUIModel.Buttons)
        {
            button.OnTap += TryAction;
        }

        _buttonsUIModel.AddButtonAction += UpdateButtonsSubscription;
    }

    public void UpdateButtonsSubscription(ButtonUIView button)
    {
        button.OnTap += TryAction;
    }

    private void TryAction(TapUI tapType)
    {
        switch (tapType)
        {
            case TapUI.StartGame:
                //LoadGame();
                break;
            case TapUI.NextLevel:
                Game.SaveGame(_playerData);
                break;            
        }
    }

    public void NewGame()
    {
        if (File.Exists(Application.persistentDataPath + "/progress.dat"))
        {
            File.Delete(Application.persistentDataPath + "/progress.dat");
        }
    }
    
    public PlayerData GetPlayerData()
    {
        return _playerData;
    }

}
