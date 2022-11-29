using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static com.ImmersiveMedia.Enums.InteractionEnums;

namespace com.ImmersiveMedia.Damage
{
    /// <summary>
    /// Class that allows an object to be damageable
    /// </summary>
    public class Damageable : MonoBehaviour
    {
        [SerializeField] bool activated; // Whether damage is active
        [SerializeField] float totalHealth; // The total health the object starts with
        [SerializeField] UnityEvent<float> onHealthPercentChange; // An event that tells listeners when the object's health changes

        [SerializeField] UnityEvent onDeath; // An event to fire when an object is destroyed

        [SerializeField] UnityEvent onAttacked;   
        
         [SerializeField] float onAttackedProbability = 0.0f; // the probability of damage triggering onAttacked

        [SerializeField] List<DamageableSet> damagableSets; // The list of Damageable sets this object belongs to.
        [SerializeField] float damageDebounceTime; // The amount of seconds in which damage can be triggered once, this is because every time the troll swing the mace it will collide with player twice

        [SerializeField] float overlayHoldTime = 0.4f;
        [SerializeField] float overlayFadeTime = 0.7f;
        [SerializeField] float overlayMaxAlpha = 0.6f;
        [SerializeField] Image bloodOverlay;

        private Debounce damageDebounce = new Debounce();

        private float health; // The current health of the the object

        private void Awake()
        {
            health = totalHealth; // initialize the health this particular object has
        }

        /// <summary>
        /// Resets this damagables health to its original value
        /// </summary>
        public void ResetHealth()
        {
            health = totalHealth;
            onHealthPercentChange?.Invoke(Health / totalHealth);
        }

        private IEnumerator OverlayRoutine() {
            bloodOverlay.color = new Color(bloodOverlay.color.r, bloodOverlay.color.g, bloodOverlay.color.b, overlayMaxAlpha);
            yield return new WaitForSeconds(overlayHoldTime);
            while(bloodOverlay.color.a > 0) {
                bloodOverlay.color = new Color(bloodOverlay.color.r, bloodOverlay.color.g, bloodOverlay.color.b, bloodOverlay.color.a - 1f / 255);
                yield return new WaitForSeconds(overlayFadeTime / (overlayMaxAlpha * 255));
            }
        }

        /// <summary>
        /// Applies damage to this damageable object
        /// </summary>
        /// <param name="damageAmount">The amount to damage this object</param>
        /// <param name="sets">The sets the damaging object belongs to</param>
        /// <param name="onDamage">onDamage event from damager, only invoked when damage success(not waiting)</param>
        public bool Damage(float damageAmount, List<DamageableSet> sets, UnityEvent onDamage)
        {
            if (damageDebounce.Wait) {
                return false;
            }
            onDamage?.Invoke();
            // If this object is currently damageable
            if (activated)
            {
                health -= damageAmount;

                // Let listeners know the new health percentage
                onHealthPercentChange?.Invoke(Health / totalHealth);

                // If health is less than zero raise the death event
                if (health <= 0f)
                {
                    onDeath?.Invoke();
                } else {
                    if (Random.Range(0.0f, 1.0f) < onAttackedProbability) {
                        onAttacked?.Invoke();
                    }
                }

                StartCoroutine(OverlayRoutine());
            }
            StartCoroutine(damageDebounce.Invoke(damageDebounceTime));
            return true;
        }

        public float Health { get => health; }
        public bool Activated { get => activated; set => activated = value; }
        public List<DamageableSet> DamagableSets { get => damagableSets; }
    }
}
