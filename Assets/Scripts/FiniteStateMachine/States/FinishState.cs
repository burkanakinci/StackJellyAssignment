using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class FinishState : BaseState
{

    private GameStateMachine gameStateMachine;

    public FinishState(GameStateMachine stateMachine) : base("FinishState", stateMachine)
    {
        gameStateMachine = (GameStateMachine)stateMachine;
    }
    public override void Enter()
    {
        base.Enter();

        GameManager.Instance.finishConfettiParticle.Play();
        JellyManager.Instance.StartFinishMovement();
        
    }
    public override void UpdateLogic()
    {
    }
    public override void Exit()
    {
    }
}
