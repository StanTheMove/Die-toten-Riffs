using UnityEngine;
using UnityEngine.UI;

public class VolumeScript : MonoBehaviour
{
    public Slider VolumeSlider;

    public void OnSliderChanged()
    {
        HolderScript.Volume = VolumeSlider.value;
    }
}