using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemoveBehavior : StateMachineBehaviour
{
    public float fadeTime = 0.5f;
    public float fadeDelay = 0.0f;
    private float timeElapse = 0f;
    private float fadeDelayElapsed = 0f;
    SpriteRenderer spriteRenderer;
    GameObject objToRemove;
    Color startColor;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapse = 0f;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        objToRemove = animator.gameObject;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(fadeDelay > fadeDelayElapsed)
        {
            fadeDelayElapsed += Time.deltaTime;
        }else
        {
            timeElapse += Time.deltaTime;

            float newAlpha = startColor.a * (1 - (timeElapse / fadeTime));

            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

            if (timeElapse > fadeTime)
            {
                Destroy(objToRemove);
            }
        }
    }  
}
