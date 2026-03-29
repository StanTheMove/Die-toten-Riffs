using TMPro;
using UnityEngine;

public class AliveCoralScript : MonoBehaviour, Interactable
{
    public TMP_Text TextFieldAC;

    public void Interact()
    {
        InventoryScript.AliveCoral++;
        GeneratorScript.instance.ShowText("You picked up a coral", TextFieldAC);
        gameObject.SetActive(false);
    }
}