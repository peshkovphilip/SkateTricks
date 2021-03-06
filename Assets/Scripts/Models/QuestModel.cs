using System;
public class QuestModel
{
    private int _needcount;
    private int _currentCount;
    private int _rewardMoney;
    private EItemType _itemType;
    private EItemType _rewardType;
    private QuestType _questType;
    private bool _isComplete;
    private int _id;

    public int NeedCount => _needcount;
    public int CurrentCount
    {
        get => _currentCount;
        set
        {
            _currentCount = value;
        }
    }
    public int RewardMoney => _rewardMoney;
    public EItemType ItemType => _itemType;
    public EItemType RewardType => _rewardType;
    public QuestType QuestType => _questType;
    public bool IsComplete => _isComplete;
    public int ID => _id;
        
    public event Action<QuestModel> Completed;

    public QuestModel(int id, QuestType questType, int needCount, int rewardMoney, EItemType rewardType, EItemType itemType)
    {
        _id = id;
        _questType = questType;
        _needcount = needCount;
        _currentCount = 0;
        _rewardMoney = rewardMoney;
        _itemType = itemType;
        _rewardType = rewardType;
        _isComplete = false;
    }

    public void QuestComplete()
    {
        _isComplete = true;
        Completed?.Invoke(this);
    }
}
