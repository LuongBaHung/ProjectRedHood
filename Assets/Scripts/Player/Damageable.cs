using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent damageableDeath;
    public UnityEvent<int, int> healthChanged;

    Animator animator;

    [SerializeField] private int  _maxHealth = 100;

    private CinemachineImpulseSource impulseSource;

    public int MaxHealth 
    { 
        get 
        { 
            return _maxHealth; 
        } 
        set 
        { 
            _maxHealth = value;
        } 
    }

    [SerializeField] private int _health = 100;

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = Mathf.Max(value,0);
            healthChanged?.Invoke(_health,MaxHealth);
            if( _health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField] private bool _isAlive = true;
    [SerializeField] private bool isInvincible = false;

    

    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationString.isAlive, value);
            Debug.Log("IsAlive Set " + value);

            if(value == false)
            {
                damageableDeath.Invoke();
            }
        }
    }

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationString.lockVelocity);
        }
        private set
        {
            animator.SetBool(AnimationString.lockVelocity, value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        if(isInvincible)
        {
            if(timeSinceHit > invincibilityTime)
            {
                isInvincible = false ;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }

    public bool Hit(int damage, Vector2 knockBack)
    {
        CameraShakeManager.instance.CameraShake(impulseSource);

        if(IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            //Notify other subscribed that the damageable was hit to handle the knockback and such
            animator.SetTrigger(AnimationString.hitTrigger);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockBack );
            CharacterEvents.charaterDamaged.Invoke(gameObject, damage);

            return true;
        }

        //Unable to hit
        return false;
    }

    public bool Heal(int healthRestore)
    {
        if(IsAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);
            Health += actualHeal;
            CharacterEvents.charaterHealed(gameObject, actualHeal);
            return true;
        }
        return false;
    }
}
