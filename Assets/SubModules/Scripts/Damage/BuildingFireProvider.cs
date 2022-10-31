using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ImmersiveMedia.Damage
{
    /// <summary>
    /// Allows buildings to respond to damage by lighting themselves on fire
    /// </summary>
    public class BuildingFireProvider : MonoBehaviour
    {
        [SerializeField] private float damagePercentageChange; // The percentage change necessary to enable another particle system
        private ParticleSystem[] particleSystems;
        private int systemIndex = 0; // the index of the latest activated fire particle system
        private float lastPercentage = 100;


        // Start is called before the first frame update
        void Start()
        {
            // Grab all the particle systems that are children of this object and place them in an array
            particleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();
        }

        /// <summary>
        /// Activates a new particle system when a certain damage percentage change has been reached
        /// </summary>
        /// <param name="percentage">The percentage of health the object has left</param>
        public void OnHealthPercentageChange(float percentage)
        {
            if (lastPercentage - percentage >= damagePercentageChange)
            {
                if (systemIndex < particleSystems.Length)
                {
                    particleSystems[systemIndex].Play();
                    Light light = particleSystems[systemIndex].GetComponent<Light>();
                    if(light != null)
                    {
                        light.enabled = true;
                    }
                    
                    systemIndex++;
                    lastPercentage = percentage;
                }
            }

        }
    }
}
