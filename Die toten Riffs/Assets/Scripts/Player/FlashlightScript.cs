using UnityEngine;

public class FlashlightScript : MonoBehaviour
{

    public GameObject Flashlight;
    bool FlashlightActive = false;

    void Start()
    {
        Flashlight.gameObject.SetActive(false);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (FlashlightActive == true)
            {
                Flashlight.gameObject.SetActive(false);
                FlashlightActive = false;
            }
            else if (FlashlightActive == false)
            {
                Flashlight.gameObject.SetActive(true);
                FlashlightActive = true;
            }
        }

    }

}
