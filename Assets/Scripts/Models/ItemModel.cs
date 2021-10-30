
public class ItemModel
{
    private int _count = 1;
    private ItemType _type = ItemType.Flower;

    public int Count
    {
        get => _count;
        set
        {
            _count = value;
        }
    }
    public ItemType ItemType => _type;

    public ItemModel(int count, ItemType type)
    {
        _count = count;
        _type = type;
    }
}
