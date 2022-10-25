using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ImmersiveMedia.CharacterControl
{
    /// <summary>
    /// Controls a first person character
    /// </summary>
    public class FirstPersonCharacterController : IMContinousInputCharacterController
    {
        // The camera that this controller is using
        [SerializeField] private Camera characterCamera;

        // Allows for configurable mouse sensitivity
        [SerializeField] private float mouseSensitivity;
        // Controls how far the user can look up or down
        [SerializeField] private float xAxisClampAngle = 80.0f;

        // Stores the current rotation around the y and x axes
        private float rotationAboutY = 0.0f; 
        private float rotationAboutX = 0.0f; 

        // Variables to store axis input
        private float xInputAxis = 0;
        private float yInputAxis = 0;

        // Variables for storing mouse coordinates
        float mouseX;
        float mouseY;

        protected override void Start()
        {
            base.Start();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
            rotationAboutY = transform.localRotation.eulerAngles.y;
            rotationAboutX = transform.localRotation.eulerAngles.x;
        }

        protected override void Update()
        {
            base.Update();
            CalculateMouseRotationValuesFromInput();
            ApplyMouseRotation();
        }

        private void ApplyMouseRotation()
        {
            // First apply rotation to the camera so it can move independently of the body
            characterCamera.transform.rotation = Quaternion.Euler(rotationAboutX, rotationAboutY, 0.0f);
            // Second apply only the y axis rotation to the body of the controller
            rb.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotationAboutY, transform.rotation.eulerAngles.z);
        }

        private void CalculateMouseRotationValuesFromInput()
        {
            // Get axis input for the mouse
            mouseX = Input.GetAxis("Mouse X");
            mouseX += Input.GetAxis("Joystick X");
            mouseY = -Input.GetAxis("Mouse Y");
            mouseY += Input.GetAxis("Joystick Y");

            // Calculate the rotation about the y and x axes resulting from mouse motion
            rotationAboutY += mouseX * mouseSensitivity * Time.deltaTime;
            rotationAboutX += mouseY * mouseSensitivity * Time.deltaTime;

            // Clamp rotation about x so the user can't look too high or too low
            rotationAboutX = Mathf.Clamp(rotationAboutX, -xAxisClampAngle, xAxisClampAngle);
        }

        /// <summary>
        /// Moves the character using the rigidbody 
        /// component in the direction it is facing
        /// </summary>
        protected override void MoveCharacter()
        {
            // Calculate the move speeds in x and z
            float xMoveSpeed = xInputAxis * this.maxSpeed * Time.fixedDeltaTime;
            float zMoveSpeed = yInputAxis * this.maxSpeed * Time.fixedDeltaTime;

            // Create a new direction vector by scaling motion in x and z and adding the direction vecors together
            Vector3 newDirectionVelocity = (transform.forward * zMoveSpeed) + (transform.right * xMoveSpeed);
            rb.velocity = new Vector3(newDirectionVelocity.x, rb.velocity.y, newDirectionVelocity.z);
        }

        /// <summary>
        /// Get x and y axis input
        /// </summary>
        protected override void GetUserInput()
        {
            base.GetUserInput();
            xInputAxis = Input.GetAxis("Horizontal");
            yInputAxis = Input.GetAxis("Vertical");
        }


    }
}
