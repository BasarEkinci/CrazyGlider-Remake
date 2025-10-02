using System.Collections.Generic;
using System.Linq;
using _GameFolders.Scripts.Data.ScriptableObjects;
using _GameFolders.Scripts.Data.ValueObjects.DataHolders;
using _GameFolders.Scripts.Extensions;
using _GameFolders.Scripts.Items;
using UnityEngine;

namespace _GameFolders.Scripts.Managers
{
    public class ShopManager : MonoSingleton<ShopManager>
    {
        public ShopData ShopData => _shopData;
        public List<PlanePart> PurchasableParts => _purchasableParts;
        
        [SerializeField] private LevelDataSO levelDataSo;
        
        private ShopData _shopData;
        private PlanePartHolder _planePartHolder;
        private List<PlanePart> _purchasableParts;
        private int _currentLevelIndex;
        
        private void Start()
        {
            _currentLevelIndex = GameManager.Instance.CurrentLevelIndex;
            _shopData = levelDataSo.Levels[_currentLevelIndex].shopData;
            _planePartHolder = levelDataSo.Levels[_currentLevelIndex].planePartHolder;
            InitializePurchasableParts();
        }

        private void InitializePurchasableParts()
        {
            _purchasableParts = _planePartHolder.PlaneParts.Where(part => part).ToList();
        }
    }
}