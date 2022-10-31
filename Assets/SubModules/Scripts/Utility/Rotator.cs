using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ImmersiveMedia.Utility
{
    /// <summary>
    /// Generic class that rotates an object by a specified amount
    /// </summary>
    public class Rotator : MonoBehaviour
    {
        public void RotateByDegrees(float degrees)
        {
            transform.Rotate(new Vector3(0, degrees, 0));
        }
    }
}
