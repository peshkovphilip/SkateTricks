
using System;

[Serializable]
public class ItemModel
{
    private int _count = 1;
    private EItemType _type;
    private String _image;

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
        set
        {
            switch (value)
            {
                case EItemType.Coin:
                    Image = "coin";
                    break;
                case EItemType.JetPack:
                    Image = "jetpack";
                    break;
                case EItemType.Flower:
                    Image = "flowers";
                    break;
            }
            _type = value;
        }
    }

    public String Image
    {
      get => _image;
      set => _image = value;
    }

    public ItemModel(int count, EItemType type)
    {
        _count = count;
        ItemType = type;
    }
}
