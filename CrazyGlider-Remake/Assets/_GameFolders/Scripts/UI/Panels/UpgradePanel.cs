using _GameFolders.Scripts.Data.ValueObjects.DataHolders;
using _GameFolders.Scripts.Managers;
using _GameFolders.Scripts.UI.ShopButtons;
using UnityEngine;

namespace _GameFolders.Scripts.UI.Panels
{
    public class UpgradePanel : MonoBehaviour
    {
        [Header("Upgradable Item Buttons")]
        [SerializeField] private UpgradableItemButton speedUpgrade;
        [SerializeField] private UpgradableItemButton cliffUpgrade;
        [SerializeField] private UpgradableItemButton fuelUpgrade;

        [Header("Purchasable Item Buttons")] 
        [SerializeField] private PurchasableItemButton motorButton;
        [SerializeField] private PurchasableItemButton wingsButton;
        [SerializeField] private PurchasableItemButton coverButton;
        [SerializeField] private PurchasableItemButton wheelsButton;
        [SerializeField] private PurchasableItemButton tailButton;
        
        private UpgradableItemData _speedData;
        private UpgradableItemData _cliffData;
        private UpgradableItemData _fuelData;

        private void Start()
        {
            GetUpgradableItemData();
            InitializeUpgradableItemButtons();
        }

        private void InitializeUpgradableItemButtons()
        {
            speedUpgrade.Initialize(_speedData.priceList[0], _speedData.maxLevel, _speedData.priceList, _speedData.requiredPartsToUnlock);
            cliffUpgrade.Initialize(_cliffData.priceList[0], _cliffData.maxLevel, _cliffData.priceList, _cliffData.requiredPartsToUnlock);
            fuelUpgrade.Initialize(_fuelData.priceList[0], _fuelData.maxLevel, _fuelData.priceList, _fuelData.requiredPartsToUnlock);
        }
        
        private void GetUpgradableItemData()
        {
            _speedData = ShopManager.Instance.ShopData.speed;
            _cliffData = ShopManager.Instance.ShopData.cliff;
            _fuelData = ShopManager.Instance.ShopData.fuel;
        }
    }
}