using UnityEngine;

public class FilterScript : MonoBehaviour, Interactable
{

    public void Interact()
    {

        InventoryScript Filter = FindFirstObjectByType<InventoryScript>();

        Debug.Log("Filter");

        gameObject.SetActive(false);

    }

}
