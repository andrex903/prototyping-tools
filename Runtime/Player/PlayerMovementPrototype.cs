using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Redeev.PrototypingTools
{
    public class PlayerMovementPrototype : MonoBehaviour
    {
        public bool IsInitialized { get; private set; } = false;

        [SerializeField] float speed = 10f;

        private Transform cameraTransform;
        private CharacterController controller;

        private Vector3 inputVector;
        private Vector3 movementVector;
        private float currentVelocity;

        private static readonly string HORIZONTAL = "Horizontal";
        private static readonly string VERTICAL = "Vertical";

        private bool isMoving = false;

        private bool inputMovement = false;
        public bool InputMovement => inputMovement;

        public event System.Action Initialized;

        private PlayerAnimationsPrototype playerAnimations;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (IsInitialized) return;

            controller = GetComponent<CharacterController>();
            playerAnimations = GetComponent<PlayerAnimationsPrototype>();

            IsInitialized = true;
            Initialized?.Invoke();
        }

        private void Start()
        {
            cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            if (!IsInitialized) return;

#if ENABLE_INPUT_SYSTEM
            inputVector = new Vector3();
            if (Keyboard.current.wKey.isPressed)
            {
                inputVector += new Vector3(0, 1, 0);
            }
            else if (Keyboard.current.sKey.isPressed)
            {
                inputVector += new Vector3(0, -1, 0);
            }

            if (Keyboard.current.aKey.isPressed)
            {
                inputVector += new Vector3(-1, 0, 0);
            }
            else if (Keyboard.current.dKey.isPressed)
            {
                inputVector += new Vector3(1, 0, 0);
            }
#else
            inputVector = new Vector3(Input.GetAxis(HORIZONTAL), Input.GetAxis(VERTICAL));
#endif
            inputMovement = (inputVector.magnitude >= 0.2f);

            playerAnimations.SetMovement(inputMovement);

            if (inputMovement)
            {
                if (!isMoving) OnStartMoving();
                movementVector = cameraTransform.TransformDirection(inputVector);
                movementVector.y = 0f;
                movementVector.Normalize();
                currentVelocity = speed;

                transform.forward = movementVector;
                playerAnimations.SetSpeed(currentVelocity);
                movementVector += Physics.gravity;
                movementVector *= Time.deltaTime * currentVelocity;
                controller.Move(movementVector);
            }
            else
            {
                if (isMoving) OnStopMoving();
            }
        }

        public bool IsMoving()
        {
            return inputMovement;
        }

        public void Move(Vector3 delta)
        {
            controller.Move(delta);
        }

        public void OnStartMoving()
        {
            if (isMoving) return;
            isMoving = true;

            playerAnimations.SetMovement(false);

        }

        public void OnStopMoving()
        {
            if (!isMoving) return;
            isMoving = false;

            playerAnimations.SetMovement(false);
        }
    }
}