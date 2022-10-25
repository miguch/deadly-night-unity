using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace com.ImmersiveMedia.Movement
{
    /// <summary>
    /// Mover that provides NavMesh motion to an object
    /// </summary>
    public class NavMeshMover : Mover
    {
        [SerializeField] private NavMeshAgent agent; // The navmesh agent this mover will use

        /// <summary>
        /// Determines if the NavMeshAgent mover has arrived at its destination
        /// </summary>
        /// <returns>true if the mover has arrived. False if not</returns>
        protected override bool Arrived()
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    transform.LookAt(new Vector3(agent.destination.x, transform.position.y, agent.destination.z));
                    return true;
                }

            }
            return false;
        }

        protected override void Awake()
        {
            base.Awake();
            agent.speed = movementSpeed;
        }

        protected override void MovementLoop()
        {
            // Do nothing in here for now
        }

        /// <summary>
        /// Set the destination of the NavMesh mover
        /// </summary>
        /// <param name="dest">The destination of the mover</param>
        /// <param name="stopDistance">The stopping distance of the mover</param>
        protected override void SetDestination(Vector3 dest, float stopDistance)
        {
            base.SetDestination(dest, stopDistance);
            agent.ResetPath();
            agent.SetDestination(dest);
            agent.stoppingDistance = stopDistance;
        }

        /// <summary>
        /// Stops the NavMesh mover
        /// </summary>
        public override void Stop()
        {
            base.Stop();
            agent.isStopped = true;
        }
    }
}
