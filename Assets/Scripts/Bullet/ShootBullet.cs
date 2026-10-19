using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;
    private Vector2 bulletDirection;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }

    public void Start()
    {
        animator.Play("Travel", 0, 0f);
        Destroy(this.gameObject, 5f);
    }

    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        rb.linearVelocity = bulletSpeed * transform.right;
    }

}
