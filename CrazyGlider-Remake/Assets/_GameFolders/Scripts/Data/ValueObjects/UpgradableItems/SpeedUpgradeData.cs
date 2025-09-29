using System.Collections.Generic;

namespace _GameFolders.Scripts.Data.ValueObjects.UpgradableItems
{
    [System.Serializable]
    public struct SpeedUpgradeData
    {
        public int speedMaxLevel;
        public int speedInitialPrice;
        public List<int> priceList;
    }
}