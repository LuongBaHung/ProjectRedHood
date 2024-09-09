using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void BackToMainMenu()
    {
        GameManager.Instance.BackToMainMenu();
        AudioManager.Instance.PlayMusicBG();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
