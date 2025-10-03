using System.Collections;
using System.Threading.Tasks;
using _GameFolders.Scripts.Data.ValueObjects.DataHolders;
using _GameFolders.Scripts.Enums;
using _GameFolders.Scripts.Managers;
using _GameFolders.Scripts.UI.ShopButtons;
using Cysharp.Threading.Tasks;
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
        
        private PurchasableItemData _motorData;
        private PurchasableItemData _wingsData;
        private PurchasableItemData _coverData;
        private PurchasableItemData _wheelsData;
        private PurchasableItemData _tailData;

        private IEnumerator Start()
        {
            yield return null;
            GetUpgradableItemData();
            GetPurchasableItemData();
            InitializeUpgradableItemButtons();
            InitializePurchasableParts();
        }

        private void InitializeUpgradableItemButtons()
        {
            speedUpgrade.Initialize(_speedData.priceList[0], _speedData.maxLevel, _speedData.priceList,_speedData.requiredPartsToUnlock);
            cliffUpgrade.Initialize(_cliffData.priceList[0], _cliffData.maxLevel, _cliffData.priceList);
            fuelUpgrade.Initialize(_fuelData.priceList[0], _fuelData.maxLevel, _fuelData.priceList,_fuelData.requiredPartsToUnlock);
        }

        private void InitializePurchasableParts()
        {
            motorButton.Initialize(_motorData.price, _motorData.icon,ShopItemType.Motor);
            wingsButton.Initialize(_wingsData.price, _wingsData.icon,ShopItemType.Wings);
            coverButton.Initialize(_coverData.price, _coverData.icon,ShopItemType.Cover);
            wheelsButton.Initialize(_wheelsData.price, _wheelsData.icon,ShopItemType.Wheels,_wheelsData.requiredPartsToUnlock);
            tailButton.Initialize(_tailData.price, _tailData.icon,ShopItemType.Tail);
        }
        
        private void GetUpgradableItemData()
        {
            _speedData = ShopManager.Instance.ShopData.speed;
            _cliffData = ShopManager.Instance.ShopData.cliff;
            _fuelData = ShopManager.Instance.ShopData.fuel;
        }
        
        private void GetPurchasableItemData()
        {
            _fuelData = ShopManager.Instance.ShopData.fuel;
            _motorData = ShopManager.Instance.ShopData.motor;
            _wingsData = ShopManager.Instance.ShopData.wings;
            _coverData = ShopManager.Instance.ShopData.cover;
            _wheelsData = ShopManager.Instance.ShopData.wheels;
            _tailData = ShopManager.Instance.ShopData.tail;
        }
    }
}