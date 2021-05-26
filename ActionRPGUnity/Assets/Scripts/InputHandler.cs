/*
* Made by: Tristan Garzon
* 
* Script Summary:
*
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionRPG
{
    public class InputHandler : MonoBehaviour
    {
        #region Variables
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        PlayerControls inputActions;

        Vector2 movementInput;
        Vector2 cameraInput;



        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            //Sets the InputActions
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                //Sets the Movement Input
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                //Sets the Camera Input
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            }
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            MoveInput(delta);
        }


        private void MoveInput(float delta)
        {
            //Setting the variables to the Input Actions
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseX = cameraInput.y;
        }

        #endregion
    }
}