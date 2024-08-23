using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace CCM
{
    public class CameraZoneSwitcher : MonoBehaviour
    {
        public string triggerTag;
        public CinemachineVirtualCamera primaryCamera;
        public CinemachineVirtualCamera[] virtualCameras; // Renamed variable to match its plural form

        void Start()
        {
            SwitchToCamera(primaryCamera);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(triggerTag))
            {
                CinemachineVirtualCamera targetCamera = other.GetComponentInChildren<CinemachineVirtualCamera>();
                SwitchToCamera(targetCamera);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(triggerTag))
            {
                SwitchToCamera(primaryCamera);
            }
        }

        private void SwitchToCamera(CinemachineVirtualCamera targetCamera)
        {
            foreach (CinemachineVirtualCamera camera in virtualCameras) // Ensure the loop iterates over all cameras
            {
                camera.enabled = (camera == targetCamera); // Fixed the assignment operation
            }
        }
    }
}
