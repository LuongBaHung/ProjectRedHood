using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(Damageable))]
public class PlayerController : MonoBehaviour
{
    public GameObject gameOverUI;

    public float moveSpeed = 5f;
    public float jumpImpulse = 7f;
    public float airMoveSpeed = 3f;
    Vector2 moveInput;

    TouchingDirection touchingDirection;
    Damageable damageable;
    PlayerRespawn playerRespawn;
    
    

    public float CurrentMoveSpeed { get
        {
            if(CanMove)
            {
                if (IsMoving && !touchingDirection.IsOnWall)
                {
                    if (touchingDirection.IsGround)
                    {
                        return moveSpeed;
                    }
                    else
                    {
                        return airMoveSpeed;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
            
                
        } }

    [SerializeField] private bool _isMoving = false;

    public bool IsMoving { get
        {
            return _isMoving;
        } private set
        {
            _isMoving = value;
            animator.SetBool(AnimationString.isMoving, value);
        } 
    }
    public bool CanMove 
    {   get
        {
            return animator.GetBool(AnimationString.canMove);
        }
        private set
        {
            animator.SetBool(AnimationString.canMove, value);
        }
        
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationString.isAlive);
        }
        private set 
        { 
            animator.SetBool(AnimationString.isAlive, value);
        }
    }

    

    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirection = GetComponent<TouchingDirection>();
        damageable = GetComponent<Damageable>();
        playerRespawn = GetComponent<PlayerRespawn>();
    }

    private void Update()
    {
        RespawnWhenDeath();
    }

    void FixedUpdate()
    {
        if (!damageable.LockVelocity)
        {
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        }

        animator.SetFloat(AnimationString.yVelocity, rb.velocity.y);

        if (touchingDirection.IsGround && IsMoving)
        {
            AudioManager.Instance.PlayFootStep();
        }
        else
        {
            AudioManager.Instance.StopFootStep();
        }

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive && CanMove)
        {
            IsMoving = moveInput != Vector2.zero;

            SetFacingDirection(moveInput);

            if (IsMoving)
            {
                AudioManager.Instance.PlayFootStep();
            }
            else
            {
                AudioManager.Instance.StopFootStep();
            }
        }
        else
        {
            IsMoving = false;
            AudioManager.Instance.StopFootStep();
        }
        
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0)
        {
            transform.localScale = Vector3.one;
        } else if(moveInput.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && touchingDirection.IsGround && CanMove && IsAlive)
        {
            animator.SetTrigger(AnimationString.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);

            AudioManager.Instance.StopFootStep();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            animator.SetTrigger(AnimationString.attackTrigger);
        }
    }

    public void OnRangeAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationString.rangeAttackTrigger);
        }
    }

    public void OnHit(int damage,Vector2 knockBack)
    {
        rb.velocity = new Vector2(knockBack.x, rb.velocity.y + knockBack.y);
    }

    public void RespawnWhenDeath()
    {
        if(damageable.Health <= 0 && !IsAlive)
        {
            Time.timeScale = 0;
            gameOverUI.SetActive(true);
            AudioManager.Instance.StopFootStep();
        }
    }

    public void RespawnToCheckPoint()
    {
        playerRespawn.RespawnNow();
        ResetPlayer();
        AudioManager.Instance.PlayMusicBG();
    }

    public void ResetPlayer()
    {
        IsAlive = true;
        damageable.Health = damageable.MaxHealth;
        damageable.IsAlive = true;
        gameOverUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void EnableControls()
    {
        CanMove = true;
    }

    public void DisableControls()
    {
        CanMove = false;
        rb.velocity = Vector2.zero;
        IsMoving = false;
    }
}
