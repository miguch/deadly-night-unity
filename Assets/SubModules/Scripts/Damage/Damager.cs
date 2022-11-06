using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static com.ImmersiveMedia.Enums.InteractionEnums;

namespace com.ImmersiveMedia.Damage
{
    /// <summary>
    /// A class that causes damage to a damageble object
    /// </summary>
    public class Damager : MonoBehaviour
    {
        [SerializeField] bool activated; // if activated this damager will cause damage
        [SerializeField] float damageAmount; // The amount this damager damages an object per hit

        [SerializeField] List<DamageableSet> damageableSets; // The list of sets this damager can damage

        [SerializeField] UnityEvent onDamage;

        public bool Activated { get => activated; set => activated = value; }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("collide");
            if (Activated)
            {
                // If the object that was hit has a damageble script it can be damaged
                Damageable damageable = other.gameObject.GetComponent<Damageable>();
                if (damageable != null)
                {
                    // Intersect the sets of damageable and damager 
                    var intersection = damageable.DamagableSets.Intersect(damageableSets);

                    // If the intersection of the sets count is greater than zero that means this damage can damage it
                    if (intersection.Count() > 0 && damageable.Activated)
                    {
                        onDamage?.Invoke();
                        damageable.Damage(damageAmount, damageableSets);
                    }

                }
            }
        }
    }
}
