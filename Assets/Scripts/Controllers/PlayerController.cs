using UnityEngine;

public class PlayerController : IStarter, IUpdater
{
    private PlayerView playerView;
    private PlayerModel playerModel;
    private ButtonView[] buttons;
    private Level1View level1View;
    private int currentLinePlayerStood;
    public static Consts Const = new Consts();

    public void Starter()
    {
        playerView = Object.FindObjectOfType<PlayerView>(); // думаю в стартере поиском делать можно, еще не известно насколько сложным может получиться конструктор этого контроллера
        playerModel = new PlayerModel();
        buttons = Object.FindObjectsOfType<ButtonView>();
        foreach (ButtonView button in buttons)
        {
            button.OnTap += TryAction;
        }
        //playerView.transform.position = level1View.LinePositions;
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
        }
        if (typeTap == Tap.Down)
        {
            Debug.Log("move down");
            //playerView.phisics.isKinematic = true;

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

}
