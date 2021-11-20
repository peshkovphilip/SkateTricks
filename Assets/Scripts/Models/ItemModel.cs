
using UnityEngine;

public class ItemModel
{
    private int _count = 1;
    private EItemType _type;
    private Sprite _image;

    public int Count
    {
        get => _count;
        set
        {
            _count = value;
        }
    }

    public EItemType ItemType
    {
        get => _type;
        set { _type = value; }
    }

    public Sprite Image => _image;

    public ItemModel(int count, EItemType type, Sprite image)
    {
        _count = count;
        _type = type;
        _image = image;
    }
}
