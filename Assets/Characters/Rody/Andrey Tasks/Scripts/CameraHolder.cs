using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AndreyTest
{
    public class CameraHolder : MonoBehaviour
    {
        public Transform targetTransform;
        public Transform cameraTransform; //the main camera
        public Transform cameraPivotTransform; //rotation of the camera
        private Transform myTransform;
        private Vector3 cameraTransformPosition;
        private LayerMask ignoreLayers;

        public static CameraHolder singleton;

        public float lookSpeed = 0.1f;
        public float followSpeed = 0.1f;
        public float pivotSpeed = 0.03f;

        private float defaultPosition;
        private float lookAngle;
        private float pivotAngle;
        public float minimumPivot = -35;
        public float maximumPivot = 35;

        private void Awake() //se llama al awake al cargar la instancia del script
        {
            singleton = this;
            myTransform = transform;
            defaultPosition = cameraTransform.localPosition.z;
            ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
        }

        public void FollowTarget(float delta)
        {       //interpolacion entre la posicion del objeto y la posicion de la camara
            Vector3 targetPosition = Vector3.Lerp(myTransform.position, targetTransform.position, delta / followSpeed);
            myTransform.position = targetPosition; //con esto la camara seguira al objeto
        }

        public void HandleCameraRotation(float delta,float mouseXInput, float mouseYInput) 
        {
            lookAngle += (mouseXInput * lookSpeed) / delta;
            pivotAngle -= (mouseYInput * pivotSpeed) / delta;
            pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot); //media del maximo y minimo

            Vector3 rotation = Vector3.zero;
            rotation.y = lookAngle;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            myTransform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = pivotAngle;

            targetRotation = Quaternion.Euler(rotation);
            cameraPivotTransform.localRotation = targetRotation;
       }
    }
}

