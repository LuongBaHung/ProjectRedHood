using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    
    public float flySpeed = 3f;
    public float waypointReachedDistance = 0.1f;
    public float chaseRange = 10f;
    private bool isPlayer = false;

    private Transform playerTransform;

    public DetectionZoneForBat biteDetectionZone;
    public Collider2D deathCollider;
    public List<Transform> waypoints;

   
    Animator animator;
    Rigidbody2D rb;
    Damageable damageable;

    Transform nextWaypoint;
    int waypointNum = 0;

    public bool _hasTarget = false;
    

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationString.hasTarget, value);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationString.canMove);
        }
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        nextWaypoint = waypoints[waypointNum];
    }

    private void OnEnable()
    {
        damageable.damageableDeath.AddListener(OnDeath);
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if(distanceToPlayer <= chaseRange)
        {
            ChasePlayer();
        }else
        {
            isPlayer = false;
        }
    }

    private void FixedUpdate()
    {

        if (damageable.IsAlive)
        {
            if(CanMove)
            {
                if(!isPlayer)
                {
                    Flight();
                }
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
    }

    public void Flight()
    {
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;

        float distance = Vector2.Distance(nextWaypoint.position, transform.position);

        rb.velocity = directionToWaypoint * flySpeed;
        UpdateDirection();


        if(distance <= waypointReachedDistance)
        {
            waypointNum++;

            if(waypointNum >= waypoints.Count)
            {
                waypointNum = 0;
            }

            nextWaypoint = waypoints[waypointNum];
        }
    }

    private void UpdateDirection()
    {
        Vector3 locScale = transform.localScale;

        if(transform.localScale.x > 0)
        {
            if(rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
        else
        {
            if (rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
    }

    public void OnDeath()
    {
        rb.gravityScale = 2f;
        rb.velocity = new Vector2(0, rb.velocity.y);
        deathCollider.enabled = true;
    }

    private void ChasePlayer()
    {
        isPlayer = true;

        Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;

        rb.velocity = directionToPlayer * flySpeed;

        UpdateDirection();
    }
}
