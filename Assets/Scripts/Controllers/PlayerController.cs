using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;

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
    private Rigidbody2D playerRigidBody;

    public void Starter()
    {
        Debug.Log("start PlayerController");

        playerView = Object.FindObjectOfType<PlayerView>(true); 
        playerModel = Object.FindObjectOfType<PlayerModel>(true);
        playerRigidBody = playerView.GetComponentInChildren<Rigidbody2D>();

        buttons = Object.FindObjectsOfType<ButtonView>(true); //как это передать в конструктор без поиска и не присваивая все объекты в инспекторе?
        foreach (ButtonView button in buttons)
        {
            button.OnTap += TryAction;
        }
        items = Object.FindObjectsOfType<ItemView>(true);
        foreach (ItemView item in items)
        {
            item.OnEnter += GetItem;
        }
        environments = Object.FindObjectsOfType<EnvironmentView>(true);
        foreach (EnvironmentView environment in environments)
        {
            environment.OnEnter += EnvironmentCollision;
        }
        levelView = Object.FindObjectOfType<LevelView>(true);
        gameParam = Object.FindObjectOfType<GameParams>();

        SetDefaultValues();
    }

    public void SetDefaultValues()
    {
        currentLinePlayerStood = (int)levelView.StartLine;
        playerView.transform.position = levelView.SpawnPoint.position;
        playerRigidBody.gameObject.layer = currentLinePlayerStood + Const.DiffBetweenLayersAndLines;
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

        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if ((keyboard.upArrowKey.wasPressedThisFrame) || (keyboard.wKey.wasPressedThisFrame))
            {
                MoveUp();
            }
            if ((keyboard.downArrowKey.wasPressedThisFrame) || (keyboard.sKey.wasPressedThisFrame))
            {
                MoveDown();
            }
            if ((keyboard.spaceKey.wasPressedThisFrame) || (keyboard.oKey.wasPressedThisFrame))
            {
                Jump();
            }
            if ((keyboard.rightArrowKey.wasPressedThisFrame) || (keyboard.leftCtrlKey.wasPressedThisFrame) || (keyboard.lKey.wasPressedThisFrame))
            {
                Push();
            }
            if ((keyboard.rKey.wasPressedThisFrame))
            {
                Retry();
            }
        }
    }


    public void TryAction(Tap typeTap)
    {
        if (typeTap == Tap.Up)
        {
            MoveUp();
        }
        if (typeTap == Tap.Down)
        {
            MoveDown();
        }
        if (typeTap == Tap.Jump)
        {
            Jump();
        }
        if (typeTap == Tap.Push)
        {
            Push();
        }
        if (typeTap == Tap.Retry)
        {
            Retry();
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

    private void MoveUp()
    {
        Debug.Log("move up");
        if (currentLinePlayerStood > 0)
        {
            currentLinePlayerStood--;
            MovePlayerVertical();
        }
    }

    private void MoveDown()
    {
        Debug.Log("move down");
        if (currentLinePlayerStood < levelView.LinePositions.Length - 1)
        {
            currentLinePlayerStood++;
            MovePlayerVertical();
        }
    }

    private void Jump()
    {
        playerView.phisics.AddForce(Vector2.up * playerModel.jumpForce, ForceMode2D.Impulse);
        Debug.Log("jump");
    }

    private void Push()
    {
        playerView.phisics.AddForce(Vector2.right * playerModel.pushForce, ForceMode2D.Impulse);
        playerView.animator.SetInteger("state", 1);
        Debug.Log("push");
    }

    private void Retry()
    {
        Utils.GameAnalytic.SendMessage("level_retry");
        Utils.Advertise.ShowInterstitial();
        SceneManager.LoadScene("MainScene");
        Debug.Log("retry");
    }

}
