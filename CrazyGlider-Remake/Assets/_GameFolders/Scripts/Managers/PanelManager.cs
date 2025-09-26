using System;
using _GameFolders.Scripts.Enums;
using DG.Tweening;
using UnityEngine;
using static _GameFolders.Scripts.Managers.GameEventManager;

namespace _GameFolders.Scripts.Managers
{
    public class PanelManager : MonoBehaviour
    {
        [SerializeField] private GameObject statsPanel;

        private void Start()
        {
            statsPanel.SetActive(false);
        }

        private void OnEnable()
        {
            OnPlayerStateChanged += HandlePlayerStateChanged;
        }
        
        private void OnDisable()
        {
            OnPlayerStateChanged -= HandlePlayerStateChanged;
        }

        private void HandlePlayerStateChanged(PlayerState state)
        {
            if (state == PlayerState.Flying)
            {
                statsPanel.SetActive(true);
                statsPanel.transform.DOScale(Vector3.zero, 0.1f).From().SetEase(Ease.Linear);
            }
        }
    }
}