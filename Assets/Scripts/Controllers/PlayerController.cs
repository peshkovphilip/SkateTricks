using UnityEngine;

public class PlayerController : IStarter, IUpdater
{
    private PlayerView playerView;
    private PlayerModel playerModel;
    private ButtonView[] buttons;
    private ItemView[] items;
    private EnvironmentView[] environments;
    private LevelView levelView;
    private int currentLinePlayerStood;
    private Consts Const = new Consts();
    private GameParams gameParam;
    

    public void Starter()
    {
        Debug.Log("start PlayerController");

        playerView = Object.FindObjectOfType<PlayerView>(); 
        playerModel = Object.FindObjectOfType<PlayerModel>();
        buttons = Object.FindObjectsOfType<ButtonView>(); //как это передать в конструктор без поиска и не присваивая все объекты в инспекторе?
        foreach (ButtonView button in buttons)
        {
            button.OnTap += TryAction;
        }
        items = Object.FindObjectsOfType<ItemView>();
        foreach (ItemView item in items)
        {
            item.OnEnter += GetItem;
        }
        environments = Object.FindObjectsOfType<EnvironmentView>();
        foreach (EnvironmentView environment in environments)
        {
            environment.OnEnter += EnvironmentCollision;
        }
        levelView = Object.FindObjectOfType<LevelView>();
        gameParam = Object.FindObjectOfType<GameParams>();

        SetDefaultValues();
    }

    public void SetDefaultValues()
    {
        currentLinePlayerStood = (int)levelView.StartLine;
        playerView.transform.position = levelView.SpawnPoint.position;
    }

    public void Updater()
    {
        if (playerView.phisics.velocity.magnitude <= Const.MinPlayerMagnitudeForIdle)
        {
            if (playerView.animator.GetInteger("state") != 0)
            {
                playerView.animator.SetInteger("state", 0);
            }
        }
    }

    public void TryAction(Tap typeTap)
    {
        if (typeTap == Tap.Up)
        {
            Debug.Log("move up");
            if (currentLinePlayerStood > 0)
            {
                currentLinePlayerStood--;
                MovePlayerVertical();
            }
        }
        if (typeTap == Tap.Down)
        {
            Debug.Log("move down");
            if (currentLinePlayerStood < levelView.LinePositions.Length - 1)
            {
                currentLinePlayerStood++;
                MovePlayerVertical();
            }
        }
        if (typeTap == Tap.Jump)
        {
            playerView.phisics.AddForce(Vector2.up * playerModel.jumpForce, ForceMode2D.Impulse);
            Debug.Log("move jump");
        }
        if (typeTap == Tap.Push)
        {
            playerView.phisics.AddForce(Vector2.right * playerModel.pushForce, ForceMode2D.Impulse);
            playerView.animator.SetInteger("state", 1);
            Debug.Log("push");
        }
    }

    public void GetItem(ItemView itemView, Collider2D collider)
    {
        if (collider == playerView.collider)
        {
            Debug.Log("get item");
            if (itemView.ItemType == ItemType.Coin)
            {
                Debug.Log("get coin");
                gameParam.Coins += itemView.Coins;
            }
            if (itemView.ItemType == ItemType.Finish)
            {
                Debug.Log("get finish");
                gameParam.LevelDone = true;
                SetDefaultValues();
            }
        }
    }

    public void EnvironmentCollision(EnvironmentView environmentView, Collider2D collider)
    {
        if (collider == playerView.collider)
        {
            Debug.Log("collision with environment");
            if (environmentView.Damage > 0)
            {
                playerModel.Health -= environmentView.Damage;
            }
        }
    }

    private void MovePlayerVertical()
    {
        playerView.phisics.gameObject.layer = LayerMask.NameToLayer(LayerMask.LayerToName(currentLinePlayerStood + Const.DiffBetweenLayersAndLines));
        Utils.Change(playerView.transform.position, y: levelView.LinePositions[currentLinePlayerStood].transform.position.y);
    }

}
