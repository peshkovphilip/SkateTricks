using System.Collections.Generic;
using UnityEngine;

public class QuestController : IStarter
{
    private InventoryModel _inventory;
    private List<QuestModel> quests = new List<QuestModel>();
    private GameParams gameParams;
    private UICanvasView uICanvasView;

    public QuestController(InventoryModel inventory)
    {
        _inventory = inventory;
    }
    
    public void Starter()
    {
        Debug.Log("start QuestController");
        gameParams = Object.FindObjectOfType<GameParams>();
        uICanvasView = Object.FindObjectOfType<UICanvasView>();

        quests.Add(new QuestModel(QuestType.Lost, 3, 10, ItemType.Flower)); // download from database 
        uICanvasView.Panels[(int)PanelType.Quests].SetActive(true);

        _inventory.AddAction += CheckAdd;
        _inventory.RemoveAction += UpdateRemove;
    }

    private void CheckAdd(ItemModel item)
    {
        // quest where need collect items
    }

    private void UpdateRemove(ItemModel item)
    {
        quests.FindAll(x => x.QuestType == QuestType.Lost && x.ItemType == item.ItemType).ForEach(x => x.CurrentCount++);
        AcceptQuest();
    }

    private void AcceptQuest()
    {
        List<QuestModel> sortedQuests = quests.FindAll(x => x.IsComplete == false && x.CurrentCount == x.NeedCount);
        if (sortedQuests.Count > 0)
        {
            foreach (QuestModel quest in sortedQuests)
            {
                gameParams.Coins += quest.RewardMoney;
                quest.QuestComplete();
            }
        }
        UpdateWindow();
    }

    private void UpdateWindow()
    {
        List<QuestModel> sortedQuests = quests.FindAll(x => x.IsComplete == false);
        if (sortedQuests.Count > 0)
        {
            uICanvasView.Panels[(int)PanelType.Quests].SetActive(false);
        }
    }

}
