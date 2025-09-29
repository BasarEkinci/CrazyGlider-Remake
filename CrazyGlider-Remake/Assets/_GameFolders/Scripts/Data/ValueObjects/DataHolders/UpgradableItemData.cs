using _GameFolders.Scripts.Data.ValueObjects.UpgradableItems;

namespace _GameFolders.Scripts.Data.ValueObjects.DataHolders
{
    [System.Serializable]
    public struct UpgradableItemData
    {
        public SpeedUpgradeData speedUpgradeData;
        public FuelUpgradeData fuelUpgradeData;
        public CliffUpgradeData cliffUpgradeData;
    }
}