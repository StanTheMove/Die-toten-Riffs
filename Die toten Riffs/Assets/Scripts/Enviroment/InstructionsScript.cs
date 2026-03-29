using UnityEngine;

public class InstructionsScript : MonoBehaviour, Interactable
{



    public void Interact()
    {

        InventoryScript.InstructionForGenerator++;

        gameObject.SetActive(false);

    }

}
