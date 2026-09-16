using NUnit.Framework;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveGO : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private Transform movingGO;
    [SerializeField] private float speed;
    private bool goToB;
    private InputAction interactAction;
    private Coroutine switching;
    private bool touchingButton;
    private void Awake()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
        goToB = true;
        touchingButton = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            touchingButton = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            touchingButton = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (touchingButton == true && interactAction.WasPressedThisFrame())
        {
            if (switching != null)
            {
                StopCoroutine(switching);
            }

            if (goToB == true)
            {
                switching = StartCoroutine(SwitchTo(pointB));
                goToB = false;
            }
            else if (goToB == false)
            {
                switching = StartCoroutine(SwitchTo(pointA));
                goToB = true;
            }
        }
    }

    IEnumerator SwitchTo(Transform targetPoint)
    {
        while(movingGO.position != targetPoint.position)
        {
            movingGO.position = Vector3.MoveTowards(movingGO.position, targetPoint.position, speed * Time.deltaTime);
            yield return null;
        }
        switching = null;
    }
}
