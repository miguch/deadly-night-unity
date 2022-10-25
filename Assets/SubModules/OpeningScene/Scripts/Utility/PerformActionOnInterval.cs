using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace com.ImmersiveMedia.Utility
{
    /// <summary>
    /// Perfoms an action or set of actions on an interval by invoking a configurabe event
    /// </summary>
    public class PerformActionOnInterval : MonoBehaviour
    {
        [SerializeField] float intervalTimeInSeconds; // The interval at which to invoke the event
        [SerializeField] UnityEvent intervalEvent;
        [SerializeField] bool active = false; // Whether the action is active and the event should be invoked

        public bool Active { get => active; set => active = value; }

        private void Awake()
        {
            StartCoroutine(IntervalLoop());
        }

        /// <summary>
        /// Invokes an event on an interval if this action is active
        /// </summary>
        private IEnumerator IntervalLoop()
        {
            while (true)
            {

                yield return new WaitForSeconds(intervalTimeInSeconds);
                if (Active)
                {
                    intervalEvent?.Invoke();
                }
            }
        }
    }
}
