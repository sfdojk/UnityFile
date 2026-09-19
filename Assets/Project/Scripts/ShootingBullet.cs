using System;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingBullet : MonoBehaviour
{
    [SerializeField] private float speedOfBullet;
    [SerializeField] private float destroyTime;
    public int direction = 1;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * direction * speedOfBullet * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
