using System.Collections.Generic;
using UnityEngine;

public class LethalWater : MonoBehaviour
{
    [SerializeField] private float damagePerSec = 5f;
    [SerializeField] private float shallowDepth = 1.2f;
    
    [SerializeField] private float drownTime = 3f;
    [SerializeField] private float sinkForce = 15f;
    
    private BoxCollider waterCollider;

    public class WaterInfo
    {
        public IDamageable Damageable;
        public Transform Transform;
        public Rigidbody Rb;
        public float DrownTimer;
        public float NextDamageTime;
    }
    
    private Dictionary<Collider, WaterInfo> waterInfos = new Dictionary<Collider, WaterInfo>();
    
}
