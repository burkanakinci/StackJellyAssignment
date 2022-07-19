public class FailState : BaseState
{

    private GameStateMachine gameStateMachine;

    public FailState(GameStateMachine stateMachine) : base("FailState", stateMachine)
    {
        gameStateMachine = (GameStateMachine)stateMachine;
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void UpdateLogic()
    {
    }
    public override void Exit()
    {
    }
}
