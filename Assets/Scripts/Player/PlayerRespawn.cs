using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public GameObject gameOverUI;

    public Vector3 respawnPoint;

    public void RespawnNow()
    {
        transform.position = respawnPoint;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Death")
        {
            Time.timeScale = 0;
            gameOverUI.SetActive(true);
            AudioManager.Instance.StopMusicBG();
        }
    }
}
