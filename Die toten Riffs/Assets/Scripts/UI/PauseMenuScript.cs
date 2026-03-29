using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenuScript : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject DeathScrean;
    public static bool IsPaused = false;
    public static PauseMenuScript Instance;
    public AudioClip ClickSound;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
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
        if (HealthScript.IsDead && !IsPaused)
        {
            Death();
        }
    }
    public void Death()
    {
        DeathScrean.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }
    public void TryAgain()
    {
        Revive();
    }
    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }
    void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void PlayClickSoundPauseMenu()
    {
        AudioManagerScript.instance.PlaySFX(ClickSound);
    }

    public static void Revive()
    {
        InventoryScript.AliveCoral = CheckpointScript.CurrentAliveCoral;
        InventoryScript.InstructionForGenerator = CheckpointScript.CurrentInstructionForGenerator;
        InventoryScript.Gas = CheckpointScript.CurrentGas;
        HealthScript.Health = CheckpointScript.CurrentHealth;
        HealthScript.IsDead = false;
        Instance.DeathScrean.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
        Debug.Log("Teleporting to: " + CheckpointScript.SpawnPosition + " | Player at: " + CheckpointScript.Pllayer.position);
        CharacterController cc = CheckpointScript.Pllayer.GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.enabled = false;
            CheckpointScript.Pllayer.position = CheckpointScript.SpawnPosition;
            cc.enabled = true;
        }
        else
        {
            CheckpointScript.Pllayer.position = CheckpointScript.SpawnPosition;
        }
        CheckpointScript.Pllayer.position = CheckpointScript.SpawnPosition;
    }
}