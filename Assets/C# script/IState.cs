using UnityEngine;

public interface IState
{
    public void Enter();
    public void Update();
    public void FixUpdate();
    public void Exit();
}
