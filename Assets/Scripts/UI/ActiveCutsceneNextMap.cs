using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ActiveCutsceneNextMap : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;
    [SerializeField] private GameObject wereWolf;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playableDirector.Play();
            GetComponent<BoxCollider2D>().enabled = false;
            wereWolf.SetActive(true);
            AudioManager.Instance.StopMusicBG();
            AudioManager.Instance.PlayBossFight();
        }
    }
}
