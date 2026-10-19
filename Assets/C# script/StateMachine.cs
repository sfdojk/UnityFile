using UnityEngine;

public class StateMachine
{
    public IState currentState { get; private set; }


    public void Update()
    {
        currentState?.Update();
    }

    public void FixUpdate()
    {
        currentState?.FixUpdate();
    }
        
    public void ChangeState(IState state)
    {
        if (currentState == state) return;
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }
}
