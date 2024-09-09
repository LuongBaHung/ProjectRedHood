using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CloseGate : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;
    [SerializeField] private GameObject wereWolf;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playableDirector.stopped += OnPlayableDirectorStopped;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            rb.isKinematic = false;
            playableDirector.Play();
            GetComponent<CapsuleCollider2D>().enabled = false;
            AudioManager.Instance.StopBossFight();
            AudioManager.Instance.PlayMusicBG();
        }
    }

    private void OnPlayableDirectorStopped(PlayableDirector director)
    {
        StartCoroutine(DestroyEnemy(1f));
    }

    private IEnumerator DestroyEnemy(float delay)
    {
        yield return new WaitForSeconds(delay);
        wereWolf.SetActive(false);
    }

}
