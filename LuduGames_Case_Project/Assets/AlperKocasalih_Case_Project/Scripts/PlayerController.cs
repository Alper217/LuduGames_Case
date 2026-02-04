using UnityEngine;

namespace LuduArts.InteractionSystem.Player
{
    #region Classes

    /// <summary>
    /// Handles basic FPS movement and camera rotation.
    /// Integrated with Unity's CharacterController component.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        #region Fields

        [Header("Movement Settings")]
        [SerializeField] private float m_WalkSpeed = 5f;
        [SerializeField] private float m_Gravity = -9.81f;

        [Header("Look Settings")]
        [SerializeField] private Transform m_CameraTransform;
        [SerializeField] private float m_MouseSensitivity = 2f;
        [SerializeField] private float m_UpwardLookLimit = -90f;
        [SerializeField] private float m_DownwardLookLimit = 90f;

        private CharacterController m_CharacterController;
        private Vector3 m_Velocity;
        private float m_VerticalRotation = 0f;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            m_CharacterController = GetComponent<CharacterController>();
            
            // Lock cursor for FPS experience
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (m_CameraTransform != null)
            {
                Camera cam = m_CameraTransform.GetComponent<Camera>();
                if (cam != null)
                {
                    cam.nearClipPlane = 0.01f;
                }
            }
        }

        private void Update()
        {
            HandleRotation();
            HandleMovement();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes mouse input to rotate the camera and player body.
        /// </summary>
        private void HandleRotation()
        {
            float mouseX = Input.GetAxis("Mouse X") * m_MouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * m_MouseSensitivity;

            // Horizontal rotation (Player Body)
            transform.Rotate(Vector3.up * mouseX);

            // Vertical rotation (Camera)
            m_VerticalRotation -= mouseY;
            m_VerticalRotation = Mathf.Clamp(m_VerticalRotation, m_UpwardLookLimit, m_DownwardLookLimit);
            m_CameraTransform.localRotation = Quaternion.Euler(m_VerticalRotation, 0f, 0f);
        }

        /// <summary>
        /// Processes WASD input and applies gravity.
        /// </summary>
        private void HandleMovement()
        {
            if (m_CharacterController.isGrounded && m_Velocity.y < 0)
            {
                m_Velocity.y = -2f; 
            }

            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            m_CharacterController.Move(move * m_WalkSpeed * Time.deltaTime);

            m_Velocity.y += m_Gravity * Time.deltaTime;
            m_CharacterController.Move(m_Velocity * Time.deltaTime);
        }

        #endregion
    }

    #endregion
}