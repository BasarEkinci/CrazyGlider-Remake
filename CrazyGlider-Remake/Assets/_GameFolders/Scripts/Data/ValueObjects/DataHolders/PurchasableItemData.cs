using _GameFolders.Scripts.Data.ValueObjects.PurchasableItems;

namespace _GameFolders.Scripts.Data.ValueObjects.DataHolders
{
    [System.Serializable]
    public struct PurchasableItemData
    {
        public MotorData motorData;
        public WingsData wingsData;
        public WheelsData wheelsData;
        public CoverData coverData;
        public TailData tailData;
    }
}