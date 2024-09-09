using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ActiveCutSceneEnd : MonoBehaviour
{
    [SerializeField] private PlayableDirector playableDirector;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject endTransition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            endTransition.SetActive(true);
            playableDirector.Play();
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(EndMenu());
        }
    }

    IEnumerator EndMenu()
    {
        yield return new WaitForSeconds(5f);
        pauseMenu.SetActive(true);
    }
}
