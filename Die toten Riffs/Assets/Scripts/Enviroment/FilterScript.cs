using UnityEngine;

public class FilterScript : MonoBehaviour, Interactable
{

    public void Interact()
    {

        InventoryScript Filter = GetComponentInChildren<InventoryScript>();

        Filter.GasMaskFilter++;

    }

}
