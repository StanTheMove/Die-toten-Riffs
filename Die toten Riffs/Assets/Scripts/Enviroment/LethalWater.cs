using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class LethalWater : MonoBehaviour
{
    [SerializeField] private float damagePerSec = 5f;
    [SerializeField] private float shallowDepth = 1.2f; // Межа між мілиною і глибиною
    
    [SerializeField] private float drownTime = 3f;
    
    [SerializeField] private float waterDrag = 5f; // Опір води
    [SerializeField] private float gravityMultiplier = 0.2f;
    
    private Collider waterCollider; // Тепер працює з будь-якою формою

    private class WaterInfo
    {
        public IDamageable Damageable;
        public Transform Transform;
        public Rigidbody Rb;
        public float DefaultDrag;
        public float DrownTimer;
        public float NextDamageTime;
    }
    
    private Dictionary<Collider, WaterInfo> victims = new Dictionary<Collider, WaterInfo>();

    private void Awake()
    {
        waterCollider = GetComponent<Collider>();
        waterCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponentInParent<IDamageable>();
        if (damageable != null)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            float defDrag = 0f;

            if (rb != null)
            {
                defDrag = rb.linearDamping; // Запам'ятовуємо нормальний опір повітря
                rb.linearDamping = waterDrag; // Робимо рух важким у воді
            }
            
            victims[other] = new WaterInfo
            {
                Damageable = damageable,
                Transform = other.transform,
                Rb = rb,
                DefaultDrag = defDrag, // Зберігаємо нормальний опір
                DrownTimer = 0f,
                NextDamageTime = Time.time
            };
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (victims.TryGetValue(other, out WaterInfo victim))
        {
            if (victim.Rb != null)
            {
                // ПОВЕРТАЄМО нормальний рух, коли гравець виходить з води
                victim.Rb.linearDamping = victim.DefaultDrag; 
            }
            victims.Remove(other);
        }
    }

    private void FixedUpdate()
    {
        float surfaceY = waterCollider.bounds.max.y;

        foreach (var kvp in victims)
        {
            WaterInfo victim = kvp.Value;
            if (victim.Transform == null) continue;

            if (victim.Rb != null)
            {
                // Виштовхуємо наверх, щоб падіння було повільним
                Vector3 counterGravity = Physics.gravity * (1f - gravityMultiplier);
                victim.Rb.AddForce(-counterGravity, ForceMode.Acceleration);
            }
            
            float depth = surfaceY - victim.Transform.position.y;

            // ВИПРАВЛЕНО: Якщо глибина БІЛЬША за дозволену, починаємо топити
            if (depth > shallowDepth) 
            {
                victim.DrownTimer += Time.fixedDeltaTime;

                if (victim.DrownTimer >= drownTime)
                {
                    victim.Damageable.TakeDamage(9999f); // Смерть
                }
            }
            else
            {
                // На мілині обнуляємо час утоплення і просто завдаємо шкоди
                victim.DrownTimer = 0f;

                if (Time.time >= victim.NextDamageTime)
                {
                    victim.Damageable.TakeDamage(damagePerSec);
                    victim.NextDamageTime = Time.time + 1f;  
                }
            }
        }
    }
}