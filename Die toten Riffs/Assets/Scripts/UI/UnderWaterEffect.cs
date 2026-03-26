using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    public class UnderWaterEffect : MonoBehaviour
    {
        private Image waterOverlay;

        private void Start()
        {
            if (waterOverlay != null) waterOverlay.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Water") && waterOverlay != null)
                waterOverlay.enabled = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.CompareTag("Water") && waterOverlay != null)
                waterOverlay.enabled = false;
        }
    }
}