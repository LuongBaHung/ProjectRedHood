using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneRecovery : MonoBehaviour
{
    public Transform recoveryPoint;
    [SerializeField]private bool isRecovering = false;

    Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (!isRecovering && transform.position != recoveryPoint.position)
        {
            isRecovering = true; 
            Invoke(nameof(Recovery), 8f);

        }
    }

    private void Recovery()
    {
        rb.velocity = Vector2.zero;
        rb.angularDrag = 0.05f;
        rb.rotation = 0;
        transform.position = recoveryPoint.position;
        isRecovering = false;
    }
}
