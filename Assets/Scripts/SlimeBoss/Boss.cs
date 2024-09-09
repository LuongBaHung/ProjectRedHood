using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public DetectionZone attackDetectionZone;
    Animator animator;
    Rigidbody2D rb;
    Damageable damageable;
    public GameObject movingPlatform;
    public GameObject healthBoss;



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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = attackDetectionZone.detectedColliders.Count > 0;

        if (damageable.Health <= 80)
        {
            animator.SetTrigger("StageTwo");
        }

        if (damageable.Health <= 0)
        {
            movingPlatform.SetActive(true);
            healthBoss.SetActive(false);
            AudioManager.Instance.StopBossFight();
            AudioManager.Instance.PlayMusicBG();
        }
    }
}
