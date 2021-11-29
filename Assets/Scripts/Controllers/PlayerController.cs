using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : IStarter, IUpdater
{
    private PlayerView _playerView;
    private PlayerModel _playerModel;
    private ButtonView[] buttons;
    private ItemView[] _itemViews;
    private EnvironmentView[] _environments;
    private LevelView levelView;
    private int currentLinePlayerStood;
    private Consts Const = new Consts();
    private Rigidbody2D playerRigidBody;
    private PlayerData _playerData;
    private PlayerBodyView _playerBodyView;
    private Vector3 startBodyPosition;

    public PlayerController(PlayerView playerView, PlayerModel playerModel, ItemView[] itemViews, PlayerData playerData, EnvironmentView[] environments)
    {
        _playerView = playerView;
        _playerModel = playerModel;
        _itemViews = itemViews;
        _playerData = playerData;
        _environments = environments;
    }
    public void Starter()
    {
        Debug.Log("start PlayerController");
        playerRigidBody = _playerView.GetComponentInChildren<Rigidbody2D>();
        _playerBodyView = _playerView.GetComponentInChildren<PlayerBodyView>();
        startBodyPosition = new Vector3(2, 0.5f, 0);

        buttons = Object.FindObjectsOfType<ButtonView>(true); 
        foreach (ButtonView button in buttons)
        {
            button.OnTap += TryAction;
        }
        foreach (ItemView item in _itemViews)
        {
            item.OnEnter += GetItem;
        }
        foreach (EnvironmentView environment in _environments)
        {
            environment.OnEnter += EnvironmentCollision;
        }
        levelView = Object.FindObjectOfType<LevelView>(true);
        

        SetDefaultValues(false);
    }

    public void SetDefaultValues(bool loadGame = true)
    {
        Debug.Log("setDefault="+loadGame);
        _playerBodyView.JetPackSprite.sprite = null;
        _playerView.animator.SetInteger("state", 0);
        Camera.main.GetComponent<FixedJoint2D>().enabled = false;
        playerRigidBody.isKinematic = true;
        _playerView.phisics.velocity = Vector2.zero;
        Camera.main.transform.position = _playerView.cameraPosition.position;
        _playerView.transform.position = levelView.SpawnPoint.position;
        _playerBodyView.transform.position = startBodyPosition;
        Camera.main.GetComponent<FixedJoint2D>().enabled = true;
        playerRigidBody.isKinematic = false;
        currentLinePlayerStood = (int)levelView.StartLine;
        playerRigidBody.gameObject.layer = currentLinePlayerStood + Const.DiffBetweenLayersAndLines;
        
        //Camera.main.GetComponent<FixedJoint2D>().enabled = true;
        //playerRigidBody.isKinematic = false;
        if (loadGame)
        {
            _playerData = Game.LoadGame();
            
        }
            
    }
    
    

    public void Updater()
    {
        if (_playerView.phisics.velocity.magnitude <= Const.MinPlayerMagnitudeForIdle)
        {
            if (_playerView.animator.GetInteger("state") != 0)
            {
                _playerView.animator.SetInteger("state", 0);
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
    }

    public void GetItem(ItemView itemView, Collider2D collider)
    {
        if (collider == _playerView.collider)
        {
            Debug.Log("get item");
            // if (itemView.ItemType == EItemType.Coin)
            // {
            //     Debug.Log("get coin");
            //     gameParams.Coins ++;
            // }
            if (itemView.ItemType == EItemType.Finish)
            {
                Debug.Log("get finish");
                //gameParams.LevelDone = true;
                SetDefaultValues();
            }
        }
    }

    public void EnvironmentCollision(EnvironmentView environmentView, Collider2D collider)
    {
        if (collider == _playerView.collider)
        {
            Debug.Log("collision with environment");
            if (environmentView.Damage > 0)
            {
                _playerModel.Health -= environmentView.Damage;
            }
        }
    }

    private void MovePlayerVertical()
    {
        _playerView.phisics.gameObject.layer = LayerMask.NameToLayer(LayerMask.LayerToName(currentLinePlayerStood + Const.DiffBetweenLayersAndLines));
        Utils.Change(_playerView.transform.position, y: levelView.LinePositions[currentLinePlayerStood].transform.position.y);
    }

    private void MoveUp()
    {
        if (currentLinePlayerStood > 0)
        {
            currentLinePlayerStood--;
            MovePlayerVertical();
        }
    }

    private void MoveDown()
    {
        if (currentLinePlayerStood < levelView.LinePositions.Length - 1)
        {
            currentLinePlayerStood++;
            MovePlayerVertical();
        }
    }

    private void Jump()
    {
        _playerView.phisics.AddForce(Vector2.up * _playerModel.JumpForce, ForceMode2D.Impulse);
    }

    private void Push()
    {
        _playerView.phisics.AddForce(Vector2.right * _playerModel.PushForce, ForceMode2D.Impulse);
        _playerView.animator.SetInteger("state", 1);
    }

    

}
