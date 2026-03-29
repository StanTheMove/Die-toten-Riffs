using UnityEngine;

public class FlashlightScript : MonoBehaviour
{
    public AudioClip FlashlightOnSFX;
    public AudioClip FlashlightOffSFX;
    public GameObject flashlightObject;

    private bool isFlashlightOn = false;
void Start()
    {
        flashlightObject.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFlashlightOn = !isFlashlightOn;
            flashlightObject.SetActive(isFlashlightOn);

            if (isFlashlightOn)
                AudioManagerScript.instance.PlaySFX(FlashlightOnSFX);
            else
                AudioManagerScript.instance.PlaySFX(FlashlightOffSFX);
        }
    }
}
