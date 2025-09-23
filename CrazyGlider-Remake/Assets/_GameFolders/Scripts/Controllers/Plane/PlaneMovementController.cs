using System;
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
            OnPlayerStateChanged += state => _playerState = state;
            OnLevelStart += () => _playerState = PlayerState.Accelerating;
            OnLevelComplete += () => _playerState = PlayerState.Landed;
        }

        private void OnDisable()
        {
            OnPlayerStateChanged -= state => _playerState = state;
            OnLevelStart -= () => _playerState = PlayerState.Accelerating;
            OnLevelComplete -= () => _playerState = PlayerState.Landed;
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
                    HandleMovement(_inputVector);
                    break;
                case PlayerState.Rolling:
                    HandleMovement(Vector3.right);
                    break;
                case PlayerState.Flying:
                    break;
                case PlayerState.Landed:
                    break;
            }
        }

        private void HandleMovement(Vector3 moveVector)
        {
            if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hit, 5f, groundMask))
            {
                Vector3 targetPos = hit.point + hit.normal * groundOffset;
                Vector3 inputDir = new Vector3(moveVector.x, 0, moveVector.y);
                Vector3 moveDir = Vector3.ProjectOnPlane(inputDir, hit.normal).normalized;

                targetPos += moveDir * (moveSpeed * Time.fixedDeltaTime);

                _rb.MovePosition(Vector3.Lerp(_rb.position, targetPos, 1f));

                if (moveDir.sqrMagnitude > 0.01f)
                {
                    Quaternion targetRot = Quaternion.LookRotation(-moveDir, hit.normal);
                    _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, targetRot, alignSpeed * Time.fixedDeltaTime));
                }
            }
        }
    }
}