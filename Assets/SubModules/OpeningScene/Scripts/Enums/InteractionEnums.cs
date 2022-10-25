using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ImmersiveMedia.Enums
{
    public static class InteractionEnums
    {
        public enum CharacterInteraction
        {
            NAVIGATE_TO = 0,
            ATTACK = 1,
            TALK = 2,
            LOOT = 3,
            NOT_INTERACTABLE = 4
        }

        public enum DamageableSet
        {
            HUMAN,
            UNDEAD,
            PLAYER,
            BUILDING
        }
    }
}
