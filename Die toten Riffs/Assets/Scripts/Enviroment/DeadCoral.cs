using UnityEngine;
public class DeadCoral : MonoBehaviour
{
   [SerializeField] private float damage = 30f;
   [SerializeField] private float damageCooldown = 1f;
   private float lastDamageTime;

   void OnTriggerEnter(Collider other)
   {
      if (Time.time >= lastDamageTime + damageCooldown && other.TryGetComponent(out IDamageable damageable))
      {
         damageable.TakeDamage(damage);
         lastDamageTime = Time.time;
      }
   }
}
