using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace com.ImmersiveMedia.Utility
{
    public class LinearEventSequenceQueue : MonoBehaviour
    {
        [SerializeField] bool dequeueOnStart = false;
        [SerializeField] List<UnityEvent> linearEvents;

        Queue<UnityEvent> eventQueue;

        private void Start()
        {
            eventQueue = new Queue<UnityEvent>(linearEvents);

            if (dequeueOnStart)
            {
                DequeueNext();
            }
        }

        public void DequeueNext()
        {
            if (eventQueue.Count > 0)
            {
                UnityEvent nextEvent = eventQueue.Dequeue();
                if (nextEvent != null)
                {
                    nextEvent.Invoke();

                }
            }

        }
    }
}
