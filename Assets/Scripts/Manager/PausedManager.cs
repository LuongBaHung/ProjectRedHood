using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedManager : MonoBehaviour
{
    public GameObject pausedMenu;
    public GameObject player;
    public GameObject settingMenu;

    public static bool isPaused;

    void Start()
    {
        pausedMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    public void Paused()
    {
        pausedMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
        AudioManager.Instance.StopMusicBG();
    }

    public void Resume()
    {
        pausedMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        AudioManager.Instance.PlayMusicBG();
    }

    public void SaveGame()
    {
        GameSaveLoadManager.Instance.SaveGame(player.transform.position);
    }

    public void BackToMainMenu()
    {
        GameManager.Instance.BackToMainMenu();
        Time.timeScale = 1f;
        AudioManager.Instance.PlayMusicBG();
    }

    public void Setting()
    {
        settingMenu.SetActive(true);
        pausedMenu.SetActive(false);
    }

    public void Back()
    {
        pausedMenu.SetActive(true);
        settingMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
 }
