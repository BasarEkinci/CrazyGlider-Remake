using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Input;
using UnityEngine;
using static _GameFolders.Scripts.Managers.GameEventManager;

namespace _GameFolders.Scripts.Controllers.Plane
{
    public class PlaneMovementController : MonoBehaviour
    {
        public float CurrentSpeed => moveSpeed * _inputVector.magnitude;

        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float groundOffset = 0.5f;
        [SerializeField] private float alignSpeed = 10f;
        [SerializeField] private LayerMask groundMask;

        [Header("Grounding Settings")]
        [SerializeField] private float rayLength = 15f;
        [SerializeField] private float sphereRadius = 0.5f;
        [SerializeField, Range(0, 89)] private float maxSlopeAngle = 70f;

        private Rigidbody _rb;
        private Vector2 _inputVector;
        private PlayerState _playerState;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.useGravity = false;
        }

        private void OnEnable()
        {
            OnPlayerStateChanged += HandlePlayerState;
            OnLevelStart += () => _playerState = PlayerState.Accelerating; //move it start action
            OnLevelComplete += () => _playerState = PlayerState.Landed; //move it to the landing controller
        }

        private void OnDisable()
        {
            OnPlayerStateChanged -= HandlePlayerState;
            OnLevelStart -= () => _playerState = PlayerState.Accelerating;
            OnLevelComplete -= () => _playerState = PlayerState.Landed;
        }

        private void HandlePlayerState(PlayerState playerState)
        {
            _playerState = playerState; 
            switch (_playerState) 
            { 
                case PlayerState.Accelerating:
                case PlayerState.Rolling: 
                    _rb.useGravity = false;
                    break;
                case PlayerState.Flying:
                    _rb.useGravity = true;
                    Launch();
                    break;
                case PlayerState.Landed:
                        break;
                }
        }
        
        private void Update()
        {
            _inputVector = InputManager.Instance.GetMovementVector();
        }

        private void FixedUpdate()
        {
            switch (_playerState)
            {
                case PlayerState.Accelerating:
                    HandleMovement(new Vector3(_inputVector.x, 0f, _inputVector.y));
                    break;
                case PlayerState.Rolling:
                    HandleMovement(new Vector3(1f, 0f, 0f));
                    break;
                case PlayerState.Flying:
                    break;
                case PlayerState.Landed:
                    break;
                case PlayerState.Sink:
                    _rb.linearVelocity = Vector3.zero;
                    break;
            }
        }

        private void HandleMovement(Vector3 moveVector)
        {
            Vector3 origin = transform.position + Vector3.up * 2f;

            bool hasHit = Physics.SphereCast(origin, sphereRadius, Vector3.down, out RaycastHit hit, rayLength, groundMask);
            float slopeAngle = hasHit ? Vector3.Angle(hit.normal, Vector3.up) : 180f;

            Vector3 inputDir = new Vector3(moveVector.x, 0f, moveVector.z);

            if (hasHit && slopeAngle <= maxSlopeAngle)
            {
                Vector3 moveDir = Vector3.ProjectOnPlane(inputDir, hit.normal).normalized;
                Vector3 desiredPos = _rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime);
                Vector3 planePoint = hit.point + hit.normal * groundOffset;
                float d = Vector3.Dot(hit.normal, desiredPos - planePoint);
                desiredPos -= hit.normal * d;

                _rb.MovePosition(desiredPos);

                if (moveDir.sqrMagnitude > 0.0001f)
                {
                    Quaternion targetRot = Quaternion.LookRotation(moveDir, hit.normal);
                    _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, targetRot, alignSpeed * Time.fixedDeltaTime));
                }
            }
            else
            {
                Vector3 inputN = inputDir.normalized;
                Vector3 delta = inputN * (moveSpeed * Time.fixedDeltaTime);

                _rb.MovePosition(_rb.position + delta);

                if (inputN.sqrMagnitude > 0.0001f)
                {
                    Quaternion targetRot = Quaternion.LookRotation(inputN, Vector3.up);
                    _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, targetRot, alignSpeed * Time.fixedDeltaTime));
                }
            }
        }

        private void Launch()
        {
            _rb.linearVelocity = transform.forward * moveSpeed;
        }
    }
}