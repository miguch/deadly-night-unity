using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static com.ImmersiveMedia.Enums.InteractionEnums;

namespace com.ImmersiveMedia.CharacterControl
{
    /// <summary>
    /// An object that a character controller can interact with
    /// </summary>
    public class CharacterInteractable : MonoBehaviour
    {
        [SerializeField] CharacterInteraction interactionOption; // The action a character can take with this object
        [SerializeField] UnityEvent onInteract;
        [SerializeField] float interactionDistance; // The distance the character must be to interact with this interactable

        /// <summary>
        /// Changes the interaction the user can make with this object
        /// </summary>
        /// <param name="interactableID">The int value that maps to an interaction type for the interactable enum</param>
        public void SetCharacterInteraction(int interactableID)
        {
            interactionOption = (CharacterInteraction)interactableID;
        }

        /// <summary>
        /// Event to fire when an object interacts with this user
        /// </summary>
        public void OnInteract()
        {
            onInteract?.Invoke();
        }

        public float InteractionDistance { get => interactionDistance; }
        public CharacterInteraction InteractionOption { get => interactionOption; set => interactionOption = value; }
    }
}

