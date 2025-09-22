using System;
using _GameFolders.Scripts.Input;
using UnityEngine;

namespace _GameFolders.Scripts.Controllers
{
    public enum PlayerState
    {
        Accelerating,
        CliffMovement,
        Flying,
        Idle
    }
    public class PlayerMovementController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float maxSpeed = 10f;

        private Vector2 _inputVector;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _inputVector = InputManager.Instance.GetMovementVector();
        }

        private void FixedUpdate()
        {
            Vector3 movement = new Vector3(_inputVector.x, 0, _inputVector.y) * maxSpeed;
            _rigidbody.MovePosition(_rigidbody.position + movement * Time.fixedDeltaTime);
        }
    }
}