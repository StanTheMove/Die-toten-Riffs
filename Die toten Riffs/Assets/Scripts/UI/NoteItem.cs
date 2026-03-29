using UnityEngine;

public class NoteItem : MonoBehaviour, Interactable
{
    [TextArea(5, 10)] 
    public string textContent;
    
    public void Interact()
    {
        NoteViewer.Instance.ShowNote(textContent);
    }
}
