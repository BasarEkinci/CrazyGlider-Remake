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
        [SerializeField] private float smoothTime = 0.15f; 

        private Vector3 _velocity;
        private PlayerState _playerState;
        private Vector3 _followOffset;
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
            transform.position = Vector3.SmoothDamp(transform.position, desired, ref _velocity, smoothTime);
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