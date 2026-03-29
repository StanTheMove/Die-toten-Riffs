using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
public class HealthScript : MonoBehaviour, IDamageable
{
    public static float Health = 100f;
    public Image damageOverlay;
    [SerializeField, Range(0, 0.1f)] private float _Amplitude = 0.015f;
    [SerializeField, Range(0, 30)] private float _frequency = 10.0f;
    [SerializeField] private Transform _camera = null;
    [SerializeField] private Transform _cameraHolder = null;
    public static bool IsDead = false;
    private Vector3 _startPos;

    private void Awake()
    {
        _startPos = _camera.localPosition;
        CheckpointScript.Pllayer = this.transform;
        CheckpointScript.SpawnPosition = this.transform.position;
        CheckpointScript.CurrentHealth = Health;
    }

    IEnumerator FlashRed()
    {
        damageOverlay.color = new Color(1f, 0f, 0f, 0.4f);
        float duration = 0.5f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0.4f, 0f, elapsed / duration);
            damageOverlay.color = new Color(1f, 0f, 0f, alpha);
            yield return null;
        }
        damageOverlay.color = new Color(1f, 0f, 0f, 0f);
    }

    IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        float elapsed = 0f;
        Vector2 random2D = Random.insideUnitCircle.normalized;
        Vector3 shakeDir = new Vector3(random2D.x, random2D.y, 0f);
        while (elapsed < duration)
        {
            float strength = magnitude * (1f - (elapsed / duration));
            float offset = Mathf.Sin(elapsed * _frequency) * strength;
            _camera.localPosition = _startPos + shakeDir * offset;
            elapsed += Time.deltaTime;
            yield return null;
        }
        _camera.localPosition = _startPos;
    }

    public void TakeDamage(float Damage)
    {
        Health -= Damage;
        StopAllCoroutines();
        StartCoroutine(FlashRed());
        StartCoroutine(ShakeCoroutine(0.3f, Damage * 0.05f));
        if (Health <= 0)
        {
            Debug.Log(Health);
            IsDead = true;
        }
    }

    public void Shake(float magnitude)
    {
        Vector3 originalPos = _camera.localPosition;
        _camera.localPosition = new Vector3(
            originalPos.x + Random.Range(-1f, 1f) * magnitude,
            originalPos.y + Random.Range(-1f, 1f) * magnitude,
            originalPos.z
        );
        _camera.localPosition = originalPos;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(5f);
        }
    }
}