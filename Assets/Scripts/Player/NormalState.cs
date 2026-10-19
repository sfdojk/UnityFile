using UnityEngine;
using UnityEngine.InputSystem;

public class NormalState : IState
{
    private PlayerController controller;
    private Vector2 moveDirection;

    public NormalState(PlayerController controller)
    {
        this.controller = controller;
    }
    public void Enter()
    {

    }
    public void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorldPos.z = 0f;
        moveDirection = controller.MoveInput.action.ReadValue<Vector2>();
        Debug.Log(moveDirection);
        controller.Movement.Flip(mouseWorldPos);
        if (moveDirection != new Vector2(0, 0))
        {
            controller.Animator.SetBool("Run", true);
        }
        else
        {
            controller.Animator.SetBool("Run", false);
        }
        if (controller.ShootInput.action.WasPressedThisFrame())
        {
            controller.Movement.Shoot(mouseWorldPos);
        }
        
    }
    public void FixUpdate()
    {
        controller.Movement.Move(moveDirection);
    }
    public void Exit()
    {
        controller.Animator.SetBool("Run", false);
    }
}
