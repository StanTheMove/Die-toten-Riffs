using UnityEngine;
public class CheckpointScript : MonoBehaviour
{
    public static int CurrentAliveCoral;
    public static int CurrentInstructionForGenerator;
    public static int CurrentGas;
    public static float CurrentHealth;
    public static Transform Pllayer;
    public static Vector3 SpawnPosition;
    public bool IsActivated = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pllayer = other.transform;
            if (!IsActivated)
            {
                Debug.Log("Trigger hit by: " + other.name + " tag: " + other.tag);
                CurrentAliveCoral = InventoryScript.AliveCoral;
                CurrentInstructionForGenerator = InventoryScript.InstructionForGenerator;
                CurrentGas = InventoryScript.Gas;
                CurrentHealth = HealthScript.Health;
                SpawnPosition = Pllayer.position;
                IsActivated = true;
            }
        }
    }
}