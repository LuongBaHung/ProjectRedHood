using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject settingCanvas;


    public void PlayGame()
    {
        GameManager.Instance.PlayGame();
        AudioManager.Instance.StopMusicBG();
    }

    public void LoadGame()
    {
        GameSaveLoadManager.Instance.LoadGame();
        Time.timeScale = 1f;
    }

    public void Setting()
    {
        settingCanvas.SetActive(true);
        menuCanvas.SetActive(false);
    }

    public void Back()
    {
        settingCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
