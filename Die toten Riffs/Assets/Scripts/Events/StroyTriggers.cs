using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class StoryTrigger : MonoBehaviour
{
    [SerializeField] private bool triggerOnce = true;
    [SerializeField] private string requiredTag = "Player";
    
    [SerializeField] private UnityEvent onTriggerEnterEvent;
    
    private bool hasTriggered = false;

    private void Awake() => GetComponent<BoxCollider>().isTrigger = true;

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered && triggerOnce) return;

        if (other.CompareTag(requiredTag))
        {
            onTriggerEnterEvent.Invoke();
            hasTriggered = true;
        }
    }
}