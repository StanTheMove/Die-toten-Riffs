using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour, Interactable
{
    public GameObject player; 
    public Camera cinematicCamera; 
    public GameObject endingPanel; 
    public TextMeshProUGUI endingText; 
    
    [TextArea(5, 10)]
    public string finalMessage = "The reefs are saved. They tried, and you have succeeded.\n\nBut do you like what you see? Is this the future you want? Coral reefs are the natural protectors from the waves; if they die, coastal cities will suffer from floods.";
    public float typingSpeed = 0.05f; 
    public string mainMenuSceneName = "MainMenu"; 

    private bool isActivated = false;

    public void Interact()
    {
        if (isActivated) return;
        isActivated = true;
        StartCoroutine(PlayEnding());
    }

    private IEnumerator PlayEnding()
    {
        if (player != null) player.SetActive(false);
        if (cinematicCamera != null) cinematicCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f); 

        if (endingPanel != null) endingPanel.SetActive(true);
        if (endingText != null) endingText.text = ""; 
        
        foreach (char letter in finalMessage.ToCharArray())
        {
            endingText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        
        yield return new WaitForSeconds(6f);
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
