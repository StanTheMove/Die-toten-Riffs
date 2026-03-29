using UnityEngine;

public class FilterScript : MonoBehaviour, Interactable
{

    public void Interact()
    {

        InventoryScript Filter = FindFirstObjectByType<InventoryScript>();

        Filter.GasMaskFilter++;

        Debug.Log("Filter");

        gameObject.SetActive(false);

    }

}
