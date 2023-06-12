using UnityEngine;

namespace DevRowInteractive.CameraController
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float zoomSpeed = 5f;
        [SerializeField] private float minZoom = 1f;
        [SerializeField] private float maxZoom = 6f;
        [SerializeField] private float borderThickness = 20f;

        private Camera cam;
        private Vector3 moveDirection;

        [HideInInspector] public Vector3 MousePosition;
        [HideInInspector] public float ScrollWheel;

        private void Start()
        {
            cam = GetComponentInChildren<Camera>();
        }

        private void Update()
        {
            if(!MACROS_CAMERACONTROLLER.CAMERACONTROLLER_ENABLED)
                return;
            
            HandleMovement();
            HandleZoom();
        }

        private void HandleMovement()
        {
            // Reset move direction
            moveDirection = Vector3.zero;

            // Check if mouse is near the screen borders
            if (MousePosition.x < borderThickness)
                moveDirection += transform.forward;  // Camera's forward is pointing to the right due to the isometric rotation
            else if (MousePosition.x > Screen.width - borderThickness)
                moveDirection += -transform.forward;  // Camera's backward is pointing to the left due to the isometric rotation

            if (MousePosition.y < borderThickness)
                moveDirection += -transform.up;  
            else if (MousePosition.y > Screen.height - borderThickness)
                moveDirection += transform.up; 

            // Normalize move direction
            moveDirection.Normalize();

            // Move the camera
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }


        private void HandleZoom()
        {
            float zoomAmount = ScrollWheel * zoomSpeed;

            // Update the orthographic size with zoomAmount
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - zoomAmount, minZoom, maxZoom);
        }
    }
}