using UnityEngine;

namespace DevRowInteractive.CameraController
{
    public class SimpleInput : MonoBehaviour
    {
        private CameraController cameraController;

        private void Start()
        {
            cameraController = FindObjectOfType<CameraController>();
        }

        private void LateUpdate()
        {
            cameraController.MousePosition = Input.mousePosition;
            cameraController.ScrollWheel = Input.GetAxis("Mouse ScrollWheel");
        }
    }
}
