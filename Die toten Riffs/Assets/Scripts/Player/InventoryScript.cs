using UnityEngine;

public class InventoryScript : MonoBehaviour
{

    public int GasMaskFilter = 0;

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.L))
        {

            if (GasMaskFilter > 0) 
            {
                GasMaskFilter--;
            }

        }

    }

}
