using TMPro;
using UnityEngine;
using System.Collections;

public class GeneratorScript : MonoBehaviour, Interactable
{
    public TMP_Text GeneratorText;
    public static GeneratorScript instance;
    private Coroutine currentCoroutine;

    void Awake()
    {
        instance = this;
    }

    public void Interact()
    {
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
            ShowText("Success!", GeneratorText);
            InventoryScript.Gas -= 1;
            InventoryScript.AliveCoral -= 1;
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
}