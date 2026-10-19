using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private Transform gunHead;
    
    public void Flip(Vector3 mousePosition)
    {
        Vector2 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * (180 / Mathf.PI);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    public void Move(Vector2 direction)
    {
        rb.linearVelocity = direction * playerSpeed;
    }
    public void Shoot(Vector3 mousePostion)
    {
        Vector2 direction = mousePostion - gunHead.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * (180 / Mathf.PI);
        Quaternion bulletRotate = Quaternion.Euler(0, 0, angle);
        Instantiate(bulletPrefabs, gunHead.position, bulletRotate);
    }
    
}
