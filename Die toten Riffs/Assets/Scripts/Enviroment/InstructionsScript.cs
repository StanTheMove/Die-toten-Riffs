using UnityEngine;
using TMPro;

public class InstructionsScript : MonoBehaviour, Interactable
{
    public TMP_Text TextFieldAC;

    public void Interact()
    {

        InventoryScript.InstructionForGenerator++;
        GeneratorScript.instance.ShowText("You picked up an instruction", TextFieldAC);
        gameObject.SetActive(false);

    }

}
