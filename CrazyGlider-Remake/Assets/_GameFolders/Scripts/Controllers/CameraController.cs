using System;
using _GameFolders.Scripts.Enums;
using UnityEngine;
using static _GameFolders.Scripts.Managers.GameEventManager;

namespace _GameFolders.Scripts.Controllers
{
    public class CameraController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform target;
        
        [Header("Settings")]
        [SerializeField] private Vector3 baseOffset;
        [SerializeField] private Vector3 cliffOffset;
        [SerializeField] private Vector3 flyOffset;
        
        [Min(0f)] [SerializeField] private float smoothTime = 0.15f;
        
        [Space]
        [Header("Per-Axis Smoothing")]
        [SerializeField] private bool usePerAxisSmoothing;
        [SerializeField] private bool smoothX = true;
        [SerializeField] private bool smoothY = true;
        [SerializeField] private bool smoothZ = true;
        [Min(0f)] [SerializeField] private float smoothTimeX = 0.15f;
        [Min(0f)] [SerializeField] private float smoothTimeY = 0.15f;
        [Min(0f)] [SerializeField] private float smoothTimeZ = 0.15f;

        private PlayerState _playerState;
        private Vector3 _velocity;
        private Vector3 _followOffset;

        private float _velX;
        private float _velY;
        private float _velZ;
        private bool _canFollow;
        
        private void OnEnable()
        {
            OnPlayerStateChanged += HandleCameraOffset;
        }

        private void OnDisable()
        {
            OnPlayerStateChanged -= HandleCameraOffset;
        }

        private void Start()
        {
            _canFollow = true;
            if (target != null)
            {
                _followOffset = transform.position - target.position;
            }
        }

        private void FixedUpdate()
        {
            if (target == null || !_canFollow) return;
            Vector3 desired = target.position + _followOffset;

            if (!usePerAxisSmoothing)
            {
                transform.position = Vector3.SmoothDamp(transform.position, desired, ref _velocity, smoothTime);
                return;
            }
            
            Vector3 current = transform.position;

            float newX = smoothX ? Mathf.SmoothDamp(current.x, desired.x, ref _velX, Mathf.Max(0f, smoothTimeX)) : desired.x;
            if (!smoothX) _velX = 0f;

            float newY = smoothY ? Mathf.SmoothDamp(current.y, desired.y, ref _velY, Mathf.Max(0f, smoothTimeY)) : desired.y;
            if (!smoothY) _velY = 0f;

            float newZ = smoothZ ? Mathf.SmoothDamp(current.z, desired.z, ref _velZ, Mathf.Max(0f, smoothTimeZ)) : desired.z;
            if (!smoothZ) _velZ = 0f;

            transform.position = new Vector3(newX, newY, newZ);
        }

        private void HandleCameraOffset(PlayerState state)
        {
            _playerState = state;
            switch (_playerState)
            {
                case PlayerState.Accelerating:
                    _followOffset = baseOffset;
                    _canFollow = true;
                    break;
                case PlayerState.Rolling:
                    _followOffset = cliffOffset;
                    break;
                case PlayerState.Flying:
                    _followOffset = flyOffset;
                    break;
                case PlayerState.Landed:
                    _followOffset = baseOffset;
                    break;
                case PlayerState.Sink:
                    _canFollow = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}