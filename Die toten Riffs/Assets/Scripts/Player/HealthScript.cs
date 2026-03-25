using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthScript : MonoBehaviour
{

    public int Health = 100;
    public Image damageOverlay;

    void TakeDamage(int DamegeTaken)
    {
        
        Health -= DamegeTaken;
        StopAllCoroutines();
        StartCoroutine(FlashRed());

    }

    IEnumerator FlashRed()
    {
        damageOverlay.color = new Color(1f, 0f, 0f, 0.4f); // flash on
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0.4f, 0f, elapsed / duration);
            damageOverlay.color = new Color(1f, 0f, 0f, alpha);
            yield return null;
        }

        damageOverlay.color = new Color(1f, 0f, 0f, 0f); // fully hidden
    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(5);
        }

    }
}
