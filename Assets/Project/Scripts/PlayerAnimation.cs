using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Player movement;
    [SerializeField] private float gapTimeBetweenCombo;
    [SerializeField] private float isAttacking;
    private float spamAvoidTimer = 0f;
    private Rigidbody2D rb;
    private InputAction swordAttack;
    private float timer;
    private int step;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        swordAttack = InputSystem.actions.FindAction("SwordAttack");
        timer = 0;
    }
    private void Update()
    {
        if (movement.Moving > 0)
        {
            anim.SetInteger("IsRun", 2);
        }
        if (movement.Moving < 0)
        {
            anim.SetInteger("IsRun", 2);
        }
        if (movement.Moving == 0)
        {
            anim.SetInteger("IsRun", 0);
        }

        anim.SetFloat("VelocityOfY", rb.linearVelocityY);
        anim.SetBool("OnGround", movement.CheckIfGround());

        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            step = 0;
            anim.SetInteger("StepAttack", step);
        }
        if(spamAvoidTimer > 0)
        {
            spamAvoidTimer -= Time.deltaTime;
        }

        if (swordAttack.WasPressedThisFrame() && spamAvoidTimer <= 0)
        {
            step++;
            if(step > 3)
            {
                step = 1;
            }
            anim.SetInteger("StepAttack", step);
            timer = gapTimeBetweenCombo;
            spamAvoidTimer = isAttacking;
        }


    }
}
