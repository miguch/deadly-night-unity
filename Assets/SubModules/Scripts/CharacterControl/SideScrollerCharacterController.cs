using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace com.ImmersiveMedia.CharacterControl
{
    /// <summary>
    /// Controls a side scroller character
    /// </summary>
    public class SideScrollerCharacterController : IMContinousInputCharacterController
    {
        // The animator that the sidescroller character will use
        [SerializeField] private Animator animator;
        [SerializeField] UnityEvent onPlayerDeath;

        // Stores the last value retrieved from the x input axis
        private float xInputAxis = 0;

        protected override void Update()
        {
            base.Update();
            UpdateAnimator();
        }

        /// <summary>
        /// Gets horizontal axis input for 
        /// character movement and animation
        /// </summary>
        protected override void GetUserInput()
        {
            base.GetUserInput();
            xInputAxis = Input.GetAxis("Horizontal");
        }
        
        /// <summary>
        /// Updates variables on the animator
        /// </summary>
        private void UpdateAnimator()
        {
            animator.SetFloat("HorizontalVelocity", rb.velocity.z);
            animator.SetFloat("VerticalVelocity", rb.velocity.y);
            animator.SetBool("Grounded", grounded);
            animator.SetBool("PlayerJumped", jump);
        }

        /// <summary>
        /// Moves the character along the x axis using a rigidbody
        /// </summary>
        protected override void MoveCharacter()
        {
            if(grounded && MovementActive)
            {
                float speed = xInputAxis > 0 ? this.maxSpeed : this.maxSpeed / 2;

                float xMoveSpeed = xInputAxis * speed * Time.fixedDeltaTime;
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, xMoveSpeed);
            }
            else
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0f);
            }
            
        }

        public void OnPlayerDeath()
        {
            MovementActive = false;
            animator.SetTrigger("Death");
            onPlayerDeath?.Invoke();
        }
    }
}
