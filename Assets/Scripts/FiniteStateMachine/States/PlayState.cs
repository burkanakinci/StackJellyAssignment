
public class PlayState : BaseState
{

    private GameStateMachine gameStateMachine;

    public PlayState(GameStateMachine stateMachine) : base("PlayState", stateMachine)
    {
        gameStateMachine = (GameStateMachine)stateMachine;
    }
    public override void Enter()
    {
        UIManager.Instance.ShowLevel();
        base.Enter();

        JellyManager.Instance.JumpToPlayingPos();
    }
    public override void UpdateLogic()
    {
        JellyManager.Instance.jellyMovementController.CheckControls();
        JellyManager.Instance.jellyMovementController.ChangeScaleJelly();
        JellyManager.Instance.jellyMovementController.MovementForward();
    }
    public override void Exit()
    {
    }
}
