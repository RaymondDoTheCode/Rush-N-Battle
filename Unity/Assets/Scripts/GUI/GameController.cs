using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject gameWonScreen;
    public PlayerHealth player;

    public static bool GameIsPaused = false;
    private float health;

    void Update()
    {
        health = player.GetComponent<PlayerHealth>().health;
        if (health == 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        gameOverScreen.SetActive(true);
        GameObject.Find("Player").GetComponent<InputManager>().enabled = false;
        GameObject.Find("AK47").GetComponent<GunController>().enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void GameWon()
    {
        Debug.Log("Game won!");
        gameWonScreen.SetActive(true);
        GameObject.Find("Player").GetComponent<InputManager>().enabled = false;
        GameObject.Find("AK47").GetComponent<GunController>().enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    void Retry()
    {
        GameObject.Find("Player").GetComponent<InputManager>().enabled = true;
        GameObject.Find("AK47").GetComponent<GunController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
