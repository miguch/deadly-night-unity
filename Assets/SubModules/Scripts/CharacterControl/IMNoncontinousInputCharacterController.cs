using com.ImmersiveMedia.Damage;
using com.ImmersiveMedia.Movement;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using static com.ImmersiveMedia.Enums.InteractionEnums;

namespace com.ImmersiveMedia.CharacterControl
{
    /// <summary>
    /// Base class for scripts that control a game character
    /// </summary>
    public abstract class IMNoncontinousInputCharacterController : MonoBehaviour
    {

        [SerializeField] Animator anim; // The character's animator
        [SerializeField] Mover mover; // The mover that moves this character
        [SerializeField] Damager damager; // The damager that acts as the character's weapon

        [SerializeField] UnityEvent<GameObject> onInteractionComplete;
        [SerializeField] UnityEvent onDeath;
        [SerializeField] UnityEvent onCleanUp;
        [SerializeField] bool shouldCleanUp; // Whether this character should be cleaned up after death
        [SerializeField] float cleanupDelay; // The amount of time to wait before cleaning up the character


        private Vector3 selectedInteractableReferencePosition; // The position of the interactable this character has selected to interact with
        protected CharacterInteractable selectedInteractable; // The interactable currently selected by the character for interaction

        bool dead = false; // Whther this character is currently dead

        CharacterInteraction lastFrameInteraction; // The character interaction stored in the previous frame

        protected abstract void InteractionSelectionLoop(); // The method child classes use to select an interactable

        private void Awake()
        {
            if (onInteractionComplete == null)
            {
                onInteractionComplete = new UnityEvent<GameObject>();
            }
        }

        private void Start()
        {
            // When the mover arrives at its destination we should always start the interaction for that destination
            mover.ArrivedAtDestination.AddListener(StartInteraction);
        }

        protected virtual void Update()
        {
            // Debug so that we can test killing characters
            // if (Input.GetKeyDown(KeyCode.Space))
            // {
            //     KillCharacter();
            // }

            if (!dead)
            {
                InteractionSelectionLoop(); // Run the interaction selection loop if the character is not dead

                if (selectedInteractable != null)
                {
                    // This statement is to check if the interactable has moved enough that it needs to be chased
                    if (Vector3.Distance(selectedInteractable.transform.position, selectedInteractableReferencePosition) > 0.1f)
                    {
                        mover.StartMovementRoutine(selectedInteractable.transform.position, selectedInteractable.InteractionDistance);
                        selectedInteractableReferencePosition = selectedInteractable.transform.position;
                    }

                    // If the object is no longer interactable
                    if (selectedInteractable.InteractionOption == CharacterInteraction.NOT_INTERACTABLE)
                    {
                        // If the object was interactable last frame and is not this frame. Then the interaction is complete
                        if (selectedInteractable != null && selectedInteractable.InteractionOption == CharacterInteraction.NOT_INTERACTABLE
                            && lastFrameInteraction != CharacterInteraction.NOT_INTERACTABLE)
                        {
                            onInteractionComplete?.Invoke(gameObject);
                        }

                        // Interactable objects cannot be attacked so we should stop attacking and turn off the damager
                        anim.SetBool("Attacking", false); 
                        damager.Activated = false;
                    }
                    lastFrameInteraction = selectedInteractable.InteractionOption; // Need to keep track of the last frames interaction so we can determine when the interaction is over
                }

                // damager only activated when actually attacking
                damager.Activated = anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"); 

                anim.SetBool("Stopped", !mover.MovementRoutineActive); // Set the value of stopped on the animator to whatever the mover's value is
            }

        }

        /// <summary>
        /// Starts an interaction when the character reaches it's target
        /// </summary>
        public void StartInteraction()
        {
            if (selectedInteractable != null)
            {
                switch (selectedInteractable.InteractionOption)
                {
                    case CharacterInteraction.ATTACK:
                        anim.SetBool("Attacking", true);
                        damager.Activated = true;
                        selectedInteractable.OnInteract();
                        break;
                    case CharacterInteraction.TALK:
                        selectedInteractable.OnInteract();
                        break;
                }
            }


        }

        /// <summary>
        /// Sets an interaction to be executed by the character
        /// </summary>
        /// <param name="interactable">the interactble for the character interact with</param>
        public void SetInteraction(CharacterInteractable interactable)
        {
            anim.SetBool("Attacking", false); // Anytime we start a new interacion we stop attacking

            if (interactable != null)
            {
                selectedInteractable = interactable;
                selectedInteractableReferencePosition = interactable.transform.position;

                damager.Activated = false; // Turn of the damager while the character is walking to it's target
                mover.StartMovementRoutine(selectedInteractable.transform.position, selectedInteractable.InteractionDistance);
            } else {
                selectedInteractable = null;
                mover.Stop();
                anim.SetBool("Stopped", false);
            }
        }

        /// <summary>
        /// Kills the character
        /// </summary>
        public void KillCharacter()
        {
            onDeath?.Invoke(); // Fire on death callback for editor assignable events
            mover.Stop(); // Stop the mover
            onInteractionComplete.RemoveAllListeners(); // Clear out any on interaction complete listeners

            // Set the animation values for death
            anim.SetBool("Attacking", false);
            anim.SetBool("Dead", true);
            anim.SetBool("Stopped", true);

            dead = true;
            selectedInteractable = null;

            // Start the cleanup coroutine if this object has been configured for cleanup
            if (shouldCleanUp)
            {
                StartCoroutine(CleanUpAfterDelay());
            }

        }

        /// <summary>
        /// Cleans up the character after a delay reseting it to starting state
        /// </summary>
        private IEnumerator CleanUpAfterDelay()
        {
            yield return new WaitForSeconds(cleanupDelay);
            anim.SetBool("Dead", false); // Reset the character to be alive
            onCleanUp?.Invoke(); // Execute any editor callbacks
            dead = false; // Set the user to not dead
        }

        public UnityEvent<GameObject> OnInteractionComplete { get => onInteractionComplete; }
        public UnityEvent OnDeath { get => onDeath; }
    }

}
