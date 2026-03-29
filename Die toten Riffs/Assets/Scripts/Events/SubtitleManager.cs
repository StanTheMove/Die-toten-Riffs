using UnityEngine;
using TMPro; 
using System.Collections;

public class SubtitleManager : MonoBehaviour
{
    public static SubtitleManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI subtitleText;
    [SerializeField] private float defaultDisplayTime = 4f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        subtitleText.gameObject.SetActive(false);
    }
    
    public void ShowText(string text)
    {
        StopAllCoroutines();
        StartCoroutine(DisplayRoutine(text));
    }

    private IEnumerator DisplayRoutine(string text)
    {
        subtitleText.text = text;
        subtitleText.gameObject.SetActive(true);
        yield return new WaitForSeconds(defaultDisplayTime);
        subtitleText.gameObject.SetActive(false);
    }
}
