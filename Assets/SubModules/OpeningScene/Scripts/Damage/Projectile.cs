using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace com.ImmersiveMedia.Damage
{
    /// <summary>
    ///  A poolable projectile that moves at a constant speed
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb; // The rigidbody the projectile uses to move
        [SerializeField] private float initialForce; 
        [SerializeField] float persistTime; // The time the projectile should be active

        [SerializeField] UnityEvent onCollision;

        private IEnumerator waitRoutine;
        private bool waiting = false;

        private void OnEnable()
        {
            rb.AddForce(transform.forward * initialForce, ForceMode.Impulse);
            rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
            waitRoutine = WaitThenDeactivate();
            // Start the coroutine that deactivates the projectile after a specified amount of time
            StartCoroutine(waitRoutine);
        }

        void FixedUpdate()
        {
            if (rb.velocity != Vector3.zero)
            {
                rb.rotation = Quaternion.LookRotation(rb.velocity);
            }
                
        }

        /// <summary>
        /// Waits for a specified amount of time to deactivate a projectile
        /// </summary>
        private IEnumerator WaitThenDeactivate()
        {
            waiting = true;
            float spawnTime = Time.time;

            while (Time.time - spawnTime < persistTime)
            {
                yield return new WaitForEndOfFrame();
            }
            waiting = false;
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            // On disable reset the rigidbody values
            rb.velocity = Vector3.zero;
            rb.rotation = Quaternion.identity;

            if(waiting)
            {
                waiting = false;
                StopCoroutine(waitRoutine);
            }
            rb.constraints = RigidbodyConstraints.None;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(!collision.gameObject.tag.Equals("TeleportPoint"))
            {
                onCollision?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}
