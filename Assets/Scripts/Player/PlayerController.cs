using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public PlayerMovement Movement { get; private set; }
    [field: SerializeField] public Rigidbody2D rb { get; private set; }
    [field: SerializeField] public InputActionReference MoveInput { get; private set; }
    [field: SerializeField] public InputActionReference ShootInput { get; private set; }
    public StateMachine StateMachine { get; private set; }
    public NormalState NormalState { get; private set; }

    private void OnEnable()
    {
        MoveInput.action.Enable();
        ShootInput.action.Enable();
    }
    private void OnDisable()
    {
        MoveInput.action.Disable();
        ShootInput.action.Disable();
    }

    private void Awake()
    {
        StateMachine = new StateMachine();
        NormalState = new NormalState(this);
    }
    private void Start()
    {
        StateMachine.ChangeState(NormalState);
    }
    private void Update()
    {
        StateMachine.Update();
    }

    private void FixedUpdate()
    {
        StateMachine.FixUpdate();
    }

}
