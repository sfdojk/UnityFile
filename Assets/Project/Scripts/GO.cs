using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GO : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    [SerializeField] private LayerMask lm;
    [SerializeField] private float range;
    [SerializeField] private float countdown;
    private float checkTime;

    private bool check()
    {
        return Physics2D.OverlapCircle(this.transform.position, range, lm);
    }

    private void Awake()
    {
        text.gameObject.SetActive(false);
        checkTime = countdown;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            text.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            checkTime -= Time.deltaTime;

            if(checkTime < 0)
            {
                text.gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        checkTime = countdown;
        if (other.CompareTag("Player"))
        {
            text.gameObject.SetActive(false);
        }
    }

}
