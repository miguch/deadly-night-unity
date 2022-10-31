using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace com.ImmersiveMedia.Utility
{
    /// <summary>
    /// Provides an event that is invoked when a key is pressed
    /// </summary>
    public class InputResponder : MonoBehaviour
    {
        [SerializeField] KeyCode key;
        [SerializeField] UnityEvent onKeyPressed;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(key))
            {
                onKeyPressed?.Invoke();
            }
        }
    }
}
