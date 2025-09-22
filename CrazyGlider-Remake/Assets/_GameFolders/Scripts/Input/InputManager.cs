using System;
using _GameFolders.Scripts.Extensions;
using UnityEngine;

namespace _GameFolders.Scripts.Input
{
    public class InputManager : MonoSingleton<InputManager>
    {
        private InputActions _inputActions;
        
        protected override void Awake()
        {
            base.Awake();
            _inputActions = new InputActions();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }
        
        private void OnDisable()
        {
            _inputActions.Disable();
        }

        public bool IsFlyButtonPressed()
        {
            return _inputActions.Player.Fly.IsPressed();
        }
        
        public Vector2 GetMovementVector()
        {
            return _inputActions.Player.Move.ReadValue<Vector2>();
        }
    }
}
