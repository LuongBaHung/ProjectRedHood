using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTrap : MonoBehaviour
{
    Rigidbody2D rb;

    private bool hasFall = false;
    public Transform recoveryPoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !hasFall)
        {
            rb.isKinematic = false;
            hasFall = true;
            Invoke(nameof(Recovery), 3f);
        }
    }

    private void Recovery()
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        rb.angularDrag = 0;
        transform.position = recoveryPoint.position;
        hasFall = false;
    }
}
