using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private BaseState currentState;

    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateLogic();
        }
    }
    public void ChangeState(BaseState nextState)
    {

        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = nextState;
        nextState.Enter();
        
        
    }

    protected virtual BaseState GetInitialState()
    {
        return null;
    }
    public BaseState GetCurrentState()
    {
        return currentState;
    }
}
