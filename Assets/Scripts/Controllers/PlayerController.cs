using UnityEngine;

public class PlayerController : IStarter, IDestroyer, IUpdater
{
    private PlayerView playerView;
    private PlayerModel playerModel;
    private ButtonView[] buttons;

    public void Starter()
    {
        playerView = Object.FindObjectOfType<PlayerView>();
        playerModel = new PlayerModel();
        buttons = Object.FindObjectsOfType<ButtonView>();
        foreach (ButtonView button in buttons)
        {
            button.OnTap += TryAction;
        }
    }

    public void Updater()
    {
        if (playerView.phisics.velocity.magnitude <= 0.2f)
        {
            if (playerView.animator.GetInteger("state") != 0)
            {
                playerView.animator.SetInteger("state", 0);
            }
        }
    }

    public void Destroyer()
    {
        if (playerView != null)
        {
          
        }
    }

    public void TryAction(ETypes.Tap typeTap)
    {
        if (typeTap == ETypes.Tap.Up)
        {
            Debug.Log("move up");
        }
        if (typeTap == ETypes.Tap.Down)
        {
            Debug.Log("move down");
        }
        if (typeTap == ETypes.Tap.Jump)
        {
            playerView.phisics.AddForce(Vector2.up * playerModel.jumpForce, ForceMode2D.Impulse);
            Debug.Log("move jump");
        }
        if (typeTap == ETypes.Tap.Push)
        {
            playerView.phisics.AddForce(Vector2.right * playerModel.pushForce, ForceMode2D.Impulse);
            playerView.animator.SetInteger("state", 1);
            Debug.Log("push");
        }
    }

}
