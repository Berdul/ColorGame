using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pause();
        }
    }

    public void pause() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void resume() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

    }

    public void home() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main menu");
    }
}
