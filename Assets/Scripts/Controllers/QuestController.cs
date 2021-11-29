using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class QuestController : IStarter
{
    private InventoryModel _inventory;
    private List<QuestModel> quests = new List<QuestModel>();
    private UICanvasView uICanvasView;

    public QuestController(InventoryModel inventory)
    {
        _inventory = inventory;
    }
    
    public void Starter()
    {
        Debug.Log("start QuestController");
        uICanvasView = Object.FindObjectOfType<UICanvasView>();

        quests.Add(new QuestModel(1, QuestType.Lost, 3, 10, EItemType.Coin, EItemType.Flower)); // download from database 
        //uICanvasView.Panels[(int)PanelType.Quests].SetActive(true);

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
        AcceptQuest(item);
    }

    private void AcceptQuest(ItemModel item = null)
    {
        List<QuestModel> sortedQuests = quests.FindAll(x => x.IsComplete == false && x.CurrentCount == x.NeedCount);
        if (sortedQuests.Count > 0)
        {
            foreach (QuestModel quest in sortedQuests)
            {
                //gameParams.Coins += quest.RewardMoney;
                item.Count = quest.RewardMoney;
                item.ItemType = quest.RewardType;
                _inventory.PickUpItem(item);
                quest.QuestComplete();
                GameAnalytics.SendMessage("quest_completed", ("id", quest.ID));
            }
        }
        UpdateWindow();
    }

    private void UpdateWindow()
    {
        List<QuestModel> sortedQuests = quests.FindAll(x => x.IsComplete == false);
        if (sortedQuests.Count > 0)
        {
            uICanvasView.Panels[(int)PanelType.Quests].gameObject.SetActive(false);
        }
    }

}
