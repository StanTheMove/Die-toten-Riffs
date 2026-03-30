using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 

public class GeneratorScript : MonoBehaviour, Interactable
{
    public TMP_Text GeneratorText;
    public static GeneratorScript instance;
    private Coroutine currentCoroutine;
    
    public GameObject player; 
    public Camera cinematicCamera; 
    public GameObject endingPanel; 
    public TextMeshProUGUI endingText; 
    
    [TextArea(5, 10)]
    public string finalMessage = "The reefs are saved. They tried, and you have succeeded.\n\nBut do you like what you see? Is this the future you want? Coral reefs are the natural protectors from the waves; if they die, coastal cities will suffer from floods.";
    public float typingSpeed = 0.05f; 
    public string mainMenuSceneName = "MainMenuScene"; 

    private bool isActivated = false; 

    void Awake()
    {
        instance = this;
    }

    public void Interact()
    {
        if (isActivated) return; 
        if (InventoryScript.InstructionForGenerator < 1)
        {
            ShowText("You need an instruction for this generator.", GeneratorText);
            Debug.Log("GenInstr");
        }
        else if (InventoryScript.Gas < 1) 
        { 
            ShowText("This generator needs a battery.", GeneratorText);
            Debug.Log("GenBatt");
        }
        else if (InventoryScript.AliveCoral < 1)
        {
            ShowText("You need a sample of an alive coral.", GeneratorText);
            Debug.Log("GenCora");
        }
        else
        {
            isActivated = true; 
            InventoryScript.Gas -= 1;
            InventoryScript.AliveCoral -= 1;
            
            if (currentCoroutine != null) StopCoroutine(currentCoroutine);
            
            StartCoroutine(PlayEnding());
        }
    }

    public void ShowText(string text, TMP_Text textBox)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(TypeText(text, textBox));
    }

    public IEnumerator TypeText(string text, TMP_Text textBox, float delay = 0.07f)
    {
        textBox.alpha = 1f;
        textBox.text = "";
        foreach (char c in text)
        {
            textBox.text += c;
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(1.5f);
        float fadeDuration = 1f;
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            textBox.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            yield return null;
        }
        textBox.alpha = 0f;
        textBox.text = "";
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
            if (endingText != null) endingText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        
        yield return new WaitForSeconds(6f);
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}