using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Input;
using UnityEngine;
using static _GameFolders.Scripts.Managers.GameEventManager;

namespace _GameFolders.Scripts.Controllers.Plane
{
    public class PlaneMovementController : MonoBehaviour
    {
        public float CurrentSpeed => moveSpeed * _inputVector.magnitude;
        
        [Header("Flight Settings")]
        [SerializeField] private float maxPitchUp = 25f;
        [SerializeField] private float maxPitchDown = 45f;
        [SerializeField] private float pitchSpeed = 90f;
        [SerializeField] private float autoPitchDown = 10f;
        [SerializeField] private float autoPitchSpeed = 30f;
        [SerializeField, Range(0f, 1f)] private float pitchDeadZone = 0.05f;
        
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
            //OnLevelComplete += () => _playerState = PlayerState.Landed; //move it to the landing controller
        }

        private void OnDisable()
        {
            OnPlayerStateChanged -= HandlePlayerState;
            OnLevelStart -= () => _playerState = PlayerState.Accelerating;
            //OnLevelComplete -= () => _playerState = PlayerState.Landed;
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
                    HandleFlight(_inputVector);
                    break;
                case PlayerState.Landed:
                    break;
                case PlayerState.Sink:
                    _rb.linearVelocity = Vector3.zero;
                    _rb.linearVelocity = Vector3.down;
                    break;
            }
        }

        private void HandleMovement(Vector3 moveVector)
        {
            Vector3 origin = transform.position + Vector3.up * 2f;

            bool hasHit = Physics.SphereCast(origin, sphereRadius, Vector3.down, out RaycastHit hit, rayLength, groundMask);
            float slopeAngle = hasHit ? Vector3.Angle(hit.normal, Vector3.up) : 180f;
            float xInput = Mathf.Max(moveVector.x, 0f); // Plane can only move forward on the ground
            Vector3 inputDir = new Vector3(xInput, 0f, moveVector.z);

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
        
        private void HandleFlight(Vector3 moveVector)
        {
            float v = moveVector.x;
            float absV = Mathf.Abs(v);
            
            float targetPitch =
                absV < pitchDeadZone
                    ? autoPitchDown
                    : Mathf.Lerp(-maxPitchUp, maxPitchDown, (v + 1f) * 0.5f);
            
            Vector3 euler = _rb.rotation.eulerAngles;
            float currentPitch = NormalizeSignedAngle(euler.x);

            float speed = absV < pitchDeadZone ? autoPitchSpeed : pitchSpeed;
            float newPitch = Mathf.MoveTowards(currentPitch, targetPitch, speed * Time.fixedDeltaTime);
            newPitch = Mathf.Clamp(newPitch, -maxPitchUp, maxPitchDown);
            
            Quaternion targetRot = Quaternion.Euler(newPitch, euler.y, 0f);
            Quaternion next = Quaternion.RotateTowards(_rb.rotation, targetRot, speed * Time.fixedDeltaTime);
            _rb.MoveRotation(next);
        }

        private static float NormalizeSignedAngle(float angle)
        {
            angle %= 360f;
            return angle > 180f ? angle - 360f : angle;
        }

        private void Launch()
        {
            _rb.linearVelocity = transform.forward * moveSpeed;
        }
    }
}