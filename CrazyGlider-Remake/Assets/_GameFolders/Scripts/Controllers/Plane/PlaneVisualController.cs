using System.Collections.Generic;
using UnityEngine;

namespace _GameFolders.Scripts.Controllers.Plane
{
    public class PlaneVisualController : MonoBehaviour
    {
        [SerializeField] private List<Transform> wheels;
        
        private PlaneMovementController _movementController;
        
        private void Awake()
        {
            _movementController = GetComponent<PlaneMovementController>();
        }

        private void FixedUpdate()
        {
            foreach (var wheel in wheels)
            {
                wheel.Rotate(Vector3.left * (_movementController.CurrentSpeed * Time.fixedDeltaTime * 90f));
            }
        }
    }
}