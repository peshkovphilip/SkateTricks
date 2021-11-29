
using System;

[Serializable]
public class PlayerData
{
    private InventoryModelOnly inventory = new InventoryModelOnly();
    private int restarts;
    private int currentLevel;
    private int grabRewards;
    private DateTime firstGrabReward;
    
    public int Restarts
    {
        get => restarts;
        set => restarts = value;
    }
    
    public int CurrentLevel
    {
        get => currentLevel;
        set => currentLevel = value;
    }
    
    public int GrabRewards
    {
        get => grabRewards;
        set => grabRewards = value;
    }
    
    public InventoryModelOnly Inventory
    {
        get => inventory;
        set => inventory = value;
    }

    public DateTime FirstGrabReward
    {
        get => firstGrabReward;
        set => firstGrabReward = value;
    }
}
