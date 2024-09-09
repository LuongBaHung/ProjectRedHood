using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    public float chaseSpeed;
    [SerializeField]private bool isChasing = true;
    private float distance;
    
    
    Rigidbody2D rb;
    Animator animator;

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationString.canMove);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            if (isChasing)
            {
                Chasing();
                isChasing = true ;
            }
            else
            {
                isChasing = false ;
                chaseSpeed = 0f;
            }
        }
        
    }
    private bool Chasing()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < 5)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, chaseSpeed * Time.deltaTime);
            Flip();
        }
        return false;
    }
    private void Flip()
    {
       if(transform.position.x < player.transform.position.x)
       {
           transform.localScale = new Vector3(1, 1, 1);
       }
       if(transform.position.x > player.transform.position.x)
       {
           transform.localScale = new Vector3(-1, 1, 1);
       }
    } 
}
