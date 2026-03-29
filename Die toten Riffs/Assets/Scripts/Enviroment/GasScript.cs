using UnityEngine;

public class GasScript : MonoBehaviour, Interactable
{



    public void Interact()
    {

        InventoryScript.Gas++;

        gameObject.SetActive(false);

    }

}
