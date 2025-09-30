using _GameFolders.Scripts.Items;
using UnityEngine;

namespace _GameFolders.Scripts.Data.ValueObjects.DataHolders
{
    [System.Serializable]
    public class ShopData
    {
        [Header("Hangar Items")]
        public HangarItem hangarItem;
        
        [Header("Purchasable Items")]
        public PurchasableItemData motor;
        public PurchasableItemData wings;
        public PurchasableItemData tail;
        public PurchasableItemData wheels;
        public PurchasableItemData cover;
        
        [Header("Upgradable Items")]
        public UpgradableItemData cliff;
        public UpgradableItemData speed;
        public UpgradableItemData fuel;
    }
}
