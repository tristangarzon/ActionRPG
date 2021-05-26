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
    public class PlayerLocomotion : MonoBehaviour
    {
        #region Variables
        Transform cameraObject;
        InputHandler inputHandler;
        Vector3 moveDirection;

        [HideInInspector]
        public Transform myTransform;

        public new Rigidbody rigidbody;
        public GameObject normalCamera; //Free Moving Camera

        //PLAYER STATS//
        [Header("Stats")]
        [SerializeField]
        float moveSpeed = 5f;
        [SerializeField]
        float rotationSpeed = 10f;


        #endregion

        #region Unity Methods

        void Start()
        {
            //Component References
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();

            //Main Camera Reference
            cameraObject = Camera.main.transform;
            //Reference to the transform of the attached gameobject
            myTransform = transform;
        }

        public void Update()
        {
            float delta = Time.deltaTime;

            //Updates the Inputs
            inputHandler.TickInput(delta);

            //Updates the vertical & horizontal moveDirection of the player
            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;
            moveDirection.Normalize();

            //Sets the Speed of the Player
            float speed = moveSpeed;
            moveDirection *= speed;

            //Updates Movement of the Player
            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            rigidbody.velocity = projectedVelocity;

        }

        #region Movement
        Vector3 normalVector;
        Vector3 targetPosition;

        private void HandleRotation(float delta)
        {
            //Sets the Rotation for the Player
            Vector3 targetDir = Vector3.zero;
            float moveOverride = inputHandler.moveAmount;

            //Sets the target direction based off the camera direction and input
            targetDir = cameraObject.forward * inputHandler.vertical;
            targetDir += cameraObject.right * inputHandler.horizontal;

            targetDir.Normalize();
            targetDir.y = 0;

            if(targetDir == Vector3.zero)
            {
                targetDir = myTransform.forward;
            }

            //Sets the Look Rotation of the Camera
            float rs = rotationSpeed;

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);
            //Sets Rotation
            myTransform.rotation = targetRotation;

        }


        #endregion


        #endregion
    }
}
