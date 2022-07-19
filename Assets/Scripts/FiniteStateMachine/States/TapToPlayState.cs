using UnityEngine;

public class TapToPlayState : BaseState
{

    private GameStateMachine gameStateMachine;

    public TapToPlayState(GameStateMachine stateMachine) : base("TapToPlayState", stateMachine)
    {
        gameStateMachine = (GameStateMachine)stateMachine;
    }
    public override void Enter()
    {

            base.Enter();
        
        JellyManager.Instance.ResetJellyValues();
    }
    public override void UpdateLogic()
    {
        if (Input.GetMouseButtonDown(0))
        {
            JellyManager.Instance.jellyMovementController.CheckControls();

            gameStateMachine.ChangeState(gameStateMachine.playState);
        }
    }
    public override void Exit()
    {

    }
}
