using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple configurable camera follow script
/// that maintains an offset to an object. 
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform followTransform; // The transform the camera could follow

    // Variables used to define what dimensions to maintain a camera offset in.
    [SerializeField] private bool followX;
    [SerializeField] private bool followY;
    [SerializeField] private bool followZ;

    private Vector3 originalOffset; // The offset from the object when the scene starts

    private void Awake()
    {
        originalOffset = transform.position - followTransform.position;
    }

    void LateUpdate()
    {
        Vector3 offsetPosition = followTransform.position + originalOffset;

        float x = followX ? offsetPosition.x : transform.position.x;
        float y = followY ? offsetPosition.y : transform.position.y;
        float z = followZ ? offsetPosition.z : transform.position.z;

        transform.position = new Vector3(x,y,z);
    }
}
