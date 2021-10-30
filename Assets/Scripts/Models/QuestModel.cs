using System;
public class QuestModel
{
    private int _needcount;
    private int _currentCount;
    private int _rewardMoney;
    private ItemType _itemType;
    private QuestType _questType;
    private bool _isComplete;

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
    public ItemType ItemType => _itemType;
    public QuestType QuestType => _questType;
    public bool IsComplete => _isComplete;
        
    public event Action<QuestModel> Completed;

    public QuestModel(QuestType questType, int needCount, int rewardMoney, ItemType itemType)
    {
        _questType = questType;
        _needcount = needCount;
        _currentCount = 0;
        _rewardMoney = rewardMoney;
        _itemType = itemType;
        _isComplete = false;
    }

    public void QuestComplete()
    {
        _isComplete = true;
        Completed?.Invoke(this);
    }
}