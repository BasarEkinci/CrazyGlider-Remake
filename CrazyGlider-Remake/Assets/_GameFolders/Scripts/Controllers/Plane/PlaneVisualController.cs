using System.Collections.Generic;
using _GameFolders.Scripts.Data.ScriptableObjects;
using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Items;
using _GameFolders.Scripts.Managers;
using UnityEngine;

namespace _GameFolders.Scripts.Controllers.Plane
{
    public class PlaneVisualController : MonoBehaviour
    {
        [SerializeField] private Transform planePartsHolder;
        [SerializeField] private List<Transform> wheels;
        [SerializeField] private LevelDataSO levelDataSo;
        
        private PlaneMovementController _movementController;
        private List<PlanePart> _currentPlaneParts = new List<PlanePart>();
        
        private void Awake()
        {
            _movementController = GetComponent<PlaneMovementController>();
        }
        private void OnEnable()
        {
            GameEventManager.OnItemPurchased += HandlePlaneParts;
        }
        private void OnDisable()
        {
            GameEventManager.OnItemPurchased -= HandlePlaneParts;
        }
        private void Start()
        {
            GetCurrentPlaneParts();
        }
        private void HandlePlaneParts(ShopItemType itemType)
        {
            var part = _currentPlaneParts.Find(plane => plane.type == itemType);
            part.gameObject.SetActive(true);
        }
        private void GetCurrentPlaneParts()
        {
            PlanePartHolder planePartHolder = Instantiate(levelDataSo.Levels[GameManager.Instance.CurrentLevelIndex].planePartHolder, transform,false);
            _currentPlaneParts = planePartHolder.PlaneParts;
            _currentPlaneParts.ForEach(part => part.gameObject.SetActive(false));
        }
    }
}