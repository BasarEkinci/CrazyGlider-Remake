using System.Collections.Generic;

namespace _GameFolders.Scripts.Data.ValueObjects.UpgradableItems
{
    [System.Serializable]
    public struct FuelUpgradeData
    {
        public int fuelMaxLevel;
        public int fuelInitialPrice;
        public List<int> priceList;
    }
}