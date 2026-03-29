using UnityEngine;

public class DeadCoral : MonoBehaviour
{
   [SerializeField] private float damage = 30f;
   [SerializeField] private float damageCooldown = 1f;
   
   private float lastDamageTime = -999f; 

   private void OnTriggerEnter(Collider other)
   {
      IDamageable damageable = other.GetComponentInParent<IDamageable>();
      if (Time.time >= lastDamageTime + damageCooldown && damageable != null)
      {
         damageable.TakeDamage(damage);
         lastDamageTime = Time.time;
      }
   }
}
