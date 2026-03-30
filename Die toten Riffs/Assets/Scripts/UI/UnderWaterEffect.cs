using UnityEngine;
[RequireComponent(typeof(Camera))]
public class UnderwaterEffect : MonoBehaviour
{
    [SerializeField] private Color underwaterColor = new Color(0.1f, 0.25f, 0.15f, 1f);
    [SerializeField] private float underwaterFogDensity = 0.15f;
    private Camera cam;

    private bool defaultFogState;
    private Color defaultFogColor;
    private float defaultFogDensity;
    private FogMode defaultFogMode;
    private Color defaultCamColor;
    private CameraClearFlags defaultClearFlags;

    private void Start()
    {
        cam = GetComponent<Camera>();

        defaultFogState = RenderSettings.fog;
        defaultFogColor = RenderSettings.fogColor;
        defaultFogDensity = RenderSettings.fogDensity;
        defaultFogMode = RenderSettings.fogMode;

        defaultCamColor = cam.backgroundColor;
        defaultClearFlags = cam.clearFlags;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            SetUnderwater(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            SetUnderwater(false);
        }
    }

    private void SetUnderwater(bool isUnderwater)
    {
        if (isUnderwater)
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = underwaterColor;
            RenderSettings.fogDensity = underwaterFogDensity;
            RenderSettings.fogMode = FogMode.ExponentialSquared;
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = underwaterColor;
        }
        else
        {
            RenderSettings.fog = defaultFogState;
            RenderSettings.fogColor = defaultFogColor;
            RenderSettings.fogDensity = defaultFogDensity;
            RenderSettings.fogMode = defaultFogMode;
            cam.clearFlags = defaultClearFlags;
            cam.backgroundColor = defaultCamColor;
        }
    }
}