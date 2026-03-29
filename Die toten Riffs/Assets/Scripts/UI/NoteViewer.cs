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
        notePanel.SetActive(false); 
    }

 
    public void ShowNote(string text)
    {
        noteText.text = text;
        notePanel.SetActive(true);
        isOpen = true;
        Time.timeScale = 0f; 
    }

    private void Update()
    {
        if (isOpen && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E)))
        {
            notePanel.SetActive(false);
            isOpen = false;
            Time.timeScale = 1f; 
        }
    }
}
