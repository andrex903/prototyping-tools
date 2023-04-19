using UnityEngine;
using UnityEngine.AI;

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

        private NavMeshHit hit;

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


            inputVector = new Vector3(Input.GetAxis(HORIZONTAL), Input.GetAxis(VERTICAL));
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