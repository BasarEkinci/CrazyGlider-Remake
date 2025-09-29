using _GameFolders.Scripts.Data.ScriptableObjects;
using _GameFolders.Scripts.Data.ValueObjects.DataHolders;
using _GameFolders.Scripts.Managers;
using _GameFolders.Scripts.UI.PurchaseButtons;
using UnityEngine;

namespace _GameFolders.Scripts.UI.Panels
{
    public class UpgradePanel : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private LevelDataSO levelData;

        [Header("Upgradable Item Buttons")]
        [SerializeField] private UpgradableItemButton speedUpgrade;
        [SerializeField] private UpgradableItemButton cliffUpgrade;
        [SerializeField] private UpgradableItemButton fuelUpgrade;
        
        private int _currentLevel;

        private void Start()
        {
            _currentLevel = GameManager.Instance.CurrentLevelIndex;
            SetUpgradableItemButtonValues();
        }

        private void SetUpgradableItemButtonValues()
        {
            UpgradableItemData item = levelData.Levels[_currentLevel].upgradeData.upgradableItem;
            speedUpgrade.InitializeButtonValues(item.speedUpgradeData.speedInitialPrice,item.speedUpgradeData.speedMaxLevel);
            cliffUpgrade.InitializeButtonValues(item.cliffUpgradeData.cliffInitialPrice,item.cliffUpgradeData.cliffMaxLevel);
            fuelUpgrade.InitializeButtonValues(item.fuelUpgradeData.fuelInitialPrice,
                item.fuelUpgradeData.fuelMaxLevel);
        }
    }
}