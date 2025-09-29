namespace _GameFolders.Scripts.Data.ValueObjects
{
    [System.Serializable]
    public struct UpgradeData
    {
        public PurchasableItemData motor;
        public PurchasableItemData wings;
        public PurchasableItemData wheels;
        public PurchasableItemData cover;
        public PurchasableItemData tail;
        
        public UpgradableItemData cliff;
        public UpgradableItemData fuel;
        public UpgradableItemData speed;
    }
}
