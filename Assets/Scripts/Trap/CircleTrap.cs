using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTrap : MonoBehaviour
{
    public int damage = 100;
    public float moveSpeed = 5f;
    public Transform pointA;
    public Transform pointB;
    private Vector3 targetPoint;
    public Vector2 knockBack = new Vector2(0, 0);

    Rigidbody2D rb;
    Animator animator;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        targetPoint = pointA.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            if (transform.position == pointA.position)
            {
                targetPoint = pointB.position;
            }
            else
            {
                targetPoint = pointA.position;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockBack : new Vector2(-knockBack.x, knockBack.y);

            bool gotHit = damageable.Hit(damage, deliveredKnockback);

            if (gotHit)
            {
                Debug.Log(collision.name + " hit for " + damage);
            }
        }
    }
}
