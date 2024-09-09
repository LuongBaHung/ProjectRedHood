using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    [SerializeField] private Animator animator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void NextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    //public void LoadScene(string sceneName)
    //{
    //    SceneManager.LoadSceneAsync(sceneName);
    //}

    IEnumerator LoadLevel()
    {
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        animator.SetTrigger("Start");
    }
}
