using _GameFolders.Scripts.Enums;
using TMPro;
using UnityEngine;
using static _GameFolders.Scripts.Managers.GameEventManager;

namespace _GameFolders.Scripts.Managers
{
    public class StatsManager : MonoBehaviour
    {
        [SerializeField] private float seaLevel = -33f;
        [Header("UI References")] 
        [SerializeField] private TMP_Text targetDistance;
        [SerializeField] private TMP_Text distanceText;
        [SerializeField] private TMP_Text currentHeightText;
        [SerializeField] private TMP_Text maxHeightText;
        
        [Header("Player Reference")]
        [SerializeField] private Transform playerTransform;

        private Vector3 _initialPosition;
        private float _maxHeight = 0f;
        private bool _isFlying;
        
        private void OnEnable()
        {
            OnPlayerStateChanged += HandlePlayerStateChanged;
        }

        private void Update()
        {
            if (playerTransform != null && _isFlying)
            {
                float distance = Vector3.Distance(_initialPosition, playerTransform.position);
                distanceText.SetText($" {distance:F}");
                float currentHeight = playerTransform.position.y - seaLevel;
                currentHeight = Mathf.Max(currentHeight, 0f);
                currentHeightText.SetText($" {currentHeight:F}");
                if (currentHeight > _maxHeight)
                {
                    _maxHeight = currentHeight;
                }
                maxHeightText.SetText($" {Mathf.Max(currentHeight, _maxHeight):F}");
            }
        }
        private void OnDisable()
        {
            OnPlayerStateChanged -= HandlePlayerStateChanged;
        }
        private void HandlePlayerStateChanged(PlayerState state)
        {
            if (state == PlayerState.Flying)
            {
                _initialPosition = playerTransform.position;
                _isFlying = true;
            }
            else if (state == PlayerState.Sink)
            {
                _isFlying = false;
            }
        }
    }
}