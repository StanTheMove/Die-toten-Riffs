using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class LethalWater : MonoBehaviour
{
    [SerializeField] private float damagePerSec = 5f;
    [SerializeField] private float shallowDepth = 1.2f; 
    
    [SerializeField] private float drownTime = 3f;
    
    [SerializeField] private float waterDrag = 5f; 
    [SerializeField] private float gravityMultiplier = 0.2f;
    
    private Collider waterCollider; 

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
                defDrag = rb.linearDamping;
                rb.linearDamping = waterDrag; 
            }
            
            victims[other] = new WaterInfo
            {
                Damageable = damageable,
                Transform = other.transform,
                Rb = rb,
                DefaultDrag = defDrag,
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
                Vector3 counterGravity = Physics.gravity * (1f - gravityMultiplier);
                victim.Rb.AddForce(-counterGravity, ForceMode.Acceleration);
            }
            
            float depth = surfaceY - victim.Transform.position.y;
            
            if (depth > shallowDepth) 
            {
                victim.DrownTimer += Time.fixedDeltaTime;

                if (victim.DrownTimer >= drownTime)
                {
                    victim.Damageable.TakeDamage(9999f);
                }
            }
            else
            {
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