using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBehavior : MonoBehaviour {

    GameObject mainMenu;
    GameObject pauseMenu;
    GameObject creditsMenu;

    bool gamePaused = false;

	// Use this for initialization
	void Start () {
        mainMenu = GameObject.Find("MainMenuPanel");
        pauseMenu = GameObject.Find("PausePanel");
        creditsMenu = GameObject.Find("CreditsPanel");

        if (mainMenu != null)
        {
            mainMenu.SetActive(true);
        }

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }

        if (creditsMenu != null)
        {
            creditsMenu.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if(gamePaused)
            {
                ResumeGame();
            } else
            {
                PauseGame();
            }
        }
	}

    public void PlayGame()
    {
        Application.LoadLevel(1);
    }

    public void PauseGame()
    {
        // Pause gameplay
        
        pauseMenu.SetActive(true);

        gamePaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);

        gamePaused = false;
    }

    public void ToMainMenu()
    {
        Application.LoadLevel(0);
    }

    public void ViewCredits()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void ExitCredits()
    {
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
