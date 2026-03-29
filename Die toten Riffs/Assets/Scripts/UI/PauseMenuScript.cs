using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{

    public GameObject PauseMenu;
    public static bool IsPaused = false;

    void Update()
    {
        
        if(Input.GetKeyUp(KeyCode.P))
        {

            if (IsPaused)
            {
                Resume();
            }
            else 
            {
                Pause();
            }

        }

    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    void Pause()
    {
        PauseMenu?.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }

}
