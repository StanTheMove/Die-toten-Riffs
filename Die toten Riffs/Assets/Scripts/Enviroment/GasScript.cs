using UnityEngine;
using TMPro;
public class GasScript : MonoBehaviour, Interactable
{
    public TMP_Text TextFieldAC;
    public void Interact()
    {
        InventoryScript.Gas++;
        GeneratorScript.instance.ShowText("You picked up a battery", TextFieldAC);
        gameObject.SetActive(false);

    }

}
