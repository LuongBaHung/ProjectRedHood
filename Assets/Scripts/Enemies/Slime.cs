using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(Damageable))]
public class Slime : MonoBehaviour
{
    public float walkAcceleration = 3f;
    public float maxSpeed = 3f;
    public float walkStopRate = 0.05f;
    public float chaseRange = 5f;
    private bool isPlayer = false;

    private Transform playerTransform;
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;

    Rigidbody2D rb;
    TouchingDirection touchingDirection;
    Animator animator;
    Damageable damageable;
    public enum WalkaleDirection { Right, Left }

    private WalkaleDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;


    public WalkaleDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkaleDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkaleDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }

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

    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationString.attackCooldown);
        }
        private set
        {
            animator.SetFloat(AnimationString.attackCooldown, Mathf.Max(value, 0));
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirection = GetComponent<TouchingDirection>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;

        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= chaseRange)
        {
            //duoi theo player
            ChasePlayer();
        }
        else
        {
            //tiep tuc di bo        
            isPlayer = false;
        }

    }

    private void FixedUpdate()
    {

        if (touchingDirection.IsGround && touchingDirection.IsOnWall)
        {
            FlipDirection();
        }

        if (!damageable.LockVelocity)
        {
            if (CanMove && touchingDirection.IsGround && !isPlayer)
            {

                rb.velocity = new Vector2(
                    Mathf.Clamp(rb.velocity.x + (walkAcceleration * walkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed),
                    rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
            }
        }
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkaleDirection.Right)
        {
            WalkDirection = WalkaleDirection.Left;
        }
        else if (WalkDirection == WalkaleDirection.Left)
        {
            WalkDirection = WalkaleDirection.Right;
        }
        else
        {
            Debug.Log("Current walkable direction is not set to legal values of right or left");
        }
    }

    public void OnHit(int damage, Vector2 knockBack)
    {
        rb.velocity = new Vector2(knockBack.x, rb.velocity.y + knockBack.y);
    }

    public void OnCliffDetected()
    {
        if (touchingDirection.IsGround)
        {
            FlipDirection();
        }
    }

    private void ChasePlayer()
    {
        isPlayer = true;
        // xac dinh huong di chuyen dua tren vi tri cua player
        if (playerTransform.position.x > transform.position.x)
        {
            WalkDirection = WalkaleDirection.Right;
        }
        else if (playerTransform.position.x < transform.position.x)
        {
            WalkDirection = WalkaleDirection.Left;
        }

        // Di chuyen ve phia palyer
        rb.velocity = new Vector2(
            Mathf.Clamp(rb.velocity.x + (walkAcceleration * walkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed),
            rb.velocity.y);
    }
}
