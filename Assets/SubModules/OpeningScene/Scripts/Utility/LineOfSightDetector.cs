using com.ImmersiveMedia.CharacterControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static com.ImmersiveMedia.Enums.InteractionEnums;

namespace com.ImmersiveMedia.Utility
{
    /// <summary>
    /// Provides line of sight detection for objects
    /// </summary>
    public class LineOfSightDetector : MonoBehaviour
    {
        [SerializeField] string sightReactionTag; // The tag this detector should react to
        [SerializeField] UnityEvent<CharacterInteractable> onSeeCharacterEvent; // Event to fire when an object is detected

        GameObject detectedObject; // The object that has currently been detected

        private void OnTriggerStay(Collider other)
        {
            if (detectedObject == null
                || (other.gameObject.GetInstanceID() != detectedObject.gameObject.GetInstanceID()
                && other.tag.Equals(sightReactionTag)))
            {
                CharacterInteractable interactable = other.GetComponent<CharacterInteractable>();
                if (interactable != null)
                {
                    detectedObject = other.gameObject;
                    onSeeCharacterEvent?.Invoke(interactable);
                }

            }
        }

        private void OnDisable()
        {
            detectedObject = null;
        }

    }
}
