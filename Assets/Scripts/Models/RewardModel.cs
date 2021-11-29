using UnityEngine;

public class RewardModel : MonoBehaviour
{
    private int _day;
    private int _count;
    private bool _active;
    private string _image;
    private EItemType _type;

    public int Day => _day;
    public int Count => _count;
    public bool Active
    {
        get => _active;
        set => _active = value;
    }
    public string Image => _image;
    public EItemType ItemType => _type;

    public RewardModel(int day, EItemType type, int count)
    {
        _day = day;
        _type = type;
        _count = count;
        _active = true;

        switch (type)
        {
            case EItemType.Coin:
                _image = "coin";
                break;
            case EItemType.JetPack:
                _image = "jetpack";
                break;
        }
    }
    
}
