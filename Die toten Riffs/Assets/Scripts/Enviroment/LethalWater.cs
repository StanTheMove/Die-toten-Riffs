using System;
using System.Collections.Generic;
using UnityEngine;

public class LethalWater : MonoBehaviour
{
    [SerializeField] private float damagePerSec = 5f;
    [SerializeField] private float shallowDepth = 1.2f;
    
    [SerializeField] private float drownTime = 3f;
    [SerializeField] private float sinkForce = 15f;
    
    private BoxCollider waterCollider;

    private class WaterInfo
    {
        public IDamageable Damageable;
        public Transform Transform;
        public Rigidbody Rb;
        public float DrownTimer;
        public float NextDamageTime;
    }
    
    private Dictionary<Collider, WaterInfo> victims = new Dictionary<Collider, WaterInfo>();

    private void Awake()
    {
        waterCollider = GetComponent<BoxCollider>();
        waterCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            victims[other] = new WaterInfo
            {
                Damageable = damageable,
                Transform = other.transform,
                Rb = other.GetComponent<Rigidbody>(),
                DrownTimer = 0f,
                NextDamageTime = Time.time
            };
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (victims.ContainsKey(other))
        {
            victims.Remove(other);
        }
    }

    private void Update()
    {
        float surfaceY = waterCollider.bounds.max.y;

        foreach (var kvp in victims)
        {
            WaterInfo victim = kvp.Value;
            if (victim.Transform == null) continue;
            
            float depth = surfaceY - victim.Transform.position.y;

            if (depth < shallowDepth)
            {
                victim.DrownTimer += Time.deltaTime;

                if (victim.Rb != null)
                {
                    victim.Rb.AddForce(Vector3.down * sinkForce, ForceMode.Acceleration);
                }
                else
                {
                    victim.Transform.position += Vector3.down * (sinkForce * 0.1f * Time.deltaTime);
                }

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
