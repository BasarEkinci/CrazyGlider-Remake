namespace _GameFolders.Scripts.Data.ValueObjects
{
    /// <summary>
    /// Upgradable items have constant icons, but their levels can be upgraded.
    /// </summary>
    [System.Serializable]
    public struct UpgradeableItemLevelData
    {
        public int totalSpeedLevel;
        public int totalFuelLevel;
        public int totalCliffLevel;
    }
}