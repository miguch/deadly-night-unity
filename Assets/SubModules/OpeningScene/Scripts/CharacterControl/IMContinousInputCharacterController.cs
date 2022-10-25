using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ImmersiveMedia.CharacterControl
{
    /// <summary>
    /// Base class for character controllers that
    /// need continous input from the user to function.
    /// Such as first person controllers or controllers for platformers
    /// </summary>
    public abstract class IMContinousInputCharacterController : MonoBehaviour
    {
        [SerializeField] bool movementActive = true;

        // The maximum speed the character can move
        [SerializeField] protected float maxSpeed;

        // The rigidbody the character uses to move
        [SerializeField] protected Rigidbody rb;

        // Stores whether or not the character is currently touching the ground
        [SerializeField] protected bool grounded = false;
        // Stores whether the character should jump when fixed update runs
        [SerializeField] protected bool jump = false; 
        // The minimum distance a player can be from the ground and still be considered to grounded
        [SerializeField] private float groundingDistance;
        // The configurable interval on which to check whether the character is grounded
        [SerializeField] private float groundCheckInterval;

        // The configurable button that the user will press to jump
        // [SerializeField] KeyCode jumpButton;
        // The amount of upward force that will be applied when the character jumps
        [SerializeField] private float jumpForce;
        [SerializeField] LayerMask groundLayers;

        public bool MovementActive { get => movementActive; set => movementActive = value; }

        protected abstract void MoveCharacter();

        protected virtual void Start()
        {
            // Start checking whether the character is grounded
            StartCoroutine(GroundedCheckLoop());
        }

        protected virtual void Update()
        {
            GetUserInput();
        }

        private void FixedUpdate()
        {
            if (grounded && MovementActive)
            {
                if (jump)
                {
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    jump = false;
                }
                MoveCharacter();
            }
        }

        /// <summary>
        /// Gets user input and stores it for rigidbody actions in fixedupdate
        /// </summary>
        protected virtual void GetUserInput()
        {
            if (grounded && Input.GetButtonDown("Jump") && movementActive)
            {
                jump = true;
            }
        }

        /// <summary>
        /// Checks continously whether the character is touching the ground
        /// </summary>
        private IEnumerator GroundedCheckLoop()
        {
            while (true)
            {
                RaycastHit hit;

                // Raycast straight down and see if we hit anything
                if (Physics.Raycast(transform.position, Vector3.down, out hit, groundingDistance, groundLayers))
                {
                    grounded = true;
                }
                else
                {
                    grounded = false;
                }

                yield return new WaitForSeconds(groundCheckInterval);
            }
        }
    }
}
