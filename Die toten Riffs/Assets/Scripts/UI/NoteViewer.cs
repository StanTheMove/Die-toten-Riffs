using UnityEngine;
using TMPro; 

public class NoteViewer : MonoBehaviour
{
    public static NoteViewer Instance;

    public GameObject notePanel; 
    public TextMeshProUGUI noteText; 

    private bool isOpen = false;

    private void Awake()
    {
        Instance = this;
        if (notePanel != null) 
        {
            notePanel.SetActive(false); 
        }
        Time.timeScale = 1f; 
    }

    public void ShowNote(string text)
    {
        if (notePanel == null || noteText == null)
        {
            Debug.LogError("Помилка! Панель або Текст не підключені в Інспекторі (NoteViewer)!");
            return;
        }

        noteText.text = text;
        notePanel.SetActive(true);
        isOpen = true;
        Time.timeScale = 0f; 
    }

    private void Update()
    {
        if (isOpen && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab)))
        {
            notePanel.SetActive(false);
            isOpen = false;
            Time.timeScale = 1f; 
        }
    }
}
