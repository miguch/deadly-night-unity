using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static com.ImmersiveMedia.Enums.InteractionEnums;

namespace com.ImmersiveMedia.CharacterControl
{
    /// <summary>
    /// Simple character controller that controls a non-player character
    /// </summary>
    public class NPCController : IMNoncontinousInputCharacterController
    {
        [SerializeField] CharacterInteraction interactionWithPlayer;

        public void OnDetectedInteractable(CharacterInteractable interactable)
        {
            SetInteraction(interactable);
        }

        public void ClearInteractable() 
        {
            SetInteraction(null);
        }

        protected override void InteractionSelectionLoop()
        {
            // Don't do anything here yet
        }
    }
}
