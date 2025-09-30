using _GameFolders.Scripts.Data.ScriptableObjects;
using _GameFolders.Scripts.Data.ValueObjects.DataHolders;
using _GameFolders.Scripts.Extensions;
using _GameFolders.Scripts.Items;
using UnityEngine;

namespace _GameFolders.Scripts.Managers
{
    public class ShopManager : MonoSingleton<ShopManager>
    {
        public HangarItem CurrentHangarItem => _currentHangarItem;
        public ShopData ShopData => _shopData;
        
        [SerializeField] private LevelDataSO levelDataSo;
        
        private HangarItem _currentHangarItem;
        private ShopData _shopData;
        private int _currentLevelIndex;
        
        private void Start()
        {
            _currentLevelIndex = GameManager.Instance.CurrentLevelIndex;
            _shopData = levelDataSo.Levels[_currentLevelIndex].shopData;
            _currentHangarItem = levelDataSo.Levels[_currentLevelIndex].shopData.hangarItem;
        }
    }
}