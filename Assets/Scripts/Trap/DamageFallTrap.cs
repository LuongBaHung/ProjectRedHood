using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFallTrap : MonoBehaviour
{
    public int falltrapDamage = 100;
    public Vector2 knockBack = new Vector2(0, 0);

    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockBack : new Vector2(-knockBack.x, knockBack.y);

            bool gotHit = damageable.Hit(falltrapDamage, deliveredKnockback);

            if (gotHit)
            {
                Debug.Log(collision.name + " hit for " + falltrapDamage);
            }
        }
    }
}
