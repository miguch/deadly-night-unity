using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace com.ImmersiveMedia.Movement
{
    /// <summary>
    /// Mover that provides lerp motion to an object
    /// </summary>
    public class LerpMover : Mover
    {
        protected float startTime; // Start time of the lerp
        protected float journeyLength; // The journey length from the mover to its desitnation
        protected Vector3 startPosition = Vector3.zero;  // The start destination of the motion
        protected Vector3 endPosition = Vector3.zero; // The end destination of the motion
        protected float stoppingDistance; // the distance from the object to stop at

        /// <summary>
        /// Determines whether the lerp mover has arrived at its destination
        /// </summary>
        /// <returns>True if the lerpmovr has arrived. False if not</returns>
        protected override bool Arrived()
        {
            return Vector3.Distance(transform.position, destination) <= stoppingDistance;
        }

        /// <summary>
        /// Provides lerp motion to this mover
        /// </summary>
        protected override void MovementLoop()
        {
            // Set lerping data
            float distCovered = (Time.time - startTime) * movementSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);

            // Look at the destination
            transform.LookAt(new Vector3(destination.x, transform.position.y, destination.z));
        }

        /// <summary>
        /// Sets the destination of the lerp mover
        /// </summary>
        /// <param name="dest">The destination of the mover</param>
        /// <param name="stopDistance">The stop distance</param>
        protected override void SetDestination(Vector3 dest, float stopDistance)
        {
            base.SetDestination(dest, stopDistance);

            // Set all the interpolation information
            startTime = Time.time;
            startPosition = transform.position;
            endPosition = new Vector3(destination.x, transform.position.y, destination.z);
            journeyLength = Vector3.Distance(startPosition, endPosition);
            stoppingDistance = stopDistance;
        }
    }
}
