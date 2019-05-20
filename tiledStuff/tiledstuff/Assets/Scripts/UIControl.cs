using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//@Author Natalie Eidt
public class UIControl : MonoBehaviour
{
    /// <summary>
    /// reference to the pause canvas
    /// </summary>
    private GameObject pauseMenu;

    /// <summary>
    /// source for the music
    /// </summary>
    private AudioSource musicSource;


    /// <summary>
    /// ref to credits canvas
    /// </summary>
    private GameObject creditsCanvas;

    private UIControl uiControl;


    private void Start()
    {
        //makes so only 1 of these exists
        if (uiControl == null)
        {
            DontDestroyOnLoad(this.gameObject);
            uiControl = this;
        }
        else if (uiControl != this)
        {
            Destroy(gameObject);
        }

        pauseMenu = GameObject.Find("PauseMenuCanvas");
        creditsCanvas = GameObject.Find("CreditsCanvas");
    }


    #region inGame Stuff

    /// <summary>
    /// this turns the pause menu on
    /// </summary>
    public void _TogglePauseCanvasOn()
    {
        pauseMenu = GameObject.Find("PauseMenuCanvas");
        pauseMenu.GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0.0f;
    }

    /// <summary>
    /// turns the pause menu off
    /// </summary>
    public void _TogglePauseCanvasOff()
    {
        pauseMenu = GameObject.Find("PauseMenuCanvas");
        pauseMenu.GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1.0f;
    }

    /// <summary>
    /// toggles the music when the button is pressed
    /// </summary>
    public void _ToggleMusic()
    {
        musicSource = this.gameObject.GetComponent<AudioSource>();
        if (musicSource.enabled)
        {
            musicSource.enabled = false;
        }
        else
        {
            musicSource.enabled = true;
        }
    }

    /// <summary>
    /// quits the game
    /// </summary>
    public void _QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// goes back to the main menu scene
    /// </summary>
    public void _GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    #endregion

    #region mainMenuStuff

    /// <summary>
    /// starts the game
    /// </summary>
    public void _StartGame()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// toggles the credits canvas in main menu on
    /// </summary>
    public void _ToggleCreditsCanvasOn()
    {
        creditsCanvas.GetComponent<Canvas>().enabled = true;
    }

    /// <summary>
    /// toggles credits canvas off
    /// </summary>
    public void _ToggleCreditsCanvasOff()
    {
        creditsCanvas.GetComponent<Canvas>().enabled = false;
    }

    #endregion

    /// <summary>
    /// does stuff based on what level was loaded
    /// </summary>
    /// <param name="level"> the level that was loaded </param>
    private void OnLevelWasLoaded(int level)
    {
        //if main menu was loaded
        if (level == 0)
        {
            creditsCanvas = GameObject.Find("CreditsCanvas");
        }
        else if (level == 1 || level == 2 || level == 3)
        {
            pauseMenu = GameObject.Find("PauseMenuCanvas");
        }

        //checks for completion
        switch (level)
        {
            case 1:
                LevelController.instance._CompletedLevel(1);
                break;
            case 2:
                LevelController.instance._CompletedLevel(2);
                break;
            case 3:
                LevelController.instance._CompletedLevel(3);
                break;
            default:
                break;
        }
    }
}
