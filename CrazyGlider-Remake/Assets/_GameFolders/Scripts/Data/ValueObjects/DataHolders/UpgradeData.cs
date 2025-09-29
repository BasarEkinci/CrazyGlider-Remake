using UnityEngine;

namespace _GameFolders.Scripts.Data.ValueObjects.DataHolders
{
    [System.Serializable]
    public struct UpgradeData
    {
        [Header("Purchasable Items")]
        public PurchasableItemData purchasableItem;
        
        [Header("Upgradable Items")]
        public UpgradableItemData upgradableItem;
    }
}
