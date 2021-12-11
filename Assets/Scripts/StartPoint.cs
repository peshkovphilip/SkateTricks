using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private Controllers controllers;
    private UICanvasView uiCanvasView;
    private PlayerView playerView;
    private PlayerModel playerModel;
    private ButtonsUIModel buttons = new ButtonsUIModel();
    private PlayerData playerData = new PlayerData();
    private ProgressController progressController;
    private ItemView[] items;
    private EnvironmentView[] environments;
    private List<PanelView> panels;

    private void Awake()
    {
        controllers = new Controllers();
        buttons.Buttons = FindObjectsOfType<ButtonUIView>(true).ToList();
        
        ProgressController progressController = new ProgressController(playerData, buttons);
        controllers.Add(progressController);
        controllers.Awaker();
        playerData = progressController.GetPlayerData();
    }
    
    private void Start()
    {
        uiCanvasView = FindObjectOfType<UICanvasView>();
        playerView = FindObjectOfType<PlayerView>(true); 
        playerModel = FindObjectOfType<PlayerModel>(true); 
        items = FindObjectsOfType<ItemView>(true);
        environments = FindObjectsOfType<EnvironmentView>(true);
        panels = uiCanvasView.Panels.ToList();
        
        controllers.Add(new SceneController(panels));
        controllers.Starter();

        StartCoroutine(AwaitSpritesLoading());
    }

    private IEnumerator AwaitSpritesLoading()
    {   
        while (!SpriteManager.LoadingComplete)
        {
            yield return new WaitForSeconds(1f);    
        }

        LoadAllControllers();
    }

    private void LoadAllControllers()
    {
        PlayerController playerController = new PlayerController(playerView, playerModel, items, playerData, environments);
        controllers.Add(playerController);

        UIController uiController = new UIController(buttons);
        controllers.Add(uiController);
        controllers.Add(new ButterflyController());

        AbilityController abilityController = new AbilityController(playerModel);
        controllers.Add(abilityController);
        
        InventoryController inventoryController = new InventoryController(uiCanvasView, playerView, abilityController.GetAbilities(), playerData.Inventory, items);
        controllers.Add(inventoryController);
        
        controllers.Add(new QuestController(inventoryController.Inventory));

        RewardController rewardController = new RewardController(playerData, inventoryController.Inventory, buttons);
        controllers.Add(rewardController);
        
        EnvironmentController environmentController = new EnvironmentController(playerModel, environments);
        controllers.Add(environmentController);
        
        controllers.Add(new PanelsController(uiCanvasView, rewardController, items, playerView, uiController, buttons.Buttons));
        controllers.Add(new AdsController());
        controllers.Starter();
    }

    private void Update()
    {
        controllers.Updater();
    }
}
