using UnityEngine;

namespace _GameFolders.Scripts.Controllers
{
    public class CameraController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform target;
        
        [Header("Settings")]
        [SerializeField] private Vector3 offset;       
        [SerializeField] private float smoothTime = 0.15f; 

        private Vector3 _velocity;     

        private void Start()
        {
            if (target != null)
            {
                offset = transform.position - target.position;
            }
        }

        private void FixedUpdate()
        {
            if (target == null) return;
            Vector3 desired = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, desired, ref _velocity, smoothTime);
        }
    }
}