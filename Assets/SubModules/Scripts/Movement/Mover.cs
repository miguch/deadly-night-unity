using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace com.ImmersiveMedia.Movement
{
    /// <summary>
    /// A class that provides motion to an object
    /// </summary>
    public abstract class Mover : MonoBehaviour
    {
        [SerializeField] protected float movementSpeed; // the speed at which the object should move
        [SerializeField] private UnityEvent arrivedAtDestination; // Event to invoke when the object arrives at its destination

        protected Vector3 destination; // The object's destination

        private IEnumerator movementRoutine; // The coroutine that provides the motion
        bool movementRoutineActive = false; // Whether the object is moving
        private bool stopped = true; // Whther the object is stopped

        public bool MovementRoutineActive { get => movementRoutineActive; set => movementRoutineActive = value; }
        public UnityEvent ArrivedAtDestination { get => arrivedAtDestination; set => arrivedAtDestination = value; }


        protected virtual void Awake()
        {
            destination = transform.position;
            if (ArrivedAtDestination == null)
            {
                ArrivedAtDestination = new UnityEvent();
            }
        }
           
        /// <summary>
        /// Moves the object each frame
        /// </summary>
        /// <returns></returns>
        protected IEnumerator MovementRoutine()
        {
            stopped = false; // Once we have started moving we are no longer stopped
            MovementRoutineActive = true; // Movement routine is active when we start moving

            // Move while we have not arrived at out destination and we are not stopped
            while (!Arrived() && !stopped)
            {
                MovementLoop();
                yield return new WaitForEndOfFrame();
            }

            // When the while is excited we have arrived
            ArrivedAtDestination?.Invoke();
            MovementRoutineActive = false;
        }

        /// <summary>
        /// Starts the movement routine that takes the object to a destination
        /// </summary>
        /// <param name="dest">The destination of the object</param>
        /// <param name="stopDistance">The stopping distance from that destination where the object should stop</param>
        public void StartMovementRoutine(Vector3 dest, float stopDistance)
        {
            // if the mover is already in transit stop the movement routine 
            if (MovementRoutineActive)
            {
                StopCoroutine(movementRoutine);
            }

            // Set the object's destination and start moving
            SetDestination(dest, stopDistance);
            movementRoutine = MovementRoutine();
            StartCoroutine(movementRoutine);
        }

        protected virtual void SetDestination(Vector3 dest, float stopDistance)
        {
            destination = dest;
        }

        public virtual void Stop()
        {
            stopped = true;
        }

        protected abstract void MovementLoop();

        protected abstract bool Arrived();
    }
}
