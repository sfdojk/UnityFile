using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform checkPosition;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletShootingPoint;
    [SerializeField] private int bulletNumberConst;
    [SerializeField] private float recoil;
    private int score;
    InputAction MoveAction;
    InputAction JumpAction;
    InputAction AttackAction;
    InputAction ReloadAction;
    Rigidbody2D rb;
    private int bulletNumberNow;
    private int facing;
    private float recoilTimer;
    private float recoilDuration;
    public event Action<int> PlayerShooting; 

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
        ReloadAction = InputSystem.actions.FindAction("Reload");
        rb = GetComponent<Rigidbody2D>();
        score = 0;
        bulletNumberNow = bulletNumberConst;
        recoilDuration = 0.15f;
        recoilTimer = 0f;
        facing = 1;
        PlayerShooting += bulletNumber;
        PlayerShooting += recoiling;
    }

    private void bulletNumber(int bullet)
    {
        Debug.Log(bullet);
    }

    private void recoiling(int bullet)
    {
        rb.linearVelocity = new Vector2(-facing * recoil, rb.linearVelocityY);
        recoilTimer = recoilDuration;
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
        if(MoveAction.ReadValue<Vector2>().x > 0)
        {
            facing = 1;
            transform.localScale = new Vector3(1, 1, 1);
        }
        if(MoveAction.ReadValue<Vector2>().x < 0)
        {
            facing = -1;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (CheckIfGround())
        {
            Jump();
        }
        if (AttackAction.WasPressedThisFrame() && bulletNumberNow > 0)
        {
            GameObject bulletTmp = Instantiate(bullet, bulletShootingPoint.position, this.transform.rotation);
            bulletTmp.GetComponent<ShootingBullet>().direction = facing;
            bulletNumberNow--;
            PlayerShooting?.Invoke(bulletNumberNow);
        }

        if (ReloadAction.WasPerformedThisFrame())
        {
            bulletNumberNow = bulletNumberConst;
            Debug.Log(bulletNumberNow);
        }

        if (recoilTimer > 0)
            recoilTimer -= Time.deltaTime;
    }


    private void FixedUpdate()
    {
        if (recoilTimer <= 0)
        {
            rb.linearVelocity = new Vector2(MoveAction.ReadValue<Vector2>().x * speed, rb.linearVelocityY);
        }
    }
}
