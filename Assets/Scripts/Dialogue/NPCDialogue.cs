using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public GameObject tutorialCanvas;

    private Transform player;
    private SpriteRenderer dialogIcon;
    // Start is called before the first frame update
    void Start()
    {
        dialogIcon = GetComponent<SpriteRenderer>();
        dialogIcon.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            dialogIcon.enabled = true;
            tutorialCanvas.SetActive(true);

            player = collision.gameObject.GetComponent<Transform>();

            if(player.position.x > transform.position.x && transform.parent.localScale.x < 0)
            {
                Flip();
            } else if (player.position.x < transform.position.x && transform.parent.localScale.x > 0)
            {
                Flip();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            dialogIcon.enabled = false;
            tutorialCanvas.SetActive(false);
        }
    }

    private void Flip()
    {
        Vector3 currentScale = transform.parent.localScale;
        currentScale.x *= -1;
        transform.parent.localScale = currentScale;
    }
}
