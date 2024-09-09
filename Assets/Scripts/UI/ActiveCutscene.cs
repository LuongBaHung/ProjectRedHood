using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ActiveCutscene : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;

    public GameObject bossHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playableDirector.Play();
            GetComponent<BoxCollider2D>().enabled = false;
            bossHealth.SetActive(true);
            AudioManager.Instance.StopMusicBG();
            AudioManager.Instance.PlayBossFight();
        }
    }
}
