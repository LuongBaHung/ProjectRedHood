using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class GameSaveLoadManager : MonoBehaviour
{
    public static GameSaveLoadManager Instance { get; private set; }

    private string saveFilePath;

    private Vector3 loadPlayerPosition;
    private bool isLoadingGame = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            saveFilePath = Application.persistentDataPath + "/savegame.json";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    public void SaveGame(Vector3 playerPosition)
    {
        GameData data = new GameData
        {
            playerPos = playerPosition,
            sceneName = SceneManager.GetActiveScene().name,
        };
        
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Save Game" + json);
    }


    public GameData LoadGame()
    {
        if(File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            loadPlayerPosition = data.playerPos;
            isLoadingGame = true;

            Debug.Log("Load Game:" + json);

            SceneManager.LoadScene(data.sceneName);
            return data;
        }
        else
        {
            Debug.LogWarning("No save file found!");
            return null;
        }

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (isLoadingGame)
        {
            GameObject player = GameObject.FindWithTag("Player");

            GameObject timeLine = GameObject.FindWithTag("TimeLine");
            if(timeLine != null)
            {
                PlayableDirector playableDirector = timeLine.GetComponent<PlayableDirector>();
                if(playableDirector != null)
                {
                    playableDirector.time = playableDirector.duration;
                    playableDirector.Evaluate();
                    playableDirector.Stop();
                }
            }


            if (player != null)
            {
                player.transform.position = loadPlayerPosition;
                Debug.Log("Player position set to: " + loadPlayerPosition);
            }
            else
            {
                Debug.Log("Player Not Found");
            }
            isLoadingGame = false;
        }
    }
}
