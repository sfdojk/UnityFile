using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform checkPosition;
    [SerializeField] private LayerMask groundLayer;
    private int score;
    InputAction MoveAction;
    InputAction JumpAction;
    InputAction AttackAction;
    Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Coins")
        {
            score++;
            Debug.Log(score);
        }
    }


    void Awake()
    {
        MoveAction = InputSystem.actions.FindAction("Move");
        JumpAction = InputSystem.actions.FindAction("Jump");
        AttackAction = InputSystem.actions.FindAction("Attack");
        rb = GetComponent<Rigidbody2D>();
        score = 0;
    }

    private void Jump()
    {
        if (JumpAction.WasPressedThisFrame())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
        }
    }

    private bool CheckIfGround()
    {
        return Physics2D.OverlapCircle(checkPosition.position, 1f, groundLayer) != null;
    }
    void Update()
    {
        if (CheckIfGround())
        {
            Jump();
        }
        if(AttackAction.WasPressedThisFrame()){
            Debug.Log("Attack");
        }

    }


    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(MoveAction.ReadValue<Vector2>().x * speed, rb.linearVelocityY);
    }
}
