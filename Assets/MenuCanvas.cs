using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCanvas : MonoBehaviour
{
    public GameObject pausePanel;
    public Conductor conductor;

    private void Start()
    {
        conductor = GameObject.FindWithTag("Conductor").GetComponent<Conductor>();
    }

    // Closes the entire application
    public void ExitGame()
    {
        Application.Quit();
    }

    // Go to the main menu of the game (song select)
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            conductor.SwitchMusicPause();
            if (pausePanel.activeSelf)
            {
                pausePanel.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
